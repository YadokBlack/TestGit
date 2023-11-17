using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class ObjetoAnimado
{
    public GameObject destacado;
    public Vector3 posicion;
    public Vector3 escalaOriginal;
    public bool enTransicion;
    public float tiempoInicioTransicion;
    public float duracionTransicion = 0.30f;
    public float escalaPorcentaje;

    private const float tiempoCompleto = 1.0f;

    public void Inicializar()
    {
        if (destacado != null)
        {
            escalaOriginal = destacado.transform.localScale;
        }
        enTransicion = false;
    }

    public void AnimacionAgrandar()
    {
        if (destacado == null) return;

        if (enTransicion)
        {
            CambiaEscala();
        }
        else
        {
            CambiaEscalaOriginal();
        }
    }

    private void CambiaEscalaOriginal()
    {
        destacado.transform.localScale = escalaOriginal;
    }

    private void CambiaEscala()
    {
        float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
        float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);
        Vector3 escalaDeseada = escalaOriginal * escalaPorcentaje;

        destacado.transform.localScale = Vector3.Lerp(escalaOriginal, escalaDeseada, fraccionDeTiempo);

        if (fraccionDeTiempo == tiempoCompleto) enTransicion = false;
    }

    public bool DestacadoNoEstaEnTransicion()
    {
        return destacado != null && !enTransicion;
    }

    public void IniciaTransicion()
    {
        enTransicion = true;
        tiempoInicioTransicion = Time.time;
    }
}
