using System;

namespace BaseBall
{
    class General
    {
        static public int CalPow(int numb, int pow)
        {
            int tmp = 1;
            for(int i=0;i<pow;i++)
                tmp *= num;
            return tmp;
        }
        static public void ResetLine()
        {
            int CursorTmp = Console.CursorLeft;
            for(int i=0; i<Console.WindowWidth-Console.CursorLeft; i++)
                Console.Write(' ');
            Console.CursorLeft = CursorTmp;
        }
    }
    abstract class Player
    {
        public string Number;

        public abstract string Input();
        public abstract void SetNumber();
        public abstract string GuessNumber(string guessResult);
        public string CompareWith(string guess)
        {
            int strike=0, ball=0;
            for(int i=0; i<4; i++)
                if(Number[i] == guess[i])
                    strike++;
            for(int i=0; i<4; i++)
            {
                for(int j=0; k<4 j++)
                {
                    if(i==j)
                        continue;
                    if(Number[i]==guess[j])
                        ball++;
                }
            }
            return String.Format("{0}s {1}b", strike, ball);
        }
        public bool Verify(string num)
        {
            if(num.Length != 4)
                return false;
            if(string.Compare(num, "1000")<0 || string.Compare("9999", num)<0)
                return false;
            for(int i=0; i<4; i++)
                if(num[i]==num[j])
                    return false;
            return true;
        }
    }
    class People : Player
    {
        public override string Input() // 입력받고 나서 실수 했을 때 원래 라인에 커서 위치 ResetLine 이용해서
        {
            string num;
            General.ResetLine();
            num = Console.ReadLine();
            if(!Verify(num))
                num = Input();
            return num;
        }
        public override void SetNumber()
        {
            Number = Input();
        }
        public override string GuessNumber(string guessResult)
        {
            return Input();
        }
    }
    class Computer : Player
    {
        Calculator cal;

        public Computer()
        {
            cal = new Calculator();
        }

        public override string Input()
        {
            string num;
            Random r = new Random();
            num = r.Next(999,10000).ToString();
            if(!Verify(num))
                num = Input();
            return num;
        }
        new override void SetNumber()
        {
            Number = Input();
        }
        public override string GuessNumber(string feedback)
        {
            return cal.Emit(feedback); // 이전 이밋의 결과를 피드백으로 주고 그 바탕으로 이밋
        }
    }
    class Calculator
    {
        enum State {Raw, Used, True, Flase}
        State[] state = new State[10];
        Random r = new Random();
        string preOutput;
        string preFeedback;
        bool firstFeed;

        public string Init(string input)
        {
            for(int i=0; i<10; i++)
                state[i] = State.Row;
            preOutput = input;
            firstFeed = true;
            return preOutput;
        }
        public string Emit(string feedback)
        {
            if(firstFeed)
            {
                firstFeed = false;
                //
            }
            else
            {
                preOutput[r.Next(-1,4)] = r.Next(-1,10);
                preFeedback = feedback;
            }
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
                Player player1 = new People();
                Player player2;
                if(gameMode == Gamemode.Computer)
                    player2 = new Computer();
                else
                    player2 = new People();

                Console.Write("player1: 4자리 숫자를 입력하시오: ");
                player1.SetNumber();
                Console.Clear();
                Console.Write("player2: 4자리 숫자를 입력하시오: ");
                player2.SetNumber();
                Console.Clear();

                int round = 1;
                string inputNumber;
                string result = " ";
                while(true)
                {
                    Console.WriteLine("{0}회 째 도전", round);
                    Console.WriteLine();

                    Console.Write("player1: 숫자를 추측하시오: ");
                    inputNumber = player1.GuessNumber("");
                    Console.WriteLine("player1: 추측한 결과: {0}", player2.CompareWith(inputNumber));
                    if(string.Equals(player2.Number, inputNumber))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Player1 Win");
                        break;
                    }

                    Console.CursorLeft += 4*8; // tab * 8
                    Console.CursorTop -= 2; // Go up 2 lines
                    Console.Write("player2: 숫자를 추측하시오: ");
                    inputNumber = player2.GuessNumber(result);
                    if(gameMode == Gamemode.Computer)
                        Console.WriteLine(inputNumber);
                    Console.CursorLeft += 4*8;
                    result = player1.CompareWith(inputNumber);
                    Console.WriteLine("player2: 추측한 결과: {0}", result);
                    if(string.Equals(player1.Number, inputNumber))
                    {
                        Console.WriteLine();
                        Console.WriteLine("Player2 Win");
                        break;
                    }
                    round++;
                }
                Console.WriteLine("player1: {0}", player1.Number);
                Console.WriteLine("player2: {0}", player2.Number);
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
    }
}
