using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class WriterToFile : Writer
    {
        public void Write(string text, string Format = null)
        {
            File.WriteAllText($"out.{Format}", text);
        }
    }
}
