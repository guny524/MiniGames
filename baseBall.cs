using System;

namespace BaseBall
{
    class Math
    {
        static public int CalPower(int number, int power)
        {
            int tmp = 1;
            for(int i=0;i<power;i++)
                tmp *= number;
            return tmp;
        }
    }
    class Player
    {
        public int Number{get;set;}

        public int[] GetArr(int number)
        {
            string str = number.ToString();
            int length = str.Length;
            int[] array = new int[length];
            int divisor = Math.CalPower(10, length-1);
            for(int i=0; i<length; i++)
            {
                array[i] = number/divisor;
                number %= divisor;
                divisor /= 10;
            }
            return array;
        }
        public string GetScore(int guessNumber)
        {
            int strike = 0, ball = 0;
            int[] myArr = this.GetArr(this.Number);
            int[] guessArr = this.GetArr(guessNumber);
            for(int i=0; i<4; i++)
                if(myArr[i] == guessArr[i])
                    strike++;
            for(int i=0; i<4; i++)
            {
                for(int j=0; j<4; j++)
                {
                    if(i==j)
                        continue;
                    if(myArr[i] == guessArr[j])
                        ball++;
                }
            }
            return String.Format("{0}s {1}b", strike, ball);
        }
        public bool IsCorrect(int guessNumber)
        {
            if(this.GetScore(guessNumber) == "4s 0b")
                return true;
            else
                return false;
        }
        public bool Verify(int num)
        {
            if(num<1000 || num>9999)
                return false;
            int[] Arr = GetArr(num);
            for(int i=0; i<4; i++)
                for(int j=i+1; j<4; j++)
                    if(Arr[i]==Arr[j])
                        return false;
            return true;
        }
        public void SetNumber()
        {
            do
            {
                string input;
                int CursorTmp = Console.CursorLeft;
                for(int i=0; i<Console.WindowWidth-Console.CursorLeft; i++)
                    Console.Write(' ');
                Console.CursorLeft = CursorTmp;
                input = Console.ReadLine();
                this.Number = int.Parse(input);
            } while (!this.Verify(this.Number));
        }
    }
    class Computer : Player
    {
        new public void SetNumber()
        {
            Random r = new Random();
            this.Number = r.Next(999, 10000);
            if(!this.Verify(this.Number))
                this.SetNumber();
        }
        public int GuessNumber()
        {
            Random r = new Random();
            int num;
            do
            {
                num = r.Next(999, 10000);
            } while (!this.Verify(num));
            return num;
        }
    }
    class MainApp
    {
        enum Gamemode {Computer, Player}
        public static void Main(string[] args)
        {
            bool gameQuit = false;
            while(!gameQuit)
            {
                Console.Clear();
                Console.WriteLine("Updating AI...");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1. Fight with Computer");
                Console.WriteLine("2. Fight with 2P");

                Gamemode gameMode;
                while(true)
                {
                    string read = Console.ReadLine();
                    if(read=="1")
                    {
                        gameMode = Gamemode.Computer;
                        break;
                    }
                    else if(read=="2")
                    {
                        gameMode = Gamemode.Player;
                        break;
                    }
                }
                Console.Clear();
                //---------------------------------------------
                switch(gameMode)
                {
                    case Gamemode.Computer:
                        Computer();
                        break;
                    case Gamemode.Player:
                        Competition();
                        break;
                }
                //---------------------------------------------
                Console.WriteLine("Q: Quit, R: Restart");
                while(true)
                {
                    string read = Console.ReadLine();
                    if(read.ToLower() == "r")
                        break;
                    else if(read.ToLower() == "q")
                    {
                        gameQuit = true;
                        break;
                    }
                }
            }
            return;
        }
        static void Computer()
        {
            Player player = new Player();
            Console.Write("4자리 숫자를 입력하시오: ");
            player.SetNumber();

            Computer computer = new Computer();
            computer.SetNumber();
            Console.WriteLine("컴퓨터가 숫자를 정했습니다.");

            int round = 1;
            while(true)
            {
                Console.WriteLine("{0}회 째 도전", round);
                Console.WriteLine();

                Player guess = new Player();
                Console.Write("숫자를 추측하시오: ");
                guess.SetNumber();
                Console.WriteLine("내가 추측한 결과: {0}", computer.GetScore(guess.Number));
                if(computer.IsCorrect(guess.Number))
                {
                    Console.WriteLine();
                    Console.WriteLine("Player Win");
                    break;
                }

                Console.CursorLeft += 8*4; // tab * 8
                guess.Number = computer.GuessNumber();
                Console.CursorTop -= 2; // Go up 2 lines
                Console.WriteLine("컴퓨터 추측 숫자: {0}",guess.Number);
                Console.CursorLeft += 8*4;
                Console.Write("컴퓨터 추측 결과: ");
                Console.WriteLine(player.GetScore(guess.Number));
                Console.WriteLine();
                if(player.IsCorrect(guess.Number))
                {
                    Console.WriteLine();
                    Console.WriteLine("Computer Win");
                    break;
                }
                round++;
            }
            Console.WriteLine("Player: {0}", player.Number);
            Console.WriteLine("Computer: {0}", computer.Number);
        }
        static void Competition()
        {
            Player player1 = new Player();
            Console.Write("player1: 4자리 숫자를 입력하시오: ");
            player1.SetNumber();

            Console.Clear();

            Player player2 = new Player();
            Console.Write("player2: 4자리 숫자를 입력하시오: ");
            player2.SetNumber();

            Console.Clear();

            int round = 1;
            while(true)
            {
                Console.WriteLine("{0}회 째 도전", round);
                Console.WriteLine();

                Player test = new Player();
                Console.Write("Player1: 숫자를 추측 하시오: ");
                test.SetNumber();
                Console.WriteLine("Player1: 추측한 결과: {0}", player2.GetScore(test.Number));
                if(player2.IsCorrect(test.Number))
                {
                    Console.WriteLine();
                    Console.WriteLine("Player1 Win");
                    break;
                }

                Console.CursorLeft += 8*4; // tab * 8
                Console.CursorTop -= 2;
                Console.Write("Player2: 숫자 추측 하시오: ");
                test.SetNumber();
                Console.CursorLeft += 8*4;
                Console.WriteLine("Player2: 추측한 결과: {0}", player1.GetScore(test.Number));
                if(player1.IsCorrect(test.Number))
                {
                    Console.WriteLine();
                    Console.WriteLine("Player2 Win");
                    break;
                }
                round++;
            }
            Console.WriteLine("Player1: {0}", player1.Number);
            Console.WriteLine("Player2: {0}", player2.Number);
        }
    }
}
