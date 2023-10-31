using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class AccionTrabajar : AccionBase
{
    const int valorMaximo = 100;

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
                condicion.sed.valor < valorMaximo &&
                condicion.cansancio.valor < valorMaximo &&
                condicion.estres.valor < valorMaximo &&
                condicion.hambre.valor < valorMaximo)
            {

                teclasPulsadas++;

                zonaControlada.mensajeZona = mensajeInteraccion;

                reloj.AumentaTiempo(costeTiempo);
                condicion.CambioEstado(beneficios);
                if (objetoAnimado.destacado != null && !objetoAnimado.enTransicion)
                {
                    objetoAnimado.enTransicion = true;
                    objetoAnimado.tiempoInicioTransicion = Time.time;
                }

                if (audioSource != null && sonidoAEjecutar != null)
                {
                    audioSource.PlayOneShot(sonidoAEjecutar);
                }

                ReproduceClip();

                barraProgreso.vida.actual += incremento + Random.Range(0, incremento);

                ParticleSystem nuevaParticula = Instantiate(particlePrefab, posicionParticulas, Quaternion.identity);

                nuevaParticula.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

                Destroy(nuevaParticula.gameObject, 1.1f);

                // victoría
                if (barraProgreso.vida.actual >= barraProgreso.vida.maxima)
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

                if (condicion.hambre.valor == valorMaximo || condicion.sed.valor == valorMaximo ||
                    condicion.cansancio.valor == valorMaximo || condicion.estres.valor == valorMaximo)
                {
                    zonaControlada.mensajeZona = "Uf";

                    if (condicion.hambre.valor == valorMaximo)
                    {
                        zonaControlada.mensajeZona += ", tengo hambre";
                    }
                    if (condicion.sed.valor == valorMaximo)
                    {
                        zonaControlada.mensajeZona += ", estoy sediento";
                    }
                    if (condicion.cansancio.valor == valorMaximo)
                    {
                        zonaControlada.mensajeZona += ", estoy cansado";
                    }
                    if (condicion.estres.valor == valorMaximo)
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
        
        objetoAnimado.AnimacionAgrandar();

        ControlPantallaEnZona();
    }
}