using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine.Models
{
/*  
    * Method:	Constructor
    * 
*/
    public class Beverage : Product
    {
	public Beverage(int price, string name, int id, int availableProducts) : base(price, name, "Beverage", id, availableProducts)
	{

	}

	public override string Use()
	{
	    return "Drink it";
	}
    }
}
