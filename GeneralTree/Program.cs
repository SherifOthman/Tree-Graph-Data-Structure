
namespace GeneralTree;

public class TreeNode<T>
{
    public T Value { get; }
    public List<TreeNode<T>> Children { get; }

    public TreeNode(T value)
    {
        Value = value;
        Children = new List<TreeNode<T>>();
    }

    public void AddChild(T value)
    {
        Children.Add(new TreeNode<T>(value));
    }

    public void AddChild(TreeNode<T> node)
    {
        Children.Add(node);
    }

    public TreeNode<T>? Find(T value)
    {
        if (EqualityComparer<T>.Default.Equals(this.Value, value))
            return this;

        foreach (var child in Children)
        {
            var node = child.Find(value);
            if (node !=null)
                return node;
        }

        return null;
    }
}

public class Tree<T>
{
    public TreeNode<T> Root { get; }

    public Tree(T value)
    {
        Root = new TreeNode<T>(value);
    }

    public void PrintTree()
    {
        PrintTree(Root, "");
    }

    private void PrintTree(TreeNode<T> node, string indent = "")
    {
        Console.WriteLine(indent + node.Value);
        foreach (var child in node.Children)
            PrintTree(child, indent + " ");
    }

    public TreeNode<T>? Find(T value)
    {
        return Root?.Find(value);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create a tree with a root node
        Tree<string> companyHierarchy = new Tree<string>("CEO");

        // Add departments
        var sales = new TreeNode<string>("Sales Department");
        var hr = new TreeNode<string>("HR Department");
        var it = new TreeNode<string>("IT Department");

        companyHierarchy.Root.AddChild(sales);
        companyHierarchy.Root.AddChild(hr);
        companyHierarchy.Root.AddChild(it);

        // Add sub-departments and employees
        sales.AddChild("Domestic Sales");
        sales.AddChild("International Sales");

        hr.AddChild("Recruitment");
        hr.AddChild("Employee Relations");

        it.AddChild("Infrastructure");
        it.AddChild("Software Development");

        // Add more nested levels
        it.Children[1].AddChild("Frontend Team");
        it.Children[1].AddChild("Backend Team");

        // Print the tree structure
        companyHierarchy.PrintTree();


        var node = companyHierarchy.Find("Frontend Team");

        if (node is not null)
            Console.WriteLine("Found");
        else
            Console.WriteLine("NotFound");

    }
}
