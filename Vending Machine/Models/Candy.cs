using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine.Models
{
/*  
    * Method:	Constructor
    * 
*/
    public class Candy : Product
    {
	public Candy(int price, string name, int id, int availableProducts) : base(price, name, "Candy", id, availableProducts)
	{

	}

	public override string Use()
	{
	    return "Eat the candy";
	}
    }
}
