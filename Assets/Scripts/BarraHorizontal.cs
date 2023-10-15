using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarraHorizontal : MonoBehaviour
{
    const float mitad = 0.5f;

    public float valorInicialVida = 0;

    public Image barraVida;

    public Image imagenFondoBarra;

    public float vidaActual;

    public float vidaMaxima;

    private float ancho;

    public bool porcentajeVer;

    public int vidaEnPorcentaje;

    public TextMeshProUGUI texto;

    void Awake()
    {
        ancho = imagenFondoBarra.rectTransform.rect.width;

        vidaActual = valorInicialVida;
    }

    void Update()
    {
        if (porcentajeVer)
        {
            float num = vidaActual / vidaMaxima;

            if (num < mitad)
            {
                vidaEnPorcentaje = Mathf.RoundToInt(num * 100f); 
            }
            else
            {
                vidaEnPorcentaje = Mathf.FloorToInt(num * 100f); 
            }

            texto.text = "Progreso del proyecto: " + vidaEnPorcentaje.ToString("D2") + " %";
        }

        barraVida.rectTransform.offsetMin = new Vector2( vidaActual / vidaMaxima * ancho, barraVida.rectTransform.offsetMin.y);
    }
}
