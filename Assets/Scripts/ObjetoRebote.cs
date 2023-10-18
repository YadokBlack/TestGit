using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRebote : MonoBehaviour
{
    public Vector3 tama�oInicial;
    public Vector3 tama�oFinal;
    public AnimationCurve curvaDeCrecimiento = AnimationCurve.Linear(0, 0, 1, 1);
    public float duraci�n = 2.0f; 

    private float tiempoPasado = 0.0f;

    private void Start()
    {
        tiempoPasado = 0.0f;

        transform.localScale = tama�oInicial;
    }

    private void Update()
    {
        tiempoPasado += Time.deltaTime;

        float factorInterpolaci�n = Mathf.Clamp01(tiempoPasado / duraci�n);
        float factorCurva = curvaDeCrecimiento.Evaluate(factorInterpolaci�n);

        Vector3 nuevoTama�o = Vector3.Lerp(tama�oInicial, tama�oFinal, factorCurva);

        transform.localScale = nuevoTama�o;

        if (factorInterpolaci�n >= 1.0f)
        {
            // Aqu� puedes reiniciar el proceso si lo deseas
            // tiempoPasado = 0.0f;
        }
    }
}
