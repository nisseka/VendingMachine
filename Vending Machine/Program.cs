using System;
using Vending_Machine.Models;

namespace Vending_Machine
{
    class Program
    {
	static void Main(string[] args)
	{
	    int selectedMenuItemIndex;
	    int insertMoneyIndex,i;
	    string transactionSummary;
	    Product[] purchasedProducts;
	    int purchasedProductsCount;
	    bool exit = false;
	    string str = string.Empty;
	    int[] returnedChange;

	    VendingMachine vendingMachine = new VendingMachine();

	    vendingMachine.AddProduct(200, "Doll", ProductTypes.Toy, 5);
	    vendingMachine.AddProduct(8, "Coca Cola 33 Cl Can", ProductTypes.Beverage, 15);
	    vendingMachine.AddProduct(13, "Coca Cola 50 Cl Bottle", ProductTypes.Beverage, 10);
	    vendingMachine.AddProduct(7, "Snickers", ProductTypes.Candy, 20);
	    vendingMachine.AddProduct(8, "Grillchips 40g Estrella", ProductTypes.Snack, 10);

	    do
	    {
		Console.Clear();
		Console.WriteLine("Vending Machine\n");
		Console.WriteLine("Available money: {0:C0}\n",vendingMachine.Balance);
		Console.WriteLine(vendingMachine.ShowAll());

		insertMoneyIndex = vendingMachine.ProductCount + 1;
		Console.WriteLine("  0: Finished Buying");
		Console.WriteLine("{0,3:D}: Insert Money", insertMoneyIndex);

		selectedMenuItemIndex = PrintStringAndRequestNumberFromUser_Int("\nEnter selection:");          // The user must enter a value which menu item it wants

		if (selectedMenuItemIndex == 0)
		{
		    purchasedProductsCount = vendingMachine.EndTransaction(out purchasedProducts, out transactionSummary, out returnedChange);

		    Console.WriteLine("You selected to stop buying.\n");
		    Console.WriteLine(transactionSummary);

		    if (purchasedProductsCount > 0)
		    {
			Console.WriteLine("How to use products:\n");
			foreach (var item in purchasedProducts)
			{
			    Console.WriteLine("Product {0,3}: {1}", item.Name, item.Use());
			}
		    }
		    do
		    {
			str = PrintStringAndRequestStringFromUser("Run again? (y/n)").ToLower();

		    } while (str != "y" && str != "n");

		    if (str == "n")
		    {
			exit = true;
		    }
		    else
			continue;
		}
		else
		if (selectedMenuItemIndex >= 1 && selectedMenuItemIndex < insertMoneyIndex)
		{
		    Product purchasedProduct = vendingMachine.Purchase(selectedMenuItemIndex, out str);

		    Console.WriteLine(str);
		}
		else
		if (selectedMenuItemIndex == insertMoneyIndex)
		{
		    Console.WriteLine("Insert Money:\nAvailable denominations:\n");
		    Console.WriteLine(vendingMachine.MoneyDenominationsString);
		    bool exit2 = false;
		    do
		    {
			i = PrintStringAndRequestNumberFromUser_Int("\nEnter selection:");              // The user must enter a value which menu item it wants

			if (i > 0 && i <= vendingMachine.MoneyDenominations.Length)
			{
			    try
			    {
				vendingMachine.InsertMoney((MoneyDenominationTypes)i - 1, out str);
				Console.WriteLine(str);
				exit2 = true;
			    }
			    catch (ArgumentOutOfRangeException e)
			    {

			    }
			}
		    } while (!exit2);
		} else
		    continue;

		if (!exit)
		{
		    Console.Write("\nPress any key to continue..");
		    Console.ReadKey();
		}
	    } while (!exit);

	    Console.WriteLine("Bye!");
	}

/*
    * :    PrintStringAndRequestNumberFromUser_Int
    * 
    * Outputs a title text specified by displayText in the console and waits for the user to enter a number. If defaultValue isn't 0, display it to 
    * the user in parentheses, for example (56). Then if the user doesn't enter anything (only presses return), use the value specified in defaultValue
    * 
    * returns:    The number in Integer format
    * 
*/
	static int PrintStringAndRequestNumberFromUser_Int(string displayText, int defaultValue = 0)
	{
	    int r = 0;
	    string str;
	    bool exit;

	    if (defaultValue != 0)
	    {
		displayText += String.Format(" (Press only enter to use {0})", defaultValue);
	    }

	    do
	    {
		str = PrintStringAndRequestStringFromUser(displayText);

		if (str.Length == 0 && defaultValue != 0)
		{
		    r = defaultValue;
		    exit = true;
		}
		else
		    exit = int.TryParse(str, out r);

	    } while (!exit);
	    return r;
	}

/*
    * Method:    PrintStringAndRequestStringFromUser
    * 
    * Outputs a title text specified by DisplayText in the console and records the users keypresses until return key is pressed
    * 
    * returns:    The recorded text string
*/
	static string PrintStringAndRequestStringFromUser(string DisplayText)
	{
	    Console.Write("{0} ", DisplayText);
	    return Console.ReadLine();
	}

    }
}
