using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 *  Similar a Accion pero con las cosas concretas del juego principal
 *  
 *  Al poner esto de arriba me indica que quizas pueda reducir o realizar 
 *  de otro modo accion trabajar para que utilice parte de acciones.
 */

public class AccionTrabajar : MonoBehaviour
{
    public KeyCode teclaAccion = KeyCode.E;
    public string mensajeInteraccion = "Pulsa E para programar.";

    public BarraHorizontal barraProgreso;

    public float incremento;

    public ParticleSystem particlePrefab;
    public Vector3 posicionParticulas;

    // pantalla de victoria
    public GameObject victoria;
    public GameObject pantallaFin;

    public int costeTiempo = 5;
    public float[] beneficios = null;

    public GameObject objetoDestacado;
    private Vector3 escalaOriginal;
    private bool enTransicion;
    private float tiempoInicioTransicion;
    public float duracionTransicion = 0.30f;
    public float escalaPorcentaje;

    public ControlZonas zonaControl;
    public ZonaDeColision zonaControlada;

    public PasoDelTiempo control;

    public Condicion condicion;

    [SerializeField]
    private GameObject pantalla;

    public AudioManager audioManager;
    public int numClip=0;

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

    void Start()
    {
        if (objetoDestacado != null)
        {
            escalaOriginal = objetoDestacado.transform.localScale;
        }
        enTransicion = false;
    }

    public void AnimacionAgrandar()
    {
        if (enTransicion)
        {
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);
            Vector3 escalaDeseada = escalaOriginal * escalaPorcentaje;

            objetoDestacado.transform.localScale = Vector3.Lerp(escalaOriginal, escalaDeseada, fraccionDeTiempo);

            if (fraccionDeTiempo == 1f)
            {
                enTransicion = false;
            }
        }
        if (!enTransicion && objetoDestacado != null)
        {
            objetoDestacado.transform.localScale = escalaOriginal;
        }
    }

    void Update()
    {
        if (zonaControl.jugadorEnZona && zonaControl.nombreZonaJugador == zonaControlada.name && !control.pantallaNegra && !haGanado)
        {
            if (Input.GetKeyDown(teclaAccion) &&
                condicion.sed < 100 &&
                condicion.cansancio < 100 &&
                condicion.estres < 100 &&
                condicion.hambre < 100)
            {

                teclasPulsadas++;

                zonaControlada.mensajeZona = mensajeInteraccion;

                control.AumentaTiempo(costeTiempo);
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

                if (audioManager != null)
                {
                    audioManager.PlayAudioByIndex(numClip);
                }

                barraProgreso.vidaActual += incremento + Random.Range(0, incremento);

                ParticleSystem nuevaParticula = Instantiate(particlePrefab, posicionParticulas, Quaternion.identity);

                nuevaParticula.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

                Destroy(nuevaParticula.gameObject, 1.1f);

                // victoría
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

        if (zonaControl.jugadorEnZona && zonaControl.nombreZonaJugador == zonaControlada.name && pantalla != null)
        {
            pantalla.SetActive(true);
        }
        else if (!zonaControl.jugadorEnZona && pantalla != null)
        {
            pantalla.SetActive(false);
        }
    }
}