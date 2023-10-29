using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BarraHorizontal : MonoBehaviour
{
    const float mitad = 0.5f;
    const int porcentajeMaximo = 100;

    public Vida vida;

    public Image barraVida;

    public Image imagenFondoBarra;
    private float anchoImagenFondoBarra;

    public TextMeshProUGUI mensajeProgreso;

    void Awake()
    {
        anchoImagenFondoBarra = imagenFondoBarra.rectTransform.rect.width;

        vida.actual = vida.valorInicial;
    }

    void Update()
    {
        float proporcionVida = vida.actual / vida.maxima;

        if (vida.verPorcentaje)
        {            
            if (proporcionVida < mitad)
            {
                vida.porcentaje = Mathf.RoundToInt(proporcionVida * porcentajeMaximo); 
            }
            else
            {
                vida.porcentaje = Mathf.FloorToInt(proporcionVida * porcentajeMaximo); 
            }

            mensajeProgreso.text = "Progreso del proyecto: " + vida.porcentaje.ToString("D2") + " %";
        }

        barraVida.rectTransform.offsetMin = new Vector2( proporcionVida * anchoImagenFondoBarra, barraVida.rectTransform.offsetMin.y);
    }
}
