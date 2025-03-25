using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using CsvProject;
using CsvProject.Controllers;
using CsvProject.Models;
using Xunit;
using Moq;

namespace CsvProject.Tests
{
    public class MenuHandlerTests
    {
        private readonly Mock<PokemonController> _mockController;
        private readonly List<Pokemon> _mockPokemonData;

        public MenuHandlerTests()
        {
            _mockPokemonData = new List<Pokemon>
            {
                new Pokemon { Name = "Bulbasaur", Type1 = "Grass", Type2 = "Poison", HP = 45 },
                new Pokemon { Name = "Charmander", Type1 = "Fire", Type2 = "", HP = 39 },
                new Pokemon { Name = "Squirtle", Type1 = "Water", Type2 = "", HP = 44 },
                new Pokemon { Name = "Pikachu", Type1 = "Electric", Type2 = "", HP = 35 }
            };

            _mockController = new Mock<PokemonController>();
        }

        [Fact]
        public void Run_SearchByTypeSelection_CallsQueryByType()
        {
            // Arrange
            var input = new StringReader("1\nFire\n3\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            _mockController.Setup(c => c.QueryByType("Fire"));
            
            var menuHandler = new MenuHandler(_mockController.Object);

            // Act
            menuHandler.Run();

            // Assert
            _mockController.Verify(c => c.QueryByType("Fire"), Times.Once);
            Assert.Contains("Search by Pokemon type", output.ToString());
            Assert.Contains("Enter the Pokemon type:", output.ToString());
        }

        [Fact]
        public void Run_SearchByHPSelection_CallsQueryByMinHP()
        {
            // Arrange
            var input = new StringReader("2\n50\n3\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            _mockController.Setup(c => c.QueryByMinHP(50));
            
            var menuHandler = new MenuHandler(_mockController.Object);

            // Act
            menuHandler.Run();

            // Assert
            _mockController.Verify(c => c.QueryByMinHP(50), Times.Once);
            Assert.Contains("Search by minimum HP", output.ToString());
            Assert.Contains("Enter the minimum HP:", output.ToString());
        }

        [Fact]
        public void Run_ExitSelection_ExitsProgram()
        {
            // Arrange
            var input = new StringReader("3\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);
            
            var menuHandler = new MenuHandler(_mockController.Object);

            // Act
            menuHandler.Run();

            // Assert
            Assert.Contains("Exiting program", output.ToString());
            _mockController.Verify(c => c.QueryByType(It.IsAny<string>()), Times.Never);
            _mockController.Verify(c => c.QueryByMinHP(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void Run_InvalidMenuSelection_DisplaysErrorMessage()
        {
            // Arrange
            var input = new StringReader("5\n3\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);
            
            var menuHandler = new MenuHandler(_mockController.Object);

            // Act
            menuHandler.Run();

            // Assert
            Assert.Contains("Invalid option. Please try again.", output.ToString());
        }

        [Fact]
        public void Run_EmptyInput_HandlesGracefully()
        {
            // Arrange
            var input = new StringReader("\n3\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);
            
            var menuHandler = new MenuHandler(_mockController.Object);

            // Act
            menuHandler.Run();

            // Assert
            Assert.Contains("Invalid option. Please try again.", output.ToString());
        }

        [Fact]
        public void Run_InvalidHPInput_DisplaysErrorMessage()
        {
            // Arrange
            var input = new StringReader("2\nabc\n3\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);
            
            var menuHandler = new MenuHandler(_mockController.Object);

            // Act
            menuHandler.Run();

            // Assert
            Assert.Contains("Invalid HP value. Please enter a valid number.", output.ToString());
        }

        [Fact]
        public void Run_ValidTypeSearch_HandlesCorrectly()
        {
            // Arrange
            var input = new StringReader("1\nWater\n3\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            _mockController.Setup(c => c.QueryByType("Water"))
                .Callback(() => Console.WriteLine("Found Water Pokemon"));
            
            var menuHandler = new MenuHandler(_mockController.Object);

            // Act
            menuHandler.Run();

            // Assert
            _mockController.Verify(c => c.QueryByType("Water"), Times.Once);
            Assert.Contains("Found Water Pokemon", output.ToString());
        }

        [Fact]
        public void Run_MultipleMenuNavigations_HandlesCorrectly()
        {
            // Arrange
            var input = new StringReader("1\nFire\n2\n60\n3\n");
            var output = new StringWriter();
            Console.SetIn(input);
            Console.SetOut(output);

            _mockController.Setup(c => c.QueryByType("Fire"));
            _mockController.Setup(c => c.QueryByMinHP(60));
            
            var menuHandler = new MenuHandler(_mockController.Object);

            // Act
            menuHandler.Run();

            // Assert
            _mockController.Verify(c => c.QueryByType("Fire"), Times.Once);
            _mockController.Verify(c => c.QueryByMinHP(60), Times.Once);
            Assert.Contains("Search by Pokemon type", output.ToString());
            Assert.Contains("Search by minimum HP", output.ToString());
            Assert.Contains("Exiting program", output.ToString());
        }
    }
}

