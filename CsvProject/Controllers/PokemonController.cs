using CsvProject.Models;

namespace CsvProject.Controllers;

public class PokemonController
{
    private readonly List<Pokemon> _pokemons;

    public PokemonController(List<Pokemon> pokemons)
    {
        _pokemons = pokemons;
    }

    public void QueryByName(string namePart)
    {
        var results = _pokemons
            .Where(p => p.Name != null && p.Name.Contains(namePart, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("No Pokémon found with that name.");
            return;
        }

        Console.WriteLine($"\nFound {results.Count} Pokémon(s):");
        foreach (var p in results)
        {
            Console.WriteLine($"- {p.Name} | HP: {p.HP}, Attack: {p.Attack}, Type: {p.Type1}/{p.Type2}");
        }
    }
    public void QueryByLegendary(bool isLegendary)
    {
        var results = _pokemons
            .Where(p => p.Legendary == isLegendary)
            .OrderBy(p => p.Name)
            .ToList();

        string status = isLegendary ? "Legendary" : "Non-Legendary";

        if (results.Count == 0)
        {
            Console.WriteLine($"No {status} Pokémon found.");
            return;
        }

        Console.WriteLine($"\n{results.Count} {status} Pokémon found:");
        foreach (var p in results)
        {
            Console.WriteLine($"- {p.Name} | Type: {p.Type1}/{p.Type2} | Total: {p.Total}");
        }
    }
    public void QueryByStage(int stage)
    {
        var results = _pokemons
            .Where(p => p.Stage == stage)
            .OrderBy(p => p.Name)
            .ToList();

        if (results.Count == 0)
        {
            Console.WriteLine($"No Pokémon found in evolution stage {stage}.");
            return;
        }

        Console.WriteLine($"\n{results.Count} Pokémon found in Stage {stage}:");
        foreach (var p in results)
        {
            Console.WriteLine($"- {p.Name} | Stage: {p.Stage} | Type: {p.Type1}/{p.Type2}");
        }
    }



    public void QueryByType(string type)
    {
        var results = _pokemons
            .Where(p =>
                (!string.IsNullOrEmpty(p.Type1) && p.Type1.Equals(type, StringComparison.OrdinalIgnoreCase)) ||
                (!string.IsNullOrEmpty(p.Type2) && p.Type2.Equals(type, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("No Pokémon found with that type.");
            return;
        }

        Console.WriteLine($"\nFound {results.Count} Pokémon(s) of type '{type}':");
        foreach (var p in results)
        {
            Console.WriteLine($"- {p.Name} | HP: {p.HP}, Attack: {p.Attack}, Type: {p.Type1}/{p.Type2}");
        }
    }

    public void QueryByTotalRange(int min, int max)
    {
        var results = _pokemons
            .Where(p => p.Total >= min && p.Total <= max)
            .OrderByDescending(p => p.Total)
            .ToList();

        if (results.Count == 0)
        {
            Console.WriteLine("No Pokémon found in that total range.");
            return;
        }

        Console.WriteLine($"\nFound {results.Count} Pokémon(s) with total stats between {min} and {max}:");
        foreach (var p in results)
        {
            Console.WriteLine($"- {p.Name} | Total: {p.Total}, HP: {p.HP}, Attack: {p.Attack}");
        }
    }

    public void ShowTopByStat(string stat, int count)
    {
        var property = typeof(Pokemon).GetProperty(stat, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
        if (property == null)
        {
            Console.WriteLine("Invalid stat. Valid options: HP, Attack, Defense, Speed, Total");
            return;
        }

        var results = _pokemons
            .OrderByDescending(p => (int?)property.GetValue(p))
            .Take(count)
            .ToList();

        Console.WriteLine($"\nTop {count} Pokémon by {stat}:");
        foreach (var p in results)
        {
            Console.WriteLine($"- {p.Name} | {stat}: {property.GetValue(p)}, Type: {p.Type1}/{p.Type2}");
        }
    }

    public void ListAllTypes()
    {
        var types = _pokemons
            .SelectMany(p => new[] { p.Type1, p.Type2 })
            .Where(t => !string.IsNullOrWhiteSpace(t))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderBy(t => t)
            .ToList();

        Console.WriteLine("\nAvailable Pokémon Types:");
        foreach (var type in types)
        {
            Console.WriteLine($"- {type}");
        }

        Console.WriteLine($"\nTotal types: {types.Count}");
    }
}