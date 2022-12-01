using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Advent.Tests.Selector
{
    internal record Entry(string name, double weight);

    internal static class Selector
    {
        public static int Count => _Entries.Count;

        static long Ticks => DateTime.UtcNow.Ticks;
        static readonly Random _Rand;

        static Selector()
        {
            _Rand = new Random((int)Ticks);
        }

        private static readonly HashSet<Entry> _Entries = new()
        {
            new Entry("League of Legends", 1.0),
            new Entry("PUBG", 1.0),
            new Entry("Rikku Bobbu League", 1.0),
            //new Entry("Rootin' Tootin' Cowboy Shootin'", 0.5),
        };

        public static Entry Select()
        {
            return _Entries.ElementAt(_Rand.Next(_Entries.Count));
        }
    }
}
