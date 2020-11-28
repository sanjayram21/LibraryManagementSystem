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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public string constring = "Data Source=LAPTOP-I082KQUG;Initial Catalog=master;Integrated Security=True";
        private void button1_Click(object sender, EventArgs e)
        {

            String username = textBox1.Text;
            String password = textBox2.Text;
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand("select * from users where username = '"+username+"' and password = '"+password+"';");
            cmd.Parameters.AddWithValue("username", username);
            cmd.Parameters.AddWithValue("password", password);
            cmd.Connection = con;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            con.Close();

            bool loginSuccessful = ((ds.Tables.Count > 0) && (ds.Tables[0].Rows.Count > 0));

            if (loginSuccessful)
            {
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
                Form3 form3 = new Form3();
                form3.passingvalue = username;
                form3.Show();
            }
            else
            {
                MessageBox.Show("WRONG USERNAME OR PASSWORD");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
            }
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}
