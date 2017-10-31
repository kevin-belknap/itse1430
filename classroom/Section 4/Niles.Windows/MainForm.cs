using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
            try
            {
                _database.Add(child.Product);
            } catch (ValidationException ex)
            {
                MessageBox.Show(this, "Validation failed", "Error");
            } catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            } 

            UpdateList();

        }

        private Product GetSelectedProduct()
        {
            //10.23/2017
            //return _listProducts.SelectedItem as Product;
            if (_gridProducts.SelectedRows.Count > 0)
                return _gridProducts.SelectedRows[0].DataBoundItem as Product;
            ;
            
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

            EditProduct(product);
        }

        private void EditProduct( Product product )
        {
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

            DeleteProduct(product);
        }

        private void DeleteProduct( Product product )
        {
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
            //10.25.2017
            //new BindingList<Product>();

            _bsProducts.DataSource = _database.GetAll().ToList();

            //_gridProducts.DataSource = bs;

            //10.23.2017
            //_gridProducts.DataSource = _database.GetAll().ToList();

            //10.23.2017
            //_listProducts.Items.Clear();

            //foreach (var product in _database.GetAll())
            //{
            //    _listProducts.Items.Add(product);
            //}
        }

        private void OnEditRow( object sender, DataGridViewCellEventArgs e )
        {
            var grid = sender as DataGridView;

            //handle header cell being clicked
            if (e.RowIndex < 0)
                return;

            var row = grid.Rows[e.RowIndex];
            var item = row.DataBoundItem as Product;

            if (item != null)
                EditProduct(item);
        }
        
        private void OnKeyDownGrid( object sender, KeyEventArgs e )
        {

            if (e.KeyCode != Keys.Delete)
                return;

            var product = GetSelectedProduct();
            if (product != null)
                DeleteProduct(product);

            e.SuppressKeyPress = true;
        }

        //private Product[] _products = new Product[100];
        private IProductDatabase _database = new Nile.Stores.MemoryProductDatabase();

        
    }
}
