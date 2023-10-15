using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetoRebote : MonoBehaviour
{

    public Vector3 tamañoInicial;
    public Vector3 tamañoFinal;
    public AnimationCurve curvaDeCrecimiento = AnimationCurve.Linear(0, 0, 1, 1);
    public float duración = 2.0f; // Duración en segundos

    private float tiempoPasado = 0.0f;

    private void Start()
    {
        tiempoPasado = 0.0f;
        // Establecer el tamaño inicial al inicio
        transform.localScale = tamañoInicial;
    }

    private void Update()
    {
        // Aumentar el tiempo pasado
        tiempoPasado += Time.deltaTime;

        // Calcular el factor de interpolación entre 0 y 1 basado en el tiempo pasado y la curva
        float factorInterpolación = Mathf.Clamp01(tiempoPasado / duración);
        float factorCurva = curvaDeCrecimiento.Evaluate(factorInterpolación);

        // Interpolar el tamaño entre el tamaño inicial y final
        Vector3 nuevoTamaño = Vector3.Lerp(tamañoInicial, tamañoFinal, factorCurva);

        // Aplicar el nuevo tamaño al objeto
        transform.localScale = nuevoTamaño;

        // Cuando alcanzamos la duración, puedes reiniciar o hacer algo más
        if (factorInterpolación >= 1.0f)
        {
            // Aquí puedes reiniciar el proceso si lo deseas
            // tiempoPasado = 0.0f;
        }
    }
}
