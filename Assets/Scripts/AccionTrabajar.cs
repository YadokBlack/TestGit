using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 *  Similar a Accion pero con las cosas concretas del juego principal
 */


public class AccionTrabajar : MonoBehaviour
{
    public KeyCode teclaAccion = KeyCode.E;
    public string mensajeInteraccion = "Pulsa E para programar.";

    // para poder mostrar el progreso del trabajo
    public BarraHorizontal barraProgreso;
    // lo que aumenta la barra con un click
    public float incremento;

    public ParticleSystem particlePrefab;
    public Vector3 posicionParticulas;

    // pantalla de victoria
    public GameObject victoria;
    public GameObject pantallaFin;

    // para definir coste de la accion
    public int costeTiempo = 5;
    public float[] beneficios = null;

    // para la animacion del objeto a destacar
    public GameObject objetoDestacado;
    private Vector3 escalaOriginal;
    private bool enTransicion;
    private float tiempoInicioTransicion;
    public float duracionTransicion = 0.30f;
    public float escalaPorcentaje;

    // para comprobar la colision del jugador
    public ControlZonas zonaControl;
    public ZonaDeColision zonaControlada;

    // para controlar cuando pasa el dia
    public ControladorDeHora control;

    // para poder acceder a los estados del jugador
    public Condicion condicion;

    // solo rellenar en el caso de tener alguna pantalla para activar
    [SerializeField]
    private GameObject pantalla;

    public AudioManager audioManager;
    public int numClip=0;


    public bool haGanado;

    // el objeto tendra un audiosource para reproducir el sonido de teclear
    public AudioClip sonidoAEjecutar; // Asigna el clip de sonido que deseas reproducir en el Inspector.
    private AudioSource audioSource;

    // variable para contar el numero de teclas pulsadas
    public int teclasPulsadas;

   // public TextMeshProUGUI textoResultado;


    void Awake()
    {
        haGanado = false;
        teclasPulsadas = 0;

        // Obtén el componente AudioSource del objeto que tiene este script.
        audioSource = GetComponent<AudioSource>();

        // Asegúrate de que se haya asignado un AudioClip en el Inspector.
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
            // vamos a agrandar
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
            // Restaurar el tamaño original del objeto cuando no está agrandándose.
            objetoDestacado.transform.localScale = escalaOriginal;
        }
    }


    void Update()
    {
        if (zonaControl.jugadorEnZona && zonaControl.nombreZonaJugador == zonaControlada.name && !control.pantallaNegra && !haGanado)
        {
            // tecla de accion y bloquea si algo llegó al 100
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
                    //  Debug.Log("Inicia");
                    enTransicion = true;
                    tiempoInicioTransicion = Time.time;
                }

                // Reproduce el sonido de la tecla
                if (audioSource != null && sonidoAEjecutar != null)
                {
                    audioSource.PlayOneShot(sonidoAEjecutar);
                }

                if (audioManager != null)
                {
                    // Reproduce el sonido cuando se presiona la tecla de interacción.
                    audioManager.PlayAudioByIndex(numClip);
                }

                // inicio trabajo
                barraProgreso.vidaActual += incremento + Random.Range(0, incremento);

                ParticleSystem nuevaParticula = Instantiate(particlePrefab, posicionParticulas, Quaternion.identity);

                // Aplica la rotación de -90 grados en el eje X a la partícula
                nuevaParticula.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                // Destruye la partícula después de un tiempo especificado
                Destroy(nuevaParticula.gameObject, 1.1f);

                // victoría
                if (barraProgreso.vidaActual >= barraProgreso.vidaMaxima)
                {
                    // AQUI PANTALLA FINAL BUENO !!!
                    // textoResultado.text = "¡Has logrado acabar tu juego! \r\n En " + control.diasDelJuego.ToString() 
                    //                        + " dias y " + control.horasDelJuego.ToString("D2") + ":" + control.minutosDelJuego.ToString("D2") 
                    //                        + " horas.\r\nCon un total de " + teclasPulsadas + " acciones.";



                    // fin partida buena
                  //  victoria.SetActive(true);

                    // intercambia la pantalla encendida para mostrar el codigo final
                    pantalla.SetActive(false);
                    pantallaFin.SetActive(true);
                    //  control.Pausar();

                    zonaControl.pausaDeteccion = true;
                    haGanado = true;
                }

                // fin trabajo

            }
            else
            {
                // cuenta si intentó pulsar y tenía alguna condicion al 100
                if (Input.GetKeyDown(teclaAccion))
                {
                    teclasPulsadas++;
                }

                if (condicion.hambre == 100 || condicion.sed == 100 ||
                    condicion.cansancio == 100 || condicion.estres == 100)
                {
                    // textMeshPro.text = "Uf";
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

        // para encencer la pantalla del Amstrad
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

/*
 * jugadorEnzona indica que esta dentro de alguna zona, que puede que no sea la nuestra
 * es por ello que si esta en zona debe comprobar el nombre de la zona para verificar 
 * que es la nuestra.
 * 
 */