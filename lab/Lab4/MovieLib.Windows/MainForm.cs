/* ITSE1430
 * Kevin Belknap
 * November 17, 2017
 */

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MovieLib.Data.Sql;
using System.Configuration;

using System.ComponentModel.DataAnnotations;

namespace MovieLib.Windows {
    public partial class MainForm : Form {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"].ConnectionString;
            _database = new MovieLib.Data.Sql.SqlMovieDatabase(connString);
            

            _gridMovies.AutoGenerateColumns = false;

            UpdateList();
        }

        private Movie GetSelectedMovie()
        {
            if (_gridMovies.SelectedRows.Count > 0)
                return _gridMovies.SelectedRows[0].DataBoundItem as Movie;

            return null;
        }

        private void UpdateList()
        {
            try
            {
                _bsMovies.DataSource = _database.GetAll().ToList();
            } catch (Exception e)
            {
                DisplayError(e, "Refresh Failed");
                _bsMovies.DataSource = null;
            }
            
        }

        private void OnFileExit(object sender, EventArgs e)
        {
            Close();
        }

        private void OnMovieAdd(object sender, EventArgs e)
        {
            var child = new MovieDetailForm();

            if (child.ShowDialog(this) != DialogResult.OK)
                return;
            
            if (DuplicateTitleCheck(child.Movie.Title) == "duplicate")
            {
                MessageBox.Show("A movie with this title already exists.", "Invalid Operation");
                return;
            }

            //Save movie
            try
            {
                _database.Add(child.Movie);
            }
            catch (ValidationException ex)
            {
                DisplayError(ex, "Validation Failed");
            }
            catch (Exception ex)
            {
                DisplayError(ex, "Add Failed");
            };

            UpdateList();
        }
        
        public string DuplicateTitleCheck(string title)
        {
            foreach (var movie in _database.GetAll())
            {
                if (title == movie.ToString())
                {
                    return "duplicate";
                }
            }

            return "";
        }

        private void OnMovieEdit(object sender, EventArgs e)
        {
            string startingTitle = "";

            var movie = GetSelectedMovie();

            if (movie == null)
            {
                if (_database.GetAll().Count() == 0)
                    MessageBox.Show("No movies available.", "Invalid Selection");
                else
                    MessageBox.Show("No movie selected.", "Invalid Selection");

                return;
            }
            else
            {
                startingTitle = movie.Title;
            }

            EditMovie(startingTitle, movie);
        }

        private void EditMovie(string startingTitle, Movie movie)
        {
            var child = new MovieDetailForm();

            child.Movie = movie;

            if (child.ShowDialog(this) != DialogResult.OK)
                return;
            
            if (child.Movie.Title != startingTitle)
            {
                if (DuplicateTitleCheck(child.Movie.Title) == "duplicate")
                {
                    MessageBox.Show("A movie with this title already exists.", "Invalid Operation");
                    return;
                }
            }

            //Save movie
            try
            {
                _database.Update(child.Movie);
            }
            catch (Exception ex)
            {
                DisplayError(ex, "Update Failed");
            };

            UpdateList();
        }

        private void OnMovieDelete(object sender, EventArgs e)
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;

            DeleteMovie(movie);
        }

        private void DeleteMovie(Movie movie)
        {
            //Confirm
            if (MessageBox.Show(this, $"Are you sure you want to delete '{movie.Title}'?",
                                "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            //Delete movie
            try
            {
                _database.Remove(movie.Id);
            }
            catch (Exception e)
            {
                DisplayError(e, "Delete Failed");
            };

            UpdateList();
        }

        private void OnHelpAbout(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog(this);
        }

        private void OnEditRow(object sender, DataGridViewCellEventArgs e)
        { 
            //handle header cell being clicked
            if (e.RowIndex < 0)
                return;

            var movie = GetSelectedMovie();
            
            if (movie != null)
                EditMovie(movie.Title, movie);
        }

        private void OnKeyDownGrid(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Enter)
                return;
            
            var movie = GetSelectedMovie();

            if (movie != null)
            {
                if (e.KeyCode == Keys.Enter)
                    EditMovie(movie.Title, movie);
                else
                    DeleteMovie(movie);
            }

            e.SuppressKeyPress = true;
        }

        private void DisplayError(Exception error, string title = "Error")
        {
            DisplayError(error.Message, title);
        }

        private void DisplayError(string message, string title = "Error")
        {
            MessageBox.Show(this, message, title ?? "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private IMovieDatabase _database;

    }
}
