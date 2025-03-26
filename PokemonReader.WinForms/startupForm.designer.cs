namespace PokemonReader.WinForms
{
    partial class StartupForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btnStart = new System.Windows.Forms.Button();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // btnStart
            this.btnStart.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnStart.Location = new System.Drawing.Point(100, 100);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(150, 40);
            this.btnStart.Text = "Enter Pokédex";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);

            // labelWelcome
            this.labelWelcome.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.labelWelcome.Text = "Welcome to the Pokémon Reader!";
            this.labelWelcome.Location = new System.Drawing.Point(30, 30);
            this.labelWelcome.AutoSize = true;

            // StartupForm
            this.ClientSize = new System.Drawing.Size(350, 180);
            this.Controls.Add(this.labelWelcome);
            this.Controls.Add(this.btnStart);
            this.Name = "StartupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pokémon Reader";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label labelWelcome;
    }
}
