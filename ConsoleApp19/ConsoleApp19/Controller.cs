using System;
using System.Collections.Generic;
using System.Text;

public class Controller
{
    private Dictionary<string, Category> categories;

    public Controller()
    {
        categories = new Dictionary<string, Category>();
    }

    public string AddCategory(List<string> args)
    {
        string categoryName = args[0];
        Category a = new Category(categoryName);
        categories.Add(categoryName, a);
        return $"Успешно доабвена категория";
    }

    public string AddProductToCategory(List<string> args)
    {
        string categoryTitle = args[1];
        string productTitle = args[2];
        double price = double.Parse(args[3]);
        string type = args[4];

        if (!categories.ContainsKey(categoryTitle))
            return $"Category {categoryTitle} not found.";

        Product product;

        if (type == "physical")
        {
            int quantity = int.Parse(args[5]);
            product = new PhysicalProduct(productTitle, price, quantity);
        }
        else if (type == "online")
        {
            string downloadUrl = args[5];
            product = new OnlineProduct(productTitle, price, downloadUrl);
        }
        else
        {
            return "Invalid product type.";
        }

        categories[categoryTitle].AddProduct(product);
        return $"Added product {productTitle} to Category {categoryTitle}!";
    }

    

    public string GetMinPrice(List<string> args)
    {
        string categoryTitle = args[1];
        var category = categories[categoryTitle];
        double minPrice = category.MinPrice();
        return $"Min price in {categoryTitle} is {minPrice:F2} EUR";
    }

    public string GetProductsInRange(List<string> args)
    {
        string categoryTitle = args[0];
        double from = double.Parse(args[1]);
        double to = double.Parse(args[2]);

        Category category = categories.First(c => c.() == categoryTitle);
        List<Product> result = category.GetProductsInRange(from, to);

        StringBuilder sb = new StringBuilder();
        foreach (var p in result)
        {
            sb.AppendLine(p.ToString());
        }
        return sb.ToString().TrimEnd();
    }

    public string GetProductsExpensiveToCheap(List<string> args)
    {
        //TODO: implement me
        throw new NotImplementedException();
    }

    public string GetProductsCheapToExpensive(List<string> args)
    {
        //TODO: implement me
        throw new NotImplementedException();
    }
}
