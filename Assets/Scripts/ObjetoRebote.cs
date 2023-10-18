using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRebote : MonoBehaviour
{
    public Vector3 tamañoInicial;
    public Vector3 tamañoFinal;
    public AnimationCurve curvaDeCrecimiento = AnimationCurve.Linear(0, 0, 1, 1);
    public float duración = 2.0f; 

    private float tiempoPasado = 0.0f;

    private void Start()
    {
        tiempoPasado = 0.0f;

        transform.localScale = tamañoInicial;
    }

    private void Update()
    {
        tiempoPasado += Time.deltaTime;

        float factorInterpolación = Mathf.Clamp01(tiempoPasado / duración);
        float factorCurva = curvaDeCrecimiento.Evaluate(factorInterpolación);

        Vector3 nuevoTamaño = Vector3.Lerp(tamañoInicial, tamañoFinal, factorCurva);

        transform.localScale = nuevoTamaño;

        if (factorInterpolación >= 1.0f)
        {
            // Aquí puedes reiniciar el proceso si lo deseas
            // tiempoPasado = 0.0f;
        }
    }
}
