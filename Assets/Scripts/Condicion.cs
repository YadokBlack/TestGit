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
        InicializaEstadoValor();

        InicializaAlturas();

        Debug.Log(" C" + cansancio.altura + " E" + estres.altura + " S" + sed.altura + " H" + hambre.altura);

        InicializaTiempoEstados();
    }

    private void InicializaEstadoValor()
    {
        hambre.valor = 0;
        sed.valor = 0;
        InicializaCansancioYEstres();
    }

    private void InicializaCansancioYEstres()
    {
        cansancio.valor = 0.0f;
        estres.valor = 0.0f;
    }

    private void InicializaAlturas()
    {
        cansancio.altura = cansancio.fondo.rectTransform.rect.height;
        estres.altura = estres.fondo.rectTransform.rect.height;
        sed.altura = sed.fondo.rectTransform.rect.height;
        hambre.altura = hambre.fondo.rectTransform.rect.height;
    }

    private void InicializaTiempoEstados()
    {
        tiempoUltimoCambioEstres = Time.time;
        tiempoUltimoCambioHambre = Time.time;
        tiempoUltimoCambioSed = Time.time;
        tiempoUltimoCambioCansancio = Time.time;
    }

    private void Update()
    {        
        if (reloj.pantallaNegra) InicializaCansancioYEstres();

        ActualizaBarrasEstados();
    }

    private void IncrementaEstados()
    {
        float tiempoActual = Time.time;
        if (TieneEstres(tiempoActual)) IncrementaEstres(tiempoActual);
        if (TieneHambre(tiempoActual)) IncrementaHambre(tiempoActual);
        if (TieneSed(tiempoActual)) IncrementaSed(tiempoActual);
        if (TieneCansancio(tiempoActual)) IncrementaCansancio(tiempoActual);
    }

    private void ActualizaBarrasEstados()
    {
        IncrementaEstados();

        AcotarEstados();

        sed.ActualizaBarra();
        hambre.ActualizaBarra();
        cansancio.ActualizaBarra();
        estres.ActualizaBarra();
    }

    private bool TieneSed(float tiempoActual)
    {
        return tiempoActual - tiempoUltimoCambioSed >= sed.tiempoEntreCambio;
    }

    private bool TieneEstres(float tiempoActual)
    {
        return tiempoActual - tiempoUltimoCambioEstres >= estres.tiempoEntreCambio;
    }

    private bool TieneHambre(float tiempoActual)
    {
        return tiempoActual - tiempoUltimoCambioHambre >= hambre.tiempoEntreCambio;
    }

    private bool TieneCansancio(float tiempoActual)
    {
        return tiempoActual - tiempoUltimoCambioCansancio >= cansancio.tiempoEntreCambio;
    }

    private void IncrementaSed(float tiempoActual)
    {
        sed.valor += 1 + Random.Range(0.1f, aumentoAleatorio);
        tiempoUltimoCambioSed = tiempoActual;
    }

    private void IncrementaHambre(float tiempoActual)
    {
        hambre.valor += 1 + Random.Range(0.1f, aumentoAleatorio);
        tiempoUltimoCambioHambre = tiempoActual;
    }

    private void IncrementaEstres(float tiempoActual)
    {
        estres.valor += 1 + Random.Range(0.1f, aumentoAleatorio);
        tiempoUltimoCambioEstres = tiempoActual;
    }

    private void IncrementaCansancio(float tiempoActual)
    {
        cansancio.valor += 1 + Random.Range(0.1f, aumentoAleatorio);
        tiempoUltimoCambioCansancio = tiempoActual;
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
        return HambreAlMaximo() || SedAlMaximo() || CansancioAlMaximo() || EstresAlMaximo();
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
