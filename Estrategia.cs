
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using tp1;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace tpfinal
{

	public class Estrategia
	{
		
		public string GetUrlSeoPorId(ArbolGeneral<ItemCat> arbol, int id)
        {
            return "Implementar";
        }
        
        public List<List<string>> ConsultaNiveles(ArbolGeneral<ItemCat> arbol)
		{
            return [["Implementar"]];
        }


        public List<ItemCat> Todos(ArbolGeneral<ItemCat> arbol)
        {
            List<ItemCat> lista = new List<ItemCat>(); //lista que se retornará

            if (arbol.getDatoRaiz().Tipo == TipoElemento.Producto) lista.Add(arbol.getDatoRaiz()); //se agrega el obj ITEMCAT

            foreach (var h in arbol.getHijos())
            {
                lista.AddRange(Todos(h));
            }


            return  lista;
        }

        public void Agregar(ArbolGeneral<ItemCat> arbol, ItemCat dato, string rutaAlPadre)
		{
            //la ruta al padre esta vacia, hay que agregarlo al nodo actual
            if (rutaAlPadre == "") 
            {
                //se instancia un subarbol o nodo para agregar el dato como hijo
                ArbolGeneral<ItemCat> subArbol = new ArbolGeneral<ItemCat>(dato);
                arbol.agregarHijo(subArbol);
                return;
            }

            //en estas variables se extraerán la categoria actual y el resto
            string catActual;
            string rutaRestante;

            //indexof trabaja con las posiciones.
            int separador = rutaAlPadre.IndexOf("/");
            if (separador != -1) //es decir, encontró una /
            {
                catActual = rutaAlPadre.Substring(0, separador); //corta desde el inicio hasta la barra.

                rutaRestante = rutaAlPadre.Substring(separador + 1); //corta desde la posicion siguiente a la barra hasta el final.
            }
            else //es el final de la ruta
            {
                catActual = rutaAlPadre;
                rutaRestante = "";
            }

            //buscamos coincidencias entre los hijos del nodo y la primera de las categorias
            ArbolGeneral<ItemCat> hijoDestino = null;

            foreach (var h in arbol.getHijos()) 
            {
                if (h.getDatoRaiz().Nombre == catActual) 
                {
                    hijoDestino = h;
                    break;
                }

            }

            //si no se encontró la categoría, la crea
            if (hijoDestino == null)
            {
                ItemCat nuevaCat = new ItemCat(catActual, TipoElemento.Categoria); //Crea la categoria
                ArbolGeneral<ItemCat> nuevoArbol = new ArbolGeneral<ItemCat>(nuevaCat); //crea el nodo con la categoria
                arbol.agregarHijo(nuevoArbol);//Se lo agrega a la lista de hijos del arbol actual
                hijoDestino = nuevoArbol; //hijoDestino va a ser ahora el nuevo nodo
            }

            Agregar(hijoDestino, dato, rutaRestante); //llamada recursiva para continuar con las demas categorias

        }

       public List<ItemCat> Buscar(ArbolGeneral<ItemCat> arbol, string elementoABuscar)
		{ 
			List<ItemCat> resultado= new List<ItemCat>();
			BuscarRecursivo(arbol,elementoABuscar,resultado);
			return resultado;
		}
		
		private void BuscarRecursivo(ArbolGeneral<ItemCat> arbol, string elementoABuscar, List<ItemCat> res)
		{
			ItemCat dato= arbol.getDatoRaiz();
			
			if(dato.Nombre.ToLower().Contains(elementoABuscar.ToLower()))
			{
				res.Add(dato);
			}
			foreach(var x in arbol.getHijos())
			{
				BuscarRecursivo(x, elementoABuscar,res);
			}
		
		}

		// Ejercicio numero 4
		public List<String> GetURLsSEO(ArbolGeneral<ItemCat> arbol)
        { //Creamos una lista donde pondremos nuestros url creados para que no se vayan perdiendo a medida que vamos creando nuevos.
           List<string> listaURL = new List<string>();

			//Si el arbol no esta vacio, llamamos al metodo Dfs.
           if (arbol != null)
            {
                Dfs (arbol, "" ,listaURL);
            }

            return listaURL;
        }
            
        private void Dfs(ArbolGeneral <ItemCat> nodoActual, string rutaViajada, List<string> lista)
        {
            string nuevaRuta;
			//Le decimos al codigo que si el nodo en el que estamos parados no tiene ningun valor, detengamos la ejecucion para no hacerle
			//gastar tiempo.
            if (nodoActual == null)
            {
                return;
            }
			//Si la rutaViajada no tiene ningun valor, significa que estamos parados en la raiz.Procesamos la raiz a la ruta con su nombre.
            if (rutaViajada == "")
            {
                nuevaRuta = nodoActual.getDatoRaiz().Nombre ;
            }
			//En caso de que la ruta tenga valor, concatenamos la barra espaciadora con el nombre del nodo actual en el que estamos ubicados.
            else
            {
                nuevaRuta = ${rutaViajada}/{nodoActual.getDatoRaiz().Nombre};
            }

			//En esta linea lo que queremos buscar es si el nodo en el que estamos parados es una hoja. Para eso, nos guiamos de nuestro arbol general de productos, donde nos indica que si el elemento es de tipo categoria, 
			// significa que es padre. En cambio, si el elemento que estamos por procesar es de tipo producto, signfica que es hoja.
			// Un dfs avanza siempre hacia abajo por una rama hasta llegar a una hoja antes de mirar hacia los lados (hermanos), lo que nosotros hicimos fue cortar la busqueda al encontrar la hoja y añadirla en la lista con todo el url completo.
            if (nodoActual.getDatoRaiz().Tipo == TipoElemento.Producto)
            {
                lista.Add(nuevaRuta);
            }

            else
            {
                foreach (var hijo in nodoActual.getHijos())
                {
					//Como todavia no somos hojas, nos llamamos a nosotros mismos con una recursividad para seguir explorando a nuestros hijos y poder terminar nuestra ruta.
                    Dfs (hijo, nuevaRuta, lista );
                }
            }
        }
            
    }
}
