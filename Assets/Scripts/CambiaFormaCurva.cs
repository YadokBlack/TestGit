using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaFormaCurva : MonoBehaviour
{
    public AnimationCurve escalaCurve; 
    public AnimationCurve tiempoCurve; 
    public float duracionTransicion = 2.0f; 

    private Vector3 escalaOriginal;
    private Vector3 escalaDeseada;
    private bool enTransicion;
    private float tiempoInicioTransicion;

    private void Start()
    {
        enTransicion = false;

        escalaOriginal = transform.localScale; 
        escalaDeseada = escalaOriginal * 2.0f; 
    }

    private void OnMouseEnter()
    {
        enTransicion = true;
        tiempoInicioTransicion = Time.time;
    }

    private void Update()
    {
        AnimacionSize();
    }
    private void AnimacionSize()
    {
        if (enTransicion)
        {
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);

            float tiempoFactor = tiempoCurve.Evaluate(fraccionDeTiempo);

            float porcentajeEscala = escalaCurve.Evaluate(fraccionDeTiempo);

            transform.localScale = Vector3.Lerp(escalaOriginal, escalaOriginal * porcentajeEscala, porcentajeEscala);

            if (fraccionDeTiempo == 1f)
            {
                enTransicion = false;
            }
        }
    }

    private void OnMouseExit()
    {
        enTransicion = true;
        tiempoInicioTransicion = Time.time;
    }
}