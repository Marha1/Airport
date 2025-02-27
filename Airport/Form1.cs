using Airport.Data;
using Airport.Models.Airport.Models;
using Airport.Models;
using Airport.Primitives;
using Airport.Operator;
using System.Windows.Forms;
using Airport.User;

namespace Airport
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            // Проверяем, что это не аварийное закрытие
            if (e.CloseReason == CloseReason.UserClosing)
            {
                // Показываем окно подтверждения
                DialogResult result = MessageBox.Show(
                    "Вы уверены, что хотите выйти?",
                    "Подтверждение выхода",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                // Если пользователь выбрал "Нет", отменяем закрытие
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            CheckFly checkFly = new CheckFly();
            checkFly.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            InfoPass infoPass = new InfoPass();
            infoPass.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            СheckInfoUser сheckInfo = new СheckInfoUser();
            сheckInfo.Show();
        }
    }
}
