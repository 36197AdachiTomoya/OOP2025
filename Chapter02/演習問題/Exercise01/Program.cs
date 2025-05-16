namespace Exercise01 {
    public class Program {
        static void Main(string[] args) {
            //2.1.3
            for (int i = 0; i < 5; i++) {
                Console.Write("曲名：");
                string title = Console.ReadLine();
                Console.Write("アーティスト名：");
                string artistName = Console.ReadLine();
                Console.Write("演奏時間（秒）：");
                int length = int.Parse(Console.ReadLine());
                var songs = new Song[] {
                    new Song(title,artistName,length)
                };
                printSongs(songs);
            }
        }

        //2.1.4
        private static void printSongs(Song[] songs) {
            // ヘッダーを表示
            Console.WriteLine("{0,-30} {1,-20} {2,5}", "Title", "Artist", "Time");

            foreach (var song in songs) {
                TimeSpan time = TimeSpan.FromSeconds(song.Length);
                string formattedTime = time.ToString(@"mm\:ss");
                Console.WriteLine
                    ("{0,-30} {1,-20} {2,5}", song.Title, song.ArtistName, formattedTime);
            }
        }
    }
}
