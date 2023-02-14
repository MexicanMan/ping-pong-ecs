namespace BeresnevTest.Common
{
    public interface IReadable<out TVariable>
    {
        TVariable Value { get; }
    }
}