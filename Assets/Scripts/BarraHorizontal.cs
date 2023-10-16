using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarraHorizontal : MonoBehaviour
{
    const float mitad = 0.5f;
    const int porcentajeMaximo = 100;

    public float valorInicialVida = 0;
    public float vidaActual;
    public float vidaMaxima;
    public int vidaEnPorcentaje;
    public bool porcentajeVer;

    public Image barraVida;

    public Image imagenFondoBarra;
    private float anchoImagenFondoBarra;

    public TextMeshProUGUI mensajeProgreso;

    void Awake()
    {
        anchoImagenFondoBarra = imagenFondoBarra.rectTransform.rect.width;

        vidaActual = valorInicialVida;
    }

    void Update()
    {
        float proporcionVida = vidaActual / vidaMaxima;

        if (porcentajeVer)
        {            
            if (proporcionVida < mitad)
            {
                vidaEnPorcentaje = Mathf.RoundToInt(proporcionVida * porcentajeMaximo); 
            }
            else
            {
                vidaEnPorcentaje = Mathf.FloorToInt(proporcionVida * porcentajeMaximo); 
            }

            mensajeProgreso.text = "Progreso del proyecto: " + vidaEnPorcentaje.ToString("D2") + " %";
        }

        barraVida.rectTransform.offsetMin = new Vector2( proporcionVida * anchoImagenFondoBarra, barraVida.rectTransform.offsetMin.y);
    }
}
