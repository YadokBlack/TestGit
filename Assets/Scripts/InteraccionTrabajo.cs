using UnityEngine;
using TMPro;

public class InteraccionTrabajo : MonoBehaviour
{
    public GameObject pantalla;
    public GameObject pantallaFin;
    public GameObject panel;
    public TextMeshProUGUI textMeshPro;
    public string mensajeInteraccion = "Pulsa E para trabajar";
    public string tagDelJugador = "Player";
    public KeyCode teclaInteraccion = KeyCode.E;

    public float[] beneficios = null;

    private bool estaColisionando = false;
    private bool trabajando = false;
    private int progresoTrabajo = 0;

    public BarraHorizontal barraProgreso;

    public float incremento;

    public ParticleSystem particlePrefab;
    public Vector3 posicionParticulas;

    public AudioClip sonidoAEjecutar; // Asigna el clip de sonido que deseas reproducir en el Inspector.

    private AudioSource audioSource;

    public GameObject victoria;

    public PasoDelTiempo control;

    public Condicion condicion;

    public GameObject teclado;

    
    public bool haGanado;


    public GameObject objetoDestacado;
    private Vector3 escalaOriginal;
    private bool enTransicion;
    private float tiempoInicioTransicion;
    public float duracionTransicion = 0.30f;
    public float porcentajeEscala;

    public void grande()
    {
        if (enTransicion)
        {
            // Debug.Log("Esta agrandando");

            // vamos a agrandar
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);
            Vector3 escalaDeseada = escalaOriginal * porcentajeEscala;

            objetoDestacado.transform.localScale = Vector3.Lerp(escalaOriginal, escalaDeseada, fraccionDeTiempo);

            if (fraccionDeTiempo == 1f)
            {
                enTransicion = false;
                // Debug.Log("Para");
            }
        }
        if (!enTransicion && objetoDestacado != null)
        {
            // Restaurar el tamaño original del objeto cuando no está agrandándose.
            objetoDestacado.transform.localScale = escalaOriginal;
        }
    }


    void Awake()
    {
        haGanado = false;

        // Obtén el componente AudioSource del objeto que tiene este script.
        audioSource = GetComponent<AudioSource>();

        // Asegúrate de que se haya asignado un AudioClip en el Inspector.
        if (sonidoAEjecutar == null)
        {
            Debug.LogWarning("No se ha asignado un AudioClip para reproducir.");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        if (objetoDestacado != null)
        {
            escalaOriginal = objetoDestacado.transform.localScale;
        }
        enTransicion = false;
    }

    void Update()
    {
        if (estaColisionando && !control.pantallaNegra && !haGanado )
        {            
            // Mostrar el mensaje por pantalla
            // Debug.Log(mensajeInteraccion);
            panel.SetActive(true);
            textMeshPro.text = mensajeInteraccion;

            // Comprobar si se pulsa la tecla E para trabajar
            if (Input.GetKeyDown(teclaInteraccion) && 
                    condicion.sed < 100 && 
                    condicion.cansancio < 100 &&
                    condicion.estres < 100 && 
                    condicion.hambre < 100)
            {
                // Reproduce el sonido
                if (audioSource != null && sonidoAEjecutar != null)
                {
                    audioSource.PlayOneShot(sonidoAEjecutar);
                }

                if (objetoDestacado != null && !enTransicion)
                {
                    //  Debug.Log("Inicia");
                    enTransicion = true;
                    tiempoInicioTransicion = Time.time;
                }


                trabajando = true;

                condicion.CambioEstado(beneficios);

                barraProgreso.vidaActual += incremento + Random.Range(0, incremento);
                // Instancia la partícula en la posición del objeto que tiene este script
               // Instantiate(particlePrefab, posicionParticulas, Quaternion.identity);

                ParticleSystem nuevaParticula = Instantiate(particlePrefab, posicionParticulas, Quaternion.identity);

                // Aplica la rotación de -90 grados en el eje X a la partícula
                nuevaParticula.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                // Destruye la partícula después de un tiempo especificado
                Destroy(nuevaParticula.gameObject, 1.1f);

                if ( barraProgreso.vidaActual >= barraProgreso.vidaMaxima )
                {
                    // muestra el pantalla del final partida
                    // fin partida buena
                    victoria.SetActive(true);
                    pantalla.SetActive(false);
                    pantallaFin.SetActive(true);
                    control.Pausar();
                    haGanado = true;
                }

            }
            else             
            {
                if (condicion.hambre == 100 || condicion.sed == 100
                    || condicion.cansancio == 100 || condicion.estres == 100)
                {
                    textMeshPro.text = "Uf";

                    if (condicion.hambre == 100)
                    {
                        textMeshPro.text += ", tengo hambre";
                    }
                    if (condicion.sed == 100)
                    {
                        textMeshPro.text += ", estoy sediento";
                    }
                    if (condicion.cansancio == 100)
                    {
                        textMeshPro.text += ", estoy cansado";
                    }
                    if (condicion.estres == 100)
                    {
                        textMeshPro.text += ", me estoy estresando";
                    }

                    textMeshPro.text += ", no puedo continuar.";
                }



                if (Input.GetKeyUp(teclaInteraccion))
                {
                    trabajando = false;
                }                    
            }
            /*
            // Incrementar la variable de progreso mientras se trabaja
            if (trabajando)
            {
                progresoTrabajo++;
                Debug.Log("Progreso: " + progresoTrabajo);
            }
            */
        }
        grande();
    }

    void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto que colisiona tiene la etiqueta del jugador
        if (other.CompareTag(tagDelJugador))
        {
            estaColisionando = true;
            pantalla.SetActive(true);
            panel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Comprobar si el objeto que sale de la colisión tiene la etiqueta del jugador
        if (other.CompareTag(tagDelJugador))
        {
            estaColisionando = false;
            trabajando = false; // Detener el trabajo si el jugador sale de la colisión
            // progresoTrabajo = 0; // Reiniciar el progreso
            textMeshPro.text = "";
            pantalla.SetActive(false);
            panel.SetActive(false);
        }
    }
}
