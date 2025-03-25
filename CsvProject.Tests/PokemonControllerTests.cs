using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using CsvProject.Controllers;
using CsvProject.Models;

namespace CsvProject.Tests
{
    public class PokemonControllerTests
    {
        private readonly List<Pokemon> _testPokemon;

        public PokemonControllerTests()
        {
            // Setup test data
            _testPokemon = new List<Pokemon>
            {
                new Pokemon { Name = "Bulbasaur", Type1 = "Grass", Type2 = "Poison", HP = 45 },
                new Pokemon { Name = "Charmander", Type1 = "Fire", Type2 = null, HP = 39 },
                new Pokemon { Name = "Squirtle", Type1 = "Water", Type2 = null, HP = 44 },
                new Pokemon { Name = "Pikachu", Type1 = "Electric", Type2 = null, HP = 35 },
                new Pokemon { Name = "Charizard", Type1 = "Fire", Type2 = "Flying", HP = 78 },
                new Pokemon { Name = "Gengar", Type1 = "Ghost", Type2 = "Poison", HP = 60 },
                new Pokemon { Name = "Gyarados", Type1 = "Water", Type2 = "Flying", HP = 95 },
                new Pokemon { Name = "Mewtwo", Type1 = "Psychic", Type2 = null, HP = 106 }
            };
        }

        #region QueryByType Tests

        [Fact]
        public void QueryByType_PrimaryType_ReturnsMatchingPokemon()
        {
            // Arrange
            var controller = new PokemonController(_testPokemon);
            var originalConsoleOut = Console.Out;
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            controller.QueryByType("Fire");
            var output = stringWriter.ToString().Trim();

            // Assert
            Assert.Contains("Charmander - Fire/", output);
            Assert.Contains("Charizard - Fire/Flying", output);
            Assert.DoesNotContain("Bulbasaur", output);
            Assert.DoesNotContain("Squirtle", output);

            // Cleanup
            Console.SetOut(originalConsoleOut);
        }

        [Fact]
        public void QueryByType_SecondaryType_ReturnsMatchingPokemon()
        {
            // Arrange
            var controller = new PokemonController(_testPokemon);
            var originalConsoleOut = Console.Out;
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            controller.QueryByType("Poison");
            var output = stringWriter.ToString().Trim();

            // Assert
            Assert.Contains("Bulbasaur - Grass/Poison", output);
            Assert.Contains("Gengar - Ghost/Poison", output);
            Assert.DoesNotContain("Pikachu", output);
            Assert.DoesNotContain("Mewtwo", output);

            // Cleanup
            Console.SetOut(originalConsoleOut);
        }

        [Fact]
        public void QueryByType_CaseInsensitiveSearch_ReturnsMatchingPokemon()
        {
            // Arrange
            var controller = new PokemonController(_testPokemon);
            var originalConsoleOut = Console.Out;
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            controller.QueryByType("water"); // lowercase search
            var output = stringWriter.ToString().Trim();

            // Assert
            Assert.Contains("Squirtle - Water/", output);
            Assert.Contains("Gyarados - Water/Flying", output);
            Assert.DoesNotContain("Charmander", output);
            Assert.DoesNotContain("Pikachu", output);

            // Cleanup
            Console.SetOut(originalConsoleOut);
        }

        [Fact]
        public void QueryByType_TypeNotFound_DisplaysNotFoundMessage()
        {
            // Arrange
            var controller = new PokemonController(_testPokemon);
            var originalConsoleOut = Console.Out;
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            controller.QueryByType("Rock"); // No Rock type in test data
            var output = stringWriter.ToString().Trim();

            // Assert
            Assert.Equal("No Pokémon found with that type.", output);
            Assert.DoesNotContain("Bulbasaur", output);
            Assert.DoesNotContain("Charmander", output);

            // Cleanup
            Console.SetOut(originalConsoleOut);
        }

        #endregion

        #region QueryByMinHP Tests

        [Fact]
        public void QueryByMinHP_ValidThreshold_ReturnsPokemonAboveThreshold()
        {
            // Arrange
            var controller = new PokemonController(_testPokemon);
            var originalConsoleOut = Console.Out;
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            controller.QueryByMinHP(70); // Only Charizard, Gyarados, and Mewtwo exceed this
            var output = stringWriter.ToString().Trim();

            // Assert
            Assert.Contains("Mewtwo: 106 HP", output);
            Assert.Contains("Gyarados: 95 HP", output);
            Assert.Contains("Charizard: 78 HP", output);
            Assert.DoesNotContain("Bulbasaur", output);
            Assert.DoesNotContain("Pikachu", output);

            // Cleanup
            Console.SetOut(originalConsoleOut);
        }

        [Fact]
        public void QueryByMinHP_HighThreshold_ReturnsNoPokemon()
        {
            // Arrange
            var controller = new PokemonController(_testPokemon);
            var originalConsoleOut = Console.Out;
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            controller.QueryByMinHP(200); // No Pokemon have HP this high
            var output = stringWriter.ToString().Trim();

            // Assert
            Assert.Empty(output);
            Assert.DoesNotContain("Mewtwo", output);
            Assert.DoesNotContain("Gyarados", output);

            // Cleanup
            Console.SetOut(originalConsoleOut);
        }

        [Fact]
        public void QueryByMinHP_ZeroThreshold_ReturnsAllPokemon()
        {
            // Arrange
            var controller = new PokemonController(_testPokemon);
            var originalConsoleOut = Console.Out;
            using var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act
            controller.QueryByMinHP(0); // All Pokemon have HP > 0
            var output = stringWriter.ToString().Trim();

            // Assert
            Assert.Contains("Mewtwo: 106 HP", output);
            Assert.Contains("Gyarados: 95 HP", output);
            Assert.Contains("Charizard: 78 HP", output);
            Assert.Contains("Gengar: 60 HP", output);
            Assert.Contains("Bulbasaur: 45 HP", output);
            Assert.Contains("Squirtle: 44 HP", output);
            Assert.Contains("Charmander: 39 HP", output);
            Assert.Contains("Pikachu: 35 HP", output);

            // Cleanup
            Console.SetOut(originalConsoleOut);
        }

        #endregion
    }
}

