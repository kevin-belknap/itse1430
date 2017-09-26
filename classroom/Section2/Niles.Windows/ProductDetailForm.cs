using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Nile.Windows {
    public partial class ProductDetailForm : Form {
        public ProductDetailForm()
        {
            InitializeComponent();
        }

        private void ProductDetailForm_Load( object sender, EventArgs e )
        {

        }

        /// <summary>/// Gets or Sets the product being shown/// </summary>
        public Product Product { get; set; }

        private void OnCancel( object sender, EventArgs e )
        {
            Close();

        }

       

        private void OnSave( object sender, EventArgs e )
        {
            var product = new Product();
            product.Name = _txtName.Text;
            product.Description = _txtDescription.Text;
            product.Price = GetPrice();
            product.IsDiscontinued = _chkIsDiscontinued.Checked;

            //TODO: Add validation

            Product = product;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private decimal GetPrice()
        {
            if (Decimal.TryParse(_txtPrice.Text, out decimal price))
                return price;

            this.DialogResult = DialogResult.Cancel;
            //TODO: Validate price
            return 0;
        }
    }
}
