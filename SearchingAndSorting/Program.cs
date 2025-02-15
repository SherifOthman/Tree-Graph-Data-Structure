
namespace SearchingAndSorting;
internal static class Program
{
    static void Main(string[] args)
    {
        // List<int> arr = new List<int>([22, 25, 37, 41, 45, 46, 49, 51, 55, 58, 70, 80, 82, 90, 95]);
        // int[] arr = [65, 33, 96, 1, 4, 660, 8, 7, 7, 90, 2];
        int[] arr = [64, 34, 25, 12, 22, 11, 90];

        //   BubbleSort(arr);
        // SelectionSort(arr);
        InsertionSort(arr);
        arr.Print();

        int valueToSearch = 7;
        int index = BinarySearch(arr, valueToSearch);

        Console.WriteLine($"Index of {valueToSearch} is {index}");

        Console.ReadKey();

    }

    static int BinarySearch(int[] arr, int element)
    {
        int start = 0;
        int end = arr.Length;
        int middle = 0;

        while (start <= end)
        {
            middle = (start + end) / 2;

            if (arr[middle] == element)
                return middle;

            if (arr[middle] > element)
                end = middle - 1;
            else
                start = middle + 1;
        }

        return middle * -1;

    }

    static void BubbleSort(int[] arr)
    {

        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }

    }

    static void InsertionSort(int[] arr)
    {
        int n = arr.Length;
        for (int i = 1; i < n; ++i)
        {
            int key = arr[i];
            int j = i - 1;

            // Move elements of arr[0..i-1], that are greater than key,
            // to one position ahead of their current position
            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j = j - 1;
            }
            arr[j + 1] = key;
        }
    }

    public static void Print<T>(this IEnumerable<T> source)
    {
        foreach (var item in source)
        {
            Console.Write(item + " ");
        }
        Console.WriteLine();
    }

}
