namespace DemoApp.Services.Common;

public class SequentialCounter(int seed) : ICountGenerator
{
    private int current = seed;

    public int NextCount(int step) => Interlocked.Add(ref current, step);
}
