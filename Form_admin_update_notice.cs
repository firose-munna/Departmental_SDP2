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
    public partial class Form_admin_update_notice : Form
    {
        public Form_admin_update_notice()
        {
            InitializeComponent();
            GetNoticeRecord();
        }
        private int serial;

        private void GetNoticeRecord()
        {
            MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
            con.Open();
            MySqlCommand cmd;
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM all_notice;";

            MySqlDataReader sdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            con.Close();
            dataGridView_admin_update_notice.DataSource = dt;
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

        private void Form_admin_update_notice_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView_admin_update_notice_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            serial = Convert.ToInt32(dataGridView_admin_update_notice.SelectedRows[0].Cells[0].Value);
        }

        private void btn_admin_update_notice_deleteGo_Click(object sender, EventArgs e)
        {
            if (this.serial != 0)
            {
                MySqlConnection con = new MySqlConnection(AppSettings.ConnectionString());
                con.Open();
                MySqlCommand cmd;
                cmd = con.CreateCommand();

                cmd.CommandText = "DELETE FROM all_notice WHERE Serial=@serial_NO";
                cmd.Parameters.AddWithValue("@serial_NO", this.serial);

                cmd.ExecuteNonQuery();


                con.Close();
                MessageBox.Show("Data successfully Deleted.");

                MySqlConnection con1 = new MySqlConnection(AppSettings.ConnectionString());
                con1.Open();
                MySqlCommand cmd1;
                cmd1 = con1.CreateCommand();
                cmd1.CommandText = "ALTER TABLE all_notice DROP Serial";
                cmd1.ExecuteNonQuery();
                con1.Close();

                MySqlConnection con2 = new MySqlConnection(AppSettings.ConnectionString());
                con2.Open();
                MySqlCommand cmd2;
                cmd2 = con2.CreateCommand();
                cmd2.CommandText = "ALTER TABLE all_notice ADD Serial int not null auto_increment primary key first";
                cmd2.ExecuteNonQuery();
                con2.Close();


                GetNoticeRecord();


            }
            else
            {
                MessageBox.Show("Please, select a Row.");
            }
        }
    }
}
