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

            UpdateList();
        }

        private Movie GetSelectedMovie()
        {
            return _listMovies.SelectedItem as Movie;
        }

        private void UpdateList()
        {
            _listMovies.Items.Clear();

            foreach (var movie in _database.GetAll())
                _listMovies.Items.Add(movie);
        }

        private void OnFileExit(object sender, EventArgs e)
        {
            Close();
        }

        private void OnMovieAdd(object sender, EventArgs e)
        {
            var child = new MovieDetailForm("Movie Details");
            if (child.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            if (DuplicateTitleCheck(child.Movie.Title) == "duplicate")
            {
                MessageBox.Show("A movie with this title already exists.");
                return;
            }
            
            //Save movie
            _database.Add(child.Movie);
            UpdateList();
        }

        public string DuplicateTitleCheck(string title)
        {
            foreach (var movie in _listMovies.Items)
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
                MessageBox.Show("No movies selected or available.");
                return;
            }
            else
            {
                startingTitle = movie.Title;
            }

            var child = new MovieDetailForm("Movie Details");

            child.Movie = movie;

            if (child.ShowDialog(this) != DialogResult.OK)
                return;

            if (child.Movie.Title != startingTitle)
            {
                if (DuplicateTitleCheck(child.Movie.Title) == "duplicate")
                {
                    MessageBox.Show("A movie with this title already exists.");
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

        public delegate void ButtonClickCall(object sender, EventArgs e);

        private void CallButton(ButtonClickCall functionToCall)
        {
            functionToCall(this, EventArgs.Empty);
        }

        private IMovieDatabase _database = new MemoryMovieDatabase();

        
    }
}
