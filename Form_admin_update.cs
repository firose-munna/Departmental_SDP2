using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace smart_department
{
    public partial class Form_admin_update : Form
    {
        public Form_admin_update()
        {
            InitializeComponent();
        }

        private void btn_log_out_Click(object sender, EventArgs e)
        {
            Form_main fm2 = new Form_main();

            fm2.Show();
            MessageBox.Show("Successfully Log out");
            this.Hide();
        }

        private void btn_back_fm4_Click(object sender, EventArgs e)
        {
            Admin_Login_After_form1 fm4 = new Admin_Login_After_form1();
            fm4.Show();
            this.Hide();
        }

        private void btn_admin_update_basic_Click(object sender, EventArgs e)
        {
            Form_admin_update_basic fm31 = new Form_admin_update_basic();
            fm31.Show();
            this.Hide();
        }

        private void btn_admin_update_classlink_Click(object sender, EventArgs e)
        {
            Form_admin_update_classlink fm32 = new Form_admin_update_classlink();
            fm32.Show();
            this.Hide();
        }

        private void btn_admin_update_RBook_Click(object sender, EventArgs e)
        {
            Form_admin_update_RBook fm33 = new Form_admin_update_RBook();
            fm33.Show();
            this.Hide();
        }

        private void btn_admin_update_Slide_Record_Click(object sender, EventArgs e)
        {
            Form_admin_update_SlideRecord fm34 = new Form_admin_update_SlideRecord();
            fm34.Show();
            this.Hide();

        }

        private void btn_admin_update_routine_Click(object sender, EventArgs e)
        {
            form_admin_update_routine fm35 = new form_admin_update_routine();
            fm35.Show();
            this.Hide();
        }

        private void btn_admin_update_notice_Click(object sender, EventArgs e)
        {
            Form_admin_update_notice fm36 = new Form_admin_update_notice();
            fm36.Show();
            this.Hide();
        }
    }
}
