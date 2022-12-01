using System.Linq;

using Advent._2020;

using Xunit;

namespace Advent.Tests._2020
{
    public class Day16 : IDailyTest
    {
        public int Number => 16;
        public int Year => 2020;

        public string Input => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownTickets()
        {
            var input = TestFile.Parse();

            var manager = new TicketManager(input);
            int error = manager.GetScanningErrorRate();

            Assert.True(error == 71);
        }

        [Fact]
        public void Test_KnownFields()
        {
            var input = TestHelper.GetTestFile(this, "Test2").Parse();

            var manager = new TicketManager(input);
            var positions = manager.GetValidFieldPositions();
            var values = manager.GetMyTicketValues(positions);

            Assert.True(values["row"] == 11);
            Assert.True(values["class"] == 12);
            Assert.True(values["seat"] == 13);
        }

        [Fact]
        public void PartOne()
        {
            var input = Input.Parse();

            var manager = new TicketManager(input);
            int error = manager.GetScanningErrorRate();

            Assert.True(error == 29759);
        }

        [Fact]
        public void PartTwo()
        {
            var input = Input.Parse();

            var manager = new TicketManager(input);
            var positions = manager.GetValidFieldPositions();
            var values = manager.GetMyTicketValues(positions);

            string target = "departure";
            //< Want the values that start with 'departure'
            var targetValues = values.Where(kvp => kvp.Key.StartsWith(target)).ToList();
            //< Ensure that it's only six values
            Assert.True(targetValues.Count == 6);

            long finalVal = 1;
            foreach (var kvp in targetValues)
            {
                finalVal *= kvp.Value;
            }

            Assert.True(finalVal == 1307550234719);
        }
    }
}
