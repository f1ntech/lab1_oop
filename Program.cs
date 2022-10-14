namespace Lab1_oop
{
    class Program
    {
        static void Main() {
            var Artem = new GameAccount("Artem");
            var Anton = new GameAccount("Anton");
            Artem.WinGame(Anton, 30);
            Artem.LoseGame(Anton, 18);
            Artem.WinGame(Anton, 10);
            Artem.LoseGame(Anton, 15);

            Artem.GetStats();
            Anton.GetStats();
            CurrentGame.GetStats();

            Artem.Print();
            Anton.Print();
        }
    }
    public class GameAccount {
        public int GamesCount { get; set; }
        public string UserName { get; set; }
        public List<int> CurrentRating = new();

        public GameAccount(string UserName) {
            this.UserName = UserName;
            CurrentRating.Add(50);
            GamesCount = 0;
        }
        public void WinGame(GameAccount Enemy, int Rating) {
            RulesOfGame(this, Enemy, Rating);
        }
        public void LoseGame(GameAccount Enemy, int Rating) {
            RulesOfGame(Enemy, this, Rating);
        }
        public static void RulesOfGame(GameAccount winner, GameAccount loser, int Rating) {
            if (Rating >= 0)
            {
                if (Rating <= loser.GetRating() - 1)
                {
                    var ourgame = new CurrentGame(winner.UserName, loser.UserName, winner.UserName, Rating);
                    CurrentGame.AllOurGames.Add(ourgame);
                    winner.CurrentRating.Add(Rating);
                    winner.GamesCount++;

                    loser.CurrentRating.Add(-Rating);
                    loser.GamesCount++;
                }
                else
                {
                    Console.WriteLine($"У гравців недостатня кількість балів\n");
                }
            }
            else
            {
                Console.WriteLine("Ти не можеш грати з негативним рейтингом");
            }
        }
        public void GetStats() {
            Console.WriteLine($"Статистика гравця {UserName}");
            foreach (var point in CurrentGame.AllOurGames) {
                if (point.FirstOpponent == UserName || point.SecondOpponent == UserName) {
                    Console.WriteLine(point.GetString());
                }
            }
            Console.WriteLine();
        }
        public int GetRating() {
            int Rating = 0;
            foreach (var point in CurrentRating) {
                Rating += point;
            }
            return Rating;
        }
        public void Print() {
            Console.WriteLine($"Ім'я: {UserName} -> Рейтинг: {GetRating()}");
        }
    }
    public class CurrentGame
    {
        public static int AllNumberOfGames { get; set; } = 0;
        public static List<CurrentGame> AllOurGames = new();
        public int NumberOfGames { get; set; }
        public string FirstOpponent { get; set; }
        public string SecondOpponent { get; set; }
        public string Winner { get; set; }
        public int Rating { get; set; }

        public CurrentGame(string FirstOpponent, string SecondOpponent, string Winner, int Rating) {
            this.FirstOpponent = FirstOpponent;
            this.SecondOpponent = SecondOpponent;
            this.Winner = Winner;
            this.Rating = Rating;
            NumberOfGames = ++AllNumberOfGames;     
        }
        public static void GetStats() {
            Console.WriteLine("Загальна статистика");
            foreach (var point in AllOurGames) {
                Console.WriteLine(point.GetString());
            }
            Console.WriteLine();
        }
        public string GetString() {
            return$"Номер гри: {NumberOfGames};  {FirstOpponent,3} проти {SecondOpponent,3};  Рейтинг: {Rating,3};  Переможець - {Winner,3};"; 
        }
    }

}
   

