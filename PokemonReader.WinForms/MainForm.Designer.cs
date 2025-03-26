namespace PokemonReader.WinForms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.btnEnter = new System.Windows.Forms.Button();

            this.tabControlFeatures = new System.Windows.Forms.TabControl();
            this.tabName = new System.Windows.Forms.TabPage();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnSearchByName = new System.Windows.Forms.Button();

            // 
            // panelMenu
            // 
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMenu.Controls.Add(this.lblWelcome);
            this.panelMenu.Controls.Add(this.btnEnter);
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(800, 450);
            this.panelMenu.TabIndex = 0;

            // 
            // lblWelcome
            // 
            this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWelcome.Location = new System.Drawing.Point(0, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(800, 100);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome to the Pokédex!";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEnter.Location = new System.Drawing.Point(350, 200);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(100, 40);
            this.btnEnter.TabIndex = 1;
            this.btnEnter.Text = "Enter Pokedex";
            this.btnEnter.UseVisualStyleBackColor = true;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);

            // 
            // tabControlFeatures
            // 
            this.tabControlFeatures.Controls.Add(this.tabName);
            this.tabControlFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlFeatures.Location = new System.Drawing.Point(0, 0);
            this.tabControlFeatures.Name = "tabControlFeatures";
            this.tabControlFeatures.SelectedIndex = 0;
            this.tabControlFeatures.Size = new System.Drawing.Size(800, 450);
            this.tabControlFeatures.TabIndex = 1;
            this.tabControlFeatures.Visible = false;

            // 
            // tabName
            // 
            this.tabName.Controls.Add(this.txtName);
            this.tabName.Controls.Add(this.btnSearchByName);
            this.tabName.Location = new System.Drawing.Point(4, 24);
            this.tabName.Name = "tabName";
            this.tabName.Padding = new System.Windows.Forms.Padding(3);
            this.tabName.Size = new System.Drawing.Size(792, 422);
            this.tabName.TabIndex = 0;
            this.tabName.Text = "Search by Name";
            this.tabName.UseVisualStyleBackColor = true;

            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(20, 20);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(200, 23);
            this.txtName.TabIndex = 0;

            // 
            // btnSearchByName
            // 
            this.btnSearchByName.Location = new System.Drawing.Point(230, 20);
            this.btnSearchByName.Name = "btnSearchByName";
            this.btnSearchByName.Size = new System.Drawing.Size(120, 23);
            this.btnSearchByName.TabIndex = 1;
            this.btnSearchByName.Text = "Search";
            this.btnSearchByName.UseVisualStyleBackColor = true;
            this.btnSearchByName.Click += new System.EventHandler(this.btnSearchByName_Click);

            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tabControlFeatures);
            this.Controls.Add(this.panelMenu);
            this.Name = "MainForm";
            this.Text = "Pokémon Reader";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);

            this.panelMenu.ResumeLayout(false);
            this.tabControlFeatures.ResumeLayout(false);
            this.tabName.ResumeLayout(false);
            this.tabName.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnEnter;

        private System.Windows.Forms.TabControl tabControlFeatures;
        private System.Windows.Forms.TabPage tabName;

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnSearchByName;
    }
}
