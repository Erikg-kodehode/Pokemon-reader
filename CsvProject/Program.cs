using CsvProject.Controllers;
using CsvProject.Services;
using CsvProject.Models;

namespace CsvProject;

class Program
{
    static void Main()
    {
        var csvPath = Path.Combine(AppContext.BaseDirectory, "Data", "pokemon.csv");
        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV file not found in 'Data' folder. Please enter the full path:");
            csvPath = Console.ReadLine()?.Trim() ?? "";
        }

        if (!File.Exists(csvPath))
        {
            Console.WriteLine("File not found. Exiting...");
            return;
        }

        List<Pokemon> pokemons;

        try
        {
            pokemons = CsvReaderService.ReadPokemonCsv(csvPath);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading CSV: {ex.Message}");
            return;
        }

        var controller = new PokemonController(pokemons);
        var menuHandler = new MenuHandler(controller);
        menuHandler.Run();
    }
}
