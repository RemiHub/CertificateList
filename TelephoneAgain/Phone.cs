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

        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\irem\Desktop\C#\List\TelephoneAgain\Logs.mdf;Integrated Security=True");
        
        
        
        public Phone()
        {
            InitializeComponent();
        }

        private void Phone_Load(object sender, EventArgs e)
        {
            textBox6.Text = "Enter Date: YYYY-MM-DD";
            textBox6.ForeColor = Color.Silver;
            textBox7.Text = "Enter Date: YYYY-MM-DD";
            textBox7.ForeColor = Color.Silver;

            AlertDate();

            Display();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearAll();
            comboBox1.SelectedIndex = -1;
            textBox1.Focus();

            AlertDate();
        }

        //clear fields
        void ClearAll()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox6.Clear();
            textBox7.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = new SqlCommand(@"INSERT Logs ([Address 1:], [Address 2:], [Post Code:], [Number:], [Start Date:], [End Date:]) " +
                "VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "' , '" + textBox3.Text + "', '" + textBox4.Text + "', '" + textBox6.Text + "', '" + textBox7.Text + "')", con);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Your information has been uploaded...!");


            Display();
        }

        void AlertDate()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Logs", con);
            DataTable dts = new DataTable();
            sda.Fill(dts);

            string alertDate = '#' + System.DateTime.Now.ToString("MM/dd/yyyy") + '#';

            DataRow[] alertresult = dts.Select("[End Date:] <= " + alertDate);

            

            foreach (DataRow row in alertresult)
            {

                //title of warning/message box
                string title = "Warning!";

                //row record  
                MessageBox.Show("The following certificates need updating: " + row[0].ToString(), title);  
            }

           

        }

        //populate cells in window view
        void Display()
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Logs Order By [End Date:] Asc", con);
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
                dataGridView1.Rows[n].Cells[5].Value = item[5].ToString();
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
                textBox6.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                textBox7.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            
        }

        //delete row by deleting the primary key = Mobile
        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand(@"DELETE FROM Logs 
                WHERE ([Address 1:] = '" + textBox1.Text + "')", con);

            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Your information has been deleted...!");

            Display();
            ClearAll();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
                con.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(@"UPDATE Logs 
                SET [Address 1:] = '" + textBox1.Text + "', [Address 2:] = '" + textBox2.Text + "', [Post Code:] = '" + textBox3.Text + "', [Number:] = '" + textBox4.Text + "', [Start Date:] = '" + textBox6.Text + "', [End Date:] = '" + textBox7.Text + "'" +
                     " WHERE ([Address 1:] = '" + textBox1.Text + "')", con);


                cmd.ExecuteNonQuery();
                MessageBox.Show("Your information has been updated...!");

            }

            //catches error of wrong date given
            catch (SqlException err)
            {
                MessageBox.Show("An error occurred: " + err.Message + " Please check date and date format is correct. ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

                con.Close();

                Display();
        }

        //use the search bar to find needed data
        //only applies to address line 1, post code, start date and end date
        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select * from Logs Where ([Address 1:] like '%" + textBox5.Text + "%') or ([Post Code:] like '%" + textBox5.Text + "%') or ([Start Date:] like '%" + textBox5.Text + "%') or ([End Date:] like '%" + textBox5.Text + "%') ", con);
            
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
                dataGridView1.Rows[n].Cells[5].Value = item[5].ToString();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_Enter(object sender, EventArgs e)
        {
            if (textBox6.Text == "Enter Date: YYYY-MM-DD")
            {
                textBox6.Text = "";
                textBox6.ForeColor = Color.Black;
            }
        }

        private void textBox6_Leave(object sender, EventArgs e)
        {
            if (textBox6.Text == "")
            {
                textBox6.Text = "Enter Date: YYYY-MM-DD";
                textBox6.ForeColor = Color.Silver;
            }
        }

        private void textBox7_Enter(object sender, EventArgs e)
        {
            if (textBox7.Text == "Enter Date: YYYY-MM-DD")
            {
                textBox7.Text = "";
                textBox7.ForeColor = Color.Black;
            }
        }

        private void textBox7_Leave(object sender, EventArgs e)
        {
            if (textBox7.Text == "")
            {
                textBox7.Text = "Enter Date: YYYY-MM-DD";
                textBox7.ForeColor = Color.Silver;
            }
        }
    }
}
