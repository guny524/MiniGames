using System;
using System.Collections.Generic;
using System.IO;

namespace Snake
{
    class Coordinate
    {
        public int Col{get; private set;}
        public int Row{get; private set;}
        public Coordinate(int col, int row)
        {
            this.Col = col;
            this.Row = row;
        }
        public bool Equals(Coordinate cdnt)
        {
            if(this.Col==cdnt.Col && this.Row==cdnt.Row)
                return true;
            else
                return false;
        }
        protected Coordinate(){}
        protected void SetCdnt(Coordinate cdnt)
        {
            Col = cdnt.Col;
            Row = cdnt.Row;
        }
    }
    class Limit
    {
        public int Col{get; private set;}
        public int Row{get; private set;}
        public Limit(int col, int row)
        {
            this.Col = col;
            this.Row = row;
        }
        public bool IsLimitOut(Coordinate cdnt)
        {
            if((cdnt.Col >= Col) || (cdnt.Row >= Row) || (cdnt.Col <= 0) || (cdnt.Row <= 0))
                return true;
            else
                return false;
        }
    }
    class Map : List<Coordinate>
    {
        Limit limit;
        public Map(int col, int row)
        {
            limit = new Limit(col,row);
            Init();
        }
        void Init()
        {
            for(int i=0; i<=limit.Col; i++)
                this.Add(new Coordinate(i,0));
            for(int i=0; i<=limit.Col; i++)
                this.Add(new Coordinate(i,limit.Row));
            for(int i=0; i<=limit.Row; i++)
                this.Add(new Coordinate(0,i));
            for(int i=0; i<=limit.Row; i++)
                this.Add(new Coordinate(limit.Col,i));
        }
    }
    class Printer
    {
        List<Coordinate> list;
        List<Coordinate> preList;
        public Printer()
        {
            Console.Clear();
            list = new List<Coordinate>();
            preList = list;
        }
        public void Add(Coordinate cdnt)
        {
            if(preList.Contains(cdnt))
                return;
            else
            {
                list.Add(cdnt);
                preList = list;
                Console.SetCursorPosition(2*cdnt.Col, cdnt.Row); // when print sqaure it takes double location // 네모 프린트하면 2칸 먹음 아예 좌표를 2배로 설정
                Console.Write("ㅁ");
            }
        }
        public void Delete(Coordinate cdnt)
        {
            if(!preList.Contains(cdnt))
                return;
            else
            {
                list.RemoveAt(list.IndexOf(cdnt)); // Insfect first, It can overlap in list. // list 에서 중복될 수 있어서 검사 먼저함
                preList = list;
                Console.SetCursorPosition(2*cdnt.Col, cdnt.Row); // when print sqaure it takes double location // 네모 프린트하면 2칸 먹음 아예 좌표를 2배로 설정
                Console.Write("  ");
            }
        }
        public void Add(List<Coordinate> list)
        {
            foreach(Coordinate cdnt in list)
            Add(cdnt);
        }
        public void Delete(List<Coordinate> list)
        {
            foreach(Coordinate cdnt in list)
            Delete(cdnt);
        }
    }
    class Snake : List<Coordinate>
    {
        Limit limit;
        public Snake(int col, int row) : base()
        {
            limit = new Limit(col,row);
            Random r = new Random();
            this.Add(new Coordinate(r.Next(1,limit.Col-1), r.Next(1, limit.Row-1)));
        }

        public Coordinate TmpMaker(ConsoleKeyInfo cki)
        {
            Coordinate tmp = null;
            switch(cki.Key)
            {
                case ConsoleKey.LeftArrow:
                    tmp = new Coordinate(this[0].Col-1, this[0].Row);
                    break;
                case ConsoleKey.RightArrow:
                    tmp = new Coordinate(this[0].Col+1, this[0].Row);
                    break;
                case ConsoleKey.UpArrow:
                    tmp = new Coordinate(this[0].Col, this[0].Row-1);
                    break;
                case ConsoleKey.DownArrow:
                    tmp = new Coordinate(this[0].Col, this[0].Row+1);
                    break;
            }
            return tmp;
        }
        public bool IsCollide(Coordinate tmp) // Collide With Feed
        {
            if(this[0].Equals(tmp))
            return true;
            return false;
        }
        public bool IsCrash() // Crash by itself
        {
            if(limit.IsLimitOut(this[0]))
                return true;
            if(this.Count >= 2)
                for(int i=1; i<this.Count; i++)
            if(this[0].Equals(this[i]))
                return true;
            return false;
        }
        public void Move(ConsoleKeyInfo cki)
        {
            Coordinate tmp = TmpMaker(cki);
            for(int i=this.Count-1; i>0; i--)
            this[i] = this[i-1];
            this[0] = tmp;
        }
        public void Grow(ConsoleKeyInfo cki)
        {
            this.Add(this[this.Count-1]);
            Move(cki);
        }
    }
    class Feed : Coordinate
    {
        Limit limit;
        public Feed(int col, int row, List<Coordinate> avoid)
        {
            limit = new Limit(col,row);
            base.SetCdnt(Generate(avoid));
        }

