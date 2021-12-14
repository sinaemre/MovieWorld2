using MovieWorld2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MovieWorld2
{
    public partial class Form1 : Form
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public Form1()
        {
            SeedData();
            InitializeComponent();
            LoadGenres();
        }

        private void LoadGenres()
        {
            cboGenres.DataSource = db.Genres.OrderByDescending(x => x.Name).ToList();
            cboGenres.DisplayMember = "Name";
        }

        private void SeedData()
        {
            if (!db.Genres.Any() || !db.Movies.Any())
            {
                string[] genres = { "Drama", "Adventure", "Action", "Thriller", "Comedy", "Sci-Fi", "War", "Crime", "Horror", "History", "Romance", "Biography", "Fantasy", "Western", "Family", "Musical", "Mystery", "Animation", "Documentary", "Film-Noir", "Music", "Sport" };

                foreach (string genre in genres)
                {
                    db.Genres.Add(new Genre() { Name = genre });
                    db.SaveChanges();
                }

                Genre drama = db.Genres.FirstOrDefault(x => x.Name == "Drama");
                Genre romance = db.Genres.FirstOrDefault(x => x.Name == "Romance");
                Genre adventure = db.Genres.FirstOrDefault(x => x.Name == "Adventure");
                Genre comedy = db.Genres.FirstOrDefault(x => x.Name == "Comedy");
                Genre scifi = db.Genres.FirstOrDefault(x => x.Name == "Sci-Fi");
                Genre horror = db.Genres.FirstOrDefault(x => x.Name == "Horror");
                Genre mystery = db.Genres.FirstOrDefault(x => x.Name == "Mystery");
                Genre action = db.Genres.FirstOrDefault(x => x.Name == "Action");
                Genre history = db.Genres.FirstOrDefault(x => x.Name == "History");
                Genre biography = db.Genres.FirstOrDefault(x => x.Name == "Biography");
                Genre crime = db.Genres.FirstOrDefault(x => x.Name == "Crime");
                Genre fantasy = db.Genres.FirstOrDefault(x => x.Name == "Fantasy");


                db.Movies.Add(new Movie()
                {
                    Title = "Back to the Future",
                    Year = 1985,
                    Rating = 8.5m,
                    Genres = new[] { adventure, comedy, scifi }
                });
                db.Movies.Add(new Movie()
                {
                    Title = "Borgman",
                    Year = 2013,
                    Rating = 6.8m,
                    Genres = new[] { drama, horror, mystery }
                });
                db.Movies.Add(new Movie()
                {
                    Title = "Pearl Harbor",
                    Year = 2001,
                    Rating = 6.2m,
                    Genres = new[] { action, drama, history }
                });
                db.Movies.Add(new Movie()
                {
                    Title = "The Wolf of Wall Street",
                    Year = 2013,
                    Rating = 8.2m,
                    Genres = new[] { biography, comedy, crime }
                });
                db.Movies.Add(new Movie()
                {
                    Title = "Perfume: The Story of a Murderer",
                    Year = 2006,
                    Rating = 7.5m,
                    Genres = new[] { crime, drama, fantasy }
                });
                db.SaveChanges();

            }
        }

        private void cboGenres_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboGenres.SelectedIndex == -1)
            {
                dgvMovies.DataSource = null;
                return;
            }

            Genre genre = (Genre)cboGenres.SelectedItem;
            dgvMovies.DataSource = genre.Movies.Select(x => new 
            { 
                x.Id,
                x.Title,  
                x.Year,
                x.Rating,
                Genres = string.Join(", ", x.Genres.Select(g => g.Name))
            }).ToList();

        }

        private void dgvMovies_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dgvMovies.SelectedRows.Count == 0) return;
            int id = (int)dgvMovies.SelectedRows[0].Cells[0].Value;

            Movie movie = db.Movies.Find(id);

            DialogResult dr = new EditMovieForm(db, movie).ShowDialog();

            if (dr == DialogResult.OK)
            {
                LoadGenres();
            }
        }
    }
}
