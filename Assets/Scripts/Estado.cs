using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Estado
{
    public float valor;
    public Image barra;
    public Image fondo;
    public float altura;
    public float tiempoEntreCambio;


    public void ActualizaBarra()
    {
        barra.rectTransform.offsetMin = new Vector2(barra.rectTransform.offsetMin.x, valor / 100 * altura);
    }
}
