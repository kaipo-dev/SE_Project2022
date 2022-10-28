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
    public partial class frm_main : Form
    {
        String conn_str = "server=localhost;user id=root;Password=Masonlily2002!;database=se_project";
        public frm_main()
        {
            InitializeComponent();
            populateListView();
        }

        private void populateListView()
        {
            listView1.View = View.Details;

            listView1.Columns.Add("ID", 30, HorizontalAlignment.Left);
            listView1.Columns.Add("First Name", 100, HorizontalAlignment.Left);
            listView1.Columns.Add("Last Name", 100, HorizontalAlignment.Left);


            String query = "SELECT * FROM se_project.client";

            using (MySqlConnection conn = new MySqlConnection(conn_str))
            {
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ListViewItem row = new ListViewItem(reader.GetInt32(0).ToString());
                            row.SubItems.Add(reader.GetString(1));
                            row.SubItems.Add(reader.GetString(2));
                            listView1.Items.Add(row);



                            //listView1.Items.Add(reader.GetInt32(0)+"\t"+reader.GetString(1)+"\t"+reader.GetString(2));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No rows found");
                    }
                    reader.Close();
                }
                conn.Close();
            }
        }
    }
}
