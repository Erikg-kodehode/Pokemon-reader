using CsvProject.Controllers;

namespace CsvProject.Services;

public class MenuHandler
{
    public static class MenuOptions
    {
        public const int SEARCH_BY_NAME = 1;
        public const int FILTER_BY_TYPE = 2;
        public const int FILTER_BY_LEGENDARY = 3;
        public const int FILTER_BY_STAGE = 4;
        public const int FILTER_BY_TOTAL_RANGE = 5;
        public const int SHOW_TOP_BY_STAT = 6;
        public const int LIST_ALL_TYPES = 7;
        public const int EXIT = 8;
    }

    private readonly PokemonController _controller;

    public MenuHandler(PokemonController controller)
    {
        _controller = controller;
    }

    public void Run()
    {
        while (true)
        {
            DisplayMenuOptions();
            var input = GetUserInput("Select an option: ");
            if (!ProcessMenuSelection(input)) break;
        }
    }

    private void DisplayMenuOptions()
    {
        Console.Clear();
        Console.WriteLine("==== Pokémon Query Menu ====\n");
        Console.WriteLine($"  [{MenuOptions.SEARCH_BY_NAME}] Search by Name");
        Console.WriteLine($"  [{MenuOptions.FILTER_BY_TYPE}] Filter by Type");
        Console.WriteLine($"  [{MenuOptions.FILTER_BY_LEGENDARY}] Filter by Legendary Status");
        Console.WriteLine($"  [{MenuOptions.FILTER_BY_STAGE}] Filter by Evolution Stage (1–3)");
        Console.WriteLine($"  [{MenuOptions.FILTER_BY_TOTAL_RANGE}] Filter by Total Stat Range");
        Console.WriteLine($"  [{MenuOptions.SHOW_TOP_BY_STAT}] Show Top N Pokémon by Stat");
        Console.WriteLine($"  [{MenuOptions.LIST_ALL_TYPES}] List All Available Types");
        Console.WriteLine($"  [{MenuOptions.EXIT}] Exit\n");
    }

    private string GetUserInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    private bool ProcessMenuSelection(string input)
    {
        if (!int.TryParse(input?.Trim(), out int choice)) return true;

        switch (choice)
        {
            case MenuOptions.SEARCH_BY_NAME:
                HandleSearchByName(); return true;
            case MenuOptions.FILTER_BY_TYPE:
                HandleSearchByType(); return true;
            case MenuOptions.FILTER_BY_LEGENDARY:
                HandleFilterByLegendary(); return true;
            case MenuOptions.FILTER_BY_STAGE:
                HandleFilterByStage(); return true;
            case MenuOptions.FILTER_BY_TOTAL_RANGE:
                HandleSearchByTotalRange(); return true;
            case MenuOptions.SHOW_TOP_BY_STAT:
                HandleTopByStat(); return true;
            case MenuOptions.LIST_ALL_TYPES:
                _controller.ListAllTypes(); return true;
            case MenuOptions.EXIT:
                Console.WriteLine("Exiting."); return false;
            default:
                Console.WriteLine("Invalid choice."); return true;
        }
    }

    private void HandleSearchByName()
    {
        var input = GetUserInput("Enter part of the Pokémon name: ").Trim();
        if (!string.IsNullOrEmpty(input))
            _controller.QueryByName(input);
    }

    private void HandleSearchByType()
    {
        var input = GetUserInput("Enter Pokémon type: ").Trim();
        if (!string.IsNullOrEmpty(input))
            _controller.QueryByType(input);
    }

    private void HandleSearchByTotalRange()
    {
        var minInput = GetUserInput("Minimum total stat value: ");
        var maxInput = GetUserInput("Maximum total stat value: ");
        if (int.TryParse(minInput, out int min) && int.TryParse(maxInput, out int max))
            _controller.QueryByTotalRange(min, max);
        else
            Console.WriteLine("Invalid input. Please enter numeric values.");
    }

    private void HandleTopByStat()
    {
        var stat = GetUserInput("Stat (HP, Attack, Defense, SpAtk, SpDef, Speed, Total): ").Trim();
        var countInput = GetUserInput("How many to show: ");
        if (int.TryParse(countInput, out int count))
            _controller.ShowTopByStat(stat, count);
        else
            Console.WriteLine("Invalid number.");
    }

    private void HandleFilterByLegendary()
    {
        var input = GetUserInput("Show only Legendary Pokémon? (yes/no): ").Trim().ToLower();
        if (input == "yes" || input == "y")
            _controller.QueryByLegendary(true);
        else if (input == "no" || input == "n")
            _controller.QueryByLegendary(false);
        else
            Console.WriteLine("Invalid input.");
    }

    private void HandleFilterByStage()
    {
        var input = GetUserInput("Enter evolution stage (1, 2, or 3): ").Trim();
        if (int.TryParse(input, out int stage) && stage is >= 1 and <= 3)
            _controller.QueryByStage(stage);
        else
            Console.WriteLine("Stage must be 1, 2, or 3.");
    }
}
