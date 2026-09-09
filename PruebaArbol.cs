using Proyecto2.Estructuras;

namespace Proyecto2
{
    public class PruebaArbol
    {
        public static void Probar()
        {
            ArbolCategorias arbol = new ArbolCategorias();

            arbol.AgregarCategoria("Ciencia", null);

            arbol.AgregarCategoria("Computación", "Ciencia");

            arbol.AgregarCategoria("Física", "Ciencia");
        }
    }
}