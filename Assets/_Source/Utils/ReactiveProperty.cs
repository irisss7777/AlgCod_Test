using System;

namespace Utils
{
    public class ReactiveProperty<T>
    {
        public ReactiveProperty(T value)
        {
            Value = value;
        }

        public event Action<T> OnValueChanged;
        public T Value { get; private set; }

        public void SetValue(T value)
        {
            Value = value;
            OnValueChanged?.Invoke(Value);
        }
    }
}