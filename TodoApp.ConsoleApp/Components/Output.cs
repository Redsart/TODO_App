using System;

namespace TodoApp.ConsoleApp.Components
{
    internal static class Output
    {
        public static void WriteTitle(string title)
        {
            Console.WriteLine("-- {0} --", title);
            Console.WriteLine();
        }

        public static void WriteLabel(string label)
        {
            Console.Write("{0}: ", label);
        }

        public static void WriteField(string name, object value)
        {
            WriteLabel(name);
            Console.WriteLine(value);
        }

        public static void WriteParagraph(string p)
        {
            Console.WriteLine(p);
        }

        public static void WriteError(string warn)
        {
            Console.WriteLine("[ {0} ]", warn);
        }
    }
}
