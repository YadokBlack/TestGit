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

    public AudioClip sonidoAEjecutar; 

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
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);
            Vector3 escalaDeseada = escalaOriginal * porcentajeEscala;

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


    void Awake()
    {
        haGanado = false;

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

    void Update()
    {
        if (estaColisionando && !control.pantallaNegra && !haGanado )
        {            
            panel.SetActive(true);
            textMeshPro.text = mensajeInteraccion;

            if (Input.GetKeyDown(teclaInteraccion) && 
                    condicion.sed < 100 && 
                    condicion.cansancio < 100 &&
                    condicion.estres < 100 && 
                    condicion.hambre < 100)
            {
                if (audioSource != null && sonidoAEjecutar != null)
                {
                    audioSource.PlayOneShot(sonidoAEjecutar);
                }

                if (objetoDestacado != null && !enTransicion)
                {
                    enTransicion = true;
                    tiempoInicioTransicion = Time.time;
                }

                trabajando = true;

                condicion.CambioEstado(beneficios);

                barraProgreso.vidaActual += incremento + Random.Range(0, incremento);

                ParticleSystem nuevaParticula = Instantiate(particlePrefab, posicionParticulas, Quaternion.identity);

                nuevaParticula.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);

                Destroy(nuevaParticula.gameObject, 1.1f);

                if ( barraProgreso.vidaActual >= barraProgreso.vidaMaxima )
                {
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
        }
        grande();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagDelJugador))
        {
            estaColisionando = true;
            pantalla.SetActive(true);
            panel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(tagDelJugador))
        {
            estaColisionando = false;
            trabajando = false; 
            textMeshPro.text = "";
            pantalla.SetActive(false);
            panel.SetActive(false);
        }
    }
}
