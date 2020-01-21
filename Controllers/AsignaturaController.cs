using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Nuevo.Models;

namespace Nuevo.Controllers
{
    public class AsignaturaController : Controller
    {
        [Route("Asignatura/Index")]
        [Route("Asignatura/Index/{asignaturaId}")]
        public IActionResult Index(string asignaturaId)
        {
            if(!string.IsNullOrWhiteSpace(asignaturaId))
            {
                var asignatura = from asig in _context.Asignaturas
                                where asig.Id == asignaturaId
                                select asig;

                return View(asignatura.SingleOrDefault());
            }
            else
            {
                return View("Asignaturas", _context.Asignaturas);
            }

            
        }
        public IActionResult Listar()
        {
            ViewBag.parametros = "Cursos de Programación";
            ViewBag.Fecha = DateTime.Now;

            return View("Asignaturas", _context.Asignaturas);
        }

        private EscuelaContext _context;
        public AsignaturaController(EscuelaContext context)
        {
            _context = context;
        }
    }
}