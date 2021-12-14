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
    public partial class EditMovieForm : Form
    {
        private readonly ApplicationDbContext db;
        private readonly Movie movie;

        public EditMovieForm(ApplicationDbContext db, Movie movie)
        {
            this.db = db;
            this.movie = movie;
            InitializeComponent();
            Text = "Editing : \"" + movie.Title + "\"";
            LoadMovie();
        }

        private void LoadMovie()
        {
            txtTitle.Text = movie.Title;
            nudYear.Value = movie.Year;
            nudRating.Value = movie.Rating;

            clbGernes.DataSource = db.Genres.OrderByDescending(x => x.Name).ToList();
            clbGernes.DisplayMember = "Name";

            for (int i = 0; i < clbGernes.Items.Count; i++)
            {
                Genre genre = (Genre)clbGernes.Items[i];
                if (movie.Genres.Any(x => x.Id == genre.Id))
                {
                    clbGernes.SetItemChecked(i, true);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string title = txtTitle.Text;
            int year = (int)nudYear.Value;
            int rating = (int)nudRating.Value;

            if (title == "")
            {
                MessageBox.Show("The title is required");
                return;
            }

            movie.Title = title;
            movie.Year = year;
            movie.Rating = rating;

            movie.Genres.Clear();
            foreach (Genre genre in clbGernes.CheckedItems)
            {
                movie.Genres.Add(genre);
            }

            db.SaveChanges();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
