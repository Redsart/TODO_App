using System;
using TodoApp.Repositories.Interfaces;
using TodoApp.Services;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using TodoApp.ConsoleApp.Framework;
using Microsoft.Extensions.DependencyInjection;
using VM = TodoApp.ConsoleApp.ViewModels;
using Cmd = TodoApp.ConsoleApp.Commands;
using P = TodoApp.ConsoleApp.Props;
using Xml = TodoApp.Repositories.XmlRepository;

namespace TodoApp.ConsoleApp
{
    class Program
    {
        class AppArgs
        {
            public string Xml = @"..\..\_data\todos.xml";
        }

        static async Task Main(string[] args)
        {
            var appArgs = ProcessArgs(args);

            using (IHost host = CreateHostBuilder(appArgs).Build())
            {
                await host.RunAsync();
            }
        }

        static IHostBuilder CreateHostBuilder(AppArgs args)
        {
            return Host.CreateDefaultBuilder()
                .UseTodoFramework()
                .ConfigureServices((_, services) =>
                {
                    services
                        // Views
                        .AddView<Views.Home>(true)
                        .AddView<Views.TodoDetails>()
                        .AddView<Views.Goodbye>()
                        // Props
                        .AddProps<P.Todo>()
                        // Commands
                        .AddCommand<Cmd.Back>()
                        .AddCommand<Cmd.Exit>()
                        // View Models
                        .AddViewModel<VM.Navigation>()
                        .AddViewModel<VM.Todo>()
                        .AddViewModel<VM.Goodbye>()
                        // Services
                        .AddSingleton<ITodoService, TodoService>()
                        // Repositories
                        .AddSingleton<ITodoRepository, Xml.TodoRepository>()
                        .AddSingleton<Xml.Utils.IXmlContext>((s) => new Xml.Utils.XmlContext(args.Xml));
                })
                ;
        }

        static AppArgs ProcessArgs(string[] args)
        {
            var result = new AppArgs();

            for (int i = 0; i < args.Length; i++)
            {
                var name = args[i];
                if (name == ".xml" && i + 1 < args.Length)
                {
                    var value = args[i + 1];
                    if (!string.IsNullOrEmpty(value))
                    {
                        result.Xml = value.Trim();
                    }
                }
            }

            return result;
        }
    }
}
