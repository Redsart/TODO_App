using System;
using VM = TodoApp.ConsoleApp.ViewModels;
using TodoApp.ConsoleApp.Framework;

namespace TodoApp.ConsoleApp.Commands
{
    class Exit : Command<VM.Navigation>
    {
        public Exit()
    : base("Exit the app.", "e")
        { }

        protected override void Execute(string input)
        {
            DataSource.Goodbye();
        }
    }
}
