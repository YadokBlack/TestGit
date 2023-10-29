using UnityEngine;

public class ZonaDeColision : MonoBehaviour
{
    public bool jugadorEnLaZona = false;
    public string mensajeZona = ""; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnLaZona = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEnLaZona = false;
        }
    }
}