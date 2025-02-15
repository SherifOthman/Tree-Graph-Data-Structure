namespace MatrixGraph;

public class Graph
{
    public enum enGraphDirectionType { unDirected, Directed };
    private enGraphDirectionType _directionType;

    private readonly Dictionary<string, int> _verticesDictionary;
    private int[,] _verticesMatrix;

    private int _count = 0;
    public Graph(List<string> vertices, enGraphDirectionType directionType = enGraphDirectionType.unDirected)
    {
        _count = vertices.Count;

        _verticesDictionary = new Dictionary<string, int>();
        _verticesMatrix = new int[_count, _count];

        _directionType = directionType;
        foreach (var vertex in vertices.Index())
        {
            _verticesDictionary[vertex.Item] = vertex.Index;
        }
    }


    public void AddVertex(string vertex)
    {
        if (_verticesDictionary.ContainsKey(vertex))
            return;

        _verticesDictionary[vertex] = _count;

        Resize();
    }

    public void RemoveVertex(string vertex)
    {
        if (!_verticesDictionary.ContainsKey(vertex))
            return;

        int vertexIndex = _verticesDictionary[vertex];
        _verticesDictionary.Remove(vertex);
        ReAssignVertexIndices();

        shrink(vertexIndex);
    }

    private void shrink(int indexToBeRmoved)
    {
        int[,] temp = _verticesMatrix;
        _verticesMatrix = new int[_count - 1, _count - 1];

        int row = 0;
        int column = 0;

        for (int i = 0; i < _count; i++)
        {
            if (i == indexToBeRmoved)
                continue;

            for (int j = 0; j < _count; j++)
            {
                if (j == indexToBeRmoved)
                    continue;

                _verticesMatrix[row, column] = temp[i, j];
                column++;
            }

            row++;
            column = 0;
        }

        _count--;
    }

    private void Resize()
    {

        int[,] temp = _verticesMatrix;
        _verticesMatrix = new int[_count + 1, _count + 1];

        for (int i = 0; i < _count; i++)
        {
            for (int j = 0; j < _count; j++)
            {
                _verticesMatrix[i, j] = temp[i, j];
            }
        }

        _count++;
    }

    private void ReAssignVertexIndices()
    {

        foreach (var element in _verticesDictionary.Index())
        {
            _verticesDictionary[element.Item.Key] = element.Index;
        }
    }


    public void AddEdge(string source, string destination, int weight)
    {
        if (_verticesDictionary.ContainsKey(source) && _verticesDictionary.ContainsKey(destination))
        {
            int sourceIndex = _verticesDictionary[source];
            int destinationIndex = _verticesDictionary[destination];

            _verticesMatrix[sourceIndex, destinationIndex] = weight;

            if (_directionType == enGraphDirectionType.unDirected)
                _verticesMatrix[destinationIndex, sourceIndex] = weight;
        }
    }

    public int GetInDegree(string vertex)
    {
        int inDgree = 0;

        if (_verticesDictionary.ContainsKey(vertex))
        {
            int vertexIndex = _verticesDictionary[vertex];


            for (int row = 0; row < _count; row++)
            {
                inDgree += _verticesMatrix[row, vertexIndex];
            }
        }


        return inDgree;
    }

    public int GetOutDegree(string vertex)
    {
        int outDgree = 0;

        if (_verticesDictionary.ContainsKey(vertex))
        {
            int vertexIndex = _verticesDictionary[vertex];

            for (int column = 0; column < _count; column++)
            {
                outDgree += _verticesMatrix[vertexIndex, column];
            }
        }

        return outDgree;
    }

    public bool IsEdge(string source, string destantion)
    {
        if (_verticesDictionary.ContainsKey(source) && _verticesDictionary.ContainsKey(destantion))
        {
            int weight = 0;
            int sourceIndex = _verticesDictionary[source];
            int destantionIndex = _verticesDictionary[destantion];
            weight = _verticesMatrix[sourceIndex, destantionIndex];

            return weight > 0;
        }

        return false;
    }

