﻿using TodoApp.ConsoleApp.Framework.Services;
using P = TodoApp.ConsoleApp.Test.Props;

namespace TodoApp.ConsoleApp.Test.ViewModels
{
    public class Todo : Navigation
    {
        public int Id = 1;
        public string Name = "My todo";

        public Todo(Router router, P.Todo props)
            : base(router)
        {
            Id = props?.Id ?? 0;
        }

        public void Update(string name)
        {
            Name = name;

            NotifyPropertyChanged();
        }
    }
}