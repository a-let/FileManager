using Microsoft.AspNetCore.Mvc;

using System;
using System.Reflection;

namespace FileManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public string Index() => $"File Manager - {Environment.MachineName} - {Assembly.GetExecutingAssembly().GetName().Version}";
    }
}