using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace smart_department
{
    public partial class Form_admin_update_basic : Form
    {
        public Form_admin_update_basic()
        {
            InitializeComponent();
            Fill_Combo_Intake_Select();
        }
        private int serial;
        void Fill_Combo_Intake_Select()
        {
            MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
            string query = "select * from intake;";
            MySqlCommand cmd = new MySqlCommand(query, con);
            MySqlDataReader dt_reader;

            try
            {
                con.Open();
                dt_reader = cmd.ExecuteReader();

                while (dt_reader.Read())
                {
                    string sName = dt_reader.GetString("Intake_No");
                    comboBox_admin_update_basic_intake_select.Items.Add(sName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }
        private void Form_admin_update_basic_Load(object sender, EventArgs e)
        {

        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            Form_main fm2 = new Form_main();

            fm2.Show();
            MessageBox.Show("Successfully Log out");
            this.Hide();
        }

        private void btn_back_fm12_show_Click(object sender, EventArgs e)
        {
            Form_admin_update fm30 = new Form_admin_update();
            
            fm30.Show();
            this.Hide();
        }

        private void GetCourseRecord()
        {
            MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM courses where Intake_No = '" + comboBox_admin_update_basic_intake_select.Text + "';";

            MySqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            con.Close();
            dataGridView_admin_update_basic.DataSource = dt;
        }

        private void bnt_admin_update_basic_go_Click(object sender, EventArgs e)
        {
            GetCourseRecord();
        }

        private void dataGridView_admin_update_basic_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            serial = Convert.ToInt32(dataGridView_admin_update_basic.SelectedRows[0].Cells[0].Value);

            txt_admin_update_basic_intake.Text = dataGridView_admin_update_basic.SelectedRows[0].Cells[1].Value.ToString();
            txt_admin_update_basic_course_id.Text = dataGridView_admin_update_basic.SelectedRows[0].Cells[2].Value.ToString();
            txt_admin_update_basic_course_name.Text = dataGridView_admin_update_basic.SelectedRows[0].Cells[3].Value.ToString();

            
        }

        private void btn_admin_update_basic_updateGo_Click(object sender, EventArgs e)
        {
            if (this.serial != 0)
            {
                MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();

                cmd.CommandText = "UPDATE courses SET Intake_No = @intake, Course_ID=@C_ID, Course_Name=@C_Name WHERE Serial = @serial_NO";
                cmd.Parameters.AddWithValue("@serial_NO", this.serial);
                cmd.Parameters.AddWithValue("@intake", txt_admin_update_basic_intake.Text);
                cmd.Parameters.AddWithValue("@C_ID", txt_admin_update_basic_course_id.Text);
                cmd.Parameters.AddWithValue("@C_Name", txt_admin_update_basic_course_name.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data successfully updated.");
                GetCourseRecord();

                txt_admin_update_basic_intake.Clear();
                txt_admin_update_basic_course_id.Clear();
                txt_admin_update_basic_course_name.Clear();


            }
            else
            {
                MessageBox.Show("Please, select a Row.");
            }
        }

        private void btn_admin_update_basic_deleteGo_Click(object sender, EventArgs e)
        {
            if (this.serial != 0)
            {
                MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();

                cmd.CommandText = "DELETE FROM courses WHERE Serial=@serial_NO";
                cmd.Parameters.AddWithValue("@serial_NO", this.serial);
                
                cmd.ExecuteNonQuery();

                
                con.Close();
                MessageBox.Show("Data successfully updated.");

                MySqlConnection con1 = new MySqlConnection(AppSettings.ConnectionString());
                con1.Open();
                MySqlCommand cmd1;
                cmd1 = con1.CreateCommand();
                cmd1.CommandText = "ALTER TABLE courses DROP Serial";
                cmd1.ExecuteNonQuery();
                con1.Close();

                MySqlConnection con2 = new MySqlConnection(AppSettings.ConnectionString());
                con2.Open();
                MySqlCommand cmd2;
                cmd2 = con2.CreateCommand();
                cmd2.CommandText = "ALTER TABLE courses ADD Serial int not null auto_increment primary key first";
                cmd2.ExecuteNonQuery();
                con2.Close();

                
                GetCourseRecord();
                txt_admin_update_basic_intake.Clear();
                txt_admin_update_basic_course_id.Clear();
                txt_admin_update_basic_course_name.Clear();

            }
            else
            {
                MessageBox.Show("Please, select a Row.");
            }
        }
    }
}
