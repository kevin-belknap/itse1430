using System;
using System.Windows.Forms;

namespace Nile.Windows {
    public partial class MainForm : Form {
        public MainForm()
        {
            InitializeComponent();
           
        }

        private void MainForm_Load( object sender, EventArgs e )
        {

        }

        private void button1_Click( object sender, EventArgs e )
        {
            var child = new ProductDetailForm();
             if (child.ShowDialog(this) != DialogResult.OK)
                return;

            //TODO: Save Product
            var product = child.Product;


        }
    }
}
