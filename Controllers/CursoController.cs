using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nuevo.Models;

namespace Nuevo.Controllers
{
    public class CursoController : Controller
    {
        public IActionResult Index(string id)
        {
            if(!string.IsNullOrWhiteSpace(id))
            {
                var curso = from cur in _context.Cursos
                                where cur.Id == id
                                select cur;

                return View(curso.SingleOrDefault());
            }
            else
            {
                return View("Cursos", _context.Cursos);
            }

            
        }
        public IActionResult Listar()
        {
            ViewBag.parametros = "Cursos de Programación";
            ViewBag.Fecha = DateTime.Now;

            return View("Cursos", _context.Cursos);
        }
        public IActionResult Create()
        {
            ViewBag.Fecha = DateTime.Now;

            return View();
        }
        [HttpPost]
        public IActionResult Create(Curso curso)
        {
            ViewBag.Fecha = DateTime.Now;
            var escuela = _context.Escuelas.FirstOrDefault();

            curso.EscuelaId = escuela.Id;
            _context.Cursos.Add(curso);
            _context.SaveChanges();
            
            return View();
        }
        private EscuelaContext _context;
        public CursoController(EscuelaContext context)
        {
            _context = context;
        }
    }
}