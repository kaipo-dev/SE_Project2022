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

namespace Login_Form
{
    public partial class frm_login : Form
    {
        String conn_str = "server=localhost;user id=root;Password=Masonlily2002!;database=se_project";

        public frm_login()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            if (tb_username.Text == "" && tb_password.Text == "")
            {
                MessageBox.Show("Username and/or password was missing, Please try again", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            else
            {
                String query = "SELECT user_ID FROM user WHERE username='" + tb_username.Text + "' AND password='" + tb_password.Text + "'";
                String result = "";

                using(MySqlConnection conn = new MySqlConnection(conn_str))
                {
                    using(MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        conn.Open();
                        result = (cmd.ExecuteScalar()).ToString();
                        
                    }
                }

                if (String.IsNullOrEmpty(result))
                {
                    MessageBox.Show("Username and/or password is incorrect, please try again", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                } 
                else
                {
                    MessageBox.Show("Logged IN!");
                    var main = new frm_main();
                    main.Show();
                    this.Hide();
                }
            }
        }

        private void cb_showPass_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_showPass.Checked)
            {
                tb_password.PasswordChar = '\0';
            }
            else
            {
                tb_password.PasswordChar = '*';
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
            var frm_register = new frm_register();
            frm_register.Show();
            this.Hide();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you would like to exit the application?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
