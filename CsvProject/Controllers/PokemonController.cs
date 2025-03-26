namespace CsvProject.Controllers
{
    using CsvProject.Models;

    public class PokemonController
    {
        private readonly List<Pokemon> _pokemons;

        public PokemonController(List<Pokemon> pokemons)
        {
            _pokemons = pokemons;
        }

        // GUI methods (return List<string>)

        public List<string> SearchByNameResults(string namePart)
        {
            return _pokemons
                .Where(p => p.Name.Contains(namePart, StringComparison.OrdinalIgnoreCase))
                .Select(p => $"{p.Name} | HP: {p.HP} | Type: {p.Type1}/{p.Type2}")
                .ToList();
        }

        public List<string> SearchByTypeResults(string type)
        {
            return _pokemons
                .Where(p => p.Type1.Equals(type, StringComparison.OrdinalIgnoreCase) ||
                            p.Type2.Equals(type, StringComparison.OrdinalIgnoreCase))
                .Select(p => $"{p.Name} | Type: {p.Type1}/{p.Type2}")
                .ToList();
        }

        public List<string> GetAllTypes()
        {
            return _pokemons
                .SelectMany(p => new[] { p.Type1, p.Type2 })
                .Where(t => !string.IsNullOrWhiteSpace(t))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .OrderBy(t => t)
                .ToList();
        }
        public List<string> QueryByTotalRangeResults(int min, int max)
        {
            return _pokemons
                .Where(p => p.Total >= min && p.Total <= max)
                .OrderByDescending(p => p.Total)
                .Select(p => $"{p.Name} | Total: {p.Total} | Type: {p.Type1}/{p.Type2}")
                .ToList();
        }

        public List<string> ShowTopByStatResults(string stat, int count)
        {
            var property = typeof(Pokemon).GetProperty(stat,
                System.Reflection.BindingFlags.IgnoreCase |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance);

            if (property == null)
            {
                return new List<string> { "Invalid stat selected." };
            }

            return _pokemons
                .OrderByDescending(p => (int)property.GetValue(p))
                .Take(count)
                .Select(p => $"{p.Name} | {stat}: {property.GetValue(p)} | Type: {p.Type1}/{p.Type2}")
                .ToList();
        }

        public List<string> QueryByLegendaryResults(bool isLegendary)
        {
            return _pokemons
                .Where(p => p.Legendary == isLegendary)
                .OrderBy(p => p.Name)
                .Select(p => $"{p.Name} | Type: {p.Type1}/{p.Type2} | Total: {p.Total}")
                .ToList();
        }

        public List<string> QueryByStageResults(int stage)
        {
            return _pokemons
                .Where(p => p.Stage == stage)
                .OrderBy(p => p.Name)
                .Select(p => $"{p.Name} | Stage: {p.Stage} | Type: {p.Type1}/{p.Type2}")
                .ToList();
        }

        // Console methods (no return, directly write to Console)

        public void QueryByNameConsole(string namePart)
        {
            var results = _pokemons
                .Where(p => p.Name.Contains(namePart, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!results.Any())
            {
                Console.WriteLine("No Pokémon found with that name.");
                return;
            }

            foreach (var p in results)
                Console.WriteLine($"{p.Name} | Type: {p.Type1}/{p.Type2} | HP: {p.HP}");
        }

        public void QueryByTypeConsole(string type)
        {
            var results = _pokemons
                .Where(p => p.Type1.Equals(type, StringComparison.OrdinalIgnoreCase) ||
                            p.Type2.Equals(type, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!results.Any())
            {
                Console.WriteLine("No Pokémon found with that type.");
                return;
            }

            foreach (var p in results)
                Console.WriteLine($"{p.Name} | Type: {p.Type1}/{p.Type2}");
        }

        public void QueryByTotalRangeConsole(int min, int max)
        {
            var results = _pokemons
                .Where(p => p.Total >= min && p.Total <= max)
                .OrderByDescending(p => p.Total)
                .ToList();

            if (!results.Any())
            {
                Console.WriteLine("No Pokémon found within that total range.");
                return;
            }

            foreach (var p in results)
                Console.WriteLine($"{p.Name} | Total: {p.Total}");
        }

        public void ShowTopByStatConsole(string stat, int count)
        {
            var property = typeof(Pokemon).GetProperty(stat,
                System.Reflection.BindingFlags.IgnoreCase |
                System.Reflection.BindingFlags.Public |
                System.Reflection.BindingFlags.Instance);

            if (property == null)
            {
                Console.WriteLine("Invalid stat selected.");
                return;
            }

            var results = _pokemons
                .OrderByDescending(p => (int)property.GetValue(p))
                .Take(count)
                .ToList();

            foreach (var p in results)
                Console.WriteLine($"{p.Name} | {stat}: {property.GetValue(p)}");
        }

        public void ListAllTypesConsole()
        {
            var types = GetAllTypes();
            Console.WriteLine("Pokémon Types:");
            foreach (var type in types)
                Console.WriteLine($"- {type}");
        }

        public void QueryByLegendaryConsole(bool isLegendary)
        {
            var results = _pokemons
                .Where(p => p.Legendary == isLegendary)
                .OrderBy(p => p.Name)
                .ToList();

            string label = isLegendary ? "Legendary" : "Non-Legendary";

            if (!results.Any())
            {
                Console.WriteLine($"No {label} Pokémon found.");
                return;
            }

            foreach (var p in results)
                Console.WriteLine($"{p.Name} | Type: {p.Type1}/{p.Type2} | Total: {p.Total}");
        }

        public void QueryByStageConsole(int stage)
        {
            var results = _pokemons
                .Where(p => p.Stage == stage)
                .OrderBy(p => p.Name)
                .ToList();

            if (!results.Any())
            {
                Console.WriteLine($"No Pokémon found in stage {stage}.");
                return;
            }

            foreach (var p in results)
                Console.WriteLine($"{p.Name} | Stage: {p.Stage}");
        }
    }
}
