using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarraHorizontal : MonoBehaviour
{
    public Image barraVida;

    public Image fondo;

    public float vidaActual;

    public float vidaMaxima;

    public float ancho;

    public bool porcentajeVer;

    public int porcentaje;

    // public float porcentaje;

    public TextMeshProUGUI texto;

    // Start is called before the first frame update
    void Awake()
    {
        ancho = fondo.rectTransform.rect.width;

        // asigna un valor actual para la barra de progreso
        vidaActual = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (porcentajeVer)
        {
            float num = vidaActual / vidaMaxima;
            int vidaEnPorcentaje = 0;
            if (num < .5f)
            {
                vidaEnPorcentaje = Mathf.RoundToInt(num * 100f); // Redondear a un número entero
            }
            else
            {
                vidaEnPorcentaje = Mathf.FloorToInt(num * 100f); // Redondear hacia abajo a un número entero
            }
 
            porcentaje = vidaEnPorcentaje;
            texto.text = "Progreso del proyecto: " + vidaEnPorcentaje.ToString("D2") + " %";

           //  Debug.Log("Vida:" + vidaActual + " Max:" + vidaMaxima);
        }

        //  barraVida.rectTransform.offsetMin = new Vector2(barraVida.rectTransform.offsetMin.x, vidaActual / vidaMaxima * altura);
        barraVida.rectTransform.offsetMin = new Vector2( vidaActual / vidaMaxima * ancho, barraVida.rectTransform.offsetMin.y);
    }
}
