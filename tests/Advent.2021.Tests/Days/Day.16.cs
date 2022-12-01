using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent.Solutions.Utility;
using Advent.Solutions.Days;

namespace Advent.Tests.DailyTests
{
    public class Day16 : Interfaces.IDailyTest
    {
        public int Number => 16;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        static (string str, int idSum)[] TestInput = new[]
        {
            ("D2FE28", 16),
            ("620080001611562C8802118E34", 12),
            ("C0015000016115A2E0802F182340", 23),
            ("A0016C880162017C3686B18A3D4780", 31),
        };

        [Fact(Skip = "This feels like work but without the money")]
        public void Test_KnownInputs()
        {
            foreach (var input in TestInput)
            {
                var packet = new PacketDecoder(input.str);
                int debug = 0;
            }
        }

        [Fact(Skip = "This feels like work but without the money")]
        public void PartOne()
        {
           
        }

        [Fact(Skip = "This feels like work but without the money")]
        public void PartTwo()
        {
           
        }
    }
}
