using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Entretenimiento : MonoBehaviour
{
    public string mensajeInteraccion = "Pulsa E para mirar por la ventana";
    public TextMeshProUGUI textMeshPro;
    public GameObject panel;
    public KeyCode teclaInteraccion = KeyCode.E;
    public string tagDelJugador = "Player";
    public int costeTiempo = 5;
    public float[] beneficios = null;

    private bool estaColisionando = false;

    public PasoDelTiempo control;

    public Condicion condicion;

    public GameObject pantalla;

    public GameObject objetoDestacado;
    private Vector3 escalaOriginal;
    private bool enTransicion;
    private float tiempoInicioTransicion;
    public float duracionTransicion = 0.30f;
    public float escalaPorcentaje;

    public AudioSource audioSource;
    public AudioClip sonido;


    public void grande()
    {
        if (enTransicion)
        {
           // Debug.Log("Esta agrandando");

            // vamos a agrandar
            float tiempoTranscurrido = Time.time - tiempoInicioTransicion;
            float fraccionDeTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionTransicion);
            Vector3 escalaDeseada = escalaOriginal * escalaPorcentaje;

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

    // Start is called before the first frame update
    void Start()
    {
        if ( objetoDestacado != null )
        {
            escalaOriginal = objetoDestacado.transform.localScale;
        }        
        enTransicion = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (estaColisionando && !control.pantallaNegra)
        {
            // Mostrar el mensaje por pantalla
            // Debug.Log(mensajeInteraccion);

            textMeshPro.text = mensajeInteraccion;

            if (Input.GetKeyDown(teclaInteraccion))
            {
                control.AumentaTiempo(costeTiempo);
                condicion.CambioEstado(beneficios);
                if (objetoDestacado != null && !enTransicion)
                {
                  //  Debug.Log("Inicia");
                    enTransicion = true;
                    tiempoInicioTransicion = Time.time;
                }
                // Reproduce el sonido cuando se presiona la tecla de interacción.
                if (audioSource != null)
                {
                    audioSource.PlayOneShot(sonido);
                }
            }            
        }
        grande();
        if (estaColisionando && pantalla != null)
        {
            pantalla.SetActive(true);
        }else if (!estaColisionando && pantalla != null)
        {
            pantalla.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Comprobar si el objeto que colisiona tiene la etiqueta del jugador
        if (other.CompareTag(tagDelJugador))
        {
            estaColisionando = true;
            panel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Comprobar si el objeto que sale de la colisión tiene la etiqueta del jugador
        if (other.CompareTag(tagDelJugador))
        {
            estaColisionando = false;
            textMeshPro.text = "";
            panel.SetActive(false);
        }
    }
}
