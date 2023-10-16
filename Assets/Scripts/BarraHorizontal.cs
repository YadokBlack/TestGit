using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarraHorizontal : MonoBehaviour
{
    const float mitad = 0.5f;

    public float valorInicialVida = 0;
    public float vidaActual;
    public float vidaMaxima;
    public int vidaEnPorcentaje;
    public bool porcentajeVer;

    public Image barraVida;

    public Image imagenFondoBarra;
    private float anchoImagenFondoBarra;

    public TextMeshProUGUI texto;

    void Awake()
    {
        anchoImagenFondoBarra = imagenFondoBarra.rectTransform.rect.width;

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

        barraVida.rectTransform.offsetMin = new Vector2( vidaActual / vidaMaxima * anchoImagenFondoBarra, barraVida.rectTransform.offsetMin.y);
    }
}
