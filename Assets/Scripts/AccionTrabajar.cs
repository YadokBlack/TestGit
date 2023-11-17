using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class AccionTrabajar : AccionBase
{    
    public string mensajeInteraccion = "Pulsa E para programar.";

    public BarraHorizontal barraProgreso;

    public float incremento;

    public ParticleSystem particlePrefab;
    public Vector3 posicionParticulas;

    public GameObject menuVictoria;
    public GameObject pantallaFin;
    
    public bool haGanado;

    public AudioClip sonidoAEjecutar; 
    private AudioSource audioSource;

    public int teclasPulsadas;

    void Awake()
    {
        haGanado = false;
        teclasPulsadas = 0;

        audioSource = GetComponent<AudioSource>();

        if (sonidoAEjecutar == null)
        {
            Debug.LogError("No se ha asignado un AudioClip para reproducir.");
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource no encontrado");
        }
    }

    void Update()
    {
        if (JugadorEnMiZonaActivo())
        {
            if (RealizaUnaAccion())
            {
                teclasPulsadas++;

                zona.colision.mensajeZona = mensajeInteraccion;

                RealizandoAccion();
            }
            else
            {
                teclasPulsadas += PulsaTeclaAccion() ? 1 : 0;

                zona.colision.mensajeZona = condicion.AlgunEstadoAlMaximo() ? GeneraMensaje() : mensajeInteraccion;
            }
        }
        
        objetoAnimado.AnimacionAgrandar();

        ControlPantallaEnZona();
    }

    private void RealizandoAccion()
    {
        reloj.AumentaTiempo(costeTiempo);
        condicion.CambioEstado(beneficios);
        if (objetoAnimado.DestacadoNoEstaEnTransicion()) objetoAnimado.IniciaTransicion();
        
        audioSource.PlayOneShot(sonidoAEjecutar);

        ReproduceClip();

        barraProgreso.vida.actual += incremento + Random.Range(0, incremento);

        GeneraParticula();

        if (Victoria())
        {
            PantallaFinalBueno();
        }
    }

    private bool RealizaUnaAccion()
    {
        return PulsaTeclaAccion() && !condicion.AlgunEstadoAlMaximo();
    }

    private bool JugadorEnMiZonaActivo()
    {
        return JugadorEnMiZona() && !reloj.pantallaNegra && !haGanado;
    }

    private void GeneraParticula()
    {
        ParticleSystem nuevaParticula = Instantiate(particlePrefab, posicionParticulas, Quaternion.identity);
        nuevaParticula.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
        Destroy(nuevaParticula.gameObject, 1.1f);
    }

    private void PantallaFinalBueno()
    {
        pantalla.SetActive(false);
        pantallaFin.SetActive(true);
        zona.control.pausaDeteccion = true;
        haGanado = true;
    }

    private bool PulsaTeclaAccion()
    {
        return Input.GetKeyDown(teclaAccion);
    }

    private string GeneraMensaje()
    {
        string mensaje = "Uf";
        mensaje += condicion.ObtenerMensajeCondicion(", tengo hambre", condicion.HambreAlMaximo());
        mensaje += condicion.ObtenerMensajeCondicion(", estoy sediento", condicion.SedAlMaximo());
        mensaje += condicion.ObtenerMensajeCondicion(", estoy cansado", condicion.CansancioAlMaximo());
        mensaje += condicion.ObtenerMensajeCondicion(", me estoy estresando", condicion.EstresAlMaximo());
        mensaje += ", no puedo continuar.";

        return mensaje;
    }

    private bool Victoria()
    {
        return barraProgreso.vida.actual >= barraProgreso.vida.maxima;
    }
}