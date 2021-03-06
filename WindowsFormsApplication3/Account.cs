﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WindowsFormsApplication3
{
    public partial class Account : Form
    {
        MySqlConnection con = new MySqlConnection(@"Data Source = localhost;Initial Catalog = students;User id = root; password=''");
        MySqlDataReader MyReader;
        int id = 0;
        public Account()
        {
            InitializeComponent();
            GetAllStdByAccount();
        }

        //Open ViewAll Form
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewAll fm = new ViewAll();
            fm.Show();
        }

        //Refresh Operation
        private void button2_Click(object sender, EventArgs e)
        {
            GetAllStdByAccount();
        }

        //Search Operation
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select id, Name, Faculty, Grade, Bills from tbl_std where Name ='" 
                    + textBox6.Text + "' or id ='" + textBox6.Text + "'"; ;
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);          
            }
            con.Close();
        }

        //Insert Operation
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox5.Text != "")
            {
                string Query = "INSERT INTO tbl_std(id, Name, Faculty, Grade, Bills) VALUES ('" + this.textBox1.Text +
                    "','" + this.textBox2.Text +
                    "','" + this.textBox3.Text +
                    "','" + this.textBox4.Text +
                    "','" + this.textBox5.Text + "');";
                MySqlCommand cmd = new MySqlCommand(Query, con);

                try
                {
                    con.Open();
                    MyReader = cmd.ExecuteReader();
                    MessageBox.Show("Saved");

                    while (MyReader.Read())
                    {
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);   
                }
            }
            else
            {
                MessageBox.Show("Enter valid input.");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            con.Close();

            ClearData();
            GetAllStdByAccount();
            
        }

        //Update Operation
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "" && textBox5.Text != "")
                {
                    string Query = "update tbl_std set Name='" + this.textBox2.Text + "',Faculty='" 
                        + this.textBox3.Text + "',Grade='" + this.textBox4.Text + "',Bills='" 
                        + this.textBox5.Text + "' where id='" + this.textBox1.Text + "';";
                    MySqlCommand cmd2 = new MySqlCommand(Query, con);

                    con.Open();
                    MyReader = cmd2.ExecuteReader();
                    MessageBox.Show("Data Updated");
                    
                    
                    
                    while (MyReader.Read())
                    {
                    }
                }
                else
                {
                    MessageBox.Show("Invalid entry. Enter all fields");
                    
                }
            }
                
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);   
            }
            con.Close();
            
            ClearData();
            GetAllStdByAccount();            
        }

        //Delete Operation
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                string Query = "delete from tbl_std where id='" + this.textBox1.Text + "';";
                MySqlCommand MyCommand2 = new MySqlCommand(Query, con);

                con.Open();
                MyReader = MyCommand2.ExecuteReader();
                MessageBox.Show("Data Deleted");

               
                while (MyReader.Read())
                {
                }

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }

            con.Close();

            ClearData();
            GetAllStdByAccount();

            
        }

        //Operate on rows when clicked on
        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
        }

        //Display Student Table
        private void GetAllStdByAccount() {
            try
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select id, Name, Faculty, Grade, Bills from tbl_std";
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    dataGridView1.DataSource = dt;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        //Clear Input Box
        private void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

      
    }
}
