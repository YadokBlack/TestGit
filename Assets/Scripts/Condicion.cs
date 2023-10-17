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
    public float alturaEstres;
    public float hambre;
    public Image barraHambre;
    public Image fondoHambre;
    public float alturaHambre;
    public float sed;
    public Image barraSed;
    public Image fondoSed;
    public float alturaSed;
    public float cansancio;
    public Image barraCansancio;
    public Image fondoCansancio;
    public float alturaCansancio;

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

        alturaCansancio = fondoCansancio.rectTransform.rect.height;
        alturaEstres = fondoEstres.rectTransform.rect.height;
        alturaSed = fondoSed.rectTransform.rect.height;
        alturaHambre = fondoHambre.rectTransform.rect.height;

        Debug.Log(" C" + alturaCansancio + " E" + alturaEstres + " S" + alturaSed + " H" + alturaHambre);

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

        barraSed.rectTransform.offsetMin = new Vector2(barraSed.rectTransform.offsetMin.x, sed / 100 * alturaSed);
        barraHambre.rectTransform.offsetMin = new Vector2(barraHambre.rectTransform.offsetMin.x, hambre / 100 * alturaHambre);
        barraCansancio.rectTransform.offsetMin = new Vector2(barraCansancio.rectTransform.offsetMin.x, cansancio / 100 * alturaCansancio);
        barraEstres.rectTransform.offsetMin = new Vector2(barraEstres.rectTransform.offsetMin.x, estres / 100 * alturaEstres);
    }
    public void CambioEstado(float[] beneficios)
    {
        if ( beneficios != null && beneficios.Length == 4)
        {
            estres += beneficios[0];
            hambre += beneficios[1];
            sed += beneficios[2];
            cansancio += beneficios[3];

            AcotarEstados();
        }
    }

    private void AcotarEstados()
    {
        estres = Mathf.Clamp(estres, 0, 100);
        hambre = Mathf.Clamp(hambre, 0, 100);
        sed = Mathf.Clamp(sed, 0, 100);
        cansancio = Mathf.Clamp(cansancio, 0, 100);
    }
}
