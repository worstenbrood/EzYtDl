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

    public class LockedList<T> : List<T>
    {
        public new void Add(T item)
        {
            lock (this)
            {
                base.Add(item);
            }
        }

        public new void Insert(int index, T item)
        {
            lock (this)
            {
                base.Insert(index, item);
            }
        }

        public new void Remove(T item)
        {
            lock (this)
            {
                base.Remove(item);
            }
        }
    }

    public class History : JsonFile
    {
        // Config

        [JsonProperty(PropertyName = "items")]
        public LockedList<HistoryItem> Items { get; set; } = new LockedList<HistoryItem>();

        private const string DefaultFilename = "history.json";

        private static readonly Lazy<History> Lazy = new Lazy<History>
            (() => new History(DefaultFilename, CommonTools.ApplicationName));

        public static History Default => Lazy.Value;

        public History(string filename, string subFolder = null, bool load = true) : base(filename, load, subFolder)
        {
        }

        public void Add(string title, string url)
        {
            var item = Items.FirstOrDefault(i => i.Url == url);
            if (item != null)
            {
                Items.Remove(item);
                item.Title = title;
            }
            else
            {
                item = new HistoryItem(title, url);
            }

            Items.Insert(0, item);
        }
    }
}
