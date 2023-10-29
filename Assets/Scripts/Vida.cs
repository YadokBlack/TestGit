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
    public int porcentaje { 
        get
        {
            if (proporcion < mitad)
            {
                return Mathf.RoundToInt(proporcion * porcentajeMaximo);
            }
            else
            {
                return Mathf.FloorToInt(proporcion * porcentajeMaximo);
            }
        }
    }
    
    public float proporcion {
        get{ return actual / maxima; } }

    public Vida()
    {
        actual = valorInicial;
    }
}