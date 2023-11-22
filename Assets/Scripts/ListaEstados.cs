using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PosiblesEstados
{
    Estres,
    Hambre,
    Sed,
    Cansancio
}

[System.Serializable]
public class ListaEstados
{
    const int valorMaximo = 100;
    public float aumentoAleatorio;
    public Estado[] estados;

    public void InicializarEstados()
    {
        estados = new Estado[System.Enum.GetNames(typeof(PosiblesEstados)).Length];
        for (int i = 0; i < estados.Length; i++)
        {
            InicializaUnEstado(i);
        }
    }

    private void InicializaUnEstado(int i)
    {
        estados[i].valor = 0;
        estados[i].altura = estados[i].fondo.rectTransform.rect.height;
        estados[i].tiempoUltimoCambio = Time.time;
    }


    public void ActualizarBarras()
    {
        for (int i = 0; i < estados.Length; i++)
        {
            float tiempoActual = Time.time;
            if (EsHoraDeCambiar(i, tiempoActual))
            {
                IncrementaValor(i, tiempoActual);
            }
            estados[i].ActualizaBarra();
        }
    }

    public bool EsHoraDeCambiar(int i, float tiempoActual)
    {
        return tiempoActual - estados[i].tiempoUltimoCambio >= estados[i].tiempoEntreCambio;
    }

    private void IncrementaValor(int i, float tiempoActual)
    {
        estados[i].valor += 1 + Random.Range(0.1f, aumentoAleatorio);
        estados[i].valor = Mathf.Clamp(estados[i].valor, 0, 100);
        estados[i].tiempoUltimoCambio = tiempoActual;
    }

    public void InicializaCansancioYEstres()
    {
        if (estados != null && estados.Length > 0)
        {
            estados[(int)PosiblesEstados.Estres].valor = 0;
            estados[(int)PosiblesEstados.Cansancio].valor = 0;
        }
    }
    public void AsignarValorEstados(float[] beneficios)
    {
        for (int i = 0; i < estados.Length; i++)
        {
            estados[i].valor = beneficios[i];
            estados[i].valor = Mathf.Clamp(estados[i].valor, 0, 100);
        }
    }

    public bool EstadoAlMaximo(PosiblesEstados estado)
    {
        return this.estados[(int)estado].valor == valorMaximo;
    }
}
