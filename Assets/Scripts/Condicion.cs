using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Condicion : MonoBehaviour
{
    public float estres;
    public Image barraEstres;
    public Image fondoEstres;
    public float alturaE;
    public float hambre;
    public Image barraHambre;
    public Image fondoHambre;
    public float alturaH;
    public float sed;
    public Image barraSed;
    public Image fondoSed;
    public float alturaS;
    public float cansancio;
    public Image barraVidaCansancio;
    public Image fondoCansancio;
    public float alturaC;

    public float tiempoEntreCambioEstres = 10f;  
    public float tiempoEntreCambioHambre = 15f;  
    public float tiempoEntreCambioSed = 20f;     
    public float tiempoEntreCambioCansancio = 30f; 

    private float tiempoUltimoCambioEstres;
    private float tiempoUltimoCambioHambre;
    private float tiempoUltimoCambioSed;
    private float tiempoUltimoCambioCansancio;

    public PasoDelTiempo control;

    public float aumentoAleatorio;

    private void Awake()
    {
        estres = 0;
        hambre = 0;
        sed = 0;
        cansancio = 0;

        alturaC = fondoCansancio.rectTransform.rect.height;
        alturaE = fondoEstres.rectTransform.rect.height;
        alturaS = fondoSed.rectTransform.rect.height;
        alturaH = fondoHambre.rectTransform.rect.height;

        Debug.Log(" C" + alturaC + " E" + alturaE + " S" + alturaS + " H" + alturaH);

        tiempoUltimoCambioEstres = Time.time;
        tiempoUltimoCambioHambre = Time.time;
        tiempoUltimoCambioSed = Time.time;
        tiempoUltimoCambioCansancio = Time.time;
    }

    private void Update()
    {
        float tiempoActual = Time.time;

        if ( control.pantallaNegra )
        {
            cansancio -= 5;
            estres -= 5f;
        }

        if (tiempoActual - tiempoUltimoCambioEstres >= tiempoEntreCambioEstres)
        {
            estres += 1 + Random.Range(0.1f, aumentoAleatorio);  
            tiempoUltimoCambioEstres = tiempoActual;  
        }

        if (tiempoActual - tiempoUltimoCambioHambre >= tiempoEntreCambioHambre)
        {
            hambre += 1 + Random.Range(0.1f, aumentoAleatorio);  
            tiempoUltimoCambioHambre = tiempoActual;  
        }

        if (tiempoActual - tiempoUltimoCambioSed >= tiempoEntreCambioSed)
        {
            sed += 1 + Random.Range(0.1f, aumentoAleatorio); 
            tiempoUltimoCambioSed = tiempoActual;  
        }

        if (tiempoActual - tiempoUltimoCambioCansancio >= tiempoEntreCambioCansancio)
        {
            cansancio += 1 + Random.Range(0.1f, aumentoAleatorio); 
            tiempoUltimoCambioCansancio = tiempoActual;  
        }

        AcotarEstados();

        barraSed.rectTransform.offsetMin = new Vector2(barraSed.rectTransform.offsetMin.x, sed / 100 * alturaS);
        barraHambre.rectTransform.offsetMin = new Vector2(barraHambre.rectTransform.offsetMin.x, hambre / 100 * alturaH);
        barraVidaCansancio.rectTransform.offsetMin = new Vector2(barraVidaCansancio.rectTransform.offsetMin.x, cansancio / 100 * alturaC);
        barraEstres.rectTransform.offsetMin = new Vector2(barraEstres.rectTransform.offsetMin.x, estres / 100 * alturaE);
    }
    public void CambioEstado(float[] beneficios)
    {
        if ( beneficios != null && beneficios.Length == 4)
        {
            estres += beneficios[0];
            hambre += beneficios[1];
            sed += beneficios[2];
            cansancio += beneficios[3];

            ComprobarEstados();
        }
    }

    private void AcotarEstados()
    {
        estres = Mathf.Clamp(estres, 0, 100);
        hambre = Mathf.Clamp(hambre, 0, 100);
        sed = Mathf.Clamp(sed, 0, 100);
        cansancio = Mathf.Clamp(cansancio, 0, 100);
    }

    private void ComprobarEstados()
    {
        /*
        if ( estres >= 100 )
        {
            // tiene mucho estres
            cansancio += Random.Range(1.1f, aumentoAleatorio);
        }
        if ( hambre >= 100)
        {
            // mucha hambre
            estres += Random.Range(1.1f, aumentoAleatorio);
        }
        if( sed >= 100)
        {
            // mucha sed
            hambre += Random.Range(1.1f, aumentoAleatorio);
        }
        if( cansancio >= 100)
        {
            // muy cansado
            sed += Random.Range(1.1f, aumentoAleatorio);
        }
        */
        AcotarEstados();
    }

}
