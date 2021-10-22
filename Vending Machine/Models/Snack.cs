using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine.Models
{
    public class Snack : Product
    {

/*  
    * Method:	Constructor
    * 
*/
	public Snack(int price, string name, int id, int availableProducts) : base(price, name, "Snack",id, availableProducts)
	{

	}

	public override string Use()
	{
	    return "Eat the snack";
	}
    }
}
