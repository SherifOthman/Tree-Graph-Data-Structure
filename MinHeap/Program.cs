using System;
using System.Collections;
using System.Threading.Channels;

namespace MinHeap;


public class MinHeap
{
    private readonly List<int> _heap;

    public MinHeap()
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

            if (_heap[index] >= _heap[parentIndex])
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

    public int ExtractMin()
    {
        if (_heap.Count == 0)
        {
            throw new InvalidOperationException("Heap is empty.");
        }

        int min = _heap[0];

        _heap[0] = _heap[^1];
        _heap.RemoveAt(_heap.Count - 1);

        ModifyDown(0);

        return min;
    }

    public void ModifyDown(int index)
    {

        while (true)
        {
            int leftIndex = 2 * index + 1;
            int rightIndex = 2 * index + 2;

            int smallestIndex = index;

            if (leftIndex < _heap.Count && _heap[leftIndex] < _heap[smallestIndex])
                smallestIndex = leftIndex;

            if (rightIndex < _heap.Count && _heap[rightIndex] < _heap[smallestIndex])
                smallestIndex = rightIndex;

            if (index == smallestIndex)
                break;

            Swap(index, smallestIndex);

            index = smallestIndex;
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

        MinHeap minHeap = new MinHeap();

        Console.WriteLine("Inserting elements into the Min-Heap...\n");
        minHeap.Insert(10);
        minHeap.Insert(4);
        minHeap.Insert(15);
        minHeap.Insert(2);
        minHeap.Insert(8);

        minHeap.DisplayHeap();

        Console.WriteLine("\nPeek Minimum Element: Minimum Element is: " + minHeap.Peek());

        minHeap.DisplayHeap();

        Console.WriteLine("\nExtracting elements from the Min-Heap:");
        Console.WriteLine("Extracted Minimum: " + minHeap.ExtractMin());
        minHeap.DisplayHeap();

        Console.WriteLine("\nExtracted Minimum: " + minHeap.ExtractMin());
        minHeap.DisplayHeap();
    }
}
