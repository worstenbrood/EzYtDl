namespace YtEzDL.Config
{
    public class LockedProperty<T>
    {
        private T _value;

        public LockedProperty(T @default)
        {
            _value = @default;
        }

        public void Set(T value)
        {
            lock (_value)
            {
                _value = value;
            }
        }

        public T Get()
        {
            lock (_value)
            {
                return _value;
            }
        }
    }
}