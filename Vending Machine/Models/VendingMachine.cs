using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine.Models
{
    public enum ProductTypes { Snack, Toy, Beverage, Candy}
    public enum MoneyDenominationTypes { Coin1kr, Coin5kr,Coin10kr, Banknote20, Banknote50, Banknote100, Banknote500, Banknote1000 }

    public class VendingMachine : IVending
    {
	private readonly int[] moneyDenominations;
	private readonly List<Product> products;
	private readonly List<Product> purchasedProducts;
	private int balance;

	public int[] MoneyDenominations { get => moneyDenominations; }

/*  
    * Property MoneyDenominationsString
    * 
    * returns a string describing the available denominations
    * 
    * 
*/
	public string MoneyDenominationsString
	{
	    get
	    {
		StringBuilder sb = new StringBuilder();
		int i = 0;
		foreach (var item in moneyDenominations)
		{
		    if (i > 0)
		    {
			sb.Append(", ");
		    }
		    i++;
		    sb.AppendFormat("{0}: {1:C0}",i,item);
		}
		return sb.ToString();
	    }
	}

	public int Balance { get => balance; }
	public int ProductCount { get => products.Count; }

	public Product[] Products { get => products.ToArray(); }

	public Product this[int index] 
	{ 
	    get => products[index]; 
	}

/*  
    * Method:	Constructor
    * 
*/	
	public VendingMachine()
	{
	    moneyDenominations = new int[] { 1, 5, 10, 20, 50, 100, 500, 1000 };
	    purchasedProducts = new List<Product>();
	    products = new List<Product>();

	}

/*  
    * Method:	AddProduct
    * 
    * Adds a product to the available products in the vending machine
    * 
    * returns:	The newly created product object or
    *		null if no object could be created
    * 
    * 
*/
	public Product AddProduct(int price, string name,ProductTypes type, int availableProducts)
	{
	    Product product;
	    int id = products.Count + 1;

	    switch (type)
	    {
		case ProductTypes.Snack:
		    product = new Snack(price, name, id, availableProducts);
		    break;
		case ProductTypes.Toy:
		    product = new Toy(price, name, id, availableProducts);
		    break;
		case ProductTypes.Beverage:
		    product = new Beverage(price, name, id, availableProducts);
		    break;
		case ProductTypes.Candy:
		    product = new Candy(price, name, id, availableProducts);
		    break;

		default:
		    product = null;
		    break;
	    }

	    if (product != null)
	    {
		products.Add(product);
	    }

	    return product;
	}


/*  
    * Method:	Purchase
    * 
    * Purchases a product. productID identifies the product
    * 
    * returns:	The purchased product object
    *		null if no product was purchased
    * 
    * Out variables:
    * statusString:   Receives a string with information about the status of the purchase, if successful etc
    * 
*/
	public Product Purchase(int productID,out string statusString)
	{
	    Product product=null;
	    int i = 0;

	    statusString = string.Empty;
	    foreach (var item in products)
	    {
		if (item.ID==productID)
		{
		    if (balance >= item.Price)
		    {
			if (item.AvailableProducts > 0)
			{
			    product = item;
			    purchasedProducts.Add(item);
			    balance -= item.Price;

			    item.AvailableProducts--;
			    statusString = String.Format("Purchased {0}...", item.Name);
			} else
			    statusString = String.Format("Product {0} is unavailable!", item.Name);

		    } else
		    {
			statusString = String.Format("Not enough money to purchase product {0}!",item.Name);
		    }
		    break;
		}
		i++;
	    }

	    if (product == null && i >= products.Count)
	    {
		statusString = String.Format("No product with ID {0} found!", productID);
	    }
	    return product;
	}

/*  
    * Method:	ShowAll
    * 
    * returns: A string containing all the products available for purchase in the vending machine
    * 
    * 
*/	
	public string ShowAll()
	{
	    int i;
	    StringBuilder sb = new StringBuilder("Avaliable procucts:\n\n");

	    i = 0;
	    foreach (var item in products)
	    {
		sb.AppendFormat("{0,3:D}: ",i+1);
		sb.Append(item.Examine());
		sb.Append("\n");
		i++;
	    }

	    return sb.ToString();
	}

/*  
    * Method:	InsertMoney
    * 
    * Adds money to the money pool 
    * 
    * Throws an ArgumentOutOfRangeException if type isn't of a valid denomination"
    * 
    * returns: true
    * 
    * Out variables:
    * statusString:   Receives a string with information about the inserted amount of money
    * 
*/	
	public bool InsertMoney(MoneyDenominationTypes type, out string statusString)
	{
	    bool r;
	    int moneyAmount;

	    if (type < MoneyDenominationTypes.Coin1kr || type > MoneyDenominationTypes.Banknote1000)
	    {
		throw new ArgumentOutOfRangeException("type", "Supplied denomination isn't valid");
	    }

	    moneyAmount = moneyDenominations[(int)type];
	    balance += moneyAmount;

	    statusString = String.Format("Added {0:C0} to the balance", moneyAmount);
	    r = true;
	    return r;
	}

/*  
    * Method:	EndTransaction
    * 
    * Stops the buying process 
    * 
    * returns: The number of products purchased
    * 
    * Out variables:
    * purchasedProducts:    Receives an array of the purchased products
    * transactionSummary:   Receives a string with information about the products purchased and the change returned
    * returnedChange	    Receives an int array containing the number of coins/banknotes returned of each denomination according to enum MoneyDenominationTypes. 
    *			    ( f. ex: returnedChange[MoneyDenominationTypes.Coin1kr] contains the number of returned 1 kr coins )
    * 
*/
	public int EndTransaction(out Product[] purchasedProducts, out string transactionSummary,out int[] returnedChange)
	{
	    int i;
	    int totalPrice = 0,value,count;

	    StringBuilder sb = new StringBuilder($"Purchased {this.purchasedProducts.Count} procuct(s):\n\n");

	    List<string> nameList = new List<string>();
	    List<int> countList = new List<int>();

	    // Group purchased products by name:
	    foreach (var item in this.purchasedProducts)
	    {
		if ((i = nameList.IndexOf(item.Name)) >= 0)
		{
		    countList[i]++;
		} else
		{
		    nameList.Add(item.Name);
		    countList.Add(1);
		}
		totalPrice += item.Price;
	    }

	    // Write info about purchased products:
	    i = 0;
	    foreach (var item in nameList)
	    {
		sb.AppendFormat("{0,3:D} x {1}\n",countList[i],item);
		i++;
	    }

	    sb.AppendFormat("\nTotal price:{0,13:C0}\nAmount to return:{1,8:C0}\n", totalPrice, balance);

	    sb.Append("\nChange returned:\n");

	    int[] moneyDenominationsCount = new int[moneyDenominations.Length];

	    if (balance > 0)
	    {
		// Count the change to return:

		i = moneyDenominations.Length - 1;
		value = moneyDenominations[i];
		for (; i >= 0 && balance > 0;)
		{
		    if (value <= balance)
		    {
			moneyDenominationsCount[i]++;
			balance -= value;
		    }
		    else
		    {
			i--;
			value = moneyDenominations[i];
		    }
		}

		for (i = moneyDenominations.Length - 1; i >= 0; i--)
		{
		    value = moneyDenominations[i];
		    count = moneyDenominationsCount[i];

		    if (count > 0)
		    {
			sb.AppendFormat("{0,8:C0}: {1}\n", value, count);
		    }
		}
	    } else
	    {
		sb.Append("None\n");
	    }

	    transactionSummary = sb.ToString();
	   
	    purchasedProducts = this.purchasedProducts.ToArray();                // Copy purchased products to new output array
	    this.purchasedProducts.Clear();

	    returnedChange = moneyDenominationsCount;				 // Return the change

	    return purchasedProducts.Length;
	}

    }
}
