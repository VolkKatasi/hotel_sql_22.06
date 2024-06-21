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
using Microsoft.Data.Sqlite;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)  // исчезновение текста при нажатии
        {
            if (textBox1.Text != string.Empty)
            {
                textBox1.Text = string.Empty;
            }
        }

        private void textBox2_MouseDown(object sender, MouseEventArgs e)  // исчезновение текста при нажатии
        {
            if (textBox2.Text != string.Empty)
            {
                textBox2.Text = string.Empty;
                textBox2.PasswordChar = '*';
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            using (var connection = new SqliteConnection("Data Source=db/hotel.db"))  // база данных
            {
                connection.Open();
                SqliteCommand cmd = new SqliteCommand($"select Password, Id from Users where Login like \"{textBox1.Text}\";", connection);
                using (var ex = cmd.ExecuteReader())
                {
                    if (ex.HasRows && ex.GetString(0) == textBox2.Text)
                    {
                        this.Hide();
                        Bron bron = new Bron(ex.GetInt32(1));
                        bron.FormClosed += (Object, FormClosedEventArgs) => { this.Show(); };
                        bron.Show();
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль или логин!", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void linkLabel1_Click(object sender, EventArgs e)  // переход к окну регистрации, где можно создать уч. запись
        {
            this.Hide();
            Form2 frm = new Form2();
            frm.FormClosed += (Object, FormClosedEventArgs) => { this.Show(); };
            frm.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}