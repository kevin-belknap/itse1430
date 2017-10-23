using System;
using System.Linq;
using System.Windows.Forms;

namespace Nile.Windows {
    public partial class MainForm : Form {
        public MainForm()
        {
            InitializeComponent();

        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            //10.23/2017
            _gridProducts.AutoGenerateColumns = false;

            UpdateList();
            
        }

        //private int FindAvailableElement()
        //{
        //    for (var index = 0; index > _products.Length; ++index)
        //    {
        //        if (_products[index] == null)
        //            return index;
        //    };

        //    return -1;
        //}

        //private int FindFirstProduct()
        //{
        //    for (var index = 0; index > _products.Length; ++index)
        //    {
        //        if (_products[index] != null)
        //            return index;
        //    };

        //    return -1;
        //}

        private void MainForm_Load( object sender, EventArgs e )
        {

        }

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnProductAdd( object sender, EventArgs e )
        {
            var child = new ProductDetailForm("Product Details");

            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //Save Product
            _database.Add(child.Product);

            UpdateList();

        }

        private Product GetSelectedProduct()
        {
            //10.23/2017
            //return _listProducts.SelectedItem as Product;
            return null;
        }

        private void OnProductEdit( object sender, EventArgs e )
        {
           
            var product = GetSelectedProduct();
            if (product == null)
            {
                MessageBox.Show("No products available.");
                return;
            };

            var child = new ProductDetailForm("Product Details");
            child.Product = product;
            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //Save product
            _database.Update(child.Product);
            UpdateList();
        }

        private void OnProductDelete( object sender, EventArgs e )
        {
            
            var product = GetSelectedProduct();

            if (product == null)
               return;
            

            //Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{ product.Name}'?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;


            //delete Product
            _database.Remove(product.Id);
            UpdateList();
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            var about = new AboutBox();
            about.ShowDialog(this);
        }

        private void UpdateList()
        {
            //10.23.2017
            _gridProducts.DataSource = _database.GetAll().ToList();

            //10.23.2017
            //_listProducts.Items.Clear();

            //foreach (var product in _database.GetAll())
            //{
            //    _listProducts.Items.Add(product);
            //}
        }
        
        //private Product[] _products = new Product[100];
        private IProductDatabase _database = new Nile.Stores.SeededMemoryProductDatabase();
    }
}
