namespace GenericsHomework
{
    public class Node<T>
    {
        private T _value;
        private Node<T>? _next;
        public T Value { get { return _value; } }
        public Node(T value) 
        {
            _value = value;
            _next = this;
        }

        public Node<T> Next
        {
            get { return _next ?? this; }
            private set { _next = value; }
        }

        public override string? ToString()
        {
            if (_value == null)
            {
                return null;
            }

            return _value.ToString();
        }

        public Node<T> Append(T value)
        {
            if (Exists(value)) 
            {
                throw new ArgumentException("Duplicate Value");
            }
            Node<T> toAdd = new(value);
            toAdd.Next = Next;
            Next = toAdd;
            return toAdd;
        }

        /*
         * With all "deleted" nodes pointing to themselves, after the Clear()
         * method is ran these nodes will no longer be accessable via the linked list.
         * 
         * IE nothing is pointing at the "cleared" nodes, making garbage collection a non-issue.
         */
        public void Clear()
        {
            var node = this.Next;

            while (node.Next != this) 
            {
                node = node.Next;
            }

            node.Next = Next;
            Next = this;
        }

        public bool Exists(T value)
        {
            var node = this;

            do
            {
                if (node.Value is null && value is not null)
                {
                    node = node.Next;
                }
                else if (node.Value is null && value is null)
                {
                    return true;
                }
                else if (node.Value!.Equals(value))
                {
                    return true;
                }

                node = node.Next;

            } while (node != this);

            return false;
        }
    }
}