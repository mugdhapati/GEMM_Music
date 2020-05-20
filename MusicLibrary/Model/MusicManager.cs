using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicLibrary.Model
{
    public static class MusicManager
    {
        public static void GetAllMusics(ObservableCollection<Music> musics )
        {
            var allMusics = getMusics();
            musics.Clear();
            allMusics.ForEach(music => musics.Add(music));
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
            musics.Add(new Music("Magic", MusicCategory.Brunos));
            musics.Add(new Music("Uptown", MusicCategory.Brunos));
            musics.Add(new Music("Confident", MusicCategory.Demis));
            musics.Add(new Music("Sorry", MusicCategory.Demis));
            musics.Add(new Music("Hotline", MusicCategory.Drakes));
            musics.Add(new Music("Scorpion", MusicCategory.Drakes));
            musics.Add(new Music("LoseU", MusicCategory.Selenas));
            musics.Add(new Music("StarsDance", MusicCategory.Selenas));

            return musics;
        }

    }
}
