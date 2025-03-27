using CsvProject.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace CsvProject.Services
{
    public static class CsvReaderService
    {
        public static List<Pokemon> ReadPokemonCsv(string path)
        {
            var pokemons = new List<Pokemon>();

            using (var reader = new StreamReader(path))
            {
                // Read header line and ignore it
                string? headerLine = reader.ReadLine();
                if (headerLine == null)
                    throw new Exception("CSV file is empty");

                // Read each line
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    // Simple CSV splitting (assumes no commas inside fields)
                    var fields = line.Split(',');

                    // We expect at least 13 fields:
                    // 0: #, 1: Name, 2: Type1, 3: Type2, 4: Total, 5: HP,
                    // 6: Attack, 7: Defense, 8: Sp. Atk, 9: Sp. Def, 10: Speed, 11: Generation, 12: Legendary
                    if (fields.Length < 13)
                        continue;

                    // Create a Pokemon instance. Note that Stage is not in CSV, so default it to 1.
                    var pokemon = new Pokemon
                    {
                        Number = int.Parse(fields[0], CultureInfo.InvariantCulture),
                        Name = fields[1],
                        Type1 = fields[2],
                        Type2 = string.IsNullOrWhiteSpace(fields[3]) ? null : fields[3],
                        Total = int.Parse(fields[4], CultureInfo.InvariantCulture),
                        HP = int.Parse(fields[5], CultureInfo.InvariantCulture),
                        Attack = int.Parse(fields[6], CultureInfo.InvariantCulture),
                        Defense = int.Parse(fields[7], CultureInfo.InvariantCulture),
                        SpAtk = int.Parse(fields[8], CultureInfo.InvariantCulture),
                        SpDef = int.Parse(fields[9], CultureInfo.InvariantCulture),
                        Speed = int.Parse(fields[10], CultureInfo.InvariantCulture),
                        Legendary = bool.Parse(fields[12]),
                        Stage = 1  // Default stage; will be updated below.
                    };

                    pokemons.Add(pokemon);
                }
            }

            // Now assign evolution family and stage manually.
            // (You can expand this switch to cover more evolution chains as needed.)
            foreach (var p in pokemons)
            {
                if (string.IsNullOrWhiteSpace(p.Name))
                    continue;

                switch (p.Name)
                {
                    // Bulbasaur Family
                    case "Bulbasaur":
                    case "Ivysaur":
                    case "Venusaur":
                    case "VenusaurMega Venusaur":
                        p.EvolutionFamily = "Bulbasaur Family";
                        if (p.Name == "Bulbasaur")
                            p.Stage = 1;
                        else if (p.Name == "Ivysaur")
                            p.Stage = 2;
                        else // Venusaur and Mega Venusaur are final forms.
                            p.Stage = 3;
                        break;

                    // Charmander Family
                    case "Charmander":
                    case "Charmeleon":
                    case "Charizard":
                    case "CharizardMega Charizard X":
                    case "CharizardMega Charizard Y":
                        p.EvolutionFamily = "Charmander Family";
                        if (p.Name == "Charmander")
                            p.Stage = 1;
                        else if (p.Name == "Charmeleon")
                            p.Stage = 2;
                        else
                            p.Stage = 3;
                        break;

                    // Squirtle Family
                    case "Squirtle":
                    case "Wartortle":
                    case "Blastoise":
                    case "BlastoiseMega Blastoise":
                        p.EvolutionFamily = "Squirtle Family";
                        if (p.Name == "Squirtle")
                            p.Stage = 1;
                        else if (p.Name == "Wartortle")
                            p.Stage = 2;
                        else
                            p.Stage = 3;
                        break;

                    // Caterpie Family
                    case "Caterpie":
                    case "Metapod":
                    case "Butterfree":
                        p.EvolutionFamily = "Caterpie Family";
                        if (p.Name == "Caterpie")
                            p.Stage = 1;
                        else if (p.Name == "Metapod")
                            p.Stage = 2;
                        else
                            p.Stage = 3;
                        break;

                    // Weedle Family
                    case "Weedle":
                    case "Kakuna":
                    case "Beedrill":
                    case "BeedrillMega Beedrill":
                        p.EvolutionFamily = "Weedle Family";
                        if (p.Name == "Weedle")
                            p.Stage = 1;
                        else if (p.Name == "Kakuna")
                            p.Stage = 2;
                        else
                            p.Stage = 3;
                        break;

                    // If no known evolution data, assign its own name as family.
                    default:
                        p.EvolutionFamily = p.Name;
                        p.Stage = 1;
                        break;
                }
            }

            return pokemons;
        }
    }
}
