using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

namespace Advent.Tests.Selector
{
    public class SelectorTests
    {
        [Fact]
        public void Select()
        {
            var thing = Selector.Select();

            string file = Path.Combine(Path.GetTempPath(), $"_Selector.File.txt");
            File.WriteAllText(file, thing.name);

            new Process
            {
                StartInfo = new ProcessStartInfo(file)
                {
                    UseShellExecute = true
                }
            }.Start();
        }

        [Fact]
        public void IsRandom_Test()
        {
            int items = 100;
            var things = Enumerable.Range(0, items)
                                   .Select(i => Selector.Select())
                                   .GroupBy(e => e.name)
                                   .ToDictionary(e => e.Key, e => e.Count());

            Assert.True(things.Count() == Selector.Count);
        }
    }
}
