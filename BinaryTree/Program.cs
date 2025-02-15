using System.Security;

namespace BinaryTree;

public class TreeNode<T>
{
    public T Value { get; set; }

    public TreeNode<T>? Left { get; set; }

    public TreeNode<T>? Right { get; set; }

    public TreeNode(T value)
    {
        Value = value;
    }
}


public class BinaryTree<T>
{
    public TreeNode<T> Root { get; private set; }

    public BinaryTree()
    {
        Root = null!;
    }


    public int GetHeight()
    {
        if (Root == null)
            return -1;

        return GetHeight(Root);
    }

    private int GetHeight(TreeNode<T> node)
    {
        if (node == null)
            return -1;

        int leftSubTree = GetHeight(node.Left!);
        int rightSubTree = GetHeight(node.Right!);

        return 1 + Math.Max(leftSubTree, rightSubTree);
    }

    public void InsertInorderTraversal(T value)
    {
        TreeNode<T> newNode = new TreeNode<T>(value);

        if (Root == null)
        {
            Root = newNode;
            return;
        }

        Queue<TreeNode<T>> queue = new Queue<TreeNode<T>>();
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            TreeNode<T> current = queue.Dequeue();

            if (current.Left == null)
            {
                current.Left = newNode;
                return;
            }
            else
            {
                queue.Enqueue(current.Left);
            }

            if (current.Right == null)
            {
                current.Right = newNode;
                return;
            }
            else
            {
                queue.Enqueue(current.Right);
            }


        }

    }


    public void PrintInorderTraversal()
    {
        if (Root == null)
            return;

        Queue<TreeNode<T>> queue = [];
        queue.Enqueue(Root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            Console.Write(current.Value + " ");

            if (current.Left != null)
            {
                queue.Enqueue(current.Left);
            }
            if (current.Right != null)
            {
                queue.Enqueue(current.Right);
            }
        }
    }

    public void Print()
    {
        Print(Root!, 0);
    }

    private void Print(TreeNode<T> node, int space)
    {
        if (node == null)
            return;

        int COUUNT = 10;
        space += COUUNT;
        Print(node.Right!, space);

        Console.WriteLine();
        for (int i = COUUNT; i < space; i++)
            Console.Write(" ");

        Console.WriteLine(node.Value);

        Print(node.Left!, space);
    }


    // Preorder
    public void PrintPreOrderIterative()
    {
        TreeNode<T> current = Root;
        Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();

        while (current != null || stack.Count > 0)
        {
            if (current != null)
            {
                Console.Write(current.Value + ", ");
                stack.Push(current.Right!);
                current = current.Left!;
            }
            else
            {
                current = stack.Pop();
            }
        }

    }

    public void PrintPreOrderRecursive()
    {
        PrintPreOrderRecursive(Root);
    }

    private void PrintPreOrderRecursive(TreeNode<T> node)
    {
        if (node == null)
            return;

        Console.Write(node.Value + ", ");
        PrintPreOrderRecursive(node.Left!);
        PrintPreOrderRecursive(node.Right!);
    }


    // Postorder
    public void PrintPostOrderIterative()
    {
        if (Root == null)
            return;

        Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
        stack.Push(Root);

        Stack<bool> visit = new Stack<bool>();
        visit.Push(false);

        while (stack.Count > 0)
        {
            TreeNode<T> current = stack.Pop();
            bool visited = visit.Pop();

            if (current != null)
            {
                if (visited)
                {
                    Console.Write(current.Value + ", ");
                }
                else
                {
                    stack.Push(current);
                    visit.Push(true);
                    stack.Push(current.Right!);
                    visit.Push(false);
                    stack.Push(current.Left!);
                    visit.Push(false);
                }
            }
        }

    }

    public void PrintPostOrderRecursive()
    {
        PrintPostOrderRecursive(Root);
    }

    private void PrintPostOrderRecursive(TreeNode<T> node)
    {
        if (node is null)
            return;

        PrintPostOrderRecursive(node.Left!);
        PrintPostOrderRecursive(node.Right!);
        Console.Write(node.Value + ", ");

    }


    // Inorder 

    public void PrintInorderIterative()
    {
        Stack<TreeNode<T>> stack = new Stack<TreeNode<T>>();
        TreeNode<T> current = Root;

        while (current != null || stack.Count > 0)
        {
            while (current != null)
            {
                stack.Push(current);
                current = current.Left!;
            }

            current = stack.Pop();
            Console.Write(current.Value + ", ");
            current = current.Right!;
        }
    }

    public void PrintInorderRecursive()
    {
        PrintInorderRecursive(Root);
    }

    private void PrintInorderRecursive(TreeNode<T> node)
    {
        if (node == null)
            return;

        PrintInorderRecursive(node.Left);
        Console.Write(node.Value + ", ");
        PrintInorderRecursive(node.Right);
    }

}


internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Values to be inserted: 5,3,8,1,4,6,9");

        BinaryTree<int> tree = new BinaryTree<int>();
        tree.InsertInorderTraversal(5);
        tree.InsertInorderTraversal(3);
        tree.InsertInorderTraversal(8);
        tree.InsertInorderTraversal(1);
        tree.InsertInorderTraversal(4);
        tree.InsertInorderTraversal(6);
        tree.InsertInorderTraversal(9);
        tree.InsertInorderTraversal(55);



        tree.Print();

        // PreOrder:
        //    Root->Left Sub Trees -> Right Sub Trees;
        //    useful when you want to copy the tree.

        Console.WriteLine("Preorder travesal");
        tree.PrintPreOrderRecursive();
        Console.WriteLine();
        tree.PrintPreOrderIterative();
        Console.WriteLine('\n');

        // PostOrder:
        //    Left Sub Trees->Right Sub Trees -> Root
        //    useful when you want to start from Children's to Parents(Bottom-Up)

        Console.WriteLine("Postorder traversal");
        tree.PrintPostOrderRecursive();
        Console.WriteLine();
        tree.PrintPostOrderIterative();
        Console.WriteLine();
        Console.WriteLine('\n');

        // Inorder: Left Sub Trees->Root->Right Sub Trees
        //      useful when you want to Print The Tree In Sorted order
        //      * if it's sorted by default *

        Console.WriteLine("Postorder traversal");
        tree.PrintInorderRecursive();
        Console.WriteLine();
        tree.PrintInorderIterative();
        Console.WriteLine('\n');


        Console.WriteLine("\nInorder level traversal");
        tree.PrintInorderTraversal();


        Console.WriteLine($"Height: {tree.GetHeight()}");

        Console.ReadKey();
    }
}
