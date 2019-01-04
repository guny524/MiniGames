using System;

namespace DotZ
{
    class MainApp
    {
        public static void Main()
        {
            Map map = new Map();
            Plane plane = new Plane('#');
            ConsoleKeyInfo key;
            do
            {
                int x = plane.GetX();
                int y = plane.GetY();
                plane.Visualize();
                key = Console.ReadKey(true);
                switch(key.Key)
            {
            case ConsoleKey.LeftArrow:
                if(x==1)
                    break;
                x = x - 1;
                break;
            case ConsoleKey.RightArrow:
                if(x==Map.X_MAX-1)
                    break;
                x = x + 1;
                break;
            case ConsoleKey.UpArrow:
                if(y==1)
                    break;
                y = y - 1;
                break;
            case ConsoleKey.DownArrow:
                if(y==Map.Y_MAX-1)
                    break;
                y = y + 1;
                break;
            case ConsoleKey.Q:
                Console.Clear();
                return;
            case ConsoleKey.Z:
                plane.Fire();
                break;
            }
            plane.Erase();
            plane.SetX(x);
            plane.SetY(y);
            } while (true);
        }
    }
    class Map
    {
        public const int X_MAX = 79;
        public const int Y_MAX = 24;
        public Map()
        {
            this.Visualize();
        }
        public void Visualize()
        {
            Console.Clear();
            for(int i=0; i<X_MAX; i++)
                Draw.Print('-',i,0);
            for(int i=0; i<X_MAX; i++)
                Draw.Print('-',i,Y_MAX);
            for(int i=0; i<Y_MAX; i++)
                Draw.Print('I',0,i);
            for(int i=0; i<Y_MAX; i++)
                Draw.Print('I',X_MAX,i);
        }
    }
    class Plane
    {
        int x=1;
        int y=1;
        char c;
        public Plane(char c)
        {
            this.c = c;
        }
        public int GetX()
        {
            return x;
        }
        public int GetY()
        {
            return y;
        }
        public char GetC()
        {
            return this.c;
        }
        public void SetX(int x)
        {
            this.x = x;
        }
        public void SetY(int y)
        {
            this.y = y;
        }
        public void SetC(char c)
        {
            this.c = c;
        }
        public void Erase()
        {
            Draw.Erase(this.x, this.y);
        }
        public void Visualize()
        {
            Draw.Print(this.c, this.x, this.y);
        }
        public void Fire()
        {
            int lancher = this.x;
            do
            {
                lancher++;
                Draw.Print('-', lancher, y);
                Time.Delay(5);
                Draw.Erase(lancher, y);
            } while (lancher < Map.X_MAX-1);
        }
    }
    class Draw
    {
        public static void Print(char c, int x, int y)
        {
            Console.SetCursorPosition(x,y);
            Console.Write(c);
        }
        public static void Erase(int x, int y)
        {
            Console.SetCursorPosition(x,y);
            Console.Write(" ");
        }
    }
    class Time
    {
        public static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0,0,0,0,MS);
            DateTime AfterWards = ThisMoment.Add(duration);
            while(AfterWards >= ThisMoment)
            {
                // System.Windows.Forms.Application.DoEvents(); // Does not work in ubuntu
                ThisMoment = DateTime.Now;
            }
            return DateTime.Now;
        }
    }
}
