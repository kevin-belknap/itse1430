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

        #region Construction

        public ProductDetailForm() //: base()
        {
            //Don't ever put anything in front of the InitializeComponent
            InitializeComponent();
        }

        //call base constructor of this type
        public ProductDetailForm( string title ) : this()    //constructor chaining
        {
            Text = title;
        }

        public ProductDetailForm( string title, Product product ) : this(title)  //constructor chaining.  calls the constructor that takes title as the parameter, which then calls the base constructor of the class.
        {
            Product = product;
        }
        #endregion

        //last point before showing the UI.  
        //Logic to set up the initial view of the form.  
        //The Base class knows how to call the OnLoad function.
        protected override void OnLoad( EventArgs e )
        {
            //call base.OnLoad to get standard implementation.  There are times that the base type will not be included if you don't call it explicitly.
            base.OnLoad(e);
        
            if (Product != null)
            {
                _txtName.Text = Product.Name;
                _txtDescription.Text = Product.Description;
                _txtPrice.Text = Product.Price.ToString();
                _chkIsDiscontinued.Checked = Product.IsDiscontinued;
            }

            ValidateChildren();
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

       
        private void showError (string message, string title)
        {
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnSave( object sender, EventArgs e )
        {
            if  (!ValidateChildren())
            {
                return; 
            }

            var product = new Product();
            product.Id = Product?.Id ?? 0;
            product.Name = _txtName.Text;
            product.Description = _txtDescription.Text;
            product.Price = GetPrice(_txtPrice);
            product.IsDiscontinued = _chkIsDiscontinued.Checked;

            var error = product.Validate();
            if (!String.IsNullOrEmpty(error))
            {
                showError(error, "Validation Error");
                return;
            }

            Product = product;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private decimal GetPrice(TextBox control)
        {
            if (control.TextLength > 0)
            {
                if (Decimal.TryParse(control.Text, out decimal price))
                    return price;
            }
            else
            {
                return 0;
            }

            //this.DialogResult = DialogResult.Cancel;
            //TODO: Validate price
            return -1;   //indicate error
        }

        private void ProductDetailForm_FormClosing( object sender, FormClosingEventArgs e )
        {

        }

        private void OnValidatingPrice( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;

            if (GetPrice(tb) < 0)
            {
                e.Cancel = true;
                _errors.SetError(_txtPrice, "Value must be much bigger than just a zero");
            }
            else
            {
                _errors.SetError(_txtPrice, "");
            }
        }

        private void OnValidatingName( object sender, CancelEventArgs e )
        {
            var tb = sender as TextBox;
            if (String.IsNullOrEmpty(tb.Text))
                _errors.SetError(tb, "Name is required123");
            else
                _errors.SetError(tb, "");
        }
    }
}
