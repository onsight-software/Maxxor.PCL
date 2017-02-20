using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maxxor.PCL.Collections
{
    /// <summary>
    /// A basic FIFO queue that enables arbitrary items to be moved to the front
    /// All operations run in constant time
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ImmediatePriorityQueue<T> : IProducerConsumerCollection<T>
    {
        private LinkedListNode _head = null;
        private LinkedListNode _tail = null;
        private readonly ConcurrentDictionary<T, LinkedListNode> _allItems = new ConcurrentDictionary<T, LinkedListNode>();

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode node = _head;
            if (node != null)
                yield return _head.Value;

            while (node?.Next != null)
            {
                node = node.Next;
                yield return node.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            if (array == null) throw new ArgumentNullException();
            if (index < 0) throw new ArgumentOutOfRangeException();

            var arrLength = array.Length;
            var tempArr = this.ToArray();
            for (var i = 0; i < arrLength && i < tempArr.Length; i++)
            {
                array.SetValue(tempArr[i], i);
            }
        }

        public T Peek()
        {
            return Count > 0 ? _head.Value : default(T);
        }

        public int Count => _allItems.Count;
        /// <summary>
        /// Do not use
        /// </summary>
        public bool IsSynchronized => false;
        /// <summary>
        /// Do not use
        /// </summary>
        public object SyncRoot { get; } = null;
        public void CopyTo(T[] array, int index)
        {
            if (array == null) throw new ArgumentNullException();
            if (index < 0) throw new ArgumentOutOfRangeException();

            var arrLength = array.Length;
            var tempArr = this.ToArray();
            for (var i = 0; i < arrLength && i < tempArr.Length; i++)
            {
                array[i] = tempArr[i];
            }
        }

        public T[] ToArray()
        {
            lock (_allItems)
            {
                var arr = new T[Count];
                var current = _head;
                int i = 0;
                while (current != null)
                {
                    arr[i] = current.Value;
                    current = current.Next;
                    i++;
                }
                return arr;
            }
        }

        public bool TryAdd(T item)
        {
            if (item == null || _allItems.ContainsKey(item) && _allItems[item] != null)
                return false;

            var newItemNode = new LinkedListNode(item);
            lock (_allItems)
            {
                if (_tail != null)
                {
                    _tail.Next = newItemNode;
                    newItemNode.Previous = _tail;
                    _tail = newItemNode;
                }
                else _head = _tail = newItemNode;

                _allItems[item] = newItemNode;
            }

            return true;
        }

        public bool MoveToFront(T item)
        {
            if (item == null || !_allItems.ContainsKey(item) || _allItems[item] == null)
                return false;

            if (_head.Value.Equals(item)) return true;

            lock (_allItems)
            {
                var itemNode = _allItems[item];
                itemNode.Previous.Next = itemNode.Next;
                if (itemNode.Next != null) itemNode.Next.Previous = itemNode.Previous;
                _head.Previous = itemNode;
                itemNode.Previous = null;
                itemNode.Next = _head;
                _head = itemNode;
            }

            return true;
        }

        public bool TryTake(out T item)
        {
            if (Count > 0)
            {
                lock (_allItems)
                {
                    var val = _head.Value;
                    LinkedListNode node;
                    var result = _allItems.TryRemove(val, out node);
                    if (!result)
                    {
                        item = default(T);
                        return false;
                    }

                    _head = _head?.Next;
                    if (_head != null) _head.Previous = null;
                    else _tail = null;

                    item = val;
                    return true;
                }
            }

            item = default(T);
            return false;
        }

        internal class LinkedListNode
        {
            public LinkedListNode Previous = null;
            public LinkedListNode Next = null;
            public T Value { get; }

            public LinkedListNode(T value)
            {
                Value = value;
            }
        }
    }
}
