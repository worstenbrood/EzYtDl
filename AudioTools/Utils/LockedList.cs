using System.Collections.Generic;

namespace AudioTools.Utils
{
    public class LockedList<T>
    {
        protected readonly List<T> List = new List<T>();
        protected readonly object ListLock = new object();

        public void Add(T item)
        {
            lock (ListLock)
            {
                List.Add(item);
            }
        }

        public void Add(int index, T item)
        {
            lock (ListLock)
            {
                List.Insert(index, item);
            }
        }

        public void Remove(T item)
        {
            lock (ListLock)
            {
                List.Remove(item);
            }
        }

        public void Remove(int index)
        {
            lock (ListLock)
            {
                if (index < List.Count)
                {
                    List.RemoveAt(index);
                }
            }
        }

        public int Count
        {
            get
            {
                lock (ListLock)
                {
                    return List.Count;
                }
            }
        }

        public T Get(int index)
        {
            lock (ListLock)
            {
                return List[index];
            }
        }
    }
}
