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
    public partial class form_admin_update_routine : Form
    {
        public form_admin_update_routine()
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
                    comboBox_admin_update_routine_intake_select.Items.Add(sName);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            con.Close();
        }

        private void GetRoutineRecord()
        {
            MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM all_routine where Intake_No = '" + comboBox_admin_update_routine_intake_select.Text + "';";

            MySqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            con.Close();
            dataGridView_admin_update_routine.DataSource = dt;
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

        private void bnt_admin_update_routine_go_Click(object sender, EventArgs e)
        {
            GetRoutineRecord();
        }

        private void btn_admin_update_routine_deleteGo_Click(object sender, EventArgs e)
        {
            if (this.serial != 0)
            {
                MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();

                cmd.CommandText = "DELETE FROM all_routine WHERE Serial=@serial_NO";
                cmd.Parameters.AddWithValue("@serial_NO", this.serial);

                cmd.ExecuteNonQuery();


                con.Close();
                MessageBox.Show("Data successfully updated.");

                MySqlConnection con1 = new MySqlConnection(AppSettings.ConnectionString());
                con1.Open();
                MySqlCommand cmd1;
                cmd1 = con1.CreateCommand();
                cmd1.CommandText = "ALTER TABLE all_routine DROP Serial";
                cmd1.ExecuteNonQuery();
                con1.Close();

                MySqlConnection con2 = new MySqlConnection(AppSettings.ConnectionString());
                con2.Open();
                MySqlCommand cmd2;
                cmd2 = con2.CreateCommand();
                cmd2.CommandText = "ALTER TABLE all_routine ADD Serial int not null auto_increment primary key first";
                cmd2.ExecuteNonQuery();
                con2.Close();


                GetRoutineRecord();
                

            }
            else
            {
                MessageBox.Show("Please, select a Row.");
            }
        }

        private void dataGridView_admin_update_routine_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            serial = Convert.ToInt32(dataGridView_admin_update_routine.SelectedRows[0].Cells[0].Value);
        }
    }
}
