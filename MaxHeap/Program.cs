using System;
using System.Collections;
using System.Threading.Channels;

namespace MinHeap;


public class MaxHeap
{
    private readonly List<int> _heap;

    public MaxHeap()
    {
        _heap = [];
    }

    public void Insert(int value)
    {
        _heap.Add(value);
        HeapifyUp(_heap.Count - 1);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;

            if (_heap[index] <= _heap[parentIndex])
                break;


            //(_heap[index], _heap[parentIndex]) = (_heap[parentIndex], _heap[index]);
            Swap(index, parentIndex);

            index = parentIndex;
        }
    }

    public int Peek()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        return _heap[0];
    }

    public int ExtractMax()
    {
        if (_heap.Count == 0)
        {
            throw new InvalidOperationException("Heap is empty.");
        }

        int max = _heap[0];

        _heap[0] = _heap[^1];
        _heap.RemoveAt(_heap.Count - 1);

        ModifyDown(0);

        return max;
    }

    public void ModifyDown(int index)
    {

        while (true)
        {
            int leftIndex = 2 * index + 1;
            int rightIndex = 2 * index + 2;

            int largestIndex = index;

            if (leftIndex < _heap.Count && _heap[leftIndex] > _heap[largestIndex])
                largestIndex = leftIndex;

            if (rightIndex < _heap.Count && _heap[rightIndex] > _heap[largestIndex])
                largestIndex = rightIndex;

            if (index == largestIndex)
                break;

            Swap(index, largestIndex);

            index = largestIndex;
        }

    }

    public void DisplayHeap() => _heap.ForEach(n => Console.Write(n + " "));

    private void Swap(int i, int j)
    {
        (_heap[i], _heap[j]) = (_heap[j], _heap[i]);
    }
}
internal class Program
{
    static void Main(string[] args)
    {

        MaxHeap maxHeap = new MaxHeap();

        Console.WriteLine("Inserting elements into the Max-Heap...\n");
        maxHeap.Insert(10);
        maxHeap.Insert(4);
        maxHeap.Insert(15);
        maxHeap.Insert(2);
        maxHeap.Insert(8);

        maxHeap.DisplayHeap();

        Console.WriteLine("\nPeek Maximum Element: Maximum Element is: " + maxHeap.Peek());

        maxHeap.DisplayHeap();

        Console.WriteLine("\nExtracting elements from the Min-Heap:");
        Console.WriteLine("Extracted Maximum: " + maxHeap.ExtractMax());
        maxHeap.DisplayHeap();

        Console.WriteLine("\nExtracted Maximum: " + maxHeap.ExtractMax());
        maxHeap.DisplayHeap();
    }
}
