using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Acciones : AccionBase
{
    private float alturaMaxima = 0f;

    public AccionTrabajar accionTrabajo;

    private float ObtenerAlturaMaxima(Transform objetoPadre)
    {
        float alturaObjetoPadre = 0f;

        foreach (Transform hijo in objetoPadre)
        {           
            MeshRenderer meshRenderer = hijo.GetComponent<MeshRenderer>();
            if (meshRenderer != null)
            {                
                float alturaHijo = hijo.localPosition.y + (meshRenderer.bounds.size.y * 0.5f);
                
                alturaMaxima = Mathf.Max(alturaMaxima, alturaHijo);
            }

            float alturaHijoRecursiva = ObtenerAlturaMaxima(hijo);

            alturaMaxima = Mathf.Max(alturaMaxima, alturaHijoRecursiva);

            alturaObjetoPadre = Mathf.Max(alturaObjetoPadre, alturaHijoRecursiva);
        }
        return alturaObjetoPadre;
    }

    void Update()
    {
        if (zona.control.jugadorEnZona && zona.control.nombreZonaJugador == zona.colision.name && !reloj.pantallaNegra)
        {
            if (Input.GetKeyDown(teclaAccion))
            {
                accionTrabajo.teclasPulsadas++;

                reloj.AumentaTiempo(costeTiempo);
                condicion.CambioEstado(beneficios);
                if (objetoAnimado.destacado != null && !objetoAnimado.enTransicion)
                {                   
                    objetoAnimado.enTransicion = true;
                    objetoAnimado.tiempoInicioTransicion = Time.time;
                }

                ReproduceClip();
            }
        }
        
        objetoAnimado.AnimacionAgrandar();

        ControlPantallaEnZona();
    }
}
