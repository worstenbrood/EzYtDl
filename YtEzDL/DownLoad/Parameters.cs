using System.Collections.Generic;

namespace YtEzDL.DownLoad
{
    public class Parameters<T> : Dictionary<string, string>
        where T : Parameters<T>
    {
        protected T AddParameter(string key, string value = null, bool enclose = true)
        {
            this[key] = !string.IsNullOrEmpty(value) ? enclose ? $"\"{value}\"" : value : value;
            return (T)this;
        }

        public T Reset()
        {
            Clear();
            return (T)this;
        }

        public List<string> GetParameters()
        {
            var parameters = new List<string>();
            foreach (var parameter in this)
            {
                parameters.Add(parameter.Key);

                if (!string.IsNullOrEmpty(parameter.Value))
                {
                    parameters.Add(parameter.Value);
                }
            }

            return parameters;
        }
    }
}