        Coordinate Generate(List<Coordinate> avoid)
        {
            Random r = new Random();
            Coordinate cdnt = new Coordinate(r.Next(1,limit.Col-1), r.Next(1,limit.Row-1));
            foreach(Coordinate coordinate in avoid)
            if(coordinate.Equals(cdnt))
                cdnt = Generate(avoid);
            return cdnt;
        }
    }
    class DelayControl
    {
        int DelayTime;
        int[] DelayArr = {50, 70, 100, 130, 150, 200, 300, 400, 500};
        int DelayLevel;

        public DelayControl()
        {
            DelayLevel = DelayArr.Length-1;
            DelayTime = DelayArr[DelayLevel];
        }

        public DateTime Delay()
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0,0,0,0,DelayTime);
            DateTime AfterWards = ThisMoment.Add(duration);
            while(AfterWards >= ThisMoment)
            {
                // System.Windows.Forms.Application.DoEvents(); // do not work in ubuntu
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }
        public void TimeDown()
        {
            DelayLevel -= 1;
            if(DelayLevel < 0)
            DelayLevel = 0;
            DelayTime = DelayArr[DelayLevel];
        }
    }
    class KeyControl
    {
        ConsoleKeyInfo? cki = null;

        public void InputKey()
        {
            cki = Console.ReadKey();
            while(Console.KeyAvailable)
            Console.ReadKey(false);
        }
        public bool HasValue()
        {
            if(cki.HasValue)
                return true;
            else
                return false;
        }
        public ConsoleKeyInfo GetKey()
        {
            return cki.Value;
        }
    }
    class ScoreCalculator
    {
        int LimitRow;
        int Score;

        public ScoreCalculator(int LimitRow)
        {
            this.LimitRow = LimitRow;
            Score = 0;
        }

        public void PrintScore()
        {
            Console.SetCursorPosition(0, LimitRow+1);
            Console.WriteLine("Score:"+Score);
        }
        public void EatFeed()
        {
            Score += 100;
        }
        public void EatItem()
        {
            Score += 300;
        }
    }
    class MainApp
    {
        public static void Main()
        {
            int MaxCol = 39;
            int MaxRow = 22;

            Map map = new Map(MaxCol, MaxRow);
            Printer printer = new Printer();
            Snake snake = new Snake(MaxCol, MaxRow);
            Feed feed = new Feed(MaxCol, MaxRow, snake);

            DelayControl dlctrl = new DelayControl();
            KeyControl keyctrl = new KeyControl();
            ScoreCalculator scoreCal = new ScoreCalculator(MaxRow);

            printer.Add(map);
            printer.Add(snake);
            printer.Add(feed);

            while(true)
            {
                scoreCal.PrintScore();
                dlctrl.Delay();
                if(Console.KeyAvailable)
                    keyctrl.InputKey();
                if(keyctrl.HasValue())
                {
                    if(snake.IsCollide(feed))
                    {
                        scoreCal.EatFeed();
                        printer.Delete(feed);
                        feed = new Feed(MaxCol, MaxRow, snake);
                        printer.Add(feed);
                        printer.Delete(snake);
                        snake.Grow(keyctrl.GetKey());
                        printer.Add(snake);
                        dlctrl.TimeDown();
                        continue;
                    }

                if(snake.IsCrash())
                    break;

                printer.Delete(snake);
                snake.Move(keyctrl.GetKey());
                printer.Add(snake);
                }
            }
            // Iem 클래스 만들어서 T라는 아이템 먹으면 시간 느려짐
            // 한번 더 먹으면 4칸 씩 늘어나는거
            // 껌벅거리는거 없애기
            // 키 입력하면 20점 정도 깍기
            // Console.Clear();
            Console.WriteLine("Game Over");
        }
    }
}
