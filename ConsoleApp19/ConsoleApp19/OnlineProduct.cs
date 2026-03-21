using System;
using System.Collections.Generic;
using System.Text;

public class OnlineProduct : Product
{
    private string downloadUrl;

    public string DownloadUrl
    {
        get
        {
            return downloadUrl;
        }
        set
        {
            downloadUrl = value;
        }
    }

    public OnlineProduct(string title, double price, string downloadUrl)
        : base(title, price)
    {
        this.downloadUrl = downloadUrl;
    }

    public override string ToString()
    {
        return $"Title: {this.Title}\nPrice: {this.Price:f2} EUR\nDownload: {this.DownloadUrl}";
    }
}

