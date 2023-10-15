using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class PasoDelTiempo : MonoBehaviour
{
    public int diaInicio = 1;
    public int horaInicio = 7;
    public int minutoInicio = 0;

    const int horasTieneUnDia = 24;
    const int minutosTieneUnaHora = 60;

    public int diasDelJuego;
    public int horasDelJuego;
    public int minutosDelJuego;

    public float tiempoRealPorMinuto;
    public TextMeshProUGUI diaText;
    public TextMeshProUGUI textHoraActual;
    public TextMeshProUGUI relojPC;
    public GameObject fondoNegro;
    public TextMeshProUGUI textosDias;

    private float tiempoPasadoDesdeUltimaActualizacion = 0.0f;
    public bool pantallaNegra = false;
    private float tiempoNegroPasado = 0.0f;

    public float tiempoEspera = 2f;

    public Light luzDireccional;

    private bool pausa;

    public Desorden objetosJuego;

    public int diasTopeJuego;

    private void Awake()
    {
        diasDelJuego = diaInicio;
        horasDelJuego = horaInicio;
        minutosDelJuego= minutoInicio;

        pausa = true;
    }

    public void Pausar()
    {
        pausa = true;
    }

    public void Iniciar()
    {
        pausa = false;
    }

    private void Update()
    {
        if (pausa) return;

        if (!pantallaNegra)
        {
            tiempoPasadoDesdeUltimaActualizacion += Time.deltaTime;

            if (tiempoPasadoDesdeUltimaActualizacion >= tiempoRealPorMinuto)
            {
                minutosDelJuego++;
                if (minutosDelJuego >= minutosTieneUnaHora)
                {
                    minutosDelJuego = 0;
                    horasDelJuego++;
                    if (horasDelJuego >= horasTieneUnDia)
                    {
                        horasDelJuego = 0;                       
                    }
                }

                tiempoPasadoDesdeUltimaActualizacion = 0.0f;
            }

            ActualizarHoraEnFormato24();

            CambiarColorDeLuzDireccional();

            if (horasDelJuego >= 23)
            {
                pantallaNegra = true;                
                textosDias.text = "DIA - " + diasDelJuego;
                fondoNegro.SetActive(true);
                objetosJuego.ColocarObjetos();
                diasDelJuego++;
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

                horasDelJuego = Random.Range(5, 8);
                minutosDelJuego = Random.Range(0, 59);
            }
        }
    }

    public void AumentaTiempo(int minutos)
    {
        minutosDelJuego += minutos;

        if (minutosDelJuego >= 60)
        {
            minutosDelJuego -= 60;
            horasDelJuego++;
            if (horasDelJuego >= 24)
            {
                horasDelJuego = 0;
            }
        }
    }

    private void ActualizarHoraEnFormato24()
    {
        string horaFormato24 = horasDelJuego.ToString("D2") + ":" + minutosDelJuego.ToString("D2");

        int horasFaltan = 22 - horasDelJuego;
        int minutosFaltan = 60 - minutosDelJuego;

        string quedanHoras = horasFaltan.ToString("D2") + ":" + minutosFaltan.ToString("D2");
        textHoraActual.text = quedanHoras;

        int quedanDias = diasTopeJuego - diasDelJuego; 

        diaText.text = quedanDias.ToString("D2") + " DIAS";

        relojPC.text = horaFormato24;
    }

    private void CambiarColorDeLuzDireccional()
    {
        Color colorManana = new Color(0.741f, 0.925f, 0.964f); // Azul claro para la mañana
        Color colorTarde = new Color(0.898f, 0.824f, 0.608f);  // Naranja para la tarde
        Color colorNoche = new Color(0.098f, 0.180f, 0.416f);  // Azul oscuro para la noche

        float horaInterpolacion = Mathf.InverseLerp(0, 18, horasDelJuego);
        Color colorActual = Color.Lerp(colorManana, colorTarde, horaInterpolacion);

        if ( horasDelJuego > 17 )
        {
            horaInterpolacion = Mathf.InverseLerp(17, 24, horasDelJuego);

            colorActual = Color.Lerp(colorTarde, colorNoche, horaInterpolacion);
        }

        luzDireccional.color = colorActual;
    }
}
