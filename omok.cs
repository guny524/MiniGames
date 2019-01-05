using System;

namespace Omok
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
        public Coordinate Move(Direct direct)
        {
            Coordinate cdnt;
            if(direct == Direct.Up)
                cdnt = new Coordinate(this.Col, this.Row--);
            else if(direct == Direct.Down)
                cdnt = new Coordinate(this.Col, this.Row++);
            else if(direct == Direct.Left)
                cdnt = new Coordinate(this.Col--, this.Row);
            else if(direct == Direct.Right)
                cdnt = new Coordinate(this.Col++, this.Row);
            return cdnt;
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
        public bool IsOut(Coordinate cdnt)
        {
            if((cdnt.Col >= Col) || (cdnt.Row >= Row) || (cdnt.Col <= 0) || (cdnt.Row <= 0))
                return true;
            else
                return false;
        }
        public bool IsOut(int col, int row)
        {
            if((col >= Col) || (row >= Row) || (col <= 0) || (row <= 0))
                return true;
            else
                return false;
        }
        public bool IsOut(Stone stone)
        {
            if((tone.cdnt.Col >= Col) || (stone.cdnt.Row >= Row) || (stone.cdnt.Col <= 0) ||(stone.cdnt.Row <= 0))
                return true;
            else
                return false;
        }
    }
    enum Color {Whilte, Black}
    class stone
    {
        public Color color;
        public Coordinate cdnt;

        public Stone(Coordinate cdnt, Color color)
        {
            this.cdnt = cdnt;
            this.color = color;
        }

        public bool Equals(Stone stone)
        {
            if(stone.cdnt.Equals(this.cdnt))
                return true;
            else
                return false;
        }
        public bool IsWhite()
        {
            if(this.color == Color.White)
                return true;
            else
                return false;
        }
    }
    class Printer
    {
        public void print(Stone stone)
        {
            Console.SetCursorPosition(2*stone.cdnt.Col, stone.cdnt.Row);
            if(stone.IsWhite())
                Console.Wrtie("o");
            else
                Console.Write("*");
        }
    }
    enum Direct {Up, Down, Left, Right}
    class HighLighter
    {
        Coordinate position;
        Limit limit;

        public HighLighter(Limit limit)
        {
            this.limit = limit;
            position = new Coordinate(1,1);
        }

        public Move(Direct direct)
        {
            if(limit.IsOut(position.Move(direct)))
                return;
            else
                position = position.Move(direct);
        }
    }
    class KeyControl
    {
        Coordinate position = new Coordinate(0,0);
    }
    class MainApp
    {
        public static void Main()
        {
            Console.Clear();
            /*
            KeyControl keyctrl = new KeyControl();
            if(Console.KeyAvailable)]
                keyctrl.InputKey();
            if(keyctrl.HasValue())
            {

            }
            */
            Printer printer = new Printer();
            printer.print(new Stone(new Coordinate(0,0), Color.White));
            printer.print(new Stone(new Coordinate(1,0), Color.White));
        }
    }
}
