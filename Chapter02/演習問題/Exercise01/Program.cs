namespace Exercise01 {
    public class Program {
        static void Main(string[] args) {
            //2.1.3
            var songs = new Song[] {
                new Song("Let it be", "The Beatles", 243),
                new Song("Bridge Over Troubled Water", "Simon & Garfunkel", 293),
                new Song("Close To You", "Carpenters", 276),
                new Song("Honesty", "Billy Joel", 231),
                new Song("I Will Always Love You", "Whitney Houston", 273),
            };

            printSongs(songs);
        }

        //2.1.4
        private static void printSongs(Song[] songs) {
            // ヘッダーを表示
            Console.WriteLine("{0,-30} {1,-20} {2,5}", "Title", "Artist", "Length");

            foreach (var song in songs) {
                TimeSpan time = TimeSpan.FromSeconds(song.Length);
                string formattedTime = time.ToString(@"mm\:ss");
                Console.WriteLine
                    ("{0,-30} {1,-20} {2,5}", song.Title, song.ArtistName, formattedTime);
            }
        }
    }
}
