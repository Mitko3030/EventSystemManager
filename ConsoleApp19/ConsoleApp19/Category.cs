using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Category
{
    private string title;
    private List<Product> products;

    public string Title
    {
        get
        {
            return title;
        }
        set
        {
            if (value.Length >= 3 && value.Length <= 50)
            {
                title = value;
            }
            else
            {
                throw new ArgumentException("Category title should be between 3 and 50 characters!");
            }
        }
    }


    public Category(string title)
    {
        this.title = title;
        products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double MinPrice()
    {
        return products.Min(p => p.Price);
    }

    public List<Product> GetProductsInRange(double from, double to)
    {
        return products
        .Where(p => p.Price >= from && p.Price <= to)
        .OrderBy(p => p.Title)
        .ToList();
    }

    public List<Product> GetProductsExpensiveToCheap()
    {
        return products
        .OrderByDescending(p => p.Price)
        .ToList();
    }

    public List<Product> GetProductsCheapToExpensive()
    {
        return products
        .OrderBy(p => p.Price)
        .ToList();
    }

    public override string ToString()
    {
        return $"Title: {this.Title}\nTotal Products: {this.products.Count}";
    }
}
