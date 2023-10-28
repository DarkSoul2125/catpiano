namespace System.Windows
{
    internal class Vector
    {
        private int v1;
        private int v2;

        public Vector(int v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        public int X { get; internal set; }
        public int Y { get; internal set; }
    }
}