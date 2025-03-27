using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PokemonReader.WinForms
{
    partial class MainForm
    {
        private IContainer components = null;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new Container();

            // Panels
            this.panelMenu = new Panel();
            this.panelName = new Panel();
            this.panelType = new Panel();
            this.panelLegendary = new Panel();
            this.panelStage = new Panel();

            // Menu Buttons
            this.btnNameMenu = new Button();
            this.btnTypeMenu = new Button();
            this.btnLegendaryMenu = new Button();
            this.btnStageMenu = new Button();

            // Name Panel Controls
            this.txtName = new TextBox();
            this.btnSearchByName = new Button();
            this.listNameResults = new ListBox();
            this.btnBackFromName = new Button();

            // Type Panel Controls
            this.txtType = new TextBox();
            this.btnSearchByType = new Button();
            this.btnShowAllTypes = new Button();
            this.listTypeResults = new ListBox();
            this.btnBackFromType = new Button();

            // Legendary Panel Controls
            this.listLegendaryResults = new ListBox();
            this.btnBackFromLegendary = new Button();

            // Stage Panel Controls (Evolution Family search)
            // Replace numeric input with a TextBox for search
            this.txtStageSearch = new TextBox();
            this.btnSearchStage = new Button();
            this.listStageResults = new ListBox();
            this.btnBackFromStage = new Button();

            // -------------------------------
            // panelMenu
            // -------------------------------
            this.panelMenu.Size = new Size(800, 500);
            this.panelMenu.Location = new Point(0, 0);
            this.panelMenu.Visible = true;
            // Main menu buttons (positioned vertically)
            this.btnNameMenu.Text = "Search by Name";
            this.btnNameMenu.Size = new Size(220, 60);
            this.btnNameMenu.Location = new Point(290, 50);

            this.btnTypeMenu.Text = "Search by Type";
            this.btnTypeMenu.Size = new Size(220, 60);
            this.btnTypeMenu.Location = new Point(290, 120);

            this.btnLegendaryMenu.Text = "Legendary Pokémon";
            this.btnLegendaryMenu.Size = new Size(220, 60);
            this.btnLegendaryMenu.Location = new Point(290, 190);

            // Rename Stage feature to "Evolution Family"
            this.btnStageMenu.Text = "Evolution Family";
            this.btnStageMenu.Size = new Size(220, 60);
            this.btnStageMenu.Location = new Point(290, 260);

            this.panelMenu.Controls.AddRange(new Control[]
            {
                this.btnNameMenu,
                this.btnTypeMenu,
                this.btnLegendaryMenu,
                this.btnStageMenu
            });

            // -------------------------------
            // panelName
            // -------------------------------
            this.txtName.Location = new Point(20, 20);
            this.txtName.Width = 150;
            this.btnSearchByName.Text = "Search";
            this.btnSearchByName.Location = new Point(200, 20);
            this.btnSearchByName.Size = new Size(80, 30);
            this.listNameResults.Location = new Point(20, 60);
            this.listNameResults.Size = new Size(400, 300);
            this.btnBackFromName.Text = "Back";
            this.btnBackFromName.Location = new Point(20, 370);
            this.btnBackFromName.Size = new Size(80, 30);
            this.panelName.Size = new Size(800, 500);
            this.panelName.Location = new Point(0, 0);
            this.panelName.Visible = false;
            this.panelName.Controls.AddRange(new Control[]
            {
                this.txtName,
                this.btnSearchByName,
                this.listNameResults,
                this.btnBackFromName
            });

            // -------------------------------
            // panelType
            // -------------------------------
            this.txtType.Location = new Point(20, 20);
            this.txtType.Width = 150;
            this.btnSearchByType.Text = "Search";
            this.btnSearchByType.Location = new Point(200, 20);
            this.btnSearchByType.Size = new Size(80, 30);
            this.btnShowAllTypes.Text = "Show All Types";
            this.btnShowAllTypes.Location = new Point(290, 20);
            this.btnShowAllTypes.Size = new Size(100, 30);
            this.listTypeResults.Location = new Point(20, 60);
            this.listTypeResults.Size = new Size(400, 300);
            this.btnBackFromType.Text = "Back";
            this.btnBackFromType.Location = new Point(20, 370);
            this.btnBackFromType.Size = new Size(80, 30);
            this.panelType.Size = new Size(800, 500);
            this.panelType.Location = new Point(0, 0);
            this.panelType.Visible = false;
            this.panelType.Controls.AddRange(new Control[]
            {
                this.txtType,
                this.btnSearchByType,
                this.btnShowAllTypes,
                this.listTypeResults,
                this.btnBackFromType
            });

            // -------------------------------
            // panelLegendary
            // -------------------------------
            this.listLegendaryResults.Location = new Point(20, 20);
            this.listLegendaryResults.Size = new Size(400, 340);
            this.btnBackFromLegendary.Text = "Back";
            this.btnBackFromLegendary.Location = new Point(20, 370);
            this.btnBackFromLegendary.Size = new Size(80, 30);
            this.panelLegendary.Size = new Size(800, 500);
            this.panelLegendary.Location = new Point(0, 0);
            this.panelLegendary.Visible = false;
            this.panelLegendary.Controls.AddRange(new Control[]
            {
                this.listLegendaryResults,
                this.btnBackFromLegendary
            });

            // -------------------------------
            // panelStage (Evolution Family)
            // -------------------------------
            // Use a TextBox to search for a Pokémon name
            this.txtStageSearch = new TextBox();
            this.txtStageSearch.Location = new Point(20, 20);
            this.txtStageSearch.Width = 150;
            this.txtStageSearch.PlaceholderText = "Enter Pokemon";
            this.btnSearchStage = new Button();
            this.btnSearchStage.Text = "Search";
            this.btnSearchStage.Location = new Point(200, 20);
            this.btnSearchStage.Size = new Size(80, 30);
            this.listStageResults = new ListBox();
            this.listStageResults.Location = new Point(20, 60);
            this.listStageResults.Size = new Size(400, 300);
            this.btnBackFromStage = new Button();
            this.btnBackFromStage.Text = "Back";
            this.btnBackFromStage.Location = new Point(20, 370);
            this.btnBackFromStage.Size = new Size(80, 30);
            this.panelStage = new Panel();
            this.panelStage.Size = new Size(800, 500);
            this.panelStage.Location = new Point(0, 0);
            this.panelStage.Visible = false;
            this.panelStage.Controls.AddRange(new Control[]
            {
            this.txtStageSearch,
            this.btnSearchStage,
            this.listStageResults,
            this.btnBackFromStage
            });

            // -------------------------------
            // Add panels to the Form
            // -------------------------------
            this.Controls.AddRange(new Control[]
            {
                this.panelMenu,
                this.panelName,
                this.panelType,
                this.panelLegendary,
                this.panelStage
            });

            // -------------------------------
            // Form properties
            // -------------------------------
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(800, 500);
            this.Text = "Pokémon Reader";
        }

        #endregion

        // Declarations
        private Panel panelMenu;
        private Panel panelName;
        private Panel panelType;
        private Panel panelLegendary;
        private Panel panelStage;

        private Button btnNameMenu;
        private Button btnTypeMenu;
        private Button btnLegendaryMenu;
        private Button btnStageMenu;

        private TextBox txtName;
        private Button btnSearchByName;
        private ListBox listNameResults;
        private Button btnBackFromName;

        private TextBox txtType;
        private Button btnSearchByType;
        private Button btnShowAllTypes;
        private ListBox listTypeResults;
        private Button btnBackFromType;

        private ListBox listLegendaryResults;
        private Button btnBackFromLegendary;

        private TextBox txtStageSearch;
        private Button btnSearchStage;
        private ListBox listStageResults;
        private Button btnBackFromStage;
    }
}
