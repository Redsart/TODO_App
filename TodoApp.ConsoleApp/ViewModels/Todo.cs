using System;
using System.Linq;
using TodoApp.ConsoleApp.Framework;
using P = TodoApp.ConsoleApp.Props;
using TodoApp.Services;
using TodoApp.Repositories.Models;

namespace TodoApp.ConsoleApp.ViewModels
{
    class Todo : ViewModel
    {
        private readonly ITodoService TodoService;

        public readonly Navigation Nav;

        public int Id = 1;
        public string Name = "My todo";
        public int TotalCount => TodoService.GetAll().Count();

        public Todo(Navigation nav, P.Todo props, ITodoService todoService)
        {
            Nav = nav;
            Id = props?.Id ?? 0;
            TodoService = todoService;
            var todos = todoService.GetAll();
            var id = todos.Select(x => x.Id).First();
            var model = todoService.GetByID(id);
            Name = model.Title;
        }
    }
}
