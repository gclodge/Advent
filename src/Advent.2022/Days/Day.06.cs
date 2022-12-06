namespace Advent._2022;

public class DatastreamBuffer
{
    private readonly string _input;

    public DatastreamBuffer(string input)
    {
        _input = input;
    }

    public int GetFirstStartOfPacketMarker(int markerSize)
    {
        var q = new Queue<char>();
        foreach (int i in Enumerable.Range(0, _input.Length))
        {
            if (q.Count != markerSize)
            {
                q.Enqueue(_input[i]);
                continue;
            }

            if (q.Distinct().Count() == markerSize) return i;

            q.Dequeue();
            q.Enqueue(_input[i]);
        }

        return -1;
    }

}