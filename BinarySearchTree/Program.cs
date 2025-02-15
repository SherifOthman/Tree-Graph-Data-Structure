using System;

namespace BinarySearchTreeDemo
{
    public class BinarySearchTreeNode<T> where T : IComparable<T>
    {
        public T Value { get; set; }
        public BinarySearchTreeNode<T> Left { get; set; }
        public BinarySearchTreeNode<T> Right { get; set; }

        public BinarySearchTreeNode(T value)
        {
            Value = value;
            Left = null!;
            Right = null!;
        }
    }

    public class BinarySearchTree<T> where T : IComparable<T>
    {
        public BinarySearchTreeNode<T> Root { get; private set; }

        public BinarySearchTree()
        {
            Root = null!;
        }


        public bool FindIterative(T value)
        {
            if (Root == null)
                return false;

            var current = Root;

            while (current != null)
            {
                int compare = value.CompareTo(current.Value);

                if (compare == 0)
                    return true;

                if (compare < 0)
                    current = current.Left;
                else
                    current = current.Right;
            }
            return false;
        }

        public bool Find(T vallue)
        {
            return Find(Root, vallue);
        }

        private bool Find(BinarySearchTreeNode<T> node, T value)
        {
            if (node == null)
                return false;

            int compare = value.CompareTo(node.Value);

            if (compare == 0)
                return true;

            if (compare < 0)
                return Find(node.Left, value);
            else
                return Find(node.Right, value);


        }

        public void InsertIterative(T value)
        {
            if (Root == null)
            {
                Root = new BinarySearchTreeNode<T>(value);
                return;
            }

            BinarySearchTreeNode<T> current = Root;
            BinarySearchTreeNode<T> parent = null!;

            while (current != null)
            {
                parent = current;

                int compare = value.CompareTo(current.Value);

                if (compare < 0)
                    current = current.Left;
                else
                    current = current.Right;

            }

            if (value.CompareTo(parent.Value) < 0)
                parent.Left = new BinarySearchTreeNode<T>(value);
            else
                parent.Right = new BinarySearchTreeNode<T>(value);
        }

        public void InsertIterative2(T value)
        {
            if (Root == null)
            {
                Root = new BinarySearchTreeNode<T>(value);
                return;
            }

            BinarySearchTreeNode<T> current = Root;

            while (true)
            {
                int compare = value.CompareTo(current.Value);

                if (compare < 0)
                {
                    if (current.Left == null)
                    {
                        current.Left = new BinarySearchTreeNode<T>(value);
                        break;
                    }
                    current = current.Left;
                }
                else
                {
                    if (current.Right == null)
                    {
                        current.Right = new BinarySearchTreeNode<T>(value);
                        break;
                    }
                    current = current.Right;
                }
            }
        }

        public void Insert(T value)
        {
            Root = Insert(Root, value);
        }

        private BinarySearchTreeNode<T> Insert(BinarySearchTreeNode<T> node, T value)
        {
            if (node == null)
                return new BinarySearchTreeNode<T>(value);

            int compare = value.CompareTo(node.Value);

            if (compare < 0)
                node.Left = Insert(node.Left, value);
            else
                node.Right = Insert(node.Right, value);

            return node;

        }


        public void InOrderTraversal()
        {
            InOrderTraversal(Root);
            Console.WriteLine();
        }

        private void InOrderTraversal(BinarySearchTreeNode<T> node)
        {
            if (node != null)
            {
                InOrderTraversal(node.Left);
                Console.Write(node.Value + " ");
                InOrderTraversal(node.Right);
            }
        }

        public void PreOrderTraversal()
        {
            PreOrderTraversal(Root);
            Console.WriteLine();
        }

        private void PreOrderTraversal(BinarySearchTreeNode<T> node)
        {
            if (node != null)
            {
                Console.Write(node.Value + " ");
                PreOrderTraversal(node.Left);
                PreOrderTraversal(node.Right);
            }
        }

        public void PostOrderTraversal()
        {
            PostOrderTraversal(Root);
            Console.WriteLine();
        }

        private void PostOrderTraversal(BinarySearchTreeNode<T> node)
        {
            if (node != null)
            {
                PostOrderTraversal(node.Left);
                PostOrderTraversal(node.Right);
                Console.Write(node.Value + " ");
            }
        }

        // Print the tree visually
        public void PrintTree()
        {
            PrintTree(Root, 0);
        }


        private void PrintTree(BinarySearchTreeNode<T> root, int space)
        {
            int COUNT = 10;  // Distance between levels
            if (root == null)
                return;

            space += COUNT;
            PrintTree(root.Right, space);

            Console.WriteLine();
            for (int i = COUNT; i < space; i++)
                Console.Write(" ");
            Console.WriteLine(root.Value);
            PrintTree(root.Left, space);
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\nInserting : 45, 15, 79, 90, 10, 55, 12, 20, 50\r\n");

            var bst = new BinarySearchTree<int>();
            //bst.Insert(45);
            //bst.Insert(15);
            //bst.Insert(79);
            //bst.Insert(90);
            //bst.Insert(10);
            //bst.Insert(55);
            //bst.Insert(12);
            //bst.Insert(20);
            //bst.Insert(50);
            //////////////////
            bst.InsertIterative2(45);
            bst.InsertIterative2(15);
            bst.InsertIterative2(79);
            bst.InsertIterative2(90);
            bst.InsertIterative2(10);
            bst.InsertIterative2(55);
            bst.InsertIterative2(12);
            bst.InsertIterative2(20);
            bst.InsertIterative2(50);
            bst.PrintTree();

            if (bst.Find(12))
                Console.WriteLine("Found");
            else
                Console.WriteLine("Not found");

            Console.WriteLine("\nInOrder Traversal:");
            bst.InOrderTraversal();

            Console.WriteLine("\nPreOrder Traversal:");
            bst.PreOrderTraversal();

            Console.WriteLine("\nPostOrder Traversal:");
            bst.PostOrderTraversal();

            Console.ReadKey();

        }
    }
}

