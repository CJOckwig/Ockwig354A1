using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExOckwig
{
    public class BST
    {
        public BST()
        {
            Root = null;
            string inputFile = "SYMBOLS.DAT";

            string relative = "ExOckwig" + Path.DirectorySeparatorChar + inputFile;;//this will mean symbols.dat is read from the solution folder
            string[] LineValues = { };
            try
            {
                LineValues = System.IO.File.ReadAllLines(relative);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            string[] CurrentLine;
            foreach (string LineValue in LineValues)
            {
                CurrentLine = Validation.ParseLine(LineValue);
                string symbol = Validation.FormatSymbol(CurrentLine[0]);//Symbol is formatted

                byte errorCode = Validation.CheckSymbol(CurrentLine[0]);
                //byte now has error code. 0 means good data
                bool rFlag = false;
                int flag = Validation.CheckRFlag(CurrentLine[1]);
                if (flag == 1)
                {
                    rFlag = true;
                }
                else if (flag == 0)
                {
                    rFlag = false;
                }
                else
                {
                    errorCode += 8; //Flag was not in valid input format (true,false,1,0)
                }
                int value = 0;
                if (Validation.CheckValue(CurrentLine[2]))
                {
                    value = Validation.FormatValue(CurrentLine[2]);
                }
                else
                {
                    errorCode += 16;
                }
                if (errorCode == 0)
                {
                    if (!this.AddNode(symbol, value, rFlag))
                    {
                        errorCode += 32;
                    }
                }
                Validation.ShowError(errorCode, symbol, CurrentLine);

            }
        }
        public Node Root { get; set; }
        /********************************************************************
        *** FUNCTION BST.AddNode()                                        ***
        *********************************************************************
        *** DESCRIPTION : Adds a node with the given                      ***
        *                 information to the defined BST.                 ***
        *                 if no root exists, that node will               ***
        *                 be the new node                                 ***
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
                    //this would mean that the symbol is the same
                    //set MFlag to true and exit with false
                    leaf.MFlag = true;
                    return false;
                }
            }

            Node newNode = new Node(symbol, rFlag, value);

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
        public static int LeftTraverse(Node? root, int n = 1)
        {
            if (root != null)
            {
                n = LeftTraverse(root.Left, n);
                root.PrintNode();
                if (n == 20)
                {
                    Console.ReadKey();
                    n = 0;
                }
                n++;
                n = LeftTraverse(root.Right, n);

            }
            return n;
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

        public Node Search(String symbol)
        {
            Node leaf = this.Root;
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
                    break;
                }
            }//assuming you are searching exists in tree
            return TargetNode;
            Console.WriteLine("Error - The symbol " + symbol + " was not found in the symbol table");

        }


    }
}
