namespace PriorityQueue;


public class PriorityQueueMinHeap<T>
{
    private readonly List<(T Value, int Priority)> _heap;

    public int Count => _heap.Count;

    public PriorityQueueMinHeap()
    {
        _heap = [];
    }

    public void Insert(T value, int priority)
    {
        _heap.Add(new(value, priority));
        HeapifyUp(_heap.Count - 1);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0)
        {
            int parentIndex = (index - 1) / 2;

            if (_heap[index].Priority >= _heap[parentIndex].Priority)
                break;


            //(_heap[index], _heap[parentIndex]) = (_heap[parentIndex], _heap[index]);
            Swap(index, parentIndex);

            index = parentIndex;
        }
    }

    public T Peek()
    {
        if (_heap.Count == 0)
            throw new InvalidOperationException("Heap is empty");

        return _heap[0].Value;
    }

    public T ExtractMin()
    {
        if (_heap.Count == 0)
        {
            throw new InvalidOperationException("Heap is empty.");
        }

        T min = _heap[0].Value;

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

            if (leftIndex < _heap.Count && _heap[leftIndex].Priority < _heap[smallestIndex].Priority)
                smallestIndex = leftIndex;

            if (rightIndex < _heap.Count && _heap[rightIndex].Priority < _heap[smallestIndex].Priority)
                smallestIndex = rightIndex;

            if (index == smallestIndex)
                break;

            Swap(index, smallestIndex);

            index = smallestIndex;
        }

    }

    public void DisplayHeap()
    {
        if (_heap.Count == 0)
        {
            Console.WriteLine("Heap is empty.");
            return;
        }

        Console.WriteLine("Current Heap:");
        foreach (var (value, priority) in _heap)
        {
            Console.WriteLine($"- {value} (Priority: {priority})");
        }
        Console.WriteLine();
    }

    private void Swap(int i, int j)
    {
        (_heap[i], _heap[j]) = (_heap[j], _heap[i]);
    }
}
internal class Program
{
    static void Main(string[] args)
    {

        PriorityQueueMinHeap<string> pq = new PriorityQueueMinHeap<string>();


        Console.WriteLine("Inserting elements into the Priority Queue...\n");

        Console.WriteLine("Inserting (Task 1, 5)");
        Console.WriteLine("Inserting (Task 2, 3)");
        Console.WriteLine("Inserting (Task 3, 4)");
        Console.WriteLine("Inserting (Task 4, 1)");
        Console.WriteLine("Inserting (Task 5, 2)");

        pq.Insert("Task 1", 5);
        pq.Insert("Task 2", 3);
        pq.Insert("Task 3", 4);
        pq.Insert("Task 4", 1);
        pq.Insert("Task 5", 2);

        // Peek the minimum priority element
        Console.WriteLine("\nPeek Minimum Priority Element: Name = " + pq.Peek() );

        // Extract elements based on priority
        Console.WriteLine("\nExtracting elements from the Priority Queue:");
        var extractedNode = pq.ExtractMin();
        Console.WriteLine("\nExtracted Element: Name = " + extractedNode);

        extractedNode = pq.ExtractMin();
        Console.WriteLine("Extracted Element: Name = " + extractedNode);

        extractedNode = pq.ExtractMin();
        Console.WriteLine("Extracted Element: Name = " + extractedNode);

        extractedNode = pq.ExtractMin();
        Console.WriteLine("Extracted Element: Name = " + extractedNode);

        extractedNode = pq.ExtractMin();
        Console.WriteLine("Extracted Element: Name = " + extractedNode);



        Console.WriteLine("Heap is now empty.");

    }
}

