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
            var product = new Product();
            product.Name = _txtName.Text;
            product.Description = _txtDescription.Text;
            product.Price = GetPrice();
            product.IsDiscontinued = _chkIsDiscontinued.Checked;

            var error = product.Validate();
            if (!String.IsNullOrEmpty(error))
            {
                showError(error, "Validation Error");
            }

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

        private void ProductDetailForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            //This is a runtime check. Do not do this! Need to be more safe.
            //var form = (Form)sender;//

            //do this instead.
            var form = sender as Form;
            //the AS operator is a runtime Safe type cast.  It must be a referenced type. You cannot use value types.  It attempts
            //to convert the expression to the type. If successful, you get that type bacck. If not successful, you get null of that
            //type back/  So now you can check for null.

            //for value types, you can use the IS keyword
            //if (sender is int)
            //{ 
            //    var intValue = (int)sender;
            //};


            //Pattern matching
            if (sender is int intValue)  //intValue is not a variable
            {

            };


            if (MessageBox.Show(this, "Are you sure?", "Closing", MessageBoxButtons.YesNo) == DialogResult.No)
                e.Cancel = true;
        }

        private void ProductDetailForm_FormClosed( object sender, FormClosedEventArgs e )
        {

        }
    }
}
