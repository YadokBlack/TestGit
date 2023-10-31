using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desorden : MonoBehaviour
{
    [System.Serializable]
    public struct misObjetos
    {
        public string nombre;
        public string mensaje;

        public int costeTiempo;
        public float[] beneficios;

        public GameObject objetoDestacado;

        public float duracionTransicion;
        public float escalaPorcentaje;
        public Vector3 escalaOriginal;
    }

    [SerializeField]
    public misObjetos[] listaObjetos;
    public Acciones[] listaAcciones;
    public int[] ordenLista;

    public void ColocarObjetos()
    {
        for (int i = 0; i < listaObjetos.Length; i++)
        {
            ordenLista[i] = i;
        }

        for (int i = 0;i < listaObjetos.Length; i++)
        {
            int num = Random.Range(0, listaObjetos.Length);

            int temp = ordenLista[i];
            ordenLista[i] = ordenLista[num];
            ordenLista[num] = temp;
        }

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
        listaAcciones[indexAccion].objetoAnimado.destacado = listaObjetos[indexObjeto].objetoDestacado;

        if (listaAcciones[indexAccion].objetoAnimado.destacado != null )
        {
            listaAcciones[indexAccion].objetoAnimado.destacado.transform.position = listaAcciones[indexAccion].objetoAnimado.posicion;
        }
        
        listaAcciones[indexAccion].objetoAnimado.duracionTransicion = listaObjetos[indexObjeto].duracionTransicion;
        listaAcciones[indexAccion].objetoAnimado.escalaPorcentaje = listaObjetos[indexObjeto].escalaPorcentaje;
        listaAcciones[indexAccion].objetoAnimado.escalaOriginal = listaObjetos[indexObjeto].escalaOriginal;
    }
}
