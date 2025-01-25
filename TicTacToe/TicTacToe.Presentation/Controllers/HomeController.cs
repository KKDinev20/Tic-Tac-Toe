using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Presentation.Models;

namespace TicTacToe.Presentation.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return this.View();
    }
}