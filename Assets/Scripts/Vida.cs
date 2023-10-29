using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida
{
    const float mitad = 0.5f;
    const int porcentajeMaximo = 100;

    public float valorInicial = 0;
    public float actual;
    public float maxima;
    public int porcentaje;
    public bool verPorcentaje;

    public void CalcularNumeroPorcentaje(float proporcionVida)
    {
        if (proporcionVida < mitad)
        {
            porcentaje = Mathf.RoundToInt(proporcionVida * porcentajeMaximo);
        }
        else
        {
            porcentaje = Mathf.FloorToInt(proporcionVida * porcentajeMaximo);
        }
    }
}