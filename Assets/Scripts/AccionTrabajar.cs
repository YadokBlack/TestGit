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

    // pantalla de victoria
    public GameObject victoria;
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
            Debug.LogWarning("No se ha asignado un AudioClip para reproducir.");
        }
    }

    void Update()
    {
        if (zonaControl.jugadorEnZona && zonaControl.nombreZonaJugador == zonaControlada.name && !reloj.pantallaNegra && !haGanado)
        {
            if (Input.GetKeyDown(teclaAccion) &&
                condicion.sed < 100 &&
                condicion.cansancio < 100 &&
                condicion.estres < 100 &&
                condicion.hambre < 100)
            {

                teclasPulsadas++;

                zonaControlada.mensajeZona = mensajeInteraccion;

                reloj.AumentaTiempo(costeTiempo);
                condicion.CambioEstado(beneficios);
                if (objetoDestacado != null && !enTransicion)
                {
                    enTransicion = true;
                    tiempoInicioTransicion = Time.time;
                }

                if (audioSource != null && sonidoAEjecutar != null)
                {
                    audioSource.PlayOneShot(sonidoAEjecutar);
                }

                ReproduceClip();

                barraProgreso.vidaActual += incremento + Random.Range(0, incremento);

                ParticleSystem nuevaParticula = Instantiate(particlePrefab, posicionParticulas, Quaternion.identity);

                nuevaParticula.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

                Destroy(nuevaParticula.gameObject, 1.1f);

                // victor�a
                if (barraProgreso.vidaActual >= barraProgreso.vidaMaxima)
                {
                    // AQUI PANTALLA FINAL BUENO !!!

                    pantalla.SetActive(false);
                    pantallaFin.SetActive(true);
                    zonaControl.pausaDeteccion = true;
                    haGanado = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(teclaAccion))
                {
                    teclasPulsadas++;
                }

                if (condicion.hambre == 100 || condicion.sed == 100 ||
                    condicion.cansancio == 100 || condicion.estres == 100)
                {
                    zonaControlada.mensajeZona = "Uf";

                    if (condicion.hambre == 100)
                    {
                        zonaControlada.mensajeZona += ", tengo hambre";
                    }
                    if (condicion.sed == 100)
                    {
                        zonaControlada.mensajeZona += ", estoy sediento";
                    }
                    if (condicion.cansancio == 100)
                    {
                        zonaControlada.mensajeZona += ", estoy cansado";
                    }
                    if (condicion.estres == 100)
                    {
                        zonaControlada.mensajeZona += ", me estoy estresando";
                    }

                    zonaControlada.mensajeZona += ", no puedo continuar.";
                }
                else
                {
                    zonaControlada.mensajeZona = mensajeInteraccion;
                }
            }
        }
        
        AnimacionAgrandar();

        ControlPantallaEnZona();
    }
}