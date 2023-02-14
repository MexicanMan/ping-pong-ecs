using System;

namespace BeresnevTest.Common
{
    public class ObservableVariable<TVariable> : IReadable<TVariable>, IChangeable<TVariable>
    {
        private TVariable _value;

        public event Action<TVariable> ValueChanged;

        public ObservableVariable()
            : this(default)
        {
        }

        public ObservableVariable(TVariable value)
        {
            _value = value;
        }

        public TVariable Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke(_value);
            }
        }
    }
}