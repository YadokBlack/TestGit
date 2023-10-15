using UnityEngine;
using TMPro;
using UnityEngine.Rendering;

public class ControladorDeHora : MonoBehaviour
{
    public int diasDelJuego = 0;
    public int horasDelJuego = 0;
    public int minutosDelJuego = 0;
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
        diasDelJuego = 1;
        horasDelJuego = 7;
        minutosDelJuego= 0;
        // horasDelJuego = System.DateTime.Now.Hour;
        // minutosDelJuego = System.DateTime.Now.Minute;

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


        /*
        // para cambiar cuando uno quiera los objetos de poscicion 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            objetosJuego.ColocarObjetos();
        }
        */

        if (!pantallaNegra)
        {
            tiempoPasadoDesdeUltimaActualizacion += Time.deltaTime;

            if (tiempoPasadoDesdeUltimaActualizacion >= tiempoRealPorMinuto)
            {
                minutosDelJuego++;
                if (minutosDelJuego >= 60)
                {
                    minutosDelJuego = 0;
                    horasDelJuego++;
                    if (horasDelJuego >= 24)
                    {
                        horasDelJuego = 0;                       
                    }
                }

                tiempoPasadoDesdeUltimaActualizacion = 0.0f;
            }

            ActualizarHoraEnFormato24();

            // Cambia el color de la luz direccional seg�n la hora del d�a
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
            // Si la pantalla est� en negro, contabiliza el tiempo
            tiempoNegroPasado += Time.deltaTime;

            if (tiempoNegroPasado >= tiempoEspera)
            {
                // Ha pasado el tiempo de espera, quita la pantalla en negro
                fondoNegro.SetActive(false);
                pantallaNegra = false;
                tiempoNegroPasado = 0.0f;

                

                // Puedes reiniciar las horas y minutos aqu� si lo deseas
                horasDelJuego = Random.Range(5, 8);
                minutosDelJuego = Random.Range(0, 59);
            }
        }
    }

    // no poner mas de 60 minutos o se pierde lo que pongas de mas
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
        //textHoraActual.text = horaFormato24;

        int horasFaltan = 22 - horasDelJuego;
        int minutosFaltan = 60 - minutosDelJuego;

        string quedanHoras = horasFaltan.ToString("D2") + ":" + minutosFaltan.ToString("D2");
        textHoraActual.text = quedanHoras;

        int quedanDias = diasTopeJuego - diasDelJuego; 

        diaText.text = quedanDias.ToString("D2") + " DIAS";

        //diaText.text = "DIA " + diasDelJuego.ToString("D2");
        relojPC.text = horaFormato24;
    }

    private void CambiarColorDeLuzDireccional()
    {

        Color colorManana = new Color(0.741f, 0.925f, 0.964f); // Azul claro para la ma�ana
        Color colorTarde = new Color(0.898f, 0.824f, 0.608f);  // Naranja para la tarde
        Color colorNoche = new Color(0.098f, 0.180f, 0.416f);  // Azul oscuro para la noche

        // Calcula el factor de interpolaci�n basado en la hora actual
        float horaInterpolacion = Mathf.InverseLerp(0, 18, horasDelJuego);
        Color colorActual = Color.Lerp(colorManana, colorTarde, horaInterpolacion);

        if ( horasDelJuego > 17 )
        {
            horaInterpolacion = Mathf.InverseLerp(17, 24, horasDelJuego);
            // Interpola entre los colores seg�n la hora del d�a
            colorActual = Color.Lerp(colorTarde, colorNoche, horaInterpolacion);
        }

        // Aplica el color a la luz direccional
        luzDireccional.color = colorActual;
    }
}
