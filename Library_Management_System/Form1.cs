using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace Library_Management_System
{

    
    public partial class Form1 : Form
    {
        //public static string username = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public string constring = "Data Source=LAPTOP-I082KQUG;Initial Catalog=master;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            //MessageBox.Show("Connected");
            //string username = textBox2.Text;
            //username = textBox2.Text;
            SqlCommand cmd = new SqlCommand("Select count(*) from users where username = '"+textBox2.Text+"'", con);
            cmd.Parameters.AddWithValue("USERNAME", this.textBox2.Text);
            //var result = cmd.ExecuteScalar();
            bool exists = false;
            exists = (int)cmd.ExecuteScalar() > 0;
            if (exists)
            {
                //MessageBox.Show("Username already exist");
                label7.Hide();
                label6.Show();
                label4.Hide();
            }
            else
            {
                if (textBox1.Text.Equals("") || textBox2.Text.Equals("") || textBox3.Text.Equals(""))
                {
                    //MessageBox.Show("Empty Field !");
                    label6.Hide();
                    label4.Hide();
                    label7.Show();
                }
                else
                {
                    label7.Hide();
                    label6.Hide();
                    label4.Show();
                    string q = "insert into users values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + 0 + "','" + 0 + "')";
                    SqlCommand cmd2 = new SqlCommand(q, con);
                    cmd2.ExecuteNonQuery();
                }
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox1.Focus();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
