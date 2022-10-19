using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class WriterToConsole : Writer
    {
        public void Write(string text, string Format = null)
        {
            Console.WriteLine(text);
        }
    }
}
