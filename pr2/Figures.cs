using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace pr2
{
    abstract public class Figure
    {
        public Point p;
        public Color color;
        public Size size;
        public static Color bkcolor;
        public static Graphics g;
        public string type = "default";
        protected abstract void draw(Color c);
        public abstract double area();
        public void hide()
        {
            draw(bkcolor);
        }
        public void show()
        {
            draw(color);
        }
        public void move(int dx, int dy)
        {
            hide();
            p.X += dx;
            p.Y += dy;
            show();
        }
    }
    public class Sphere : Figure
    {
        public override double area()
        {
            return 4 * Math.PI * (size.Width / 2) * (size.Height / 2);
        }
        protected override void draw(Color c)
        {
            Pen pen = new Pen(c, 2);
            g.DrawEllipse(pen, p.X, p.Y, size.Width, size.Height);
            g.DrawArc(pen, p.X, p.Y + size.Height / 4, size.Width, size.Height / 2, 0, 180);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            g.DrawArc(pen, p.X, p.Y + size.Height / 4, size.Width, size.Height / 2, 180, 180);
        }
    }
    public class Triangle : Figure
    {
        public int delta_apex;
        public Point A;
        public Point B;
        public Point C;

        public override double area()
        {
            A = new Point(p.X, p.Y + size.Height);
            B = new Point(p.X + size.Width, p.Y + size.Height);
            C = new Point(p.X + delta_apex, p.Y);

            double x1 = p.X;
            double y1 = p.Y + size.Height;
            double x2 = p.X + delta_apex;
            double y2 = p.Y;
            double x3 = p.X;
            double y3 = p.Y + size.Height;

            double AB = Math.Sqrt((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));
            double AC = Math.Sqrt((C.X - A.X) * (C.X - A.X) + (C.Y - A.Y) * (C.Y - A.Y));
            double BC = Math.Sqrt((C.X - B.X) * (C.X - B.X) + (C.Y - B.Y) * (C.Y - B.Y));
            double P = (AB + AC + BC) / 2;
            return Math.Sqrt(P * (P - AC) * (P - AB) * (P - BC)); 
        }
       
      
        protected override void draw(Color c)
        {
            A = new Point(p.X, p.Y + size.Height);
            B = new Point(p.X + size.Width, p.Y + size.Height);
            C = new Point(p.X + delta_apex, p.Y);

            g.DrawLine(new Pen(c), p.X, p.Y + size.Height, p.X + size.Width, p.Y + size.Height);
            g.DrawLine(new Pen(c), p.X + size.Width, p.Y + size.Height, p.X + delta_apex, p.Y);
            g.DrawLine(new Pen(c), p.X + delta_apex, p.Y, p.X, p.Y + size.Height);
        }
    }   
    public class Circle : Figure
    {
        public override double area()
        {
            return size.Width * size.Height;
        }

        protected override void draw(Color c)
        {
            g.DrawEllipse(new Pen(c, 2), p.X, p.Y, size.Width, size.Width);
        }
    }
    public class Ellipse : Figure
    {
        public override double area()
        {
            return Math.PI * (size.Width / 2) * (size.Height / 2);
        }
        protected override void draw(Color c)
        {
            g.DrawEllipse(new Pen(c, 2), p.X, p.Y, size.Width, size.Height);
        }
    }
    public class Rectangle_ : Figure
    {
        Random rand = new Random();
        int on;
        public Rectangle_()
        {
            on = rand.Next(1, 4);
        }
        public override double area()
        {
            switch (on)
            {
                case 1:
                    {
                        return size.Width * size.Height;
                    }
                case 2:
                    {
                        float dx = size.Width / 26;
                        float dy = size.Height / 11;
                        double d1 = Math.Sqrt(Math.Pow((p.X + 26 * dx) - (p.X + 1 * dx), 2) + Math.Pow((p.Y + 6 * dy) - (p.Y + 6 * dy), 2));
                        double d2 = Math.Sqrt(Math.Pow((p.X + 13.5 * dx) - (p.X + 13.5 * dx), 2) + Math.Pow((p.Y + 1 * dy) - (p.Y + 11 * dy), 2));

                        return 0.5 * (d1 * d2);
                    }
                case 3:
                    {
                        float dx = size.Width / 24;
                        float dy = size.Height / 16;
                        double a = Math.Sqrt(Math.Pow(((p.X + 1 * dx) - (p.X + 24 * dx)), 2) + Math.Pow(((p.Y + 16 * dy) - (p.Y + 16 * dy)), 2));
                        double b = Math.Sqrt(Math.Pow(((p.X + 8 * dx) - (p.X + 16 * dx)), 2) + Math.Pow(((p.Y + 1 * dy) - (p.Y + 1 * dy)), 2));
                        double h = Math.Abs((p.Y + 16 * dy) - (p.Y + 1 * dy));

                        return 0.5 * (a + b) * h;
                    }
                default:
                    return area();
            }
        }
        protected override void draw(Color c)
        {

            switch (on)
            {
                case 1:
                    {
                        g.DrawRectangle(new Pen(c, 2), p.X, p.Y, size.Width, size.Height);
                         area();
                    }
                    break;
                case 2:
                    {
                        float dx = size.Width / 26;
                        float dy = size.Height / 11;
                        Pen pen = new Pen(c, 2);
                        g.DrawLine(pen, p.X + 1 * dx, p.Y + 6 * dy, p.X + 13.5f * dx, p.Y + 1 * dy);
                        g.DrawLine(pen, p.X + 13.5f * dx, p.Y + 1 * dy, p.X + 26 * dx, p.Y + 6 * dy);
                        g.DrawLine(pen, p.X + 26 * dx, p.Y + 6 * dy, p.X + 13.5f * dx, p.Y + 11 * dy);
                        g.DrawLine(pen, p.X + 13.5f * dx, p.Y + 11 * dy, p.X + 1 * dx, p.Y + 6 * dy);
                        area();
                    }
                    break;
                case 3:
                    {
                        float dx = size.Width / 24;
                        float dy = size.Height / 16;
                        Pen pen = new Pen(c, 2);
                        g.DrawLine(pen, p.X + 1 * dx, p.Y + 16 * dy, p.X + 8 * dx, p.Y + 1 * dy);
                        g.DrawLine(pen, p.X + 8 * dx, p.Y + 1 * dy, p.X + 16 * dx, p.Y + 1 * dy);
                        g.DrawLine(pen, p.X + 16 * dx, p.Y + 1 * dy, p.X + 24 * dx, p.Y + 16 * dy);
                        g.DrawLine(pen, p.X + 24 * dx, p.Y + 16 * dy, p.X + 1 * dx, p.Y + 16 * dy);
                        area();
                    }
                    break;
                default:
                    break;
            }
        }
    }
    public class Cub : Figure
    {
        public override double area()
        {
            float dx = size.Width / 17;
            float dy = size.Height / 17;
            double a = Math.Sqrt(Math.Pow(((p.X + 1 * dx) - (p.X + 1 * dx)), 2) + Math.Pow(((p.Y + 16 * dy) - (p.Y + 3 * dy)), 2));
            double b = Math.Sqrt(Math.Pow(((p.X + 1 * dx) - (p.X + 7 * dx)), 2) + Math.Pow(((p.Y + 3 * dy) - (p.Y + 1 * dy)), 2));
            double c = Math.Sqrt(Math.Pow(((p.X + 1 * dx) - (p.X + 14 * dx)), 2) + Math.Pow(((p.Y + 3 * dy) - (p.Y + 3 * dy)), 2));
            return 2 * (a * b + b * c + a * c);
        }
        protected override void draw(Color c)
        {
            float dx = size.Width / 24;
            float dy = size.Height / 16;
            Pen pen = new Pen(c, 2);
            g.DrawLine(pen, p.X + 1 * dx, p.Y + 16 * dy, p.X + 1 * dx, p.Y + 3 * dy);
            g.DrawLine(pen, p.X + 1 * dx, p.Y + 3 * dy, p.X + 7 * dx, p.Y + 1 * dy);
            g.DrawLine(pen, p.X + 7 * dx, p.Y + 1 * dy, p.X + 20 * dx, p.Y + 1 * dy);
            g.DrawLine(pen, p.X + 20 * dx, p.Y + 1 * dy, p.X + 20 * dx, p.Y + 13 * dy);
            g.DrawLine(pen, p.X + 20 * dx, p.Y + 13 * dy, p.X + 14 * dx, p.Y + 16 * dy);
            g.DrawLine(pen, p.X + 14 * dx, p.Y + 16 * dy, p.X + 1 * dx, p.Y + 16 * dy);
            g.DrawLine(pen, p.X + 1 * dx, p.Y + 16 * dy, p.X + 7 * dx, p.Y + 13 * dy);
            g.DrawLine(pen, p.X + 7 * dx, p.Y + 13 * dy, p.X + 20 * dx, p.Y + 13 * dy);
            g.DrawLine(pen, p.X + 1 * dx, p.Y + 16 * dy, p.X + 7 * dx, p.Y + 13 * dy);
            g.DrawLine(pen, p.X + 1 * dx, p.Y + 3 * dy, p.X + 14 * dx, p.Y + 3 * dy);
            g.DrawLine(pen, p.X + 14 * dx, p.Y + 3 * dy, p.X + 20 * dx, p.Y + 1 * dy);
            g.DrawLine(pen, p.X + 14 * dx, p.Y + 3 * dy, p.X + 14 * dx, p.Y + 16 * dy);
            g.DrawLine(pen, p.X + 7 * dx, p.Y + 1 * dy, p.X + 7 * dx, p.Y + 13 * dy);
        }
    }
    public class Cylinder : Figure
    {
        public float R;
        public override double area()
        {
            R = size.Width / 2;
            return 2 * (Math.PI * R * size.Height) + 2 * (Math.PI * (R * R));
        }       
        protected override void draw(Color c)
        {
            float dx = size.Width / 28;
            float dy = size.Height / 25;
            Pen pen = new Pen(c, 2);
            R = size.Width / 2;
            g.DrawEllipse(pen, p.X, p.Y, size.Width, R);
            g.DrawEllipse(pen, p.X, p.Y + size.Height, size.Width, R);
            g.DrawLine(pen, p.X, p.Y + R / 2, p.X, p.Y + size.Height + R / 2);
            g.DrawLine(pen, p.X + size.Width, p.Y + R / 2, p.X + size.Width, p.Y + size.Height + R / 2);
        }
    }
}