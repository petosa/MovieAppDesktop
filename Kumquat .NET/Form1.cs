using Kumquat.NET.model;
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

    public partial class Form1 : Form
    {
        public String page = "main";
        public Form1()
        {
            InitializeComponent();
            DBHelper.startListeners();
            this.Location = new Point((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2,
                          (Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            page = "register";
            richTextBox1.Visible = true;
            richTextBox2.Visible = true;
            richTextBox3.Visible = true;
            richTextBox4.Visible = true;
            richTextBox4.UseSystemPasswordChar = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = true;
            button4.Visible = true;
            button5.Visible = false;
        }

        //BACK
        private void button4_Click(object sender, EventArgs e)
        {
            page = "main";
            richTextBox1.Visible = false;
            richTextBox2.Visible = false;
            richTextBox3.Visible = false;
            richTextBox4.Visible = false;
            label1.Text = "Name";
            label2.Text = "Email";
            richTextBox2.UseSystemPasswordChar = false;
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button3.Visible = false;
            button4.Visible = false;
            button5.Visible = false;
            button6.Visible = false;
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            richTextBox3.Text = "";
            richTextBox4.Text = "";
        }

        //Login
        private void button1_Click(object sender, EventArgs e)
        {
            page = "login";
            richTextBox1.Visible = true;
            richTextBox2.Visible = true;
            label1.Text = "Username";
            label2.Text = "Password";
            richTextBox2.UseSystemPasswordChar = true;
            label1.Visible = true;
            label2.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            button5.Visible = true;
            button6.Visible = true;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            button4_Click(sender, e);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dictionary<String,User> ud = DBHelper.getUsersMap();
            if (ud.ContainsKey(richTextBox1.Text) && ud[richTextBox1.Text].getPasswordHash() == DBHelper.getDigest(richTextBox2.Text)) {
                DBHelper.setCurrentUser(ud[richTextBox1.Text]);
                Dashboard d = new Dashboard();
                d.Show();
                this.Hide();
            }
            else{
                MessageBox.Show("Incorrect password.");
                richTextBox2.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DBHelper.getUsersMap().ContainsKey(richTextBox3.Text))
            {
                MessageBox.Show("That username is taken.");
                richTextBox3.Text = "";
            } else if(richTextBox1.Text != "" &&
                richTextBox2.Text != "" &&
                richTextBox3.Text != "" &&
                richTextBox4.Text != "")
            {
                MessageBox.Show("Registered!");
                User up = new User(richTextBox1.Text, richTextBox2.Text, richTextBox3.Text, DBHelper.getDigest(richTextBox4.Text));
                DBHelper.addUser(up);
                DBHelper.setCurrentUser(up);
                Dashboard d = new Dashboard();
                d.Show();
                this.Hide();
            } else
            {
                MessageBox.Show("Some fields were empty. Please fill in all fields.");
            }

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DBHelper.parseUsers();
        }

        private void studioButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void richTextBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13 && page.Equals("register"))
            button3_Click(sender, null);
        }

        private void richTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13 && page.Equals("login"))
                button5_Click(sender, null);
        }
    }
}
