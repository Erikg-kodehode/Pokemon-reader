using CsvHelper;
using CsvHelper.Configuration;
using CsvProject.Models;
using System.Globalization;

namespace CsvProject.Services;

public static class CsvReaderService
{
    public static List<Pokemon> ReadPokemonCsv(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HeaderValidated = null,
            MissingFieldFound = null,
            PrepareHeaderForMatch = args => args.Header?
                .Replace("Type 1", "Type1")
                .Replace("Type 2", "Type2")
                .Replace("Sp. Atk", "SpAtk")
                .Replace("Sp. Def", "SpDef")
                .Trim()
        };

        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, config);

        return csv.GetRecords<Pokemon>().ToList();
    }
}
