using System;
using TodoApp.ConsoleApp.Framework;
using V = TodoApp.ConsoleApp.Views;
using P = TodoApp.ConsoleApp.Props;

namespace TodoApp.ConsoleApp.ViewModels
{
    public class Navigation : ViewModel
    {
        protected readonly IRouter Router;

        public Navigation(IRouter router)
        {
            Router = router;
        }

        public void OpenHome()
        {
            Router.Open<V.Home>();
        }
        public void OpenTodoDetails(int id)
        {
            Router.Open<V.TodoDetails, P.Todo>(new P.Todo(id));
        }

        public void Goodbye()
        {
            Router.Open<V.Goodbye>();
        }

        public bool CanGoBack()
        {
            return Router.CanGoTo(-1);
        }

        public void GoBack()
        {
            Router.GoTo(-1);
        }
    }
}
