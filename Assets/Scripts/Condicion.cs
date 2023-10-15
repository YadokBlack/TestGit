using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*  Controla la condicion del jugador
 *  
 *  niveles de estres, hambre, sed, cansancio
 */

public class Condicion : MonoBehaviour
{
    // todos los niveles considero que tienen un maximo de 100

    public float estres;
    public Image barraVidaEstres;
    public Image fondoEstres;
    public float alturaE;
    public float hambre;
    public Image barraVidaHambre;
    public Image fondoHambre;
    public float alturaH;
    public float sed;
    public Image barraVidaSed;
    public Image fondoSed;
    public float alturaS;
    public float cansancio;
    public Image barraVidaCansancio;
    public Image fondoCansancio;
    public float alturaC;

    // Variables para controlar el tiempo entre cambios de estado
    public float tiempoEntreCambioEstres = 10f;  // Cambia cada 10 segundos
    public float tiempoEntreCambioHambre = 15f;  // Cambia cada 15 segundos
    public float tiempoEntreCambioSed = 20f;     // Cambia cada 20 segundos
    public float tiempoEntreCambioCansancio = 30f; // Cambia cada 30 segundos

    private float tiempoUltimoCambioEstres;
    private float tiempoUltimoCambioHambre;
    private float tiempoUltimoCambioSed;
    private float tiempoUltimoCambioCansancio;

    public ControladorDeHora control;

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

        // Inicializa los tiempos de cambio
        tiempoUltimoCambioEstres = Time.time;
        tiempoUltimoCambioHambre = Time.time;
        tiempoUltimoCambioSed = Time.time;
        tiempoUltimoCambioCansancio = Time.time;
    }

    private void Update()
    {
        // Calcula el tiempo actual
        float tiempoActual = Time.time;

        if ( control.pantallaNegra )
        {
            cansancio -= 5;
            estres -= 5f;
        }


        // Verifica si es hora de cambiar el estado de estrés
        if (tiempoActual - tiempoUltimoCambioEstres >= tiempoEntreCambioEstres)
        {
            estres += 1 + Random.Range(0.1f, aumentoAleatorio);  // Incrementa el estrés
            tiempoUltimoCambioEstres = tiempoActual;  // Actualiza el tiempo del último cambio
        }

        // Verifica si es hora de cambiar el estado de hambre
        if (tiempoActual - tiempoUltimoCambioHambre >= tiempoEntreCambioHambre)
        {
            hambre += 1 + Random.Range(0.1f, aumentoAleatorio);  // Incrementa el hambre
            tiempoUltimoCambioHambre = tiempoActual;  // Actualiza el tiempo del último cambio
        }

        // Verifica si es hora de cambiar el estado de sed
        if (tiempoActual - tiempoUltimoCambioSed >= tiempoEntreCambioSed)
        {
            sed += 1 + Random.Range(0.1f, aumentoAleatorio);  // Incrementa la sed
            tiempoUltimoCambioSed = tiempoActual;  // Actualiza el tiempo del último cambio
        }

        // Verifica si es hora de cambiar el estado de cansancio
        if (tiempoActual - tiempoUltimoCambioCansancio >= tiempoEntreCambioCansancio)
        {
            cansancio += 1 + Random.Range(0.1f, aumentoAleatorio);  // Incrementa el cansancio
            tiempoUltimoCambioCansancio = tiempoActual;  // Actualiza el tiempo del último cambio
        }

        // Asegúrate de que los estados no superen el valor máximo de 100
        AcotarEstados();

       // Debug.Log("Sed : " + sed / 100 * alturaS);

        barraVidaSed.rectTransform.offsetMin = new Vector2(barraVidaSed.rectTransform.offsetMin.x, sed / 100 * alturaS);
        barraVidaHambre.rectTransform.offsetMin = new Vector2(barraVidaHambre.rectTransform.offsetMin.x, hambre / 100 * alturaH);
        barraVidaCansancio.rectTransform.offsetMin = new Vector2(barraVidaCansancio.rectTransform.offsetMin.x, cansancio / 100 * alturaC);
        barraVidaEstres.rectTransform.offsetMin = new Vector2(barraVidaEstres.rectTransform.offsetMin.x, estres / 100 * alturaE);
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
