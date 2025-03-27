using CsvProject.Controllers;
using CsvProject.Models;
using CsvProject.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PokemonReader.WinForms
{
    public partial class MainForm : Form
    {
        private static readonly Color PokedexRedDark = Color.FromArgb(178, 8, 32);
        private static readonly Color ButtonBlack = Color.Black;
        private static readonly Color ScreenGreen = Color.FromArgb(50, 205, 50);

        private PokemonController _controller = null!;

        public MainForm()
        {
            InitializeComponent();

            InitializeData();
            StyleForm();
            StylePanels();
            StyleAllButtons();
            HideAllFeaturePanels();
            panelMenu.Visible = true;

            // Wire menu buttons
            btnNameMenu.Click += btnFeatureMenu_Click;
            btnTypeMenu.Click += btnFeatureMenu_Click;
            btnLegendaryMenu.Click += btnFeatureMenu_Click;
            btnStageMenu.Click += btnFeatureMenu_Click;

            // Name panel events
            btnSearchByName.Click += btnSearchByName_Click;
            btnBackFromName.Click += (s, e) => ShowMenu(panelName);

            // Type panel events
            btnSearchByType.Click += btnSearchByType_Click;
            btnShowAllTypes.Click += btnShowAllTypes_Click;
            btnBackFromType.Click += (s, e) => ShowMenu(panelType);
            listTypeResults.SelectedIndexChanged += listTypeResults_SelectedIndexChanged;

            // Legendary panel
            btnBackFromLegendary.Click += (s, e) => ShowMenu(panelLegendary);

            // Stage panel (evolution family)
            btnSearchStage.Click += btnStageSearch_Click;
            btnBackFromStage.Click += (s, e) => ShowMenu(panelStage);

            this.FormClosing += MainForm_FormClosing;
        }

        private void InitializeData()
        {
            try
            {
                string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "pokemon.csv");
                var pokemons = CsvReaderService.ReadPokemonCsv(csvPath);
                _controller = new PokemonController(pokemons);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
                Environment.Exit(1);
            }
        }

        private void HideAllFeaturePanels()
        {
            panelName.Visible = false;
            panelType.Visible = false;
            panelLegendary.Visible = false;
            panelStage.Visible = false;
        }

        private void ShowMenu(Panel currentPanel)
        {
            currentPanel.Visible = false;
            panelMenu.Visible = true;
            this.AcceptButton = null;
        }

        private void btnFeatureMenu_Click(object? sender, EventArgs e)
        {
            HideAllFeaturePanels();
            panelMenu.Visible = false;
            this.AcceptButton = null;

            if (sender == btnNameMenu)
            {
                panelName.Visible = true;
                this.AcceptButton = btnSearchByName;
            }
            else if (sender == btnTypeMenu)
            {
                panelType.Visible = true;
                this.AcceptButton = btnSearchByType;
            }
            else if (sender == btnLegendaryMenu)
            {
                panelLegendary.Visible = true;
                var legendaryResults = _controller.QueryByLegendaryResults(true);
                DisplayResults(legendaryResults, listLegendaryResults);
            }
            else if (sender == btnStageMenu)
            {
                panelStage.Visible = true;
                this.AcceptButton = btnSearchStage;
            }
        }

        // --- Name Panel ---
        private void btnSearchByName_Click(object? sender, EventArgs e)
        {
            string input = txtName.Text.Trim();
            DisplayResults(_controller.SearchByNameResults(input), listNameResults);
        }

        // --- Type Panel ---
        private void btnSearchByType_Click(object? sender, EventArgs e)
        {
            string type = txtType.Text.Trim();
            DisplayResults(_controller.SearchByTypeResults(type), listTypeResults);
        }

        private void btnShowAllTypes_Click(object? sender, EventArgs e)
        {
            var allTypes = _controller.GetAllTypes();
            DisplayResults(allTypes, listTypeResults);
        }

        private void listTypeResults_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (listTypeResults.SelectedItem != null)
            {
                string selectedType = listTypeResults.SelectedItem.ToString();
                txtType.Text = selectedType;
                var results = _controller.SearchByTypeResults(selectedType);
                DisplayResults(results, listTypeResults);
            }
        }

        // --- Stage Panel (Evolution Family) ---
        private void btnStageSearch_Click(object? sender, EventArgs e)
        {
            string searchName = txtStageSearch.Text.Trim();
            var family = _controller.GetEvolutionFamily(searchName);

            listStageResults.Items.Clear();

            if (family == null || family.Count == 0)
            {
                listStageResults.Items.Add("No results found.");
                return;
            }

            string familyName = $"Evolution Family: {string.Join(" → ", family)}";
            listStageResults.Items.Add(familyName);
            listStageResults.Items.Add(""); // Spacer
            listStageResults.Items.Add("Members:");
            foreach (var member in family)
            {
                listStageResults.Items.Add($"- {member}");
            }
        }

        // --- Helper ---
        private void DisplayResults(List<string> results, ListBox listBox)
        {
            listBox.Items.Clear();
            if (results.Count == 0)
            {
                listBox.Items.Add("No results found.");
            }
            else
            {
                foreach (var line in results)
                {
                    listBox.Items.Add(line);
                }
            }
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            Application.Exit();
            Environment.Exit(0);
        }

        // --- Styling ---
        private void StyleForm()
        {
            this.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = PokedexRedDark;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void StylePanels()
        {
            panelMenu.BackColor = PokedexRedDark;
            panelName.BackColor = PokedexRedDark;
            panelType.BackColor = PokedexRedDark;
            panelLegendary.BackColor = PokedexRedDark;
            panelStage.BackColor = PokedexRedDark;
        }

        private void StyleAllButtons()
        {
            void StyleButton(Button btn)
            {
                btn.BackColor = ButtonBlack;
                btn.ForeColor = ScreenGreen;
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
                btn.MouseEnter += (s, e) => btn.BackColor = Color.FromArgb(30, 30, 30);
                btn.MouseLeave += (s, e) => btn.BackColor = ButtonBlack;
                if (btn == btnNameMenu || btn == btnTypeMenu || btn == btnLegendaryMenu || btn == btnStageMenu)
                {
                    btn.Width = 220;
                    btn.Height = 60;
                }
            }

            // Menu
            StyleButton(btnNameMenu);
            StyleButton(btnTypeMenu);
            StyleButton(btnLegendaryMenu);
            StyleButton(btnStageMenu);

            // Name
            StyleButton(btnSearchByName);
            StyleButton(btnBackFromName);

            // Type
            StyleButton(btnSearchByType);
            StyleButton(btnShowAllTypes);
            StyleButton(btnBackFromType);

            // Legendary
            StyleButton(btnBackFromLegendary);

            // Stage
            StyleButton(btnSearchStage);
            StyleButton(btnBackFromStage);
        }
    }
}
