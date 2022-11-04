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
    public partial class frm_register : Form
    {

        String conn_str = "server=localhost;user id=root;Password=Masonlily2002!;database=se_project";

        public frm_register()
        {
            InitializeComponent();

        }
         
        private void btn_register_Click(object sender, EventArgs e)
        {
            if (tb_username.Text == "" && tb_password.Text == "" && tb_conPass.Text == "")
            {
                MessageBox.Show("Some details were missing, please make sure none of the text boxes are empty", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
            else if(tb_password.Text != tb_conPass.Text)
            {
                MessageBox.Show("The passwords you entered did not match, please make sure they are the same", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                Console.WriteLine(tb_username.Text);
                Console.WriteLine(tb_password.Text);

                using (MySqlConnection conn = new MySqlConnection(conn_str))
                {
                    String query = "INSERT INTO se_project.user (username, password) VALUES (@username, @password)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@username", tb_username.Text);
                        cmd.Parameters.AddWithValue("@password", tb_password.Text);

                        conn.Open();
                        int result = cmd.ExecuteNonQuery();

                        if (result < 0)
                        {
                            MessageBox.Show("Error inserting dataing into database", "Failure", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        MessageBox.Show("User '" + tb_username.Text + "' added to database", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }

                tb_username.Text = "";
                tb_password.Text = "";
                tb_conPass.Text = "";

                MessageBox.Show("You have succesfully registered an account", "Registration Successful", MessageBoxButtons.OK, MessageBoxIcon.Error);

                var frm_login = new frm_login();
                frm_login.Show();
                this.Hide();
                
            }
        }

        private void cb_showPass_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_showPass.Checked)
            {
                tb_password.PasswordChar = '\0';
                tb_conPass.PasswordChar = '\0';
            } else
            {
                tb_password.PasswordChar = '*';
                tb_conPass.PasswordChar = '*';
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {
            var frm_login = new frm_login();
            frm_login.Show();
            this.Hide();
        }
    }
}
