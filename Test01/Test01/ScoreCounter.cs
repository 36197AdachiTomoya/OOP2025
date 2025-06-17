namespace Test01 {
    public class ScoreCounter {
        private IEnumerable<Student> _score;

        // コンストラクタ
        public ScoreCounter(string filePath) {
            _score = ReadScore(filePath);

        }

        //メソッドの概要： 
        private static IEnumerable<Student> ReadScore(string filePath) {
            var score = new List<Student>();
            var lines = File.ReadAllLines(filePath);
            
            foreach (var line in lines) {
                string[] items = line.Split(',');
                var addscore = new Student() {
                    Name = items[0],
                    Subject = items[1],
                    Score = int.Parse(items[2]),
                };
                score.Add(addscore);
            }
            return score;
        }

        //メソッドの概要： 
        public IDictionary<string, int> GetPerStudentScore() {
            var dict = new Dictionary<string, int>();
            foreach (var s in _score) {
                if (dict.ContainsKey(s.Name)) {
                    dict[s.Name] += s.Score;
                } else {
                    dict[s.Name] = s.Score;
                }
            }
            return dict;




        }
    }
}
