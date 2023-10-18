using TMPro;
using UnityEngine;

public class ControlZonas : MonoBehaviour
{
    public LayerMask capaZonas;
    public float radioDeDeteccion = 2f;
    public Transform jugador; 
    public bool jugadorEnZona;
    public string nombreZonaJugador;

    public TextMeshProUGUI textoMensaje;
    public GameObject panelMensaje;

    public bool pausaDeteccion;
    public bool inicia;

    private void Start()
    {
        jugadorEnZona = false;
        pausaDeteccion=false;
        inicia = true;
    }

    private void Update()
    {
        DetectarZonasCercanas();
    }

    private void DetectarZonasCercanas()
    {
        if (pausaDeteccion) return;

        if (jugador == null)
        {
            Debug.LogError("La referencia al jugador no está configurada en el script ControlZonas.");
            return;
        }

        Collider[] colliders = Physics.OverlapSphere(jugador.position, radioDeDeteccion, capaZonas);

        jugadorEnZona = false;
        foreach (Collider collider in colliders)
        {
            ZonaDeColision zona = collider.GetComponent<ZonaDeColision>();
            if (zona != null)
            {
                if (zona.jugadorEnLaZona)
                {
                    MostrarMensaje(zona.mensajeZona);
                    jugadorEnZona = true;
                    nombreZonaJugador = zona.name; 
                }
            }
        }
        if (!jugadorEnZona && !inicia)
        {
            OcultarMensaje();
            nombreZonaJugador = "";
        }
    }

    private void OcultarMensaje()
    {
        textoMensaje.text = "";
        panelMensaje.SetActive(false);
    }

    private void MostrarMensaje(string mensaje)
    {
        panelMensaje.SetActive(true); 

        textoMensaje.text = mensaje;

        if (inicia)
        {
            inicia = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; 
        Gizmos.DrawWireSphere(jugador.position, radioDeDeteccion);
    }
}
