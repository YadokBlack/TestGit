using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BarraHorizontal : MonoBehaviour
{    
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
            vida.CalcularNumeroPorcentaje(proporcionVida);

            mensajeProgreso.text = "Progreso del proyecto: " + vida.porcentaje.ToString("D2") + " %";
        }

        barraVida.rectTransform.offsetMin = new Vector2( proporcionVida * anchoImagenFondoBarra, barraVida.rectTransform.offsetMin.y);
    }
}
