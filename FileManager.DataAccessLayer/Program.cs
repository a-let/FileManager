using System;
using Microsoft.Extensions.DependencyInjection;

namespace FileManager.DataAccessLayer
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = Setup.CreateServices();
        }
    }
}
