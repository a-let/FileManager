using System;
using Microsoft.AspNetCore.Mvc;

namespace FileManager.Web.Controllers
{
    public class HomeController : Controller
    {
        public string Index() => $"File Manager - {DateTime.Now}";
    }
}