    public void RemoveEdge(string source, string destantion)
    {
        if (_verticesDictionary.ContainsKey(source) && _verticesDictionary.ContainsKey(destantion))
        {
            int sourceIndex = _verticesDictionary[source];
            int destanationIndex = _verticesDictionary[destantion];

            _verticesMatrix[sourceIndex, destanationIndex] = 0;
            if (_directionType == enGraphDirectionType.unDirected)
                _verticesMatrix[destanationIndex, sourceIndex] = 0;
        }
    }

    public void DisplayGraph(string message)
    {
        Console.WriteLine($"\n {message} \n");
        Console.Write("  ");

        foreach (var vertex in _verticesDictionary)
        {
            Console.Write($"{vertex.Key} ");
        }
        Console.WriteLine();

        foreach (var vertex in _verticesDictionary)
        {
            Console.Write($"{vertex.Key} ");

            foreach (var v in _verticesDictionary.Index())
            {
                Console.Write($"{_verticesMatrix[vertex.Value, v.Index]} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}

internal class Program
{
    static void Main(string[] args)
    {
        List<string> vertices = new List<string> { "A", "B", "C", "D", "E" };

        Graph graph1 = new Graph(vertices, Graph.enGraphDirectionType.unDirected);

        graph1.AddEdge("A", "B", 1);
        graph1.AddEdge("A", "C", 1);
        graph1.AddEdge("B", "D", 1);
        graph1.AddEdge("B", "E", 1);
        graph1.AddEdge("C", "D", 1);
        graph1.AddEdge("D", "E", 1);


        graph1.DisplayGraph("Adjacency Matrix for Example1 (Undirected Graph):");

        //graph1.AddVertex("Y");

        //graph1.DisplayGraph("After adding Y");

        graph1.RemoveVertex("C");

        graph1.DisplayGraph("After Removing  C");


        Console.WriteLine("\n----------------------------\n\n\n");

        Graph graph2 = new Graph(vertices, Graph.enGraphDirectionType.Directed);

        graph2.AddEdge("A", "A", 1);
        graph2.AddEdge("A", "B", 1);
        graph2.AddEdge("A", "C", 1);
        graph2.AddEdge("B", "E", 1);
        graph2.AddEdge("D", "B", 1);
        graph2.AddEdge("D", "C", 1);
        graph2.AddEdge("D", "E", 1);

        graph2.DisplayGraph("Adjacency Matrix for Example (Directed Graph):");

        Console.WriteLine($"InDegree of vertex D: {graph2.GetInDegree("D")}");
        Console.WriteLine($"OutDgree of vertex D: {graph2.GetOutDegree("D")}");


        Graph graph3 = new Graph(vertices);

        graph3.AddEdge("A", "B", 5);
        graph3.AddEdge("A", "C", 3);
        graph3.AddEdge("C", "D", 10);
        graph3.AddEdge("B", "D", 12);
        graph3.AddEdge("B", "E", 2);
        graph3.AddEdge("D", "E", 7);

        graph3.DisplayGraph("Adjacency Matrix for Example3 (Weighted Graph):");

        Console.WriteLine("\nIs there an edge between A and B in Graph3? " + graph3.IsEdge("A", "B"));


        Console.WriteLine("\nIs there an edge between B and C in Graph3? " + graph3.IsEdge("B", "C"));


        Console.WriteLine("\nIs there an edge between E and D in Graph3? " + graph3.IsEdge("E", "D"));

        Console.WriteLine("\nIs there an edge between A and A in Graph3? " + graph3.IsEdge("A", "A"));

        Console.WriteLine("\nInDegree of vertex A: " + graph3.GetInDegree("A"));

        Console.WriteLine("\nOutDegree of vertex A: " + graph3.GetOutDegree("A"));

        Console.WriteLine("\n------------------------------\n");


        Console.WriteLine("\nRemoveing Edge between E and D:");
        graph3.RemoveEdge("E", "D");

        graph3.DisplayGraph("After Removeing Edge between E and D:");


        Console.WriteLine("\nIs there an edge between E and D in Graph3? " + graph3.IsEdge("E", "D"));
    }
}