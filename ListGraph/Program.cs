namespace MatrixGraph;

public class Graph
{
    public enum enGraphDirectionType { unDirected, Directed };
    private enGraphDirectionType _directionType;

    private readonly Dictionary<string, List<(string vertex, int weight)>> _verticesDictionary;

    public Graph(List<string> vertices, enGraphDirectionType directionType = enGraphDirectionType.unDirected)
    {
        _verticesDictionary = new();

        _directionType = directionType;

        vertices.ForEach(v => _verticesDictionary.Add(v, new()));
    }

    public void AddVertex(string vertex)
    {
        if (_verticesDictionary.ContainsKey(vertex))
            return;

        _verticesDictionary[vertex] = new();

    }

    public void RemoveVertex(string vertex)
    {
        if (!_verticesDictionary.ContainsKey(vertex))
            return;

        _verticesDictionary.Remove(vertex);

        foreach (var v in _verticesDictionary)
        {
            v.Value.RemoveAll(v => v.vertex == vertex);
        }

    }

    public void AddEdge(string source, string destination, int weight)
    {
        if (_verticesDictionary.ContainsKey(source) && _verticesDictionary.ContainsKey(destination))
        {
            _verticesDictionary[source].Add(new(destination, weight));


            if (_directionType == enGraphDirectionType.unDirected)
                _verticesDictionary[destination].Add(new(source, weight));
        }
    }

    public int GetInDegree(string vertex)
    {
        int inDgree = 0;

        if (_verticesDictionary.ContainsKey(vertex))
        {
            foreach (var v in _verticesDictionary)
            {
                foreach (var item in v.Value)
                {
                    if (vertex == item.vertex)
                    {
                        inDgree++;
                        break;
                    }
                }
            }
        }


        return inDgree;
    }

    public int GetOutDegree(string vertex)
    {
        int outDgree = 0;

        if (_verticesDictionary.ContainsKey(vertex))
        {
            outDgree = _verticesDictionary[vertex].Count;
        }

        return outDgree;
    }

    public bool IsEdge(string source, string destantion)
    {
        if (_verticesDictionary.ContainsKey(source) && _verticesDictionary.ContainsKey(destantion))
        {
            return _verticesDictionary[source].Any(v => v.vertex == destantion);
        }

        return false;
    }

    public void RemoveEdge(string source, string destantion)
    {
        if (_verticesDictionary.ContainsKey(source) && _verticesDictionary.ContainsKey(destantion))
        {
            _verticesDictionary[source].RemoveAll(v => v.vertex == destantion);

            if (_directionType == enGraphDirectionType.unDirected)
                _verticesDictionary[destantion].RemoveAll(v => v.vertex == source);
        }
    }

    public void DisplayGraph(string message)
    {
        Console.WriteLine($"\n {message} \n");


        foreach (var vertex in _verticesDictionary)
        {
            Console.Write($"{vertex.Key} ==> ");

            vertex.Value.ForEach(v => Console.Write(v + " "));

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

        graph1.AddVertex("Y");

        graph1.DisplayGraph("After adding Y");

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