using System;

namespace BeresnevTest.Common
{
    public interface IChangeable<out TVariable>
    {
        event Action<TVariable> ValueChanged;
    }
}