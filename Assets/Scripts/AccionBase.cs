using UnityEngine;


public class AccionBase : MonoBehaviour
{
    public Color colorPunto = Color.red;

    public KeyCode teclaAccion = KeyCode.E;

    public int costeTiempo = 5;
    public float[] beneficios = null;

    public ObjetoAnimado objetoAnimado;

    public ControlZonas zonaControl;
    public ZonaDeColision zonaControlada;

    public PasoDelTiempo reloj;

    public Condicion condicion;

    public GameObject pantalla;

    public AudioManager audioManager;
    public int numClip = 0;

    public void Start()
    {
        objetoAnimado.Inicializar();
    }

    public void ReproduceClip()
    {
        if (audioManager != null)
        {
            audioManager.PlayAudioByIndex(numClip);
        }
    }

    public void ControlPantallaEnZona()
    {
        if (zonaControl.jugadorEnZona && zonaControl.nombreZonaJugador == zonaControlada.name && pantalla != null)
        {
            pantalla.SetActive(true);
        }
        else if (!zonaControl.jugadorEnZona && pantalla != null)
        {
            pantalla.SetActive(false);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = colorPunto; // Color del punto
        Gizmos.DrawSphere(objetoAnimado.posicion, 0.01f); // Dibujar un punto en la posición deseada
    }
}
