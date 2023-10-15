using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  Tendra una lista de ubicaciones y una lista de objetos que se
 *  repartiran por estas ubicaciones.
 *  
 *  Cada dia que pase se cambiará la ubicacion de los objetos.
 */




public class Desorden : MonoBehaviour
{

    [System.Serializable]
    public struct misObjetos
    {
        public string nombre;
        public string mensaje;

        public int costeTiempo;
        public float[] beneficios;

        // para la animacion del objeto a destacar
        public GameObject objetoDestacado;

        public float duracionTransicion;
        public float escalaPorcentaje;
        public Vector3 escalaOriginal;
    }

    [SerializeField]
    // deben tener el mismo numero!!
    public misObjetos[] listaObjetos;
    public Acciones[] listaAcciones;

    public int[] ordenLista;

    public void ColocarObjetos()
    {
        // inicializa la lista de orden
        for (int i = 0; i < listaObjetos.Length; i++)
        {
            ordenLista[i] = i;
        }

        // barajamos la lista
        for (int i = 0;i < listaObjetos.Length; i++)
        {
            int num = Random.Range(0, listaObjetos.Length);
            // realiza el intercambio
            int temp = ordenLista[i];
            ordenLista[i] = ordenLista[num];
            ordenLista[num] = temp;
        }

        // colocamos los objetos en sus lugares
        for(int i = 0;i< listaObjetos.Length;i++)
        {
            Colocar(i, ordenLista[i]);
        }

    }

    public void Colocar( int indexAccion, int indexObjeto)
    {
        listaAcciones[indexAccion].zonaControlada.mensajeZona = listaObjetos[indexObjeto].mensaje;
        listaAcciones[indexAccion].costeTiempo = listaObjetos[indexObjeto].costeTiempo;
        listaAcciones[indexAccion].beneficios = listaObjetos[indexObjeto].beneficios;
        listaAcciones[indexAccion].objetoDestacado = listaObjetos[indexObjeto].objetoDestacado;
        // Obten la mitad de la altura del objeto
        //   float mitadAltura = listaAcciones[indexAccion].objetoDestacado.transform.localScale.y / 2.0f;
        //   listaAcciones[indexAccion].objetoDestacado.transform.position = listaAcciones[indexAccion].posicionObjeto + new Vector3(0, mitadAltura, 0);
        if (listaAcciones[indexAccion].objetoDestacado != null )
        {
            listaAcciones[indexAccion].objetoDestacado.transform.position = listaAcciones[indexAccion].posicionObjeto;
        }
        
        listaAcciones[indexAccion].duracionTransicion = listaObjetos[indexObjeto].duracionTransicion;
        listaAcciones[indexAccion].escalaPorcentaje = listaObjetos[indexObjeto].escalaPorcentaje;
        listaAcciones[indexAccion].escalaOriginal = listaObjetos[indexObjeto].escalaOriginal;
    }
}
