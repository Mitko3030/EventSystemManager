using System;
using System.Collections.Generic;
using System.Text;

public class Single : Song
{
    private DateTime releaseDate;

    public DateTime ReleaseDate
    {
        get
        {
            return releaseDate;
        }
        set
        {
            releaseDate = value;
        }
    }

    public Single(string title, int duration, string artist, string genre, DateTime releaseDate)
        : base(title, duration, artist, genre)
    {
        this.releaseDate = releaseDate;
    }

    public override string ToString()
    {
        return $"Title: {this.Title}\nDuration: {this.Duration}\nArtist: {this.Artist}\nRelease Date: {ReleaseDate:dd/MM/yyyy}";
    }
}


