
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
        

        public List<string> GetURLsSEO(ArbolGeneral<ItemCat> arbol)
		{
			return ["Implementar"];
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
			
			if(dato.Nombre.ToLower().Contains(elementoABuscar.ToLowe()))
			{
				res.Add(dato);
			}
			foreach(var x in arbol.getHijos())
			{
				BuscarRecursivo(x, elementoABuscar,res);
			}
		
		}
            
    }
}
