using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine.Models
{
    public abstract class Product
    {
	private readonly int id;
	private int availableProducts;

	protected int price;
	protected string name;
	protected string typeName;

	public int Price { get => price; }
	public int ID { get => id; }
	public int AvailableProducts 
	{ 
	    get => availableProducts;
	    set
	    {
		if  (value < 0)
		{
		    throw new ArgumentOutOfRangeException("AvailableProducts", "AvailableProducts can't be negative!");
		}
		    
		availableProducts = value;
	    }
	}
	public string Name { get => name; }
	public string TypeName { get => typeName; }

/*  
    * Method:	Constructor
    * 
*/
	public Product(int price, string name, string typeName, int id,int availableProducts)
	{
	    this.price = price;
	    this.name = name;
	    this.typeName = typeName;
	    this.id = id;
	    this.availableProducts = availableProducts;
	}

	public virtual string Use()
	{
	    return string.Empty;
	}

	public string Examine()
	{
	    return String.Format("{0,-28} Price: {1,8:C0}. {2,3} remaining", name, price,availableProducts);
	}
    }
}
