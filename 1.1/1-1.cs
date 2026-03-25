using System;

namespace LabStructures
{
    class VectorList
    {
        private string[] _items;
        private int _count;
        private int _tail;

        public VectorList(int capacity)
        {
            _items = new string[capacity];
            _count = 0;
            _tail = -1;
        }

        public bool IsFull()
        {
            return _items.Length == _count;
        }

        public bool IsEmpty()
        {
            return _count == 0;
        }

        public bool Insert(string item)
        {
            if (IsFull()) return false;

            if (int.TryParse(item, out int number) && number > 0)
            {
                _tail++;
                _items[_tail] = item;
                _count++;
                return true;
            }
            
            return false;
        }

        public string RemoveAt(int index)
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Structure is empty.");
            }

            if (index < 0 || index > _tail)
            {
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
            }

            string removedItem = _items[index];

            for (int i = index; i < _tail; i++)
            {
                _items[i] = _items[i + 1];
            }

            _items[_tail] = null;
            _tail--;
            _count--;

            return removedItem;
        }

        public void Print()
        {
            for (int i = 0; i <= _tail; i++)
            {
                Console.Write(_items[i] + " ");
            }
            Console.WriteLine();
        }
    }

    class Node
    {
        public int Data;
        public Node Next;

        public Node(int data)
        {
            Data = data;
            Next = null;
        }
    }



    class LinkedStack
    {
        private Node _top;

        public LinkedStack()
        {
            _top = null;
        }

        public bool IsEmpty()
        {
            return _top == null;
        }

        public void Push(int item)
        {
            Node newNode = new Node(item);
            newNode.Next = _top;
            _top = newNode;
        }

        public int Pop()
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException("Structure is empty.");
            }

            int removedItem = _top.Data;
            _top = _top.Next;
            
            return removedItem;
        }

        public void Print()
        {
            Node current = _top;
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main()
        {
            VectorList list = new VectorList(5);
            list.Insert("10");
            list.Insert("25");
            list.Insert("42");
            list.Print();

            Console.WriteLine(list.RemoveAt(1));
            list.Print();

            Console.WriteLine();

            LinkedStack stack = new LinkedStack();
            stack.Push(10);
            stack.Push(20);
            stack.Push(30);
            stack.Print();

            Console.WriteLine(stack.Pop());
            stack.Print();
        }
    }
}