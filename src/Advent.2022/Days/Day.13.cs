using Advent.Domain;

namespace Advent._2022;

public class DistressSignalAnalyzer
{
    abstract class Item
    {
        public abstract override string ToString();

        public static int Compare(Item lhs, Item rhs)
        {
            if (lhs == rhs) return 0;

            if (lhs is IntItem lIntItem && rhs is IntItem rIntItem)
            {
                if (lIntItem.Value == rIntItem.Value) return 0;
                //< LHS < RHS -> correct order (-1)
                if (lIntItem.Value < rIntItem.Value) return -1;
                //< RHS > LHS -> wrong order (1)
                return 1;
            }

            if (lhs is not ListItem lListItem) lListItem = ListItem.Generate(lhs);
            if (rhs is not ListItem rListItem) rListItem = ListItem.Generate(rhs);

            var index = 0;
            while (index < lListItem.Items.Count)
            {
                //< If RHS runs out of items first -> wrong order
                if (index >= rListItem.Items.Count) return 1; 

                //< Compare the next element in each list
                int comp = Compare(lListItem.Items[index], rListItem.Items[index]);
                //< Both are equal - keep checkin'
                if (comp == 0)
                {
                    index++;
                    continue;
                }

                //< Order confirmed good/bad -> return
                return comp;
            }

            //< Unable to determine order, and lists are the same length
            if (lListItem.Items.Count == rListItem.Items.Count) return 0;

            //< The lists appear to be in the correct order
            return -1;
        }
    }

    class IntItem : Item
    {
        public int Value { get; set; }

        public static IntItem Generate(int val) => new() { Value = val };
        public static IntItem Generate(ReadOnlySpan<char> input, int start, int len)
        {
            return new IntItem()
            {
                Value = int.Parse(input.Slice(start, len))
            };
        }

        public override string ToString() => Value.ToString();
    }

    class ListItem : Item
    {
        public List<Item> Items { get; set; } = new();

        public static ListItem Generate(Item item) => new() { Items = new List<Item> { item }};
        public static ListItem Generate(IEnumerable<Item> items) => new() { Items = items.ToList() };

        public override string ToString() => $"[{string.Join(',', Items.Select(v => v.ToString()))}]";
    }

    private readonly ICollection<string> _input;
    public DistressSignalAnalyzer(IEnumerable<string> input)
    {
        _input = input.Where(l => !string.IsNullOrEmpty(l)).ToList();
    }

    public int PartOne()
    {
        int pair = 1;
        var pairs = new List<int>();
        for (int i = 0; i < _input.Count; i += 2)
        {
            var L = Parse(_input.ElementAt(i));
            var R = Parse(_input.ElementAt(i + 1));

            if (Item.Compare(L, R) == -1) pairs.Add(pair);

            pair++;
        }
        return pairs.Sum();
    }

    public int PartTwo()
    {
        var items = _input.Select(x => Parse(x)).ToList();

        //< Generate the two divider packets
        var A = ListItem.Generate(IntItem.Generate(2));
        var B = ListItem.Generate(IntItem.Generate(6));

        //< Truck 'em into that list, boah
        items.Add(A);
        items.Add(B);

        //< Sort the list using the Item.Compare function we made
        items.Sort(Item.Compare);

        return (items.IndexOf(A) + 1) * (items.IndexOf(B) + 1);
    }

    const char OpenBrace = '[';
    const char CloseBrace = ']';
    static Item Parse(ReadOnlySpan<char> input)
    {
        List<Item> items = new();

        int idx = 0;
        while (idx < input.Length)
        {
            var c = input[idx];
            if (c == OpenBrace) //< Have a contained ListItem
            {
                //< Retain current & starting indices
                int listStartIdx = idx;
                int listStringLength = 0;
                int listNesting = 1;
                //< Push the current index forward one
                idx++;
                //< Beging scanning remainder of contained ListItem
                while (listNesting > 0)
                {
                    c = input[idx++];
                    
                    //< Adjust list indices
                    if (c == OpenBrace) listNesting++;
                    else if (c == CloseBrace) listNesting--;

                    //< Iterate total list length
                    listStringLength++;
                }

                //< Recursivly parse the contents of the list and store as ListItem
                items.Add(Parse(input.Slice(listStartIdx + 1, listStringLength - 1)));

                //< Iterate once more to skip the comma
                idx++;
                continue;
            }

            //< Need to parse the IntItem
            int intStartIdx = idx;
            int intStrLen = 0;

            //< Loop until next comma
            while (idx < input.Length && char.IsDigit(input[idx++])) intStrLen++;

            //< If we gathered some values, create IntItem
            if (intStrLen > 0) items.Add(IntItem.Generate(input, intStartIdx, intStrLen));
        }

        return ListItem.Generate(items);
    }
}