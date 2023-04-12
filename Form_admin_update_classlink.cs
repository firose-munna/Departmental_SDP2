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
    public partial class Form_admin_update_classlink : Form
    {
        public Form_admin_update_classlink()
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
                    comboBox_admin_update_classlink_intake_select.Items.Add(sName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
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
        private void GetclasslinkRecord()
        {
            MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM class_link where Intake_No = '" + comboBox_admin_update_classlink_intake_select.Text + "';";

            MySqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            con.Close();
            dataGridView_admin_update_classlink.DataSource = dt;
        }
        private void bnt_admin_update_classlink_go_Click(object sender, EventArgs e)
        {
            GetclasslinkRecord();
        }

        private void dataGridView_admin_update_classlink_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            serial = Convert.ToInt32(dataGridView_admin_update_classlink.SelectedRows[0].Cells[0].Value);

            txt_admin_update_classlink_intake.Text = dataGridView_admin_update_classlink.SelectedRows[0].Cells[1].Value.ToString();
            txt_admin_update_classlink_sec.Text = dataGridView_admin_update_classlink.SelectedRows[0].Cells[2].Value.ToString();
            txt_admin_update_classlink_course_id.Text = dataGridView_admin_update_classlink.SelectedRows[0].Cells[3].Value.ToString();
            txt_admin_update_classlink_google_code.Text = dataGridView_admin_update_classlink.SelectedRows[0].Cells[4].Value.ToString();
            txt_admin_update_classlink_link.Text = dataGridView_admin_update_classlink.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void btn_admin_update_classlink_updateGo_Click(object sender, EventArgs e)
        {
            if (this.serial != 0)
            {
                MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();

                cmd.CommandText = "UPDATE class_link SET Intake_No = @intake, Sec = @section, Course_ID=@C_ID, Classroom_Code=@C_code, Link = @c_link WHERE Serial = @serial_NO";
                cmd.Parameters.AddWithValue("@serial_NO", this.serial);
                cmd.Parameters.AddWithValue("@intake", txt_admin_update_classlink_intake.Text);
                cmd.Parameters.AddWithValue("@section", txt_admin_update_classlink_sec.Text);
                cmd.Parameters.AddWithValue("@C_ID", txt_admin_update_classlink_course_id.Text);
                cmd.Parameters.AddWithValue("@C_code", txt_admin_update_classlink_google_code.Text);
                cmd.Parameters.AddWithValue("@c_link", txt_admin_update_classlink_link.Text);
                
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data successfully updated.");
                GetclasslinkRecord();
                
                txt_admin_update_classlink_intake.Clear();
                txt_admin_update_classlink_sec.Clear();
                txt_admin_update_classlink_course_id.Clear();
                txt_admin_update_classlink_google_code.Clear();
                txt_admin_update_classlink_link.Clear();



            }
            else
            {
                MessageBox.Show("Please, select a Row.");
            }
        }

        private void btn_admin_update_classlink_deleteGo_Click(object sender, EventArgs e)
        {
            if (this.serial != 0)
            {
                MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();

                cmd.CommandText = "DELETE FROM class_link WHERE Serial=@serial_NO";
                cmd.Parameters.AddWithValue("@serial_NO", this.serial);

                cmd.ExecuteNonQuery();


                con.Close();
                MessageBox.Show("Data successfully updated.");

                MySqlConnection con1 = new MySqlConnection(AppSettings.ConnectionString());
                con1.Open();
                MySqlCommand cmd1;
                cmd1 = con1.CreateCommand();
                cmd1.CommandText = "ALTER TABLE class_link DROP Serial";
                cmd1.ExecuteNonQuery();
                con1.Close();

                MySqlConnection con2 = new MySqlConnection(AppSettings.ConnectionString());
                con2.Open();
                MySqlCommand cmd2;
                cmd2 = con2.CreateCommand();
                cmd2.CommandText = "ALTER TABLE class_link ADD Serial int not null auto_increment primary key first";
                cmd2.ExecuteNonQuery();
                con2.Close();


                GetclasslinkRecord();
                txt_admin_update_classlink_intake.Clear();
                txt_admin_update_classlink_sec.Clear();
                txt_admin_update_classlink_course_id.Clear();
                txt_admin_update_classlink_google_code.Clear();
                txt_admin_update_classlink_link.Clear();

            }
            else
            {
                MessageBox.Show("Please, select a Row.");
            }
        }

        private void Form_admin_update_classlink_Load(object sender, EventArgs e)
        {

        }
    }
}
