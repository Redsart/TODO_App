using System;
using VM = TodoApp.ConsoleApp.ViewModels;
using TodoApp.ConsoleApp.Framework;
using TodoApp.ConsoleApp.Components;

namespace TodoApp.ConsoleApp.Views
{
    class Goodbye : View<VM.Goodbye>
    {
        public Goodbye(VM.Goodbye vm)
    : base(vm)
        { }

        public override void Render()
        {
            Output.WriteTitle("Todo app");

            Output.WriteParagraph("See you soon!");
            Input.ReadChar();
            DataSource.Exit();
        }

        public override void SetupCommands()
        { }
    }
}
