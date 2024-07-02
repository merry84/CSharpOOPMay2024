using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public class Rectangle : IDrawable
    {
        private int width;
        private int height;

        public Rectangle(int weidht, int height)
        {
            Weidht = weidht;
            Height = height;
        }

        public int Weidht { get; set; }
        public int Height { get; set; }
        public void Draw()
        {
            DrawLine(this.Weidht, '*', '*');
            for (int i = 1; i < this.Height - 1; ++i)
                DrawLine(this.Weidht, '*', ' ');
            DrawLine(this.Weidht, '*', '*');

        }
        private void DrawLine(int width, char end, char mid)
        {
            Console.Write(end);
            for (int i = 1; i < width - 1; ++i)
                Console.Write(mid);
            Console.WriteLine(end);
        }

    }
}
