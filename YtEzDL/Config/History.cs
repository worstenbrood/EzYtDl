using System;
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
        public string Url;
        
        [JsonProperty(PropertyName = "id")]
        public string Id { get; }

        public HistoryItem(string title, string url, string id)
        {
            Title = title;
            Url = url;
            Id = id;
        }
    }

    public class HistoryList : List<HistoryItem>
    {
        public void Add(string title, string url, string id)
        {
            lock (this)
            {

                var item = Find(i => i.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
                if (item != null)
                {
                    Remove(item);
                    item.Url = url;
                    item.Title = title;
                }
                else
                {
                    item = new HistoryItem(title, url, id);
                }

                Insert(0, item);
            }
        }

        public new void Clear()
        {
            lock (this)
            {
                base.Clear();
            }
        }

        public new int Count
        {
            get
            {
                lock (this)
                {
                    return base.Count;
                }
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

        public void Add(string title, string url, string id)
        {
            if (Configuration.Default.ApplicationSettings.EnableHistory)
            {
                Items.Add(title, url, id);
            }
        }

        public void Clear()
        {
            Items.Clear();
        }

        [JsonProperty(PropertyName = "count")]
        public int Count => Items.Count;
    }
}
