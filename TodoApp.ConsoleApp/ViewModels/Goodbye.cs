using System;
using TodoApp.ConsoleApp.Framework;
using Microsoft.Extensions.Hosting;

namespace TodoApp.ConsoleApp.ViewModels
{
    class Goodbye : ViewModel
    {
        IHostApplicationLifetime AppLifetime;

        public Goodbye(IHostApplicationLifetime appLifetime)
        {
            AppLifetime = appLifetime;
        }

        public void Exit()
        {

            AppLifetime.StopApplication();
        }
    }
}
