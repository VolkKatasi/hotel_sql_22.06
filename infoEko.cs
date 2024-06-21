using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class infoEko : Form
    {
        private readonly int m_nID;
        public infoEko(int ID)
        {
            m_nID = ID;
            InitializeComponent();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void info_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var connection = new SqliteConnection("Data Source=db/hotel.db"))  // база данных
            {
                connection.Open();

                SqliteCommand cmd = new SqliteCommand($"select busy from eko where n like \"{numericUpDown1.Value}\";", connection);
                using (var ex = cmd.ExecuteReader())
                {
                    if (ex.GetInt32(0) == 1)
                    {
                        MessageBox.Show("Номер уже занят! Займите другой", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                cmd.CommandText = $"UPDATE eko SET f_id=\"{m_nID}\", count=\"{numericUpDown2.Value}\", busy=\"1\" WHERE n=\"{numericUpDown1.Value}\"";
                cmd.ExecuteNonQuery();
                MessageBox.Show("Номер успешно забронирован.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }
    }
}
