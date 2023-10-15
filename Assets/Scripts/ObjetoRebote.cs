using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRebote : MonoBehaviour
{

    public Vector3 tama�oInicial;
    public Vector3 tama�oFinal;
    public AnimationCurve curvaDeCrecimiento = AnimationCurve.Linear(0, 0, 1, 1);
    public float duraci�n = 2.0f; // Duraci�n en segundos

    private float tiempoPasado = 0.0f;

    private void Start()
    {
        tiempoPasado = 0.0f;
        // Establecer el tama�o inicial al inicio
        transform.localScale = tama�oInicial;
    }

    private void Update()
    {
        // Aumentar el tiempo pasado
        tiempoPasado += Time.deltaTime;

        // Calcular el factor de interpolaci�n entre 0 y 1 basado en el tiempo pasado y la curva
        float factorInterpolaci�n = Mathf.Clamp01(tiempoPasado / duraci�n);
        float factorCurva = curvaDeCrecimiento.Evaluate(factorInterpolaci�n);

        // Interpolar el tama�o entre el tama�o inicial y final
        Vector3 nuevoTama�o = Vector3.Lerp(tama�oInicial, tama�oFinal, factorCurva);

        // Aplicar el nuevo tama�o al objeto
        transform.localScale = nuevoTama�o;

        // Cuando alcanzamos la duraci�n, puedes reiniciar o hacer algo m�s
        if (factorInterpolaci�n >= 1.0f)
        {
            // Aqu� puedes reiniciar el proceso si lo deseas
            // tiempoPasado = 0.0f;
        }
    }
}
