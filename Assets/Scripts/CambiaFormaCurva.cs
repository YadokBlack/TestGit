using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiaFormaCurva : MonoBehaviour
{
    public AnimationCurve escalaCurve; // Curva de escala personalizada
    public AnimationCurve tiempoCurve; // Curva de tiempo personalizada
    public float duracionTransicion = 2.0f; // Duración de la transición en segundos

    private Vector3 escalaOriginal;
    private Vector3 escalaDeseada;
    private bool enTransicion;
    private float tiempoInicioTransicion;

    private void Start()
    {
        enTransicion = false;

        escalaOriginal = transform.localScale; // Guardar la escala original del objeto
        escalaDeseada = escalaOriginal * 2.0f; // Escala deseada (por ejemplo, el doble de tamaño)
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

            // Evalúa la curva de tiempo para controlar la velocidad de la animación
            float tiempoFactor = tiempoCurve.Evaluate(fraccionDeTiempo);

            // Evalúa la curva de escala para obtener el porcentaje de escala
            float porcentajeEscala = escalaCurve.Evaluate(fraccionDeTiempo);

            // Interpola entre la escala original y la escala deseada usando el porcentaje de escala
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