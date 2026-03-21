using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Playlist
{
    private string title;
    private List<Song> songs;

    public string Title
    {
        get
        {
            return title;
        }
        set
        {
            if (value.Length < 3 || value.Length > 50)
            {
                throw new ArgumentException("Playlist title should be between 3 and 50 characters!");
            }
            else
            {
                title = value;
            }
        }
    }


    public Playlist(string title)
    {
        this.title = title;
        songs = new List<Song>();
    }

    public void AddSong(Song song)
    {
        songs.Add(song);
    }

    public int TotalDuration()
    {
        return songs.Sum(s => s.Duration);
    }

    public List<Song> GetSongsByArtist(string artist)
    {
        return songs.Where(s => s.Artist == artist).OrderBy(s => s.Title).ToList();
    }

    public List<Song> GetSongsByGenre(string genre)
    {
        return songs.Where(s => s.Genre == genre).OrderBy(s => s.Title).ToList();
    }

    public List<Song> GetSongsAboveDuration(int duration)
    {
        return songs.Where(s => s.Duration > duration).OrderByDescending(s => s.Duration).ToList();
    }

    public override string ToString()
    {
        return $"Playlist: {this.title}\nTotal songs: {songs.Count}";
    }
}

