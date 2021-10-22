using System;
using Xunit;
using Vending_Machine.Models;

namespace Vending_Machine.Tests
{
    public class VendingMachineTests
    {
	[Fact]
	public void VendingMachine_Test()
	{
	    // Arrange
	    string str, expectedString;
	    string transactionSummary;
	    int value;
	    int insertedMoney;
	    int purchasedProductsCount;
	    int[] returnedChangeArray;
	    bool success;
	    Product[] product = new Product[4];
	    Product[] purchasedProductsArray;
	    Product purchasedProduct;

	    VendingMachine vendingMachine = new VendingMachine();

	    Assert.NotNull(vendingMachine);
	    Assert.NotNull(product);

	    // Act

	    // Add some products..
	    product[0] = vendingMachine.AddProduct(200, "Doll", ProductTypes.Toy, 5);
	    product[1] = vendingMachine.AddProduct(13, "Coca Cola 50 Cl Bottle", ProductTypes.Beverage, 10);
	    product[2] = vendingMachine.AddProduct(7, "Snickers", ProductTypes.Candy, 20);
	    product[3] = vendingMachine.AddProduct(8, "Grillchips 40g Estrella", ProductTypes.Snack, 10);

	    Assert.NotNull(product[0]);
	    Assert.NotNull(product[1]);
	    Assert.NotNull(product[2]);
	    Assert.NotNull(product[3]);


	    str=vendingMachine.ShowAll();							    // Check ShowAll method..

	    expectedString = "Avaliable procucts:\n\n  1: Doll                         Price:   200 kr.   5 remaining\n";
	    expectedString += "  2: Coca Cola 50 Cl Bottle       Price:    13 kr.  10 remaining\n";
	    expectedString += "  3: Snickers                     Price:     7 kr.  20 remaining\n";
	    expectedString += "  4: Grillchips 40g Estrella      Price:     8 kr.  10 remaining\n";

	    Assert.Equal(expectedString, str);

	    insertedMoney = vendingMachine.MoneyDenominations[(int)MoneyDenominationTypes.Banknote50];

	    try
	    {
		success = vendingMachine.InsertMoney(MoneyDenominationTypes.Banknote50, out str);   // Insert 50 kr into the vending machine
//		success = true;

		expectedString = String.Format("Added {0:C0} to the balance", insertedMoney);	    // Check that the returned status string from InsertMoney()
												    // is correct
		Assert.Equal(expectedString, str);
	    }
	    catch (ArgumentOutOfRangeException e)
	    {
		success = false;
	    }
	    Assert.True(success);

	    Assert.Equal(insertedMoney, vendingMachine.Balance);				// Check if the balance is correct

	   
	    purchasedProduct = vendingMachine.Purchase(product[0].ID, out str);			// Purchase a doll
	    Assert.Null(purchasedProduct);                                                      // Which should fail because it costs 200kr,
												// only 50kr available in the vending machine

	    expectedString = String.Format("Not enough money to purchase product {0}!", product[0].Name);
	    Assert.Equal(expectedString, str);                                                  // Check that the returned status string from Purchase()
												// reports that the purchase was unsuccessful

	    purchasedProduct = vendingMachine.Purchase(product[1].ID, out str);                 // Purchase a beverage
	    Assert.NotNull(purchasedProduct);                                                   

	    expectedString = String.Format("Purchased {0}...", product[1].Name);
	    Assert.Equal(expectedString, str);                                                  // Check that the returned status string from Purchase()
												// is correct

	    value = insertedMoney - product[1].Price;
	    Assert.Equal(value, vendingMachine.Balance);					// Check if the balance is correct

	    // Stop buying products
	    purchasedProductsCount = vendingMachine.EndTransaction(out purchasedProductsArray, out transactionSummary, out returnedChangeArray);
	    Assert.NotNull(purchasedProductsArray);
	    Assert.NotNull(returnedChangeArray);

	    Assert.Equal(1, purchasedProductsCount);						// Check that 1 product has been purchased
	    Assert.Single(purchasedProductsArray);                                              // Check that purchasedProductsArray contains a 1 cell
	    Assert.Equal(vendingMachine.MoneyDenominations.Length, returnedChangeArray.Length); // Check that size of the returnedChangeArray is the same as MoneyDenominations array
	    Assert.Equal(purchasedProduct, purchasedProductsArray[0]);                          // Check that purchasedProductsArray contains the purchased
												// product object
	    // Check the returned change:
	    Assert.Equal(2, returnedChangeArray[0]);	    // 2 1kr coins
	    Assert.Equal(1, returnedChangeArray[1]);        // 1 5kr coin
	    Assert.Equal(1, returnedChangeArray[2]);        // 1 10kr coin
	    Assert.Equal(1, returnedChangeArray[3]);        // 1 20kr banknote
	    Assert.Equal(0, returnedChangeArray[4]);        // 0 50kr banknote
	    Assert.Equal(0, returnedChangeArray[5]);        // 0 100kr banknote
	    Assert.Equal(0, returnedChangeArray[6]);        // 0 500kr banknote
	    Assert.Equal(0, returnedChangeArray[7]);        // 0 1000kr banknote

	    expectedString = "Purchased 1 procuct(s):\n\n  1 x Coca Cola 50 Cl Bottle\n\nTotal price:        13 kr\nAmount to return:   37 kr\n";
	    expectedString += "\nChange returned:\n   20 kr: 1\n   10 kr: 1\n    5 kr: 1\n    1 kr: 2\n";

	    Assert.Equal(expectedString, transactionSummary);

	    str = purchasedProduct.Use();		    // Test Use()	
	    Assert.Equal("Drink it", str);
	}
    }
}
