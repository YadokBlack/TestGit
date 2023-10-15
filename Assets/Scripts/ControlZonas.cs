using TMPro;
using UnityEngine;

/*
 *  Se encarga de controlar las zonas que hay cercas y cual de ellas esta activa
 *  De la zona activa muestra el mensaje correspondiente. 
 *  Activando o desactivando el cuadro de texto segun se encuentre el jugador
 *  
 *  Todas las zonas que tendrá en cuenta se en cuentran en la capa capaZonas
 * 
 *  Utiliza ZonaDeColision.
 */

public class ControlZonas : MonoBehaviour
{
    public LayerMask capaZonas;
    public float radioDeDeteccion = 2f;
    public Transform jugador; // Referencia al jugador
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

        // Debug.Log("Comprobando..");

        jugadorEnZona = false;
        foreach (Collider collider in colliders)
        {
            ZonaDeColision zona = collider.GetComponent<ZonaDeColision>();
            if (zona != null)
            {
                if (zona.jugadorEnLaZona)
                {
                    // Mostrar el mensaje de la zona
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
        // Aquí puedes mostrar el mensaje en tu cuadro de texto.
        textoMensaje.text = mensaje;

        if (inicia)
        {
            inicia = false;
        }
    }

    // Este método se llama en el editor de Unity para dibujar la esfera de detección
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue; // Color de la esfera de detección
        Gizmos.DrawWireSphere(jugador.position, radioDeDeteccion);
    }
}
