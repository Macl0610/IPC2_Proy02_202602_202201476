using Proyecto2.Models;

namespace Proyecto2.Estructuras
{
    public class NodoCategoria
    {
        public Categoria Dato { get; set; }

        public NodoCategoria? Padre { get; set; }

        public NodoCategoria? PrimerHijo { get; set; }

        public NodoCategoria? SiguienteHermano { get; set; } //mismo nivel

        public NodoCategoria(Categoria dato) //categoria del nodo
        {
            //constructor
            Dato = dato;
            Padre = null;
            PrimerHijo = null;
            SiguienteHermano = null;
        }
    }
}