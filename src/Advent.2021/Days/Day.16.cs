namespace Advent._2021;

enum PacketType
{
    Literal,
    Operator,
    YerMum
}

class Packet
{
    public int Version { get; }
    public int TypeID { get; }

    public PacketType PacketType => TypeID != 4 ? PacketType.Operator : PacketType.Literal;

    public Packet(string source)
    {
        Version = source.GetValue(0, 3);
        TypeID = source.GetValue(3, 3);
    }
}

public class PacketDecoder
{
    public string Source { get; }
    public string Decoded { get; }

    Packet Packet { get; }

    IEnumerable<Packet> Packets;

    static readonly Dictionary<char, string> Decoder = new()
    {
        { '0', "0000" },
        { '1', "0001" },
        { '2', "0010" },
        { '3', "0011" },
        { '4', "0100" },
        { '5', "0101" },
        { '6', "0110" },
        { '7', "0111" },
        { '8', "1000" },
        { '9', "1001" },

        { 'A', "1010" },
        { 'B', "1011" },
        { 'C', "1100" },
        { 'D', "1101" },
        { 'E', "1110" },
        { 'F', "1111" },
    };

    public PacketDecoder(string input)
    {
        Source = input;
        Decoded = string.Join("", Source.Select(Decode));

        Packets = new List<Packet>();

        //< Parse the decoded string


        Packet = new Packet(input);

    }



    static string Decode(char c)
    {
        return Decoder[c];
    }
}

static class Extensions
{
    public static int GetValue(this string input, int index, int count)
    {
        var str = input.Substring(index, count);
        return Domain.Functions.CalculateBinaryValue(str);
    }
}