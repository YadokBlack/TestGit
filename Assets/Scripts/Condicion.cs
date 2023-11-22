using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Condicion : MonoBehaviour
{
    public ListaEstados listadoEstados;
    public PasoDelTiempo reloj;
    
    private void Awake()
    {
        listadoEstados.InicializarEstados();
    }

    private void Update()
    {        
        if (reloj.pantallaNegra) listadoEstados.InicializaCansancioYEstres();

        listadoEstados.ActualizarBarras();
    }


    public void CambioEstado(float[] beneficios)
    {
        if (BeneficiosCorrectoTamanyo(beneficios)) listadoEstados.AsignarValorEstados(beneficios);        
    }

    private bool BeneficiosCorrectoTamanyo(float[] beneficios)
    {
        return beneficios != null && beneficios.Length == 4;
    }

    public string ObtenerMensajeCondicion(string mensaje, bool condicion)
    {
        return condicion ? mensaje : string.Empty;
    }

    public bool AlgunEstadoAlMaximo()
    {
        return HambreAlMaximo() || SedAlMaximo() || CansancioAlMaximo() || EstresAlMaximo();
    }
    

    public bool HambreAlMaximo()
    {
        return listadoEstados.EstadoAlMaximo(PosiblesEstados.Hambre);
    }

    public bool EstresAlMaximo()
    {
        return listadoEstados.EstadoAlMaximo(PosiblesEstados.Estres);
    }

    public bool CansancioAlMaximo()
    {
        return listadoEstados.EstadoAlMaximo(PosiblesEstados.Cansancio);
    }

    public bool SedAlMaximo()
    {
        return listadoEstados.EstadoAlMaximo(PosiblesEstados.Sed);
    }    
}
