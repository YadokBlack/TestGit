using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barras : MonoBehaviour
{
    public Image barraVida;

    public Image fondo;

    public float vidaActual;

    public float vidaMaxima;

    public float altura;

    void Start()
    {
        altura = fondo.rectTransform.rect.height;
    }

    void Update()
    {
        barraVida.rectTransform.offsetMin = new Vector2(barraVida.rectTransform.offsetMin.x, vidaActual / vidaMaxima * altura);
    }
}
