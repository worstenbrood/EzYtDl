using System.Collections.Generic;

namespace YtEzDL.DownLoad
{
    public class Parameters : Dictionary<string, string>
    {
        protected T AddParameter<T>(string key, string value = null, bool enclose = true)
            where T: Parameters
        {
            this[key] = !string.IsNullOrEmpty(value) ? enclose ? $"\"{value}\"" : value : value;
            return (T)this;
        }

        public T Reset<T>()
            where T : Parameters
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
