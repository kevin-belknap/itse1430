/* ITSE1430
 * Kevin Belknap
 * October 27, 2017
 */

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MovieLib.Data.Memory;


namespace MovieLib.Windows {
    public partial class MainForm : Form {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

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
            _bsMovies.DataSource = _database.GetAll().ToList();
        }

        private void OnFileExit(object sender, EventArgs e)
        {
            Close();
        }

        private void OnMovieAdd(object sender, EventArgs e)
        {
            var child = new MovieDetailForm();

            if (child.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            
            if (DuplicateTitleCheck(child.Movie.Title) == "duplicate")
            {
                MessageBox.Show("A movie with this title already exists.", "Invalid Operation");
                return;
            }

            //Save movie
            _database.Add(child.Movie);
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
            _database.Update(child.Movie);
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
            _database.Remove(movie.Id);
            UpdateList();
        }

        private void OnHelpAbout(object sender, EventArgs e)
        {
            var about = new AboutBox();
            about.ShowDialog(this);
        }

        private void OnEditRow(object sender, DataGridViewCellEventArgs e)
        {
            var grid = sender as DataGridView;

            //handle header cell being clicked
            if (e.RowIndex < 0)
                return;

            var row = grid.Rows[e.RowIndex];
            var item = row.DataBoundItem as Movie;
            
            if (item != null)
                EditMovie(item.Title, item);
        }

        private void OnKeyDownGrid(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete)
                return;

            var movie = GetSelectedMovie();
            if (movie != null)
                DeleteMovie(movie);
        }

        private IMovieDatabase _database = new MemoryMovieDatabase();
    }
}
