using System;
using System.Collections.Generic;
using System.Linq;

public class User
{
    private string username;
    private int age;
    private List<Playlist> playlists;

    public string Username
    {
        get
        {
            return username;
        }
        set
        {
            if (value.Length < 3 || value.Length > 30)
            {
                throw new ArgumentException("Username should be between 3 and 30 characters!");
            }
            else
            {
                username = value;
            }
        }
    }


    public int Age
    {
        get
        {
            return age;
        }
        set
        {
            if (value < 13)
            {
                throw new ArgumentException("User must be at least 13 years old!");
            }
        }
    }


    public User(string username, int age)
    {
        this.username = username;
        this.age = age;
        playlists = new List<Playlist>();
    }

    public void AddPlaylist(Playlist playlist)
    {
        playlists.Add(playlist);
    }

    public Playlist GetPlaylistByTitle(string title)
    {
        return playlists.FirstOrDefault(p => p.Title == title);
    }

    public override string ToString()
    {
        return $"Username: {this.Username}\nAge: {this.Age}\nTotal Playlists: {playlists.Count}";
    }
}
