using System;
using VM = TodoApp.ConsoleApp.ViewModels;
using TodoApp.ConsoleApp.Framework;

namespace TodoApp.ConsoleApp.Commands
{
    class Back : Command<VM.Navigation>
    {
        public Back()
            : base("Go Back.", "b")
        { }

        protected override bool CanExecute()
        {
            return DataSource.CanGoBack();
        }

        protected override void Execute(string input)
        {
            DataSource.GoBack();
        }
    }
}
