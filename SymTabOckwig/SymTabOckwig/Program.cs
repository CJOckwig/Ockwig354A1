

/********************************************************************
*** NAME : Caleb Ockwig                                           ***
*** CLASS : CSc 354 Systems Programming                           ***
*** ASSIGNMENT : A1 - Symbol Table                                ***
*** DUE DATE : 9/14/2022                                          ***
*** INSTRUCTOR : GAMRADT                                          ***
*********************************************************************
*** DESCRIPTION : Populate a binary search tree which will hold   ***
***               nodes consisting of a alphanumeric tag, a value,*** 
***               and an rFlag which must be                      ***
***               given in a speicifed format                     ***
********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SymTabOckwig 
{
    internal class Program
    {

        //    .\SymTabOckwig\SymTabOckwig\bin\Debug\net6.0\Symbols.dat
        static void Main(string[] args)
        {
            BST SymbolTable = new BST();
            string relativePath = "..\\..\\..\\Symbols.dat";//this will mean symbols.dat is read from the solution folder
            string[] LineValues = { };
            try {
                LineValues = System.IO.File.ReadAllLines(relativePath);
                } 
            catch (Exception e)
            {
                Console.WriteLine(relativePath + " could not return a valid file. Try moving the Symbols.dat file into the solution folder.");
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
                    if (!SymbolTable.AddNode(symbol, value, rFlag))
                    {
                        errorCode += 32;
                    }
                }
                Validation.ShowError(errorCode,symbol, CurrentLine);
           
            }
            Console.WriteLine("\n\nSymbol    RFlag   MFlag   IFlag   Value");
            Console.WriteLine("Enter the name of the file you wish to read the symbols from: ");
            String newFile = Console.ReadLine();
            try
            {
                LineValues = System.IO.File.ReadAllLines("..\\..\\..\\" + newFile);

                foreach(String symbol in LineValues)
                {

                    String CurrentSymbol = symbol.Trim(' ');
                    CurrentSymbol = Validation.FormatSymbol(CurrentSymbol);
                    byte errorCode = Validation.CheckSymbol(CurrentSymbol);
                    if(errorCode == 0)
                    {
                        BST.Search(SymbolTable.Root, CurrentSymbol);
                        //search for the symbol
                    }else
                    {
                        if (errorCode % 2 == 1)
                            Console.WriteLine("ERROR - " + symbol + " has a symbol greater than 12 characters");
                        errorCode /= 2;
                        if (errorCode % 2 == 1)
                            Console.WriteLine("ERROR - " + symbol + " is not alphanumeric");
                        errorCode /= 2;
                        if (errorCode % 2 == 1)
                            Console.WriteLine("ERROR - " + symbol + " first symbol must be a letter");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("..\\..\\..\\" + newFile);
                Console.WriteLine(newFile + " was not found in the top level of the project folder. Please move the file there.");
            }
        }
    }
}


