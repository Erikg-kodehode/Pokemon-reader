using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PokemonReader.WinForms
{
    public partial class StartupForm : Form
    {
        public StartupForm()
        {
            InitializeComponent();
            SetupBackground();
            this.KeyDown += StartupForm_KeyDown;
            this.DoubleBuffered = true;
        }

        private void SetupBackground()
        {
            string imagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "pokedex_bg.png");

            if (File.Exists(imagePath))
            {
                using (var original = Image.FromFile(imagePath))
                {
                    // Adjust this crop to tightly frame the full Pokédex device
                    Rectangle cropArea = new Rectangle(120, 60, 820, 720); // X, Y, Width, Height

                    Bitmap cropped = new Bitmap(cropArea.Width, cropArea.Height);
                    using (Graphics g = Graphics.FromImage(cropped))
                    {
                        g.DrawImage(original, new Rectangle(0, 0, cropped.Width, cropped.Height), cropArea, GraphicsUnit.Pixel);
                    }

                    // Set form size and background
                    this.ClientSize = new Size(cropArea.Width, cropArea.Height);
                    this.BackgroundImage = cropped;
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }
            }
            else
            {
                MessageBox.Show("Background image not found at: " + imagePath, "Missing Image", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.BackColor = Color.Black;
            }

            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void StartupForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var mainForm = new MainForm();
                mainForm.Show();
                this.Hide();
                mainForm.FormClosed += (s, args) => this.Close();
            }
        }
    }
}