using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barras : MonoBehaviour
{
    public Image barraVida;
    public Image fondoBarraVida;

    public float vidaActual;
    public float vidaMaxima;

    public float alturaBarra;

    void Start()
    {
        alturaBarra = fondoBarraVida.rectTransform.rect.height;
    }

    void Update()
    {
        barraVida.rectTransform.offsetMin = new Vector2(barraVida.rectTransform.offsetMin.x, vidaActual / vidaMaxima * alturaBarra);
    }
}
