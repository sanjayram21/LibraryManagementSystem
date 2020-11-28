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
    public partial class Form5 : Form
    {

        public string username;

        public string passingvalue
        {
            get { return username; }
            set { username = value; }

        }
        public Form5()
        {
            InitializeComponent();
        }
        public string constring = "Data Source=LAPTOP-I082KQUG;Initial Catalog=master;Integrated Security=True";
        private void Form5_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            //MessageBox.Show(username);
            cmd.CommandText = "select bId,bName,bauthor from book where bId = (select bookid1 from users where username = '"+username+"') " +
                "or bId = (select bookid2 from users where username = '"+username+"')";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
        }

        int bid;
        Boolean clo = false;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            {
                //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                
                if (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() == "")
                {
                    MessageBox.Show("No Books to return !");
                    clo = true;
                    this.Close();
                }
                else
                
                    bid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            panel1.Visible = true;

            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from book where bId = '"+bid+"'";

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (clo == false)
            {
                textBox1.Text = ds.Tables[0].Rows[0][1].ToString();
                textBox2.Text = ds.Tables[0].Rows[0][2].ToString();
            }

            SqlCommand cmd3 = new SqlCommand();
            cmd3.Connection = con;
            cmd3.CommandText = "select * from book where bId = " + bid + "";
            SqlDataAdapter da1 = new SqlDataAdapter(cmd);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1);
            if (clo == false)
            {
                int quan = int.Parse(ds.Tables[0].Rows[0][3].ToString());
                quan = quan + 1;
                string up = "update book set bQuantity = '" + quan + "' where bId = '" + bid + "'";
                SqlCommand cmd4 = new SqlCommand(up, con);
                cmd4.ExecuteNonQuery();
            }

            //this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            String bName = textBox1.Text;
            if(bName!="")
            {
                //SqlCommand cmdd = new SqlCommand();
                //cmdd.Connection = con;
                //cmdd.CommandText = "if bookid1 = '"+bid+"' update users set bookid1 = 0 else update users set bookid2 = 0";
                SqlCommand cmd1 = new SqlCommand("update users set bookid1 = 0 where bookid1 = '"+bid+"'", con);
                cmd1.ExecuteNonQuery();
                SqlCommand cmd2 = new SqlCommand("update users set bookid2 = 0 where bookid2 = '" + bid + "'", con);
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Return success !");

            }
            this.Close();
        }
    }
}
