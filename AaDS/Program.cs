using AaDS.DataStructures.LinkedList;
using AaDS.DataStructures.Shared;

Console.WriteLine();

Node<int> meow = new Node<int>(2);
meow.Next = new Node<int>(6);
Node<int> meow1 = new Node<int>(1);
meow1.Next = new(4);
meow1.Next.Next = new(5);
Node<int> meow2 = new(1);
meow2.Next = new(3);
meow2.Next.Next = new(4);

SinglyLinkedList<int> res = new();
res.MergeKListsDivideAndConqueror([meow2, meow1, meow]);