using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace TelephoneAgain
{
    public partial class Phone : Form
    {
        

        
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\irem\Desktop\C#\TelephoneAgain\TelephoneAgain\TelephoneAgainDB.mdf;Integrated Security=True");
        
        
        
        public Phone()
        {
            InitializeComponent();
        }

        private void Phone_Load(object sender, EventArgs e)
        {
            Display();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT mobiles (First, Last, Email, Mobile, Catagory) " +
                "VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "' , '" + textBox3.Text + "', '" + textBox4.Text + "', '" + comboBox1.Text + "')", con);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Your information has been uploaded...!");

            Display();
        }

        //populate cells in window view
        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from mobiles", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView1.Rows.Clear();

            //add the data inserted to each row in the main window
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            //change values in table to each respective textbox when clicked on row
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
        }

        //delete row by deleting the primary key = Mobile
        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"DELETE FROM mobiles 
                WHERE (Mobile = '" + textBox4.Text + "')", con);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Your information has been deleted...!");

            Display();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"UPDATE mobiles 
                SET First = '" + textBox1.Text + "', Last = '" + textBox2.Text + "', Email = '" + textBox3.Text + "', Mobile = '" + textBox4.Text + "', Catagory = '" + comboBox1.Text + "'" +
                " WHERE (Mobile = '" + textBox4.Text + "')", con);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Your information has been updated...!");

            Display();
        }

        //use the search bar to see
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from mobiles Where (Mobile like '%" + textBox5.Text + "%') or (First like '%" + textBox5.Text + "%') or (Last like '%" + textBox5.Text + "%') ", con);
            
            DataTable dt = new DataTable();
            sda.Fill(dt);

            dataGridView1.Rows.Clear();

            //add the data inserted to each row in the main window
            foreach (DataRow item in dt.Rows)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = item[0].ToString();
                dataGridView1.Rows[n].Cells[1].Value = item[1].ToString();
                dataGridView1.Rows[n].Cells[2].Value = item[2].ToString();
                dataGridView1.Rows[n].Cells[3].Value = item[3].ToString();
                dataGridView1.Rows[n].Cells[4].Value = item[4].ToString();
            }
        }
    }
}
