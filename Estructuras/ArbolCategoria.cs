using Proyecto2.Models;

namespace Proyecto2.Estructuras
{
    public enum ResultadoAgregarCategoria //OPCIONES
    {
        Exito, 
        NombreInvalido,
        CategoriaDuplicada,
        PadreNoEncontrado
    }

    public class ArbolCategorias
    {
        private NodoCategoria raizVirtual;// CONEXION DE CATEGORIAS PRINCIPALES
        //CONSTRUCTOR
        public ArbolCategorias()
        {
            raizVirtual = new NodoCategoria(
                new Categoria("__RAIZ__") //BASE INTERNA LLAMADA ASI
            );
        }
        //PRIMER NIVEL
        public NodoCategoria? PrimerNivel //DEVUELVE UN NODO CATEGORIA
        {
            get //OBTENER VALOR
            {
                return raizVirtual.PrimerHijo; //TOMA PRIMER VALOR
            }
        }
        //AGREGAR CATEGORIA
        public ResultadoAgregarCategoria AgregarCategoria( //CON ENUMS DE RESULTADOS
            string? nombre, //? PUEDE LLEVAR NULL
            string? nombrePadre)
        {
            //SI ESTA VACIO O NULO DEVUELVE NOMBRE INVALIDO
            if (string.IsNullOrWhiteSpace(nombre))
            {
                return ResultadoAgregarCategoria.NombreInvalido;
            }
            string nombreLimpio = nombre.Trim(); //SI HAY ESPACIOS LOS ELIMINA

            //SI YA EXISTE UNA CATEGORIA CON ESE NOMBRE, DEVUELVE DUPLICADA
            if (BuscarCategoria(nombreLimpio) != null) //!NULL SI SE ENCUENTRA
            {
                return ResultadoAgregarCategoria.CategoriaDuplicada; //MENSAJE DE ENUM
            }
            NodoCategoria padre; //GUARDAR PADRE

            if (string.IsNullOrWhiteSpace(nombrePadre)) //SI ESTA VACIO EL NODO PADRE
            {
                padre = raizVirtual;
            }
            else
            {
                //CUANDO SI SE ESCRIBE EL NOMBRE PADRE
                NodoCategoria? padreEncontrado =
                    BuscarCategoria(nombrePadre.Trim());
                if (padreEncontrado == null) //SI NO EXISTE EL PADRE, DEVUELVE PADRE NO ENCONTRADO
                {
                    return ResultadoAgregarCategoria.PadreNoEncontrado;
                }
                padre = padreEncontrado;//SI SI EXISTE, SE GUARDA EL PADRE
            }
            //CREACION DE NUEVO NODO CATEGORIA
            NodoCategoria nuevoNodo =
                new NodoCategoria(
                    new Categoria(nombreLimpio)
                );
            //SI PADRE ESTA VACIO, SE AGREGA EL NUEVO NODO COMO PRIMER HIJO DEL PADRE
            if (padre != raizVirtual)
            {
                nuevoNodo.Padre = padre;
            }
            //ORDENAR PADRE Y NUEVO NODO
            InsertarHijoOrdenado(padre, nuevoNodo);
            return ResultadoAgregarCategoria.Exito; //ENUM
        }
        //AGREGAR CATECORIA
        public NodoCategoria? BuscarCategoria(string? nombre) //NOMBRE DE CATEGORIA
        {
            if (string.IsNullOrWhiteSpace(nombre)) //SI ESTA VACIO O NULO
            {
                return null;
            }
            //BUSQUEDA DESDE LA PRIMERA RAIZ REAL
            return BuscarRecursivo(
                raizVirtual.PrimerHijo,
                nombre.Trim()
            );
        }
        private NodoCategoria? BuscarRecursivo(
            NodoCategoria? actual,
            string nombre)
        {
            NodoCategoria? cursor = actual;

            //MIENTRAS SEA VERDADERO
            while (cursor != null)
            {
                //2 TEXTOS SON IGUALES
                //SI ES EL NOMBRE QUE SE BUSCA
                if (string.Equals(
                cursor.Dato.Nombre,
                nombre,
                StringComparison.OrdinalIgnoreCase)) //OMITE MAYUSCULAS Y MINUSCULAS
                {
                    return cursor; //DEVUELVE NOMBRE DEL NODO
                }
                NodoCategoria? encontradoEnHijos =
                BuscarRecursivo( //BUSCA CATEGORIA
                    cursor.PrimerHijo, //TOMA EL NOMNRE DEL PRIMER HIJO
                    nombre
                );
                //SI SE ENCUENTRA EN LOS HIJOS, DEVUELVE EL NODO
                if (encontradoEnHijos != null)
                {
                    return encontradoEnHijos;
                }
                cursor = cursor.SiguienteHermano; //MOVIMIENTOE DE CURSOR
            }
            return null;
        }

        //ORDENAR HIJOS
        private void InsertarHijoOrdenado(
            NodoCategoria padre,
            NodoCategoria nuevo)
        {
            if (padre.PrimerHijo == null) //SI PADRE NO TIENE HIJO
            {
                padre.PrimerHijo = nuevo; //PASA A SER HIJO
                return;
            }
            //COMPARACION ALFABETICA ENTRE HIJOS
            if (string.Compare(
                nuevo.Dato.Nombre, //NUEVO NOMBRE
                padre.PrimerHijo.Dato.Nombre, //COMPARA PRIMER HIJO ACTUAL
                StringComparison.OrdinalIgnoreCase) < 0) //SIN MAYUSCULAS O MINUSCULAS
                                                         //SI NUEVO NOMBRE ES MENOR AL PRIMER HIJO, SE AGREGA ANTES
            {
                nuevo.SiguienteHermano = padre.PrimerHijo; //VARIABLE TEMPORAL
                padre.PrimerHijo = nuevo;
                return;
            }
            NodoCategoria actual = padre.PrimerHijo; //ACTUAL

            while (
                actual.SiguienteHermano != null && //SI EXISTE Y NO ESTA VACIO EL SIGUIENTE HERMANO
                                                   //COMPARACION ALFABETICA ENTRE HIJOS
                string.Compare(
                actual.SiguienteHermano.Dato.Nombre,
                nuevo.Dato.Nombre,
                StringComparison.OrdinalIgnoreCase
                                ) < 0)
            {
                actual = actual.SiguienteHermano; //SI EL SIGUIENTE HERMANO ES MENOR, SE MUEVE AL SIGUIENTE HERMANO
           
            }
            nuevo.SiguienteHermano = actual.SiguienteHermano;
            actual.SiguienteHermano = nuevo;
        }
        //MOSTRAR ESTRUCTURA DEL ARBOL
        public string MostrarEstructura()
        {
            return MostrarNodo(PrimerNivel, ""); //DESDE LA PRIMERA
        }
        private string MostrarNodo(
            NodoCategoria? nodo,
            string espacio)
        {
            if (nodo == null)
            {
                return "";
            }

            string resultado = "";

            NodoCategoria? actual = nodo;

            while (actual != null)
            {
                resultado +=
                    espacio +
                    actual.Dato.Nombre +
                    "\n";

                if (actual.PrimerHijo != null)
                {
                    resultado += MostrarNodo(
                        actual.PrimerHijo,
                        espacio + "   "
                    );
                }

                actual = actual.SiguienteHermano;
            }

            return resultado;
        }

    }
}