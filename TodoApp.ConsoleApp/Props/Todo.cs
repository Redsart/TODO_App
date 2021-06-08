using System;
using TodoApp.ConsoleApp.Framework;

namespace TodoApp.ConsoleApp.Props
{
    class Todo : IProps
    {
        public int? Id;

        public Todo()
        {
            Id = null;
        }

        public Todo(int id)
        {
            Id = id;
        }
    }
}
