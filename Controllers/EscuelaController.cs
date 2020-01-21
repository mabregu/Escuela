using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nuevo.Models;

namespace Nuevo.Controllers
{
    public class EscuelaController : Controller
    {        
        public IActionResult Index()
        {
            ViewBag.parametros = "Cursos de Programación";
            var escuela = _context.Escuelas.FirstOrDefault();
            return View(escuela);
        }

        private EscuelaContext _context;
        public EscuelaController(EscuelaContext context)
        {
            _context = context;
        }
    }
}