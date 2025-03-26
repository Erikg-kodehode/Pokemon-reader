using CsvProject.Controllers;
using CsvProject.Models;
using CsvProject.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace PokemonReader.WinForms
{
    public partial class MainForm : Form
    {
        private PokemonController _controller;

        public MainForm()
        {
            InitializeComponent();

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

        private void btnEnter_Click(object sender, EventArgs e)
        {
            panelMenu.Visible = false;
            tabControlFeatures.Visible = true;
        }

        private void btnSearchByName_Click(object sender, EventArgs e)
        {
            string input = txtName.Text.Trim();
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Please enter a name to search.");
                return;
            }

            List<string> results = _controller.SearchByNameResults(input);
            DisplayResults(results);
        }

        private void DisplayResults(List<string> lines)
        {
            // You can later route this to a listbox inside each tab
            MessageBox.Show(string.Join(Environment.NewLine, lines), "Results");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
