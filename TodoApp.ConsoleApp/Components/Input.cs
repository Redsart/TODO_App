using System;

namespace TodoApp.ConsoleApp.Components
{
    public static class Input
    {
        private delegate bool TryParse<T>(string input, out T result);

        private static T Read<T>(string label, TryParse<T> tryParse, string errorMsg)
        {
            bool isValid = false;
            T result = default;

            while (!isValid)
            {
                var input = ReadText(label);
                isValid = tryParse(input, out result);

                if (!isValid)
                {
                    Output.WriteError(errorMsg);
                }
            }

            return result;
        }

        public static char ReadChar()
        {
            return Console.ReadKey().KeyChar;
        }

        public static string ReadText(string label = "")
        {
            if (!string.IsNullOrEmpty(label))
            {
                Output.WriteLabel(label);
            }

            return Console.ReadLine();
        }


        public static int ReadInt(string label = "", string msg = "Please, enter a valid integer number!")
        {
            return Read<int>(label, int.TryParse, msg);
        }

        public static double ReadDouble(string label = "", string msg = "Please, enter a valid integer number!", double min = double.NegativeInfinity, double max = double.PositiveInfinity)
        {
            bool tryParse(string input, out double number)
            {
                var parsed = double.TryParse(input, out number);
                return parsed && number >= min && number <= max;
            }

            return Read<double>(label, tryParse, msg);
        }
    }
}
