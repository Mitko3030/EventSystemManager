using System;
using System.Collections.Generic;
using System.Text;

public abstract class Song
{
    private string title;
    private int duration;
    private string artist;
    private string genre;

    public string Title
    {
        get
        {
            return title;
        }
        set
        {
            if (value.Length < 2 || value.Length > 100)
            {
                throw new ArgumentException("Title should be between 2 and 100 characters!");
            }
            else
            {
                title = value;
            }
        }
    }


    public int Duration
    {
        get
        {
            return duration;
        }
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("Duration should be positive!");
            }
            else
            {
                duration = value;
            }
        }
    }


    public string Artist
    {
        get
        {
            return artist;
        }
        set
        {
            if (value.Length < 3 || value.Length > 50)
            {
                throw new ArgumentException("Artist name should be between 3 and 50 characters!");
            }
            else
            {
                artist = value;
            }
        }
    }


    public string Genre
    {
        get
        {
            return genre;
        }
        set
        {
            genre = value;
        }
    }

    public Song(string title, int duration, string artist, string genre)
    {
        Title = title;
        Duration = duration;
        Artist = artist;
        Genre = genre;
    }

    public override string ToString()
    {
        return $"Title: {this.Title}\nDuration: {this.Duration}\nArtist: {this.Artist}";
    }
}
