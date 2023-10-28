using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace pr2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Figure.g = this.CreateGraphics();
            Figure.bkcolor = this.BackColor;
          
        }
        List<Figure> figures = new List<Figure>();
        Random rand = new Random();
        string LastFig = "";
        bool move = false;
        Point prev_p;
        Color save_color;
        void show_all()
        {
            foreach (Figure f in figures)
            {
                f.show();
            }
        }
        void param(Figure r, Color c)
        {
            r.p = new Point(rand.Next(0, this.Width), rand.Next(panel1.Height, this.Height));
            r.size = new Size(rand.Next(50, 100), rand.Next(50, 100));
            r.color = c;
            r.show();
            figures.Add(r);
            comboBox1.Items.Add(r);
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LastFig != "")
            {
              ((Figure)comboBox1.SelectedItem).color = Color.Black;
              ((Figure)comboBox1.SelectedItem).show();
            }
            label1.Text = "S = " + ((Figure)comboBox1.SelectedItem).area();
            ((Figure)comboBox1.SelectedItem).show();
            LastFig = comboBox1.SelectedIndex.ToString();
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {

                ((Figure)comboBox1.SelectedItem).color = save_color;
                ((Figure)comboBox1.SelectedItem).show();
            }
            foreach (Figure f in figures)
            {
                if (f.p.X < e.X && f.p.X + f.size.Width > e.X && f.p.Y < e.Y && f.p.Y + f.size.Height > e.Y)
                {
                    save_color = f.color;
                    comboBox1.SelectedIndex = comboBox1.Items.IndexOf(f);
                    move = true;
                    prev_p = new Point(e.X, e.Y);
                }
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (move)
            {
                ((Figure)comboBox1.SelectedItem).color = save_color;
                ((Figure)comboBox1.SelectedItem).show();
            }
            move = false;
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move)
            {
                ((Figure)comboBox1.SelectedItem).move(e.X - prev_p.X, e.Y - prev_p.Y);
                prev_p.X = e.X;
                prev_p.Y = e.Y;
                show_all();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int switch_on = rand.Next(1, 3);
            switch (switch_on)
            {
                case 1:
                    {
                        Triangle r = new Triangle();
                        param(r, Color.Green);
                    }
                    break;
                case 2:
                    {
                        Triangle triangle = new Triangle();
                        triangle.type = "triangle";
                        triangle.size = new Size(rand.Next(50, 100), rand.Next(50, 100));
                        triangle.p = new Point(rand.Next(0, this.ClientRectangle.Width), rand.Next(panel1.Height + 10, this.ClientRectangle.Height - triangle.size.Height));
                        triangle.delta_apex = triangle.size.Width / 2;
                        triangle.color = Color.Green;
                        triangle.show();
                        figures.Add(triangle);
                        comboBox1.Items.Add(triangle);
                    }
                    break;
                case 3:
                    {
                        Triangle triangle = new Triangle();
                        triangle.type = "triangle";
                        triangle.size = new Size(rand.Next(50, 100), rand.Next(50, 100));
                        triangle.p = new Point(rand.Next(0, this.ClientRectangle.Width), rand.Next(panel1.Height + 10, this.ClientRectangle.Height - triangle.size.Height));
                        triangle.delta_apex = triangle.size.Width / 2;
                        triangle.color = Color.Green;
                        triangle.show();
                        figures.Add(triangle);
                        comboBox1.Items.Add(triangle);
                    }
                    break;
                default:
                    break;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Rectangle_ r = new Rectangle_();
            param(r, Color.Blue);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            int switch_on = rand.Next(1, 3);
            switch (switch_on)
            {
                case 1:
                    {
                        Circle r = new Circle();
                        param(r, Color.Red);
                    }
                    break;
                case 2:
                    {
                        Ellipse r = new Ellipse();
                        param(r, Color.Purple);
                    }
                    break;
                default:
                    break;
            }

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Cylinder r = new Cylinder();
            param(r, Color.Brown);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Cub r = new Cub();
            param(r, Color.Orange);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            Sphere r = new Sphere();
            param(r, Color.Aqua);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                ((Figure)comboBox1.SelectedItem).hide();
                ((Figure)comboBox1.SelectedItem).size.Width = ((Figure)comboBox1.SelectedItem).size.Width + 1;
                show_all();
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                ((Figure)comboBox1.SelectedItem).hide();
                ((Figure)comboBox1.SelectedItem).size.Width = ((Figure)comboBox1.SelectedItem).size.Width - 1;
                show_all();
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                ((Figure)comboBox1.SelectedItem).hide();
                ((Figure)comboBox1.SelectedItem).size.Height = ((Figure)comboBox1.SelectedItem).size.Height - 1;
                show_all();
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                ((Figure)comboBox1.SelectedItem).hide();
                ((Figure)comboBox1.SelectedItem).size.Height = ((Figure)comboBox1.SelectedItem).size.Height + 1;
                show_all();
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            double s = 0;
            for (int i = 0; i < figures.Count(); i++)
            {
                s += figures[i].area();
            }
            label1.Text = "S = " + s;
        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex != -1)
            {
                figures.RemoveAt(0);
                ((Figure)comboBox1.SelectedItem).hide();
                comboBox1.Items.RemoveAt(0);
            }
        }
    }  
}