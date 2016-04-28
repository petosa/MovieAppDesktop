using Kumquat.NET.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kumquat.NET
{
    public partial class Dashboard : Form
    {
        public String htmlCode = "";

        public Dashboard()
        {
            InitializeComponent();
            profmajor.SelectedIndex = 0;
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
            if(DBHelper.getCurrentUser().getProfile() != null)
            {
                createprof_Click(null, null);
            }
        }
        private void studioButton4_Click(object sender, EventArgs e)
        {
            this.Close();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void createprof_Click(object sender, EventArgs e)
        {
            if (DBHelper.getCurrentUser().getProfile() == null)
            DBHelper.getCurrentUser().setProfile(new Profile("CS","Add description here."));
            createprof.Visible = false;
            noprof.Visible = false;
            userprof.Visible = true;
            profpic.Visible = true;
            profname.Visible = true;
            profemail.Visible = true;
            mymajor.Visible = true;
            mydescription.Visible = true;
            profmajor.Visible = true;
            profdesc.Visible = true;
            saveprof.Visible = true;
            profmajor.Text = DBHelper.getCurrentUser().getProfile().getMajor();
            profdesc.Text = DBHelper.getCurrentUser().getProfile().getDesc();
            userprof.Text = DBHelper.getCurrentUser().getUsername();
            profname.Text = DBHelper.getCurrentUser().getName();
            profemail.Text = DBHelper.getCurrentUser().getEmail();

        }

        private void doSearch2(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                doSearchAct();
            }
        }
        private void doSearch(object sender, EventArgs e)
        {
            doSearchAct();
        }
        private void doSearchAct()
        {
            //Flip
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            using (WebClient client = new WebClient())
            {
                String q = searchbox.Text;
                if (!q.Equals(""))
                {
                    htmlCode = client.DownloadString("http://www.omdbapi.com/?s=" + q);
                    if (htmlCode.Contains("Error\":\"Movie"))
                    {
                        MessageBox.Show("Movie was not found.");
                    }
                    else if (htmlCode.Contains("Error\":"))
                    {
                        MessageBox.Show("Search terms must be at least 2 characters long.");
                    }
                    else
                    {
                        //Lists of data from get
                        List<String> titles = Utils.getAspect(htmlCode, "Title");
                        List<String> years = Utils.getAspect(htmlCode, "Year");
                        List<String> posters = Utils.getAspect(htmlCode, "Poster");

                        //Clear listview
                        listView1.Items.Clear();

                        //Go through all movies
                        for (int i = 0; i < titles.Count; i++)
                        {
                            ListViewItem lvi = new ListViewItem();
                            //Poster available
                            if (!posters[i].Equals("N/A"))
                            {
                                WebRequest requestPic = WebRequest.Create(posters[i]);
                                WebResponse responsePic = requestPic.GetResponse();
                                Image webImage = Image.FromStream(responsePic.GetResponseStream());
                                imageList1.Images.Add(posters[i], webImage);
                                lvi.ImageKey = posters[i];
                            }
                            //No poster
                            else
                            {
                                lvi.ImageKey = "notfound.png";
                            }
                            //Generate ListViewItem
                            lvi.Text = titles[i] + " (" + years[i] + ") RATING/5";
                            lvi.Tag = titles[i] + "☻" + years[i] + "☻" + "4";
                            listView1.Items.Add(lvi);

                            //Flop
                            timer1.Enabled = false;
                        }
                    }
                }
            }
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.Item.Selected == true)
            {
                String data = e.Item.Text;
                Image img = imageList1.Images[e.Item.ImageKey];
                MovieProfile mp = new MovieProfile(e.Item, data, img);
                mp.Show();
            }
        }

        private void saveprof_Click(object sender, EventArgs e)
        {
            Profile p = DBHelper.getCurrentUser().getProfile();
            p.setMajor(profmajor.Text);
            p.setDesc(profdesc.Text);
            DBHelper.getCurrentUser().setProfile(p);
            DBHelper.setMajor(DBHelper.getCurrentUser().getUsername(), profmajor.Text);
            DBHelper.setDescription(DBHelper.getCurrentUser().getUsername(), profdesc.Text);
            MessageBox.Show("Saved!");
        }

        private void studioButton5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
