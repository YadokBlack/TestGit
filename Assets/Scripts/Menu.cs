using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public MovimientoJugador jugador;

    public PasoDelTiempo reloj;

    public GameObject menuInicial;

    public GameObject panelMensaje;

    public GameObject panelPartida;

    public GameObject panelVictoria;
    public TextMeshProUGUI textoResultado;

    public GameObject panelGameOver;

    public BarraHorizontal barraProgreso;

    public bool juegoIniciado;

    public int diasTopeJuego;

    public bool gameOver;
    public bool gameWin;

    public AccionTrabajar trabajando;

    void menuInicio()
    {
        menuInicial.SetActive(true);
        panelMensaje.SetActive(false);
        panelPartida.SetActive(false);
        panelVictoria.SetActive(false);
        panelGameOver.SetActive(false);
        juegoIniciado = false;
        gameOver = false;
        gameWin = false;
        reloj.diasTopeJuego = diasTopeJuego;
    }

    private void Awake()
    {
        menuInicio();

        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (!juegoIniciado && Input.anyKeyDown)
        {
            juegoIniciado = true;
            menuInicial.SetActive(false);
            panelMensaje.SetActive(true);
            panelMensaje.GetComponentInChildren<TextMeshProUGUI>().text = "Es hora de programar ve al ordenador.\r\n\r\nUtiliza W,A,S,D para moverte y el ratón para girar.";
            jugador.Iniciar();
            reloj.Iniciar();            
            panelPartida.SetActive(true);
        }

        if (gameWin && juegoIniciado)
        {
            panelMensaje.SetActive(true);
            panelMensaje.GetComponentInChildren<TextMeshProUGUI>().text = "Pulsa Espacio para volver al menú.";
        }

        if (gameWin && juegoIniciado && Input.GetKeyDown(KeyCode.Space))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex);
        }

        if (gameOver && juegoIniciado && Input.GetKeyDown(KeyCode.Space))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex);
        }

        if (juegoIniciado && reloj.diasDelJuego > diasTopeJuego && !reloj.pantallaNegra && !gameOver)
        {
            panelPartida.SetActive(false);

            panelGameOver.SetActive(true);

            jugador.Pausar();
            reloj.Pausar();
            gameOver = true;

            trabajando.zonaControl.pausaDeteccion = true;

            panelMensaje.SetActive(true);
            panelMensaje.GetComponentInChildren<TextMeshProUGUI>().text = $"Tu progreso del juego llegó al :{barraProgreso.porcentaje.ToString()}%\r\n\r\nPulsa Espacio para volver al menú.";
        }

        if (juegoIniciado && trabajando.haGanado && !gameWin)
        {
            panelPartida.SetActive(false);

            panelMensaje.SetActive(true);
            panelMensaje.GetComponentInChildren<TextMeshProUGUI>().text = "Pulsa Espacio para volver al menú.";

            panelVictoria.SetActive(true);
            jugador.Pausar();
            reloj.Pausar();

            string diaOdia = reloj.diasDelJuego == 1 ? "día" : "días";
            string horaOs = reloj.horasDelJuego == 1 ? "hora" : "horas";
            string minOs = reloj.minutosDelJuego == 1 ? "minuto" : "minutos";
            string tiempoJuego = $"¡Has logrado acabar tu juego!\r\nEn {reloj.diasDelJuego-1} {diaOdia}, {reloj.horasDelJuego:D2} {horaOs} y {reloj.minutosDelJuego:D2} {minOs}.\r\nCon un total de {trabajando.teclasPulsadas} acciones.";
            textoResultado.text = tiempoJuego;

            gameWin = true;
        }
    }
}
