using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Acciones : MonoBehaviour
{
    public KeyCode teclaAccion = KeyCode.E;

    public int costeTiempo = 5;
    public float[] beneficios = null;

    public GameObject objetoDestacado;
    public Vector3 posicionObjeto;
    public Vector3 escalaOriginal;
    private bool enTransicion;
    private float tiempoInicioTransicion;
    public float duracionTransicion = 0.30f;
    public float escalaPorcentaje;

    public ControlZonas zonaControl;
    public ZonaDeColision zonaControlada;

    public PasoDelTiempo control;

    public Condicion condicion;

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
        }
        enTransicion = false;
    }

    private float ObtenerAlturaMaxima(Transform objetoPadre)
    {
        float alturaObjetoPadre = 0f;

        foreach (Transform hijo in objetoPadre)
        {           
            MeshRenderer meshRenderer = hijo.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {
                
                float alturaHijo = hijo.localPosition.y + (meshRenderer.bounds.size.y * 0.5f);

                
                alturaMaxima = Mathf.Max(alturaMaxima, alturaHijo);
            }

            float alturaHijoRecursiva = ObtenerAlturaMaxima(hijo);

            alturaMaxima = Mathf.Max(alturaMaxima, alturaHijoRecursiva);

            alturaObjetoPadre = Mathf.Max(alturaObjetoPadre, alturaHijoRecursiva);
        }
        return alturaObjetoPadre;
    }

    public void AnimacionAgrandar()
    {
        if (enTransicion)
        { 
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
                    enTransicion = true;
                    tiempoInicioTransicion = Time.time;
                }

                if (audioManager != null)
                {                    
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
