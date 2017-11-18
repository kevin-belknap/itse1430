/* ITSE1430
 * Kevin Belknap
 * November 17, 2017
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
            };
            
            var movie = new Movie() {
                Id = Movie?.Id ?? 0,
                Title = _txtTitle.Text,
                Description = _txtDescription.Text,
                Length = GetLength(_txtLength),
                Owned = _chkOwned.Checked,
            };

            if (!ObjectValidator.TryValidate(movie, out var errors))
            {
                ShowError("Not valid", "Validation Error");
                return;
            };

            Movie = movie;
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private int GetLength(TextBox control)
        {
            //Handle empty textbox scenario
            if (control.TextLength > 0)
            {
                if (int.TryParse(control.Text, out int length))
                    return length;
            }
            else
            {
                return 0;
            }

            return -1;   //indicates error
        }

        private void OnValidatingTitle(object sender, CancelEventArgs e)
        {
            var tb = sender as TextBox;
            if (String.IsNullOrEmpty(tb.Text))
                _errors.SetError(tb, "Title is required");
            else
                _errors.SetError(tb, "");
        }

        private void OnValidatingLength(object sender, CancelEventArgs e)
        {
            var tb = sender as TextBox;

            if (GetLength(tb) < 0)
            {
                _errors.SetError(_txtLength, "Value must be >= 0.");
            }
            else
            {
                _errors.SetError(_txtLength, "");
            }
        }
    }
}
