using Microsoft.AspNetCore.Mvc;
using Proyecto2.Models;
using System.Diagnostics;
using Proyecto2.Estructuras;

namespace Proyecto2.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //CREAMOS EL ARBOL
            ArbolCategorias arbol = new ArbolCategorias();

            //AGREGAMOS CATEGORIAS DE PRUEBA
            arbol.AgregarCategoria("Ciencia", null);

            arbol.AgregarCategoria("Computación", "Ciencia");

            arbol.AgregarCategoria("Física", "Ciencia");

            arbol.AgregarCategoria("Programación", "Computación");

            //MOSTRAMOS LA ESTRUCTURA DEL ARBOL
            ViewBag.Resultados = arbol.MostrarEstructura();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId =
                        Activity.Current?.Id ??
                        HttpContext.TraceIdentifier
                }
            );
        }
    }
}