using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Controllers;

public class HomeController : Controller
{
    public string Index()
    {
        return "Hello World";
    }
}
