using CsvProject.Controllers;
using CsvProject.Services;

namespace CsvProject
{
    internal class Program
    {
        static void Main()
        {
            var pokemons = CsvReaderService.ReadPokemonCsv("Data/pokemon.csv");
            var controller = new PokemonController(pokemons);
            var menu = new MenuHandler(controller);
            menu.Run();
        }
    }
}
