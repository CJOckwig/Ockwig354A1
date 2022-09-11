using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymTabOckwig
{
    internal class BST
    {
        public BST()
        {
            Root = null;
        }
        public Node Root { get; set; }
        /********************************************************************
        *** FUNCTION BST.AddNode() ***
        *********************************************************************
        *** DESCRIPTION : Adds a node with the given 
        *                 information to the defined BST.
        *                 if no root exists, that node will 
        *                 be the new node   
        *                 if a node with that symbol already exists,      ***
        *                 it will set the mFlag  to true                  *** 
        *                 and exit returning false                        ***
        *** INPUT ARGS : string symbol, int value, bool rFlag             ***
        *** OUTPUT ARGS : none                                            ***
        *** IN/OUT ARGS : none                                            ***
        *** RETURN : bool success                                         ***
        ********************************************************************/
        public bool AddNode(String symbol, int value, bool rFlag)
        {
          
            Node leaf = this.Root;
            Node TargetNode = leaf;
            while (leaf != null)
            {
                TargetNode = leaf;
                if(string.Compare(leaf.Symbol,symbol) > 0)
                {
                    leaf = leaf.Left;
                }
                else if(string.Compare(leaf.Symbol, symbol) < 0)
                {
                    leaf = leaf.Right;
                }
                else
                {
                    //this would mean that the symbol is the same
                    //set MFlag to true and exit with false
                    leaf.MFlag = true;
                    return false;
                }
            }

            Node newNode = new Node(symbol, rFlag,value);

            if (this.Root == null)
            {
                this.Root = newNode;
            }
            else
            {
                if (string.Compare(TargetNode.Symbol, symbol) > 0)
                {
                    TargetNode.Left = newNode;
                }
                else if (string.Compare(TargetNode.Symbol, symbol) < 0)
                {
                    TargetNode.Right = newNode;
                }
            }
            return true;
        }
        /********************************************************************
        *** FUNCTION LeftTraverse                                         ***
        *********************************************************************
        *** DESCRIPTION : Traverse the tree and print the contents of each node
        *** INPUT ARGS : Node root                                        ***
        *** OUTPUT ARGS : string symbol                                   ***
        *** IN/OUT ARGS : none                                            ***
        *** RETURN : none                                                 ***
        ********************************************************************/
        public static void LeftTraverse(Node? root)
        {
            if(root != null)
            {
                LeftTraverse(root.Left);
                root.ToString();
                LeftTraverse(root.Right);
            }
        }
        /********************************************************************
        *** BST.Destroy ***
        *********************************************************************
        *** DESCRIPTION : creates a substring of the passed in symbol     ***
        *** INPUT ARGS  : Node root                                       ***
        *** OUTPUT ARGS : None                                            ***
        *** IN/OUT ARGS : None                                            ***
        *** RETURN      : none                                            ***
        ********************************************************************/
        public static void Destroy(Node? n)
        {
            if (n != null)
            {
                Destroy(n.Left);
                Destroy(n.Right);
                
                n = null;
            }
        }

        public static void Search(Node root, String symbol)
        {
            Node leaf = root;
            Node TargetNode = leaf;
            while (leaf != null)
            {
                TargetNode = leaf;
                if (string.Compare(leaf.Symbol, symbol) > 0)
                {
                    leaf = leaf.Left;
                }
                else if (string.Compare(leaf.Symbol, symbol) < 0)
                {
                    leaf = leaf.Right;
                }
                else
                {
                    TargetNode.ToString();    
                    return;
                }
            }
            Console.WriteLine("The symbol " + symbol + " was not found in the symbol table");

        }


    }
}
