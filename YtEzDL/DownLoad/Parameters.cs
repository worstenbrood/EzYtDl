using System.Collections.Generic;

namespace YtEzDL.DownLoad
{
    public class Parameters : Dictionary<string, string>
    {
        protected void AddParameter(string key, string value = null)
        {
            this[key] = value;
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
