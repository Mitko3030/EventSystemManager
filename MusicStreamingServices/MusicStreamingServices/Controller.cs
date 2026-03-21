using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Controller
{
    private Dictionary<string, User> users;

    public Controller()
    {
        users = new Dictionary<string, User>();
    }

    public string AddUser(List<string> args)
    {
        string username = args[0];
        int age = int.Parse(args[1]);
        User user = new User(username, age);
        users[username] = user;
        return $"Created User {username}!";
    }

    public string AddPlaylist(List<string> args)
    {
        string username = args[0];
        string title = args[1];
        User user = users[username];
        Playlist playlist = new Playlist(title);
        user.AddPlaylist(playlist);
        return $"Created Playlist {title} for User {username}!";
    }

    public string AddSongToPlaylist(List<string> args)
    {
        string username = args[0];
        string playlistTitle = args[1];
        string songTitle = args[2];
        int duration = int.Parse(args[3]);
        string artist = args[4];
        string genre = args[5];
        string type = args[6];

        User user = users[username];
        Playlist playlist = user.GetPlaylistByTitle(playlistTitle);

        Song song;

        if (type.ToLower() == "single")
        {
            DateTime releaseDate = DateTime.Parse(args[7]);
            song = new Single(songTitle, duration, artist, genre, releaseDate);
        }
        else if (type.ToLower() == "albumsong")
        {
            string albumName = args[7];
            song = new AlbumSong(songTitle, duration, artist, genre, albumName);
        }
        else
        {
            throw new ArgumentException("Invalid song type!");
        }

        playlist.AddSong(song);
        return $"Added song {songTitle} to Playlist {playlistTitle}!";
    }

    public string GetTotalDurationOfPlaylist(List<string> args)
    {
        string username = args[0];
        string playlistTitle = args[1];

        User user = users[username];
        Playlist playlist = user.GetPlaylistByTitle(playlistTitle);

        int total = playlist.TotalDuration();
        return $"Total duration of {playlistTitle}: {total} seconds";
    }

    public string GetSongsByArtistFromPlaylist(List<string> args)
    {
        string username = args[0];
        string playlistTitle = args[1];
        string artist = args[2];

        User user = users[username];
        Playlist playlist = user.GetPlaylistByTitle(playlistTitle);

        var songs = playlist.GetSongsByArtist(artist);
        if (songs.Count == 0)
        {
            return "No songs found.";
        }

        StringBuilder sb = new StringBuilder();
        foreach (var song in songs)
        {
            sb.AppendLine(song.ToString());
        }

        return sb.ToString().TrimEnd();
    }

    public string GetSongsByGenreFromPlaylist(List<string> args)
    {
        string username = args[0];
        string playlistTitle = args[1];
        string genre = args[2];

        User user = users[username];
        Playlist playlist = user.GetPlaylistByTitle(playlistTitle);

        var songs = playlist.GetSongsByGenre(genre);
        if (songs.Count == 0)
        {
            return "No songs found.";
        }

        StringBuilder sb = new StringBuilder();
        foreach (var song in songs)
        {
            sb.AppendLine(song.ToString());
        }

        return sb.ToString().TrimEnd();
    }

    public string GetSongsAboveDurationFromPlaylist(List<string> args)
    {
        string username = args[0];
        string playlistTitle = args[1];
        int duration = int.Parse(args[2]);

        User user = users[username];
        Playlist playlist = user.GetPlaylistByTitle(playlistTitle);

        var songs = playlist.GetSongsAboveDuration(duration);
        if (songs.Count == 0)
        {
            return "No songs found.";
        }

        StringBuilder sb = new StringBuilder();
        foreach (var song in songs)
        {
            sb.AppendLine(song.ToString());
        }

        return sb.ToString().TrimEnd();
    }
}
