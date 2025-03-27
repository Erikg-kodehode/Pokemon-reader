using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace PokemonReader.WinForms
{
    public partial class StartupForm : Form
    {
        // Adjust these color constants as needed
        private static readonly Color ButtonBlack = Color.Black;
        private static readonly Color ScreenGreen = Color.FromArgb(50, 205, 50);

        public StartupForm()
        {
            InitializeComponent();
            StyleStartupForm();
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            var mainForm = new MainForm();
            mainForm.Show();
            this.Hide();
            mainForm.FormClosed += (s, args) => this.Close();
        }

        /// <summary>
        /// Applies styling, sets a background image, and positions controls.
        /// </summary>
        private void StyleStartupForm()
        {
            // 1) Try to load a background image (put your actual image path here)
            string bgImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "pokedex_bg.png");
            if (File.Exists(bgImagePath))
            {
                this.BackgroundImage = Image.FromFile(bgImagePath);
                this.BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                // Fallback color if image not found
                this.BackColor = Color.Crimson;
            }

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            // 2) Label styling (still use white text so it's readable)
            lblTitle.ForeColor = Color.White;
            lblTitle.Text = "Welcome to the Pokédex";
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Width) / 2;
            lblTitle.Top = 30;

            // 3) Button styling
            btnEnter.Width = 100;
            btnEnter.Height = 40;
            btnEnter.BackColor = ButtonBlack;
            btnEnter.ForeColor = ScreenGreen;
            btnEnter.FlatStyle = FlatStyle.Flat;
            btnEnter.FlatAppearance.BorderSize = 0;
            btnEnter.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnEnter.Left = (this.ClientSize.Width - btnEnter.Width) / 2;
            btnEnter.Top = 100;
            MakeButtonRounded(btnEnter, 15);

            // Optional hover effect
            btnEnter.MouseEnter += (s, e) => btnEnter.BackColor = Color.FromArgb(30, 30, 30);
            btnEnter.MouseLeave += (s, e) => btnEnter.BackColor = ButtonBlack;
        }

        /// <summary>
        /// Gives a button rounded corners.
        /// </summary>
        private void MakeButtonRounded(Button btn, int cornerRadius)
        {
            var path = new GraphicsPath();
            var rect = new Rectangle(0, 0, btn.Width, btn.Height);
            int diameter = cornerRadius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            btn.Region = new Region(path);
        }
    }
}
