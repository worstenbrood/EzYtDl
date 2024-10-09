using System.Timers;

namespace YtEzDL.Utils
{
    public class TimedVariable<T>
    {
        private readonly object _lock = new object();
        private T _value;
        private readonly double _lifetime;
        private readonly T _default;
        private Timer _timer;

        private void Callback(object o, ElapsedEventArgs a)
        {
            lock (_lock)
            {
                _value = _default;
            }
        }

        private void InitTimer()
        {
            _timer = new Timer(_lifetime)
            {
                AutoReset = true
            };
            _timer.Elapsed += Callback;
            _timer.Start();
        }

        public TimedVariable(T initialValue, double lifetime, T @default = default(T))
        {
            _value = initialValue;
            _lifetime = lifetime;
            _default = @default;
            InitTimer();
        }

        public T Value
        {
            get
            {
                lock (_lock)
                {
                    return _value;
                }
            }

            set
            {
                lock (_lock)
                {
                    _value = value;
                }
            }
        }
    }
}
