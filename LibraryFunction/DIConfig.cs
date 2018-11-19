using LibraryDataAgent;
using LibraryDataAgent.Interfaces;
using Autofac;
using AzureFunctions.Autofac.Configuration;

namespace LibraryFunction
{
    internal class DIConfig
    {
        public DIConfig(string functionName)
        {
            DependencyInjection.Initialize(builder =>
            {
                builder.RegisterType<BooksDataAgent>().As<IBooksDataAgent>();
            }, functionName);

        }
    }
}
