using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Condicion : MonoBehaviour
{
    const int valorMaximo = 100;

    public Estado estres;
    public Estado hambre;
    public Estado sed;
    public Estado cansancio;

    private float tiempoUltimoCambioEstres;
    private float tiempoUltimoCambioHambre;
    private float tiempoUltimoCambioSed;
    private float tiempoUltimoCambioCansancio;

    public PasoDelTiempo reloj;

    public float aumentoAleatorio;

    private void Awake()
    {
        estres.valor = 0;
        hambre.valor = 0;
        sed.valor = 0;
        cansancio.valor = 0;

        cansancio.altura = cansancio.fondo.rectTransform.rect.height;
        estres.altura = estres.fondo.rectTransform.rect.height;
        sed.altura = sed.fondo.rectTransform.rect.height;
        hambre.altura = hambre.fondo.rectTransform.rect.height;

        Debug.Log(" C" + cansancio.altura + " E" + estres.altura + " S" + sed.altura + " H" + hambre.altura);

        tiempoUltimoCambioEstres = Time.time;
        tiempoUltimoCambioHambre = Time.time;
        tiempoUltimoCambioSed = Time.time;
        tiempoUltimoCambioCansancio = Time.time;
    }

    private void Update()
    {
        float tiempoActual = Time.time;

        if ( reloj.pantallaNegra )
        {
            cansancio.valor = 0.0f;
            estres.valor = 0.0f;
        }

        if (tiempoActual - tiempoUltimoCambioEstres >= estres.tiempoEntreCambio)
        {
            estres.valor += 1 + Random.Range(0.1f, aumentoAleatorio);  
            tiempoUltimoCambioEstres = tiempoActual;  
        }

        if (tiempoActual - tiempoUltimoCambioHambre >= hambre.tiempoEntreCambio)
        {
            hambre.valor += 1 + Random.Range(0.1f, aumentoAleatorio);  
            tiempoUltimoCambioHambre = tiempoActual;  
        }

        if (tiempoActual - tiempoUltimoCambioSed >= sed.tiempoEntreCambio)
        {
            sed.valor += 1 + Random.Range(0.1f, aumentoAleatorio); 
            tiempoUltimoCambioSed = tiempoActual;  
        }

        if (tiempoActual - tiempoUltimoCambioCansancio >= cansancio.tiempoEntreCambio)
        {
            cansancio.valor += 1 + Random.Range(0.1f, aumentoAleatorio); 
            tiempoUltimoCambioCansancio = tiempoActual;  
        }

        AcotarEstados();

        sed.barra.rectTransform.offsetMin = new Vector2(sed.barra.rectTransform.offsetMin.x, sed.valor / 100 * sed.altura);
        hambre.barra.rectTransform.offsetMin = new Vector2(hambre.barra.rectTransform.offsetMin.x, hambre.valor / 100 * hambre.altura);
        cansancio.barra.rectTransform.offsetMin = new Vector2(cansancio.barra.rectTransform.offsetMin.x, cansancio.valor / 100 * cansancio.altura);
        estres.barra.rectTransform.offsetMin = new Vector2(estres.barra.rectTransform.offsetMin.x, estres.valor / 100 * estres.altura);
    }
    public void CambioEstado(float[] beneficios)
    {
        if ( beneficios != null && beneficios.Length == 4)
        {
            estres.valor += beneficios[0];
            hambre.valor += beneficios[1];
            sed.valor += beneficios[2];
            cansancio.valor += beneficios[3];

            AcotarEstados();
        }
    }

    private void AcotarEstados()
    {
        estres.valor = Mathf.Clamp(estres.valor, 0, 100);
        hambre.valor = Mathf.Clamp(hambre.valor, 0, 100);
        sed.valor = Mathf.Clamp(sed.valor, 0, 100);
        cansancio.valor = Mathf.Clamp(cansancio.valor, 0, 100);
    }

    public bool AlgunEstadoAlMaximo()
    {
        return hambre.valor == valorMaximo || sed.valor == valorMaximo ||
                    cansancio.valor == valorMaximo || estres.valor == valorMaximo;
    }

    public bool HambreAlMaximo()
    {
        return hambre.valor == valorMaximo;
    }

    public bool EstresAlMaximo()
    {
        return estres.valor == valorMaximo;
    }

    public bool CansancioAlMaximo()
    {
        return cansancio.valor == valorMaximo;
    }

    public bool SedAlMaximo()
    {
        return sed.valor == valorMaximo;
    }

    public string ObtenerMensajeCondicion(string mensaje, bool condicion)
    {
        return condicion ? mensaje : string.Empty;
    }
}
