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

    // Start is called before the first frame update
    void Start()
    {
        altura = fondo.rectTransform.rect.height;
    }

    // Update is called once per frame
    void Update()
    {
        barraVida.rectTransform.offsetMin = new Vector2(barraVida.rectTransform.offsetMin.x, vidaActual / vidaMaxima * altura);
    }
}
