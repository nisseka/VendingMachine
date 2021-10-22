using System;
using System.Collections.Generic;
using System.Text;

namespace Vending_Machine.Models
{
    interface IVending
    {
	Product Purchase(int ID, out string statusString);
	string ShowAll();

	bool InsertMoney(MoneyDenominationTypes type, out string statusString);
	int EndTransaction(out Product[] purchasedProducts, out string transactionSummary, out int[] returnedChange);
    }
}
