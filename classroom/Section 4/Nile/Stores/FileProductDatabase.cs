using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nile.Stores {
    /// <summary>Base class for product database</summary>
    public class FileProductDatabase : MemoryProductDatabase {
        public FileProductDatabase( string filename )
        {
            if (filename == null)
                throw new ArgumentNullException(nameof(filename));

            if (String.IsNullOrEmpty(filename))
                throw new ArgumentException("Filename cannot be empty", nameof(filename));

            _filename = filename;

            LoadFile(filename);
        }
        
        /// <summary>Adds a Product</summary>
        /// <param name="product">The product to add</param>
        protected override Product AddCore( Product product )
        {
            var newProduct = base.AddCore(product);

            SaveFile(_filename);

            return newProduct;

           // throw new NotImplementedException();
        }
        
                /// <summary>Removes the product</summary>
        /// <param name="product">The product to remove</param>
        protected override void RemoveCore( int id )
        {
            base.RemoveCore(id);

            SaveFile(_filename);
        }

        /// <summary>Updates a product</summary>
        /// <param name="product">The updated product.</param>
        protected override Product UpdateCore( Product existing, Product product )
        {
            var newProduct = base.UpdateCore(existing, product);
            SaveFile(_filename);

            return newProduct;
        }

       private void LoadFile ( string filename )
        {
            if (!File.Exists(filename))
                return;

            string[] lines = File.ReadAllLines(filename);

            foreach(var line in lines)
            {
                if (String.IsNullOrEmpty(line))
                    continue;

                var fields = line.Split(',');

                var product = new Product() {
                    Id = Int32.Parse(fields[0]),
                    Name = fields[1],
                    Description = fields[2],
                    Price = Decimal.Parse(fields[3]),
                    IsDiscontinued = Boolean.Parse(fields[4])
                };

                base.AddCore(product);
            }
        }

        private void SaveFile( string filename )
        {
            //streaming
            //StreamWriter writer = null;

            //var stream = File.OpenWrite(filename);

            //try
            //{
            //    writer = new StreamWriter(stream);


            //} finally
            //{
            //    writer?.Dispose();
            //    stream.Close();
            //}

            using (var writer = new StreamWriter(filename))
            {
                //write stuff
                foreach (var product in GetAllCore())
                {
                    var row = String.Join(",", product.Id, product.Name, product.Description, product.Price, product.IsDiscontinued);

                    writer.WriteLine(row);
                }
            };
        }

        private readonly string _filename;
    }
}
