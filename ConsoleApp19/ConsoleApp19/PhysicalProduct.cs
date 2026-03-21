using System;
using System.Collections.Generic;
using System.Text;

public class PhysicalProduct : Product
{
    private double quantity;

    public double Quantity
    {
        get
        {
            return quantity;
        }
        set
        {
            quantity = value;
        }
    }

    public PhysicalProduct(string title, double price, double quantity)
        : base(title, price)
    {
        this.quantity = quantity;
    }

    public override string ToString()
    {
        return $"Title: {this.Title}\nPrice: {this.Price:f2} EUR\nQuantity: {this.Quantity:f2}";
    }
}


