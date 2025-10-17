using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSharpClicker.Controllers;

[Authorize]
public class HomeController : Controller
{
    public string Index()
    {
        return "Hello World";
    }
}
