using System;
using System.Windows.Forms;

namespace c0br1nhada
{
    public partial class FormMainMenu : Form
    {
        public FormMainMenu()
        {
            InitializeComponent();

            // Tela cheia, sem borda, estilo profissa
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            // Opcional: centralizar botões no load, se quiser
            this.Load += (s, e) => CenterButtons();
            this.Resize += (s, e) => CenterButtons();
        }

        private void CenterButtons()
        {
            btnStart.Left = (this.ClientSize.Width - btnStart.Width) / 2;
            btnStart.Top = (this.ClientSize.Height / 2) - btnStart.Height - 10;

            btnExit.Left = (this.ClientSize.Width - btnExit.Width) / 2;
            btnExit.Top = (this.ClientSize.Height / 2) + 10;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (var gameForm = new FormGame())
            {
                gameForm.ShowDialog();
            }
            this.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
