using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library_Management_System
{
    public partial class Form4 : Form
    {

        public string username;

        public string passingvalue
        {
            get { return username; }
            set { username = value; }

        }
        public Form4()
        {
            InitializeComponent();
        }
        public string constring = "Data Source=LAPTOP-I082KQUG;Initial Catalog=master;Integrated Security=True";
        private void Form4_Load(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from book";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        int bid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null) 
            {
                bid = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }
            panel1.Visible = true;

            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = "select * from book where bId = "+bid+"";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            textBox1.Text = ds.Tables[0].Rows[0][1].ToString();
            textBox2.Text = ds.Tables[0].Rows[0][2].ToString();
            textBox3.Text = ds.Tables[0].Rows[0][3].ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(constring);
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            //string date = DateTime.ParseExact(textBox1.Text, "yyyy/MM/dd", new CultureInfo("en-US")).ToString();
            //MessageBox.Show(date);

            String bName = textBox1.Text;
            if(bName!="")
            {
                int quan = int.Parse(textBox3.Text);
                //MessageBox.Show(quan.ToString());
                
                if (quan<=0)
                {
                    MessageBox.Show("Book not available");
                }
                else
                {
                    //string book1 = "select bookid1 from users";
                    //string book2 = "select bookid1 from users";


                    SqlCommand cmdd = new SqlCommand();
                    cmdd.Connection = con;
                    cmdd.CommandText = "select * from users where username ='"+username+"'";
                    SqlDataAdapter daa = new SqlDataAdapter(cmdd);
                    DataSet dss = new DataSet();
                    daa.Fill(dss);
                    string b1 = dss.Tables[0].Rows[0][3].ToString();
                    string b2 = dss.Tables[0].Rows[0][4].ToString();

                    //MessageBox.Show(b1);
                    //MessageBox.Show(b2);
                    if (b1.Equals("0")||b2.Equals("0"))
                    {
                        quan = quan - 1;
                        string up = "update book set bQuantity = '" + quan + "' where bId = '" + bid + "'";
                        SqlCommand cmd3 = new SqlCommand(up, con);
                        cmd3.ExecuteNonQuery();
                        if(b1.Equals("0"))
                        {
                            string addbook = "update users set bookid1 = '" + bid + "' where username = '" + username + "'";
                            SqlCommand cmd4 = new SqlCommand(addbook, con);
                            cmd4.ExecuteNonQuery();
                        }
                        else
                        {
                            string addbook = "update users set bookid2 = '" + bid + "' where username = '" + username + "'";
                            SqlCommand cmd4 = new SqlCommand(addbook, con);
                            cmd4.ExecuteNonQuery();
                        }
                        MessageBox.Show("Book Issued Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Two Books already issued !!");
                        this.Close();
                    }
                }
                
            }
            this.Close();

            cmd.CommandText = "select * from book where bId = " + bid + "";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tbSearch.Clear();
            panel1.Visible = false;
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            if (tbSearch.Text != "")
            {
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from book where bName like '" + tbSearch.Text + "%' or bAuthor like '" + tbSearch.Text + "%' or bId like '" + tbSearch.Text + "%'";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                SqlConnection con = new SqlConnection(constring);
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select * from book";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                dataGridView1.DataSource = ds.Tables[0];
            }
        }
    }
}
