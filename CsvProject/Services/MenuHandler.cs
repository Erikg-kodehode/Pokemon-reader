using CsvProject.Controllers;

namespace CsvProject.Services;

public class MenuHandler
{
    public static class MenuOptions
    {
        public const int SEARCH_BY_NAME = 1;
        public const int SEARCH_BY_TYPE = 2;
        public const int SEARCH_BY_TOTAL = 3;
        public const int SHOW_TOP_BY_STAT = 4;
        public const int LIST_ALL_TYPES = 5;
        public const int QUERY_LEGENDARY = 6;
        public const int QUERY_STAGE = 7;
        public const int EXIT = 8;
    }

    private readonly PokemonController _controller;

    public MenuHandler(PokemonController controller)
    {
        _controller = controller;
    }

    public void Run()
    {
        bool running = true;
        while (running)
        {
            DisplayMenuOptions();
            var input = GetUserInput("Your choice: ");
            running = ProcessMenuSelection(input);
        }
    }

    private void DisplayMenuOptions()
    {
        Console.WriteLine("\nChoose an option:");
        Console.WriteLine($"{MenuOptions.SEARCH_BY_NAME}. Search by name");
        Console.WriteLine($"{MenuOptions.SEARCH_BY_TYPE}. Search by type");
        Console.WriteLine($"{MenuOptions.SEARCH_BY_TOTAL}. Search by total stats range");
        Console.WriteLine($"{MenuOptions.SHOW_TOP_BY_STAT}. Show top N Pokémon by stat");
        Console.WriteLine($"{MenuOptions.LIST_ALL_TYPES}. List all Pokémon types");
        Console.WriteLine($"{MenuOptions.QUERY_LEGENDARY}. Search Legendary Pokémon");
        Console.WriteLine($"{MenuOptions.QUERY_STAGE}. Search by Evolution Stage");
        Console.WriteLine($"{MenuOptions.EXIT}. Exit");
    }

    private string GetUserInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    private bool ProcessMenuSelection(string input)
    {
        if (!int.TryParse(input?.Trim(), out int choice))
            return true;

        switch (choice)
        {
            case MenuOptions.SEARCH_BY_NAME:
                HandleSearchByName();
                return true;
            case MenuOptions.SEARCH_BY_TYPE:
                HandleSearchByType();
                return true;
            case MenuOptions.SEARCH_BY_TOTAL:
                HandleSearchByTotalRange();
                return true;
            case MenuOptions.SHOW_TOP_BY_STAT:
                HandleTopByStat();
                return true;
            case MenuOptions.LIST_ALL_TYPES:
                _controller.ListAllTypesConsole();
                return true;
            case MenuOptions.QUERY_LEGENDARY:
                HandleLegendarySearch();
                return true;
            case MenuOptions.QUERY_STAGE:
                HandleStageSearch();
                return true;
            case MenuOptions.EXIT:
                Console.WriteLine("Exiting program...");
                return false;
            default:
                Console.WriteLine("Invalid choice, try again.");
                return true;
        }
    }

    private void HandleSearchByName()
    {
        var input = GetUserInput("Enter Pokémon name: ").Trim();
        if (!string.IsNullOrEmpty(input))
            _controller.QueryByNameConsole(input);
    }

    private void HandleSearchByType()
    {
        var input = GetUserInput("Enter Pokémon type: ").Trim();
        if (!string.IsNullOrEmpty(input))
            _controller.QueryByTypeConsole(input);
    }

    private void HandleSearchByTotalRange()
    {
        var min = GetUserInput("Minimum total: ");
        var max = GetUserInput("Maximum total: ");
        if (int.TryParse(min, out int minTotal) && int.TryParse(max, out int maxTotal))
            _controller.QueryByTotalRangeConsole(minTotal, maxTotal);
    }

    private void HandleTopByStat()
    {
        var stat = GetUserInput("Stat (HP, Attack, Defense, Speed, Total): ").Trim();
        var count = GetUserInput("How many Pokémon to list: ");
        if (int.TryParse(count, out int topN))
            _controller.ShowTopByStatConsole(stat, topN);
    }

    private void HandleLegendarySearch()
    {
        var legendaryInput = GetUserInput("Legendary Pokémon only? (yes/no): ").Trim().ToLower();
        bool isLegendary = legendaryInput is "yes" or "y";
        _controller.QueryByLegendaryConsole(isLegendary);
    }

    private void HandleStageSearch()
    {
        var stageInput = GetUserInput("Evolution stage (1, 2, or 3): ").Trim();
        if (int.TryParse(stageInput, out int stage))
            _controller.QueryByStageConsole(stage);
    }
}
