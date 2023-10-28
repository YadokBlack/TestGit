using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccionBase : MonoBehaviour
{
    private const float tiempoCompleto = 1.0f;

    public Color colorPunto = Color.red;

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

    public PasoDelTiempo reloj;

    public Condicion condicion;

    [SerializeField]
    private GameObject pantalla;

    public AudioManager audioManager;
    public int numClip = 0;

    void Start()
    {
        if (objetoDestacado != null)
        {
            escalaOriginal = objetoDestacado.transform.localScale;
        }
        enTransicion = false;
    }

    public void AnimacionAgrandar()
    {
        if (enTransicion)
        {
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);
            Vector3 escalaDeseada = escalaOriginal * escalaPorcentaje;

            objetoDestacado.transform.localScale = Vector3.Lerp(escalaOriginal, escalaDeseada, fraccionDeTiempo);

            if (fraccionDeTiempo == tiempoCompleto)
            {
                enTransicion = false;
            }
        }
        if (!enTransicion && objetoDestacado != null)
        {
            objetoDestacado.transform.localScale = escalaOriginal;
        }
    }

    private void ControlPantallaEnZona()
    {
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
        Gizmos.color = colorPunto; // Color del punto
        Gizmos.DrawSphere(posicionObjeto, 0.01f); // Dibujar un punto en la posición deseada
    }
}
