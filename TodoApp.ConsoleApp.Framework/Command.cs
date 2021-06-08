using System;
using System.Text.RegularExpressions;

namespace TodoApp.ConsoleApp.Framework
{
    public class Command
    {
        /**
         * Command template shown to the user.
         */
        protected string Display { get; set; }

        /**
         * Name & description of the command. Shown to the user.
         */
        System.Array[,] arrays;
        protected string Name { get; set; }

        /**
         * If the given input matching the command? 
         */
        protected Func<string, bool> Match { get; set; }

        /**
         * Executed when the input matched the command.
         */
        protected Action<string> Action { get; set; }

        internal Command(string name, string match, Action<string> action)
            : this(name, match.Trim(), match.Equals, action)
        { }

        internal Command(string name, string display, Regex match, Action<string> action)
            : this(name, display, match.IsMatch, action)
        { }

        internal Command(string name, string display, Func<string, bool> match, Action<string> action)
        {
            Name = name;
            Display = display;
            Match = match;
            Action = action;
        }

        internal bool IsMatch(string input)
        {
            return Match(input);
        }

        internal void Run(string input)
        {
            bool success = IsMatch(input);

            if (!success)
            {
                throw new ArgumentException("Provided input do not match the Command.", nameof(input));
            }

            Action(input);
        }

        internal void TryRun(string input, out bool success)
        {
            success = IsMatch(input);

            if (success)
            {
                Action(input);
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Name))
            {
                if (string.IsNullOrEmpty(Display))
                {
                    return "";
                }

                return Display;
            }

            return string.Format("{0,-16} - {1}", Display, Name);
        }
    }

    public abstract class Command<TVm> : Command
        where TVm : ViewModel
    {
        internal protected TVm DataSource { protected get;  set; }

        public Command(string name, string match)
            :this(name, match.Trim(), match.Equals)
        {
            Action = Execute;
        }

        public Command(string name, string display, Regex match)
            : this(name, display, match.IsMatch)
        { }

        public Command(string name, string display, Func<string, bool> match)
            : base(name, display, match, null)
        {
            Action = Execute;
        }

        abstract protected void Execute(string input);

        internal bool CanRun()
        {
            return CanExecute();
        }

        virtual protected bool CanExecute()
        {
            return true;
        }
    }
}
