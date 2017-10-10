/* ITSE1430
 * Kevin Belknap
 * October 7, 2017
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieLib.Windows {
    public partial class MovieDetailForm : Form {
        public MovieDetailForm()
        {
            InitializeComponent();
        }

        public MovieDetailForm(string title) : this()    //constructor chaining
        {
            Text = title;
        }

        public MovieDetailForm(string title, Movie movie) : this(title)  //constructor chaining.  calls the constructor that takes title as the parameter, which then calls the base constructor of the class.
        {
            Movie = movie;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Fill in form with existing values
            if (Movie != null)
            {
                _txtTitle.Text = Movie.Title;
                _txtDescription.Text = Movie.Description;

                if (Movie.Length == -1)
                {
                    _txtLength.Text = "";
                }
                else
                {
                    _txtLength.Text = Movie.Length.ToString();
                }

                _chkOwned.Checked = Movie.Owned;
            }

            ValidateChildren();
        }

        /// <summary>/// Gets or Sets the Movie being shown/// </summary>
        public Movie Movie { get; set; }

        private void OnCancel(object sender, EventArgs e)
        {
            Close();
        }

        private void ShowError(string message, string title)
        {
            MessageBox.Show(this, message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void OnSave(object sender, EventArgs e)
        {
            if (!ValidateChildren())
            {
                return;
            }

            var movie = new Movie();
            movie.Title = _txtTitle.Text;
            movie.Description = _txtDescription.Text;
            movie.Length = GetLength(_txtLength);
            movie.Owned = _chkOwned.Checked;

            var error = movie.Validate(_txtLength.Text.Trim());

            if (!String.IsNullOrEmpty(error))
            {
                ShowError(error, "Validation Error");
                return;
            }

            Movie = movie;
            this.DialogResult = DialogResult.OK;
            Close();

        }

        private int GetLength(TextBox control)
        {
            if (int.TryParse(control.Text, out int length))
                return length;

            return -1;   //indicate error
        }
        //TODO: Error check for movie length
        //private void OnValidatingLength( object sender, CancelEventArgs e )
        //{
        //    var tb = sender as TextBox;

        //    if (GetLength(tb) < 0)
        //    {
        //        e.Cancel = true;
        //        _errors.SetError(_txtPrice, "Value must be much bigger than just a zero");
        //    } else
        //    {
        //        _errors.SetError(_txtPrice, "");
        //    }
        //}
    }
}
