﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Xunit;
using Assert = Xunit.Assert;

using Advent._2021;

namespace Advent.Tests._2021
{
    public class Day04 : IDailyTest
    {
        public int Number => 4;
        public int Year => 2021;

        public string Input => TestHelper.GetInputFile(this);
        public string Test => TestHelper.GetTestFile(this);

        [Fact]
        public void Test_KnownInputs()
        {
            var bingo = new Bingo(Test.Parse());
            bingo.Play();

            int score = bingo.Scores.First();
            int expectedScore = 4512;
            Assert.Equal(expectedScore, score);
        }

        [Fact]
        public void PartOne()
        {
            var bingo = new Bingo(Input.Parse());
            bingo.Play();

            int score = bingo.Scores.First();
            int expectedScore = 34506;
            Assert.Equal(expectedScore, score);
        }

        [Fact]
        public void PartTwo()
        {
            var bingo = new Bingo(Input.Parse());
            bingo.Play();

            int score = bingo.Scores.Last();
            int expectedScore = 7686;
            Assert.Equal(expectedScore, score);
        }
    }
}
