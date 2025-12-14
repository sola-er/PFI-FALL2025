using System;
using System.Collections.Generic;

namespace GraphLib3
{
    /// <summary>
    /// Simple binary min-heap priority queue for .NET Standard 2.1.
    /// Uses (item, priority) tuples.
    /// </summary>
    public class PriorityQueue<T>
    {
        private readonly List<(T Item, int Priority)> _data;

        public int Count => _data.Count;

        public PriorityQueue()
        {
            _data = new List<(T, int)>();
        }

        /// <summary>
        /// Insert an item with the given priority (lower = earlier).
        /// </summary>
        public void Enqueue(T item, int priority)
        {
            _data.Add((item, priority));
            int child = _data.Count - 1;

            // Bubble up
            while (child > 0)
            {
                int parent = (child - 1) / 2;
                if (_data[parent].Priority <= _data[child].Priority)
                    break;

                Swap(parent, child);
                child = parent;
            }
        }

        /// <summary>
        /// Remove and return the item with the smallest priority.
        /// </summary>
        public T Dequeue()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("Priority queue is empty.");

            T rootItem = _data[0].Item;

            int last = _data.Count - 1;
            _data[0] = _data[last];
            _data.RemoveAt(last);

            last = _data.Count - 1;
            int parent = 0;

            // Bubble down
            while (true)
            {
                int left = 2 * parent + 1;
                if (left > last)
                    break; // no children

                int right = left + 1;

                // Pick smaller child
                int smallestChild = (right <= last &&
                                     _data[right].Priority < _data[left].Priority)
                                    ? right
                                    : left;

                if (_data[parent].Priority <= _data[smallestChild].Priority)
                    break;

                Swap(parent, smallestChild);
                parent = smallestChild;
            }

            return rootItem;
        }

        /// <summary>
        /// Look at the item with the smallest priority without removing it.
        /// </summary>
        public T Peek()
        {
            if (_data.Count == 0)
                throw new InvalidOperationException("Priority queue is empty.");
            return _data[0].Item;
        }

        private void Swap(int i, int j)
        {
            (_data[j], _data[i]) = (_data[i], _data[j]);
        }
    }
}