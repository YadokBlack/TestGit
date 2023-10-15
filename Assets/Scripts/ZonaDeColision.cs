using UnityEngine;

/*
 *   Para crear zonas de accion.
 *   
 *   A tener en cuenta:
 *   Todas las zonas deben estar en una misma capa.
 *   Requiere que el jugador tenga la etiqueta Player.
 */


public class ZonaDeColision : MonoBehaviour
{
    // public string nombre;
    public bool jugadorEnLaZona = false;
    public string mensajeZona = ""; // Puedes establecer un mensaje predeterminado.

    // Implementa los métodos OnTriggerEnter y OnTriggerExit para cambiar el estado.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnLaZona = true;
            // Debug.Log("jugador dentro");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnLaZona = false;
            // Debug.Log("jugador sale");
        }
    }
}