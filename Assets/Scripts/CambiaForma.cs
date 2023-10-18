using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaForma : MonoBehaviour
{
    public float aumentoPorcentaje = 25f; 
    public float duracionTransicion = 0.30f; 

    public AnimationCurve escalaCurva;

    private Vector3 escalaOriginal;
    private Vector3 escalaDeseada;
    private bool enTransicion;
    private float tiempoInicioTransicion;
    private bool isBig;

    private void Start()
    {
        enTransicion = false;
        isBig = false;

        escalaOriginal = transform.localScale; 
        escalaDeseada = escalaOriginal * (1 + (aumentoPorcentaje / 100f)); 
    }

    private void OnMouseEnter()
    {
        enTransicion = true;
        tiempoInicioTransicion = Time.time;
    }

    private void Update()
    {
        AnimacionSizeCurva();
    }

    private void AnimacionSizeCurva()
    {
        if (enTransicion)
        {
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);

            float escalaFactor = escalaCurva.Evaluate(fraccionDeTiempo);

            transform.localScale = Vector3.Lerp(escalaOriginal, escalaDeseada, escalaFactor);

            if (fraccionDeTiempo == 1f)
            {
                enTransicion = false;
            }
        }
    }

    private void AnimacionSize()
    {
        if (enTransicion && !isBig)
        {
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);

            transform.localScale = Vector3.Lerp(escalaOriginal, escalaDeseada, fraccionDeTiempo);

            if (fraccionDeTiempo == 1f)
            {
                enTransicion = false;
                isBig = true;
            }
        }

        if (isBig && enTransicion)
        {
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);

            transform.localScale = Vector3.Lerp(escalaDeseada, escalaOriginal, fraccionDeTiempo);
            if (fraccionDeTiempo == 1f)
            {
                enTransicion = false;
                isBig = false;
            }
        }
    }

    private void OnMouseExit()
    {
        enTransicion = true;
        tiempoInicioTransicion = Time.time;
    }
}
