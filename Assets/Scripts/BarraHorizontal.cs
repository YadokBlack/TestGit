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

    public TextMeshProUGUI texto;

    void Awake()
    {
        ancho = fondo.rectTransform.rect.width;

        vidaActual = 0;
    }

    void Update()
    {
        if (porcentajeVer)
        {
            float num = vidaActual / vidaMaxima;
            int vidaEnPorcentaje = 0;
            if (num < .5f)
            {
                vidaEnPorcentaje = Mathf.RoundToInt(num * 100f); 
            }
            else
            {
                vidaEnPorcentaje = Mathf.FloorToInt(num * 100f); 
            }
 
            porcentaje = vidaEnPorcentaje;
            texto.text = "Progreso del proyecto: " + vidaEnPorcentaje.ToString("D2") + " %";
        }

        barraVida.rectTransform.offsetMin = new Vector2( vidaActual / vidaMaxima * ancho, barraVida.rectTransform.offsetMin.y);
    }
}