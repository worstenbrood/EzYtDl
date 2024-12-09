using System;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using YtEzDL.Utils;

namespace YtEzDL.Config
{
    public class HistoryItem
    {
        [JsonProperty(PropertyName = "title")]
        public string Title;

        [JsonProperty(PropertyName = "url")] 
        public string Url { get; }
        
        public HistoryItem(string title, string url)
        {
            Title = title;
            Url = url;
        }
    }

    public class HistoryList : List<HistoryItem>
    {
        public void Add(string title, string url)
        {
            lock (this)
            {

                var item = Find(i => i.Url == url);
                if (item != null)
                {
                    Remove(item);
                    item.Title = title;
                }
                else
                {
                    item = new HistoryItem(title, url);
                }

                Insert(0, item);
            }
        }
    }

    public class History : JsonFile
    {
        // Config

        [JsonProperty(PropertyName = "items")]
        public HistoryList Items { get; set; } = new HistoryList();

        private const string DefaultFilename = "history.json";

        private static readonly Lazy<History> Lazy = new Lazy<History>
            (() => new History(DefaultFilename, CommonTools.ApplicationName));

        public static History Default => Lazy.Value;

        public History(string filename, string subFolder = null, bool load = true) : base(filename, load, subFolder)
        {
        }

        public void Add(string title, string url)
        {
            Items.Add(title, url);
        }
    }
}
