using System;
using System.Linq;
using System.Collections.Generic;

using Advent._2020;
using Xunit;

namespace Advent.Tests._2020
{
    public class Day05 : IDailyTest
    {
        public int Number => 5;
        public int Year => 2020;

        public string InputFile => TestHelper.GetInputFile(this);
        public string TestFile => TestHelper.GetTestFile(this);

        public static readonly List<Tuple<string, int>> KnownIDs = new List<Tuple<string, int>>()
        {
            Tuple.Create("BFFFBBFRRR", 567),
            Tuple.Create("FFFBBBFRRR", 119),
            Tuple.Create("BBFFBBFRLL", 820)
        };

        [Fact]
        public void Test_KnownIDs()
        {
            foreach (var tup in KnownIDs)
            {
                var id = BoardingPass.GetSeatID(tup.Item1);

                Assert.True(id.ID == tup.Item2);
            }
        }

        [Fact]
        public void PartOne()
        {
            var inputLines = InputFile.Parse();
            //< Parse each input line into a final 'SeatID'
            var seatIDs = inputLines.Select(x => BoardingPass.GetSeatID(x));
            //< Grab the largest 'SeatID.ID' encountered
            var maxID = seatIDs.Max(x => x.ID);
            //< Ensure we match that known result
            Assert.True(maxID == 878);
        }

        [Fact]
        public void PartTwo()
        {
            //< NOTE: Looking for IDs that are missing.. but also have existing +1/-1 seats

            var inputLines = InputFile.Parse();
            //< Parse each input line into a final 'SeatID'
            var seatIDs = inputLines.Select(x => BoardingPass.GetSeatID(x));
            //< Get a map of all existing ID -> SeatID pairs
            var seatMap = seatIDs.ToDictionary(x => x.ID, x => x);

            var possible = new List<SeatID>();
            for(int x = 0; x < BoardingPass.Rows.Count(); x++)
            {
                for (int y = 0; y < BoardingPass.Columns.Count(); y++)
                {
                    var seatID = new SeatID(x, y);

                    if (!seatMap.ContainsKey(seatID.ID))
                    {
                        int prevID = seatID.ID - 1;
                        int nextID = seatID.ID + 1;

                        if (seatMap.ContainsKey(prevID) && seatMap.ContainsKey(nextID))
                        {
                            possible.Add(seatID);
                        }
                    }
                }
            }

            Assert.True(possible.Single().ID == 504);
        }
    }
}
