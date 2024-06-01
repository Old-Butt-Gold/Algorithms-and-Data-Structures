using System.Collections;

namespace AaDS.DataStructures.Stack
{
    public class MinMaxStack<T> : IEnumerable<T> where T : IComparable<T>
    {
        private class MinMaxStackNode
        {
            public T Data { get; set; }
            public T Min { get; set; }
            public T Max { get; set; }
            public MinMaxStackNode? Next { get; set; }

            public MinMaxStackNode(T data, T min, T max, MinMaxStackNode? next)
            {
                Data = data;
                Min = min;
                Max = max;
                Next = next;
            }
        }

        private MinMaxStackNode? _head;
        public int Count { get; private set; }
        public bool IsEmpty => Count == 0;

        public void Push(T item)
        {
            if (_head is null)
            {
                _head = new MinMaxStackNode(item, item, item, null);
            }
            else
            {
                var min = item.CompareTo(_head.Min) < 0 ? item : _head.Min;
                var max = item.CompareTo(_head.Max) > 0 ? item : _head.Max;
                _head = new MinMaxStackNode(item, min, max, _head);
            }
            Count++;
        }

        public T Pop()
        {
            if (IsEmpty) throw new InvalidOperationException("Stack is empty");
            var item = _head!.Data;
            _head = _head.Next;
            Count--;
            return item;
        }

        public T Peek()
        {
            if (IsEmpty) throw new InvalidOperationException("Stack is empty");
            return _head!.Data;
        }

        public T GetMin()
        {
            if (IsEmpty) throw new InvalidOperationException("Stack is empty");
            return _head!.Min;
        }

        public T GetMax()
        {
            if (IsEmpty) throw new InvalidOperationException("Stack is empty");
            return _head!.Max;
        }

        public bool TryPop(out T? item)
        {
            if (!IsEmpty)
            {
                item = Pop();
                return true;
            }
            item = default;
            return false;
        }

        public bool TryPeek(out T? item)
        {
            item = _head is null ? default : _head.Data;
            return !IsEmpty;
        }

        public void Clear() => (Count, _head) = (0, null);

        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
