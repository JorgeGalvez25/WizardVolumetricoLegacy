using System;

namespace bits
{
    class Program
    {
        static void Main(string[] args)
        {
            int bits = IntPtr.Size * 8;
            Console.Write(bits.ToString());
        }
    }
}
