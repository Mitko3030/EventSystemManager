using System;
using System.Collections.Generic;
using System.Text;

public class AlbumSong : Song
{
    private string albumName;

    public string AlbumName
    {
        get
        {
            return albumName;
        }
        set
        {
            if (value.Length < 3 || value.Length > 100)
            {
                throw new ArgumentException("Album name should be between 3 and 100 characters!");
            }
            else
            {
                albumName = value;
            }
        }
    }

    public AlbumSong(string title, int duration, string artist, string genre, string albumName)
        : base(title, duration, artist, genre)
    {
        this.albumName = albumName;
    }

    public override string ToString()
    {
        return $"Title: {this.Title}\nDuration: {this.Duration}\nArtist: {this.Artist}\nAlbum: {this.AlbumName}";
    }
}

