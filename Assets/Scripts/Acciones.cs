using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Acciones : MonoBehaviour
{
    public KeyCode teclaAccion = KeyCode.E;

    // para definir coste de la accion
    public int costeTiempo = 5;
    public float[] beneficios = null;

    // para la animacion del objeto a destacar
    public GameObject objetoDestacado;
    public Vector3 posicionObjeto;
    public Vector3 escalaOriginal;
    private bool enTransicion;
    private float tiempoInicioTransicion;
    public float duracionTransicion = 0.30f;
    public float escalaPorcentaje;

    // para comprobar la colision del jugador
    public ControlZonas zonaControl;
    public ZonaDeColision zonaControlada;

    // para controlar cuando pasa el dia
    public PasoDelTiempo control;

    // para poder acceder a los estados del jugador
    public Condicion condicion;

    // solo rellenar en el caso de tener alguna pantalla para activar
    [SerializeField]
    private GameObject pantalla;

    public AudioManager audioManager;
    public int numClip=0;

    private float alturaMaxima = 0f;

    public AccionTrabajar accionTrabajo;

    void Start()
    {
        if (objetoDestacado != null)
        {
            escalaOriginal = objetoDestacado.transform.localScale;


            // float Altura = ObtenerAlturaMaxima(objetoDestacado.transform);

            // Obten la mitad de la altura del objeto
            // float mitadAltura = objetoDestacado.transform.localScale.y / 2.0f;
            // float mitadAltura = Altura / 2.0f;

            // print(objetoDestacado.name + " media Altura: " + mitadAltura);

            // añadido para intentar que no salgan mal colocados los objetos
            // objetoDestacado.transform.position = posicionObjeto + new Vector3(0, -mitadAltura, 0); 
        }
        enTransicion = false;
    }

    private float ObtenerAlturaMaxima(Transform objetoPadre)
    {
        float alturaObjetoPadre = 0f;

        foreach (Transform hijo in objetoPadre)
        {
            // Comprueba si el hijo tiene un MeshRenderer
            MeshRenderer meshRenderer = hijo.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                // Obtiene la posición local del extremo superior del MeshRenderer
                float alturaHijo = hijo.localPosition.y + (meshRenderer.bounds.size.y * 0.5f);

                // Actualiza la altura máxima si esta es mayor
                alturaMaxima = Mathf.Max(alturaMaxima, alturaHijo);
            }

            // Llama recursivamente a la función para explorar los hijos del hijo actual
            float alturaHijoRecursiva = ObtenerAlturaMaxima(hijo);

            // Actualiza la altura máxima con la altura del hijo obtenida de la recursión
            alturaMaxima = Mathf.Max(alturaMaxima, alturaHijoRecursiva);

            // Actualiza la altura del objeto padre con la altura máxima de sus hijos
            alturaObjetoPadre = Mathf.Max(alturaObjetoPadre, alturaHijoRecursiva);
        }

        // Devuelve la altura máxima del objeto padre
        return alturaObjetoPadre;
    }


    public void AnimacionAgrandar()
    {
        if (enTransicion)
        {
            // vamos a agrandar
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);
            Vector3 escalaDeseada = escalaOriginal * escalaPorcentaje;

            objetoDestacado.transform.localScale = Vector3.Lerp(escalaOriginal, escalaDeseada, fraccionDeTiempo);

            if (fraccionDeTiempo == 1f)
            {
                enTransicion = false;
            }
        }
        if (!enTransicion && objetoDestacado != null)
        {
            // Restaurar el tamaño original del objeto cuando no está agrandándose.
            objetoDestacado.transform.localScale = escalaOriginal;
        }
    }


    void Update()
    {

        if (zonaControl.jugadorEnZona && zonaControl.nombreZonaJugador == zonaControlada.name && !control.pantallaNegra)
        {
            if (Input.GetKeyDown(teclaAccion))
            {
                accionTrabajo.teclasPulsadas++;

                control.AumentaTiempo(costeTiempo);
                condicion.CambioEstado(beneficios);
                if (objetoDestacado != null && !enTransicion)
                {
                    //  Debug.Log("Inicia");
                    enTransicion = true;
                    tiempoInicioTransicion = Time.time;
                }

                if (audioManager != null)
                {
                    // Reproduce el sonido cuando se presiona la tecla de interacción.
                    audioManager.PlayAudioByIndex(numClip);
                }                
            }
        }
        
        AnimacionAgrandar();

        if (zonaControl.jugadorEnZona && zonaControl.nombreZonaJugador == zonaControlada.name && pantalla != null)
        {
            pantalla.SetActive(true);
        }
        else if (!zonaControl.jugadorEnZona && pantalla != null)
        {
            pantalla.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; // Color del punto
        Gizmos.DrawSphere(posicionObjeto, 0.01f); // Dibujar un punto en la posición deseada
    }
}



/*
 * jugadorEnzona indica que esta dentro de alguna zona, que puede que no sea la nuestra
 * es por ello que si esta en zona debe comprobar el nombre de la zona para verificar 
 * que es la nuestra.
 * 
 */