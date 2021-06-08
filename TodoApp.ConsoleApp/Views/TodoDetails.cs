using System;
using TodoApp.ConsoleApp.Framework;
using VM = TodoApp.ConsoleApp.ViewModels;
using Cmd = TodoApp.ConsoleApp.Commands;
using TodoApp.ConsoleApp.Components;

namespace TodoApp.ConsoleApp.Views
{
    class TodoDetails : View<VM.Todo>
    {
        public TodoDetails(VM.Todo vm)
            : base(vm) { }

        public override void Render()
        {
            Output.WriteTitle("Todo Details");

            Output.WriteField("ID", DataSource.Id);
            Output.WriteField("Name", DataSource.Name);
            Output.WriteField("Total count", DataSource.TotalCount);
        }

        public override void SetupCommands()
        {
            Commands.Message = "Where to go?";
            Commands.InvalidMessage = "Ooops... try again!";

            Commands.Add<Cmd.Back, VM.Navigation>(DataSource.Nav);
            Commands.Add<Cmd.Exit, VM.Navigation>(DataSource.Nav);
        }
    }
}
