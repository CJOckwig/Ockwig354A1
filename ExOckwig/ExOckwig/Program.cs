// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using ExOckwig;
internal class Program
{
        /*********************************************************************
        *** NAME : Caleb Ockwig                                            ***
        *** CLASS : CSc 354                                                ***
        *** ASSIGNMENT : 2                                                 ***
        *** DUE DATE : 10/5                                                ***
        *** INSTRUCTOR : GAMRADT ***
        *********************************************************************
        *** DESCRIPTION : Evaluates all of the expressions in a given file.***
        ********************************************************************/
    static void Main(string[] args)
    {
        String FilePath = (args.Length == 1) ? args[0] : null;
        BST SymbolTable = new BST();
        BST.LeftTraverse(SymbolTable.Root);
        LinkedList<Literal> LiteralTable; 
        ExVal expression = new ExVal(FilePath, out LiteralTable, SymbolTable);

        Console.WriteLine("Literal    Value      Size   Address");
        foreach (Literal Lit in LiteralTable)
        {
            Console.WriteLine(Lit.ToString());
        }
        Console.WriteLine("Press any key to traverse symbol table.");
        Console.ReadKey(true);
        BST.LeftTraverse(SymbolTable.Root);
    }
}