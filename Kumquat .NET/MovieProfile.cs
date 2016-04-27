using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kumquat.NET
{
    public partial class MovieProfile : Form
    {
        public MovieProfile(ListViewItem lvi, String name, Image img)
        {
            InitializeComponent();
            studioTheme1.Text = name;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            this.TopMost = true;
            pictureBox1.Image = img;
            textBox1.Text = (lvi.Tag.ToString()).Split('☻')[0] + " (" + (lvi.Tag.ToString()).Split('☻')[1] + ")";
        }

        private void studioButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void studioTheme1_Click(object sender, EventArgs e)
        {

        }

        private void star1_Click(object sender, EventArgs e)
        {
            star1.ForeColor = Color.Yellow;
            star2.ForeColor = Color.Transparent;
            star3.ForeColor = Color.Transparent;
            star4.ForeColor = Color.Transparent;
            star5.ForeColor = Color.Transparent;
        }

        private void star2_Click(object sender, EventArgs e)
        {
            star1.ForeColor = Color.Yellow;
            star2.ForeColor = Color.Yellow;
            star3.ForeColor = Color.Transparent;
            star4.ForeColor = Color.Transparent;
            star5.ForeColor = Color.Transparent;
        }

        private void star3_Click(object sender, EventArgs e)
        {
            star1.ForeColor = Color.Yellow;
            star2.ForeColor = Color.Yellow;
            star3.ForeColor = Color.Yellow;
            star4.ForeColor = Color.Transparent;
            star5.ForeColor = Color.Transparent;
        }

        private void star4_Click(object sender, EventArgs e)
        {
            star1.ForeColor = Color.Yellow;
            star2.ForeColor = Color.Yellow;
            star3.ForeColor = Color.Yellow;
            star4.ForeColor = Color.Yellow;
            star5.ForeColor = Color.Transparent;
        }

        private void star5_Click(object sender, EventArgs e)
        {
            star1.ForeColor = Color.Yellow;
            star2.ForeColor = Color.Yellow;
            star3.ForeColor = Color.Yellow;
            star4.ForeColor = Color.Yellow;
            star5.ForeColor = Color.Yellow;
        }
    }
}
