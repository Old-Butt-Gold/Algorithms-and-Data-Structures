﻿using System.Collections;
using AaDS.DataStructures.Shared;

namespace AaDS.DataStructures.LinkedList;

class SinglyLinkedList<T> : IEnumerable<T>
{
    Node<T>? _head; 
    Node<T>? _tail; 
    public int Count { get; private set; }

    public SinglyLinkedList() { }
    public SinglyLinkedList(T data) => AddLast(data);
    public SinglyLinkedList(params IEnumerable<T>[] collections)
    {
        foreach (IEnumerable<T> collection in collections)
            foreach (T item in collection)
                AddLast(item);
    }
    
    public void Reverse()
    {
        if (_head is null) return;
        Node<T> tail = _head;
        Node<T>? temp = _head.Next;
        _head!.Next = null;
        while (temp != null)
        {
            Node<T> next = new (temp.Data) { Next = _head };
            temp = temp.Next;
            _head = next;
        }
        _tail = tail;
    }

    public void AddLast(T data)
    {
        Node<T> node = new(data);
        
        if (_head == null)
            _head = node;
        else
            _tail!.Next = node;
        _tail = node;
        Count++;
    }
    
    public bool Remove(T data)
    {
        Node<T>? current = _head;
        Node<T>? previous = null;
 
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(data, current.Data))
            {
                if (previous != null)
                {
                    previous.Next = current.Next;
                    if (current.Next == null) _tail = previous;
                }
                else
                {
                    _head = _head?.Next;
                    if (_head == null) _tail = null;
                }
                Count--;
                return true;
            }
            previous = current;
            current = current.Next;
        }
        return false;
    }
    
    public void DeleteFirst()
    {
        if (_head != null)
        {
            _head = _head.Next;

            if (_head == null)
                _tail = null;
            Count--;
        }
    }

    public void DeleteLast()
    {
        if (_head == null) return;

        if (_head == _tail)
            (_head, _tail) = (null, null);
        else
        {
            Node<T>? current = _head;

            while (current?.Next != _tail)
                current = current?.Next;

            current!.Next = null;
            _tail = current;
        }
        Count--;
    }
    
    public bool IsEmpty => Count == 0;

    public void Clear() => (_head, _tail, Count) = (null, null, 0);
    
    public bool Contains(T data)
    {
        Node<T>? current = _head;
        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(data, current.Data)) return true;
            current = current.Next;
        }
        return false;
    }

    public Node<T>? Find(T data)
    {
        Node<T>? current = _head;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(data, current.Data))
                return current;

            current = current.Next;
        }

        return null;
    }
    
    public bool AddBefore(T existingData, T newData)
    {
        Node<T>? current = _head;
        Node<T>? previous = null;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(existingData, current.Data))
            {
                Node<T> newNode = new(newData) { Next = current };

                if (previous != null)
                    previous.Next = newNode;
                else
                    _head = newNode;

                Count++;
                return true;
            }

            previous = current;
            current = current.Next;
        }

        return false;
    }

    public bool AddAfter(T existingData, T newData)
    {
        Node<T>? current = _head;

        while (current != null)
        {
            if (EqualityComparer<T>.Default.Equals(existingData, current.Data))
            {
                Node<T> newNode = new(newData) { Next = current.Next };
                current.Next = newNode;

                if (current == _tail)
                    _tail = newNode;

                Count++;
                return true;
            }

            current = current.Next;
        }

        return false;
    }

    public bool AddBefore(Node<T> existingNode, T newData)
    {
        if (existingNode == null)
            throw new ArgumentNullException(nameof(existingNode), "Existing node cannot be null.");

        Node<T>? current = _head;
        Node<T>? previous = null;

        while (current != null)
        {
            if (ReferenceEquals(existingNode, current))
            {
                Node<T> newNode = new(newData) { Next = current };

                if (previous != null)
                    previous.Next = newNode;
                else
                    _head = newNode;

                Count++;
                return true;
            }

            previous = current;
            current = current.Next;
        }

        return false;
    }

    public bool AddAfter(Node<T> existingNode, T newData)
    {
        if (existingNode == null)
            throw new ArgumentNullException(nameof(existingNode), "Existing node cannot be null.");

        Node<T>? current = _head;

        while (current != null)
        {
            if (ReferenceEquals(existingNode, current))
            {
                Node<T> newNode = new(newData) { Next = current.Next };
                current.Next = newNode;

                if (current == _tail)
                    _tail = newNode;

                Count++;
                return true;
            }

            current = current.Next;
        }

        return false;
    }
    
    public void AppendFirst(T data)
    {
        Node<T> node = new(data) { Next = _head };
        _head = node;
        if (Count == 0)
            _tail = _head;
        Count++;
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node<T>? current = _head;
        while (current != null)
        {
            yield return current.Data;
            current = current.Next;
        }
    }
    
    public T[] ToArray()
    {
        T[] array = new T[Count];
        int index = 0;

        foreach (T item in this)
            array[index++] = item;

        return array;
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

