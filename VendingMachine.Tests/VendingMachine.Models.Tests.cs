using System;
using System.Collections.Generic;
using System.Text;
using Vending_Machine.Models;
using Xunit;


namespace Vending_Machine.Tests
{
    public class VendingMachineModelsTests
    {
	[Fact]
	public void ClassVendingMachine_PurchaseTestProductNotAvaliable()
	{
	    // Arrange
	    string str, expectedStatusString;
	    Product purchasedProduct,product;

	    VendingMachine vendingMachine = new VendingMachine();

	    Assert.NotNull(vendingMachine);

	    // Act
	    vendingMachine.InsertMoney(MoneyDenominationTypes.Banknote500, out str);

	    product=vendingMachine.AddProduct(200, "Doll", ProductTypes.Toy, 0);

	    expectedStatusString = String.Format("Product {0} is unavailable!", product.Name);

	    purchasedProduct = vendingMachine.Purchase(product.ID, out str); 
	    Assert.Null(purchasedProduct);
	    Assert.Equal(expectedStatusString, str);
	}

	[Fact]
	public void ClassVendingMachine_InsertMoneyInvalidDenominationTest()
	{
	    // Arrange
	    bool success;
	    string str;

	    // Act
	    VendingMachine vendingMachine = new VendingMachine();
	    Assert.NotNull(vendingMachine);

	    try
	    {
		vendingMachine.InsertMoney((MoneyDenominationTypes) 598, out str);
		success = true;
	    } catch (ArgumentOutOfRangeException e)
	    {
		success = false;
	    }

	    // Assert
	    Assert.False(success);
	}


	[Fact]
	public void ClassToy_Test()
	{
	    // Arrange
	    string expectedString;

	    // Act
	    Toy toyObject = new Toy(200, "Doll",1,1);

	    // Assert

	    Assert.NotNull(toyObject);
	    Assert.Equal(200, toyObject.Price);
	    Assert.Equal("Doll", toyObject.Name);
	    Assert.Equal(1, toyObject.ID);
	    Assert.Equal(1, toyObject.AvailableProducts);
	    Assert.Equal("Play with the toy", toyObject.Use());

	    expectedString=String.Format("{0,-28} Price: {1,8:C0}. {2,3} remaining", toyObject.Name, toyObject.Price, toyObject.AvailableProducts);
	    Assert.Equal(expectedString, toyObject.Examine());
	}
	[Fact]
	public void ClassToy_NegativeAvailableProductsTest()
	{
	    // Arrange
	    bool success;

	    // Act
	    Toy toyObject = new Toy(200, "Doll", 1, 0);     // Start with 0 available products

	    // Assert
	    Assert.NotNull(toyObject);

	    try
	    {
		toyObject.AvailableProducts--;
		success = true;
	    }
	    catch (ArgumentOutOfRangeException e)
	    {
		success = false;
	    }


	    // Assert
	    Assert.False(success);
	}

	[Fact]
	public void ClassCandy_Test()
	{
	    // Arrange
	    string expectedString;

	    // Act
	    Candy candyObject = new Candy(7, "Snickers", 1, 1);

	    // Assert

	    Assert.NotNull(candyObject);
	    Assert.Equal(7, candyObject.Price);
	    Assert.Equal("Snickers", candyObject.Name);
	    Assert.Equal(1, candyObject.ID);
	    Assert.Equal(1, candyObject.AvailableProducts);
	    Assert.Equal("Eat the candy", candyObject.Use());

	    expectedString = String.Format("{0,-28} Price: {1,8:C0}. {2,3} remaining", candyObject.Name, candyObject.Price, candyObject.AvailableProducts);
	    Assert.Equal(expectedString, candyObject.Examine());
	}

	[Fact]
	public void ClassBeverage_Test()
	{
	    // Arrange
	    string expectedString;

	    // Act
	    Beverage beverageObject = new Beverage(8, "Coca Cola 33 Cl Can", 1, 1);

	    // Assert

	    Assert.NotNull(beverageObject);
	    Assert.Equal(8, beverageObject.Price);
	    Assert.Equal("Coca Cola 33 Cl Can", beverageObject.Name);
	    Assert.Equal(1, beverageObject.ID);
	    Assert.Equal(1, beverageObject.AvailableProducts);
	    Assert.Equal("Drink it", beverageObject.Use());

	    expectedString = String.Format("{0,-28} Price: {1,8:C0}. {2,3} remaining", beverageObject.Name, beverageObject.Price, beverageObject.AvailableProducts);
	    Assert.Equal(expectedString, beverageObject.Examine());
	}

	[Fact]
	public void ClassSnack_Test()
	{
	    // Arrange
	    string expectedString;

	    // Act
	    Snack snackObject = new Snack(8, "Grillchips 40g Estrella", 1, 1);

	    // Assert

	    Assert.NotNull(snackObject);
	    Assert.Equal(8, snackObject.Price);
	    Assert.Equal("Grillchips 40g Estrella", snackObject.Name);
	    Assert.Equal(1, snackObject.ID);
	    Assert.Equal(1, snackObject.AvailableProducts);
	    Assert.Equal("Eat the snack", snackObject.Use());

	    expectedString = String.Format("{0,-28} Price: {1,8:C0}. {2,3} remaining", snackObject.Name, snackObject.Price, snackObject.AvailableProducts);
	    Assert.Equal(expectedString, snackObject.Examine());
	}

    }
}
