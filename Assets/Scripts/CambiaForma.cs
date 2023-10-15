using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// el script debe estar sobre un mesh de un objeto y
// dicho objeto debe tener un collider para funcionar


public class CambiaForma : MonoBehaviour
{
    public float aumentoPorcentaje = 25f; // Porcentaje de aumento
    public float duracionTransicion = 0.30f; // Duración de la transición en segundos

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

        escalaOriginal = transform.localScale; // Guardar la escala original del objeto
        escalaDeseada = escalaOriginal * (1 + (aumentoPorcentaje / 100f)); // Calcular el tamaño deseado
    }

    private void OnMouseEnter()
    {
        enTransicion = true;
        tiempoInicioTransicion = Time.time;
    }

    private void Update()
    {
        // AnimacionSize();
        AnimacionSizeCurva();
    }

    private void AnimacionSizeCurva()
    {
        if (enTransicion)
        {
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);

            // Evalúa la curva en el tiempo para obtener el factor de escala
            float escalaFactor = escalaCurva.Evaluate(fraccionDeTiempo);

            // Interpola entre la escala original y la escala deseada usando el factor de escala
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
            // vamos a agrandar
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
            // vamos a reducir
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
