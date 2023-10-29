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
    public bool verPorcentaje;

    public TextMeshProUGUI mensajeProgreso;

    void Awake()
    {
        anchoImagenFondoBarra = imagenFondoBarra.rectTransform.rect.width;

        vida.actual = vida.valorInicial;
    }

    void Update()
    {
        if (verPorcentaje)
        {
            mensajeProgreso.text = "Progreso del proyecto: " + vida.porcentaje.ToString("D2") + " %";
        }

        barraVida.rectTransform.offsetMin = new Vector2( vida.proporcion * anchoImagenFondoBarra, barraVida.rectTransform.offsetMin.y);
    }
}
