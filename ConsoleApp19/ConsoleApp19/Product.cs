using System;
using System.Collections.Generic;
using System.Text;

public abstract class Product
{
    private string title;
    private double price;

    public string Title
    {
        get
        {
            return title;
        }
        set
        {
            if (value.Length >= 2 && value.Length <= 100)
            {
                title = value;
            }
            else { throw new ArgumentException("Title should be between 2 and 100 characters!"); }
        }
    }


    public double Price
    {
        get
        {
            return price;
        }
        set
        {
            if (value < 0) { throw new ArgumentException("Price should be positive!"); }
            else
            {
                price = value;
            }
        }
    }

    public Product(string title, double price)
    {
        this.title = title;
        this.price = price;
    }

    

    public override string ToString()
    {
        return $"Title: {this.Title}\nPrice: {this.Price:f2} EUR";
    }
}