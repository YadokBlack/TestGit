using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    public float velocidad = 5.0f;

    public float sensibilidadMouse = 5.0f;
    private float rotacionVertical = 0.0f;

    private bool pausado;

    void Awake()
    {       
        pausado = true;        
    }

    public void Pausar()
    {
        pausado = true;
    }

    public void Iniciar()
    {
        pausado = false;
    }

    void Update()
    {
        float rotacionHorizontal = Input.GetAxis("Mouse X") * sensibilidadMouse;
        rotacionVertical -= Input.GetAxis("Mouse Y") * sensibilidadMouse;
        rotacionVertical = Mathf.Clamp(rotacionVertical, -90.0f, 90.0f); 

        transform.Rotate(0.0f, rotacionHorizontal, 0.0f);
        Camera.main.transform.localRotation = Quaternion.Euler(rotacionVertical, 0.0f, 0.0f);

        if (pausado) return;

        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical) * velocidad * Time.deltaTime;
        transform.Translate(movimiento);
    }
}
