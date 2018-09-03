using Microsoft.AspNetCore.Mvc;

using System;

namespace FileManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public string Index() => $"File Manager - {DateTime.Now}";
    }
}