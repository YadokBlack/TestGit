using UnityEngine;
using TMPro;

public class PasoDelTiempo : MonoBehaviour
{
    const int horasTieneUnDia = 24;
    const int minutosTieneUnaHora = 60;

    public int diaInicio = 1;
    public int horaInicio = 7;
    public int minutoInicio = 0;
    public TiempoJuego tiempoJuego;
    public float tiempoRealPorMinuto;
    public TextMeshProUGUI textDia;
    public TextMeshProUGUI textHoraActual;
    public TextMeshProUGUI textRelojPC;
    public TextMeshProUGUI textosDias;
    public GameObject fondoNegro;

    private float tiempoPasadoDesdeUltimaActualizacion = 0.0f;
    public bool pantallaNegra = false;
    private float tiempoNegroPasado = 0.0f;

    public float tiempoEspera = 2f;

    private bool estaPausado;

    public Desorden objetosJuego;

    public int diasTopeJuego;

    private void Awake()
    {
        tiempoJuego.dia = diaInicio;
        tiempoJuego.hora = horaInicio;
        tiempoJuego.minuto = minutoInicio;
        Pausar();
    }

    public void Pausar()
    {
        estaPausado = true;
    }

    public void Iniciar()
    {
        estaPausado = false;
    }

    private void Update()
    {
        if (estaPausado) return;

        if (!pantallaNegra)
        {
            tiempoPasadoDesdeUltimaActualizacion += Time.deltaTime;

            if (tiempoPasadoDesdeUltimaActualizacion >= tiempoRealPorMinuto)
            {
                tiempoJuego.minuto++;
                ComprobarAumentoHora();

                tiempoPasadoDesdeUltimaActualizacion = 0.0f;
            }

            ActualizarHoraEnFormato24();

            if (tiempoJuego.hora >= horasTieneUnDia - 1 )
            {
                PantallaNuevoDia();
            }
        }
        else
        {
            tiempoNegroPasado += Time.deltaTime;

            if (tiempoNegroPasado >= tiempoEspera)
            {
                fondoNegro.SetActive(false);
                pantallaNegra = false;
                tiempoNegroPasado = 0.0f;

                tiempoJuego.hora = Random.Range(5, 8);
                tiempoJuego.minuto = Random.Range(0, 59);
            }
        }
    }

    public void PantallaNuevoDia()
    {
        pantallaNegra = true;
        textosDias.text = "DIA - " + tiempoJuego.dia;
        fondoNegro.SetActive(true);
        objetosJuego.ColocarObjetos();
        tiempoJuego.dia++;
    }

    public void AumentaTiempo(int minutos)
    {
        tiempoJuego.minuto += minutos;

        ComprobarAumentoHora();
    }

    public void ComprobarAumentoHora()
    {
        while (tiempoJuego.minuto >= minutosTieneUnaHora)
        {
            tiempoJuego.minuto -= minutosTieneUnaHora;
            tiempoJuego.hora++;
            if (tiempoJuego.hora >= horasTieneUnDia)
            {
                tiempoJuego.hora = 0;
            }
        }
    }

    private void ActualizarHoraEnFormato24()
    {
        string horaFormato24 = tiempoJuego.hora.ToString("D2") + ":" + tiempoJuego.minuto.ToString("D2");

        int horaFinalDia = 22;
        int horasFaltan = horaFinalDia - tiempoJuego.hora;
        int minutosFaltan = minutosTieneUnaHora - tiempoJuego.minuto;

        string quedanHoras = horasFaltan.ToString("D2") + ":" + minutosFaltan.ToString("D2");
        textHoraActual.text = quedanHoras;

        int quedanDias = diasTopeJuego - tiempoJuego.dia; 

        textDia.text = quedanDias.ToString("D2") + " DIAS";

        textRelojPC.text = horaFormato24;
    }
}
