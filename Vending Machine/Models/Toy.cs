using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine.Models
{
/*  
    * Method:	Constructor
    * 
*/
    public class Toy : Product
    {
	public Toy(int price, string name, int id, int availableProducts) : base(price, name, "Toy", id, availableProducts)
	{

	}

	public override string Use()
	{
	    return "Play with the toy";
	}
    }
}
