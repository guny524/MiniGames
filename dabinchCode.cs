using System;

namespace DabinchCode
{
    class Card
    {
        public int Number{get;set;}
        public enum Color {Black, White} color;
        public bool Joker;
        public enum Coin {Head, Tail} coin;
        public Card()
        {

        }
    }
    class Deck
    {
        public void Init()
        {

        }
    }
    class MainApp()
    {
        enum GameMode {Player, Computer, How}
        public static void Init(out gamemode)
        {
            Console.ConsolekeyInfo key;
            Console.WriteLine("1. 1인 게임");
            Console.WriteLine("2. 2인 게임");
            Console.WriteLine("3. 게임 설명");
            key = Console.ReadKey(true);
            switch(key.Key)
            {
                case ConsoleKey.D1:
                    gamemode = Gamemode.Player;
                    break;
                case ConsoleKey.D2:
                    gamemode = Gamemode.Computer;
                    break;
                case ConsoleKey.D3:
                    gamemode = Gamemode.How;
                    break;
            }
        }
        public static void How()
        {
            Console.WriteLine("게임방법");
            Console.WrtieLine("1) 0~11카드와 조커 각각 13개씩 검은색, 흰색 카드가 있다.");
            Console.WriteLine("2) 2명의 플레이어는 4장씩 카드를 뽑고 왼쪽부터 작은숫자가 오도록 정렬한다. 이 때 같은 숫자가 나오면 검은색을 왼쪽에 둔다. 상대방에게는 자신의 숫자가 안 보인다.");
            Console.WriteLine("3) 플레이어는 순서를 정한다. 자기 차례가 오면 카드를 한 장 뽑고 정렬한다. 자기 차례가 끝날 때 상대방의 카드 중 하나를 추측한다. 추측이 성공하면 상대방이 그 카드를 뒤집고, 실패하면 내가뽑은 카드를 뒤집는다.");
            Console.WriteLine("4) 추측이 성공했을 땐 카드를 한 번 더 추측하거나 상대방에게 차례를 넘긴다. 실패했을 때도 차례를 넘긴다.");
        }
        public static void Computer()
        {
            bool gmaefinish = false;
            Console.WrtieLine("Random Drawing 4 Cards...");
            Deck player = new Deck();
            Deck Computer = new Deck();
            player.Init();
            computer.Init();
            int round = 1;
            while(!gamefinish)
            {
                Console.WriteLine(round+"째 판");

            }
        }
        public static void Main()
        {
            Gamemode gamemode;
            Init(out gamemode);
            switch(gamemode)
            {
                case Gamemode.Player:

                    break;
                case Gamemode.Computer:
                    Computer();
                    break;
                case Gamemode.How:

                    break;
            }
        }
    }
}
