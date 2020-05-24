using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Model
{
    public static class MusicManager
    {
        private static string MY_SONGS_PATH = "MyPlaylist";
        public static void GetAllMusics(ObservableCollection<Music> musics )
        {
            var allMusics = getMusics();
            musics.Clear();

            getMusics().ForEach(music => musics.Add(music));
        }
        public static void GetMusicsByCategory(ObservableCollection<Music> musics, MusicCategory category)
        {
            var allMusics = getMusics();
            var filteredmusics = allMusics.Where(music => music.Category == category).ToList();
            musics.Clear();
            filteredmusics.ForEach(music => musics.Add(music)); //lambda expression
        }

        private static List<Music> getMusics()
        {
            var musics = new List<Music>();
            musics.Add(new Music("Magic", MusicCategory.Brunos, "24K Magic", "Bruno Mars"));
            musics.Add(new Music("Uptown", MusicCategory.Brunos, "UpTownFunk", "Mark Ronson"));
            musics.Add(new Music("Confident", MusicCategory.Demis, "ConfidentR", "Demi Lovato"));
            musics.Add(new Music("Sorry", MusicCategory.Demis, "Souveniers", "Demis Rousso"));
            musics.Add(new Music("Hotline", MusicCategory.Drakes, "Hotline Bling", "Drake Graham"));
            musics.Add(new Music("Scorpion", MusicCategory.Drakes, "Scorpion 2018", "Drake Graham"));
            musics.Add(new Music("LoseU", MusicCategory.Selenas, "Lose You to Love Me", "Selena Gomez"));
            musics.Add(new Music("StarsDance", MusicCategory.Selenas, "Stars Dance 2013", "Selena Gomez"));

            var mySongs = getMySongs();
            musics.AddRange(mySongs);

            return musics;
        }

        private static List<Music> getMySongs()
        {
            var mySongs = new List<Music>();
            string[] fileEntries = Directory.GetFiles(MY_SONGS_PATH);
            foreach (string audioFilePath in fileEntries)
            {
                var file = TagLib.File.Create(audioFilePath);
                string artistname = string.Empty;
                if (file.Tag.Artists != null && file.Tag.Artists.Length > 0)
                    artistname = file.Tag.Artists[0];
                Music music = new Music(file.Tag.Title, MusicCategory.MyPlaylist, audioFilePath, file.Tag.Album, artistname);

                if (music.Name == null)
                {
                    music.Name = " ";
                }
                mySongs.Add(music);
            }
            return mySongs;
        }


        /* Search button - Searching based on name and category - starts here */

        public static void SearchByName(ObservableCollection<Music> songs, string queryText)
        {
            var allsongs = getMusics();
            var SearchedSongsByNameCategory =
                allsongs.Where(song => song.Name.ToUpper().Contains(queryText.ToUpper())
                || song.Album != null && song.Album.ToString().ToUpper().Contains(queryText.ToUpper())
                || song.Category.ToString().ToUpper().Contains(queryText.ToUpper())).ToList();
            songs.Clear();
            SearchedSongsByNameCategory.ForEach(song => songs.Add(song));

            /* Search button - Searching based on name and category - ends here */

        }

    }
}
