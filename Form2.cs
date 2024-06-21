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

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void textBox3_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty || textBox2.Text == string.Empty || textBox3.Text == string.Empty)
            {
                MessageBox.Show("Заполните все окошки", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (textBox2.Text == textBox3.Text)
            {
                using (var connection = new SqliteConnection("Data Source=db/hotel.db"))  // база данных
                {
                    connection.Open();
                    SqliteCommand cmd = new SqliteCommand($"select Password from Users where Login like \"{textBox1.Text}\";", connection);
                    using (var ex = cmd.ExecuteReader())
                    {
                        if (ex.HasRows)
                        {
                            MessageBox.Show("Такой аккаунт уже существует!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                    }
                    cmd.CommandText = $"INSERT INTO Users (Login, Password) Values (\"{textBox1.Text}\", \"{textBox2.Text}\");";
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Ваш аккаунт успешно зареган", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Пароли должны совпадать", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
