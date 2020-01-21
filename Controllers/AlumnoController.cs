using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nuevo.Models;

namespace Nuevo.Controllers
{
    public class AlumnoController : Controller
    {
        public IActionResult Index(string id)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                var alumno = from alum in _context.Alumnos
                                where alum.Id == id
                                select alum;

                return View(alumno.SingleOrDefault());
            }
            else
            {
                return View("Alumnos", _context.Alumnos);
            }

            
        }
        public IActionResult Listar()
        {
            ViewBag.parametros = "Cursos de Programación";
            ViewBag.Fecha = DateTime.Now;

            return View("Alumnos", _context.Alumnos);
        }        
        private EscuelaContext _context;
        public AlumnoController(EscuelaContext context)
        {
            _context = context;
        }
    }
}