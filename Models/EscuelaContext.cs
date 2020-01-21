using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Nuevo.Models
{
    public class EscuelaContext : DbContext
    {
        public DbSet<Escuela> Escuelas{get; set;}
        public DbSet<Asignatura> Asignaturas{get; set;}
        public DbSet<Alumno> Alumnos{get; set;}
        public DbSet<Curso> Cursos{get; set;}
        public DbSet<Evaluación> Evaluaciones{get; set;}

        public EscuelaContext(DbContextOptions<EscuelaContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var escuela = new Escuela();
            escuela.AñoDeCreación = 1958;
            escuela.Nombre = "Dr. Dalmacio Velez Sarfield";
            escuela.Id = Guid.NewGuid().ToString();
            escuela.Dirección = "Diego de Rojas s/n, Mzna 'H', Lote 5";
            escuela.Ciudad = "Famaillá";
            escuela.Pais = "Argentina";
            escuela.TipoEscuela = TiposEscuela.Secundaria;            

            //Cursos
            var cursos = CargarCursos(escuela);
            //Asignaturas
            var asignaturas = CargarAsignaturas(cursos);
            //Alumnos
            var alumnos = CargarAlumnos(cursos);

            modelBuilder.Entity<Escuela>().HasData(escuela);
            modelBuilder.Entity<Curso>().HasData(cursos.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnos.ToArray());
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursos)
        {
            var listaCompleta = new List<Asignatura>();
            foreach (var curso in cursos)
            {
                var tmpList = new List<Asignatura>
                {
                    new Asignatura
                    {
                        Nombre = "Matemáticas",
                        Id = Guid.NewGuid().ToString(),
                        CursoId = curso.Id
                    },
                    new Asignatura
                    {
                        Nombre = "Educación Física",
                        Id = Guid.NewGuid().ToString(),
                        CursoId = curso.Id
                    },
                    new Asignatura
                    {
                        Nombre = "Castellano",
                        Id = Guid.NewGuid().ToString(),
                        CursoId = curso.Id
                    },
                    new Asignatura
                    {
                        Nombre = "Ciencias Naturales",
                        Id = Guid.NewGuid().ToString(),
                        CursoId = curso.Id
                    },
                    new Asignatura
                    {
                        Nombre = "Programación",
                        Id = Guid.NewGuid().ToString(),
                        CursoId = curso.Id
                    },
                };

                listaCompleta.AddRange(tmpList);
                //curso.Asignaturas = tmpList;
            }

            return listaCompleta;
        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            return new List<Curso>() {
                new Curso() {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "101",
                    Jornada = TiposJornada.Mañana
                },
                new Curso() {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "102",
                    Jornada = TiposJornada.Mañana
                },
                new Curso() {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "103",
                    Jornada = TiposJornada.Mañana
                },
                new Curso() {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "104",
                    Jornada = TiposJornada.Tarde
                },
                new Curso() {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "105",
                    Jornada = TiposJornada.Tarde
                },
            };
        }

        private List<Alumno> CargarAlumnos(List<Curso> cursos)
        {
            var listaAlumnos = new List<Alumno>();

            Random rnd = new Random();
            foreach(var curso in cursos)
            {
                int cantRandom = rnd.Next(5, 20);
                var tmplist = GenerarAlumnosAlAzar(curso, cantRandom);
                listaAlumnos.AddRange(tmplist);
            }
            return listaAlumnos;
        }

        private List<Alumno> GenerarAlumnosAlAzar(Curso curso, int cantidad)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos =  from n1 in nombre1
                                from n2 in nombre
                                from a1 in apellido
                                select new Alumno 
                                {
                                    CursoId = curso.Id,
                                    Nombre = $"{n1} {n2} {a1}",
                                    Id = Guid.NewGuid().ToString()
                                };

            return listaAlumnos.OrderBy((al) => al.Id).Take(cantidad).ToList();
        }
    }
}