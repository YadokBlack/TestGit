using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public MovimientoJugador jugador;

    public PasoDelTiempo control;

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
        control.diasTopeJuego = diasTopeJuego;
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
            control.Iniciar();            
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

        if (juegoIniciado && control.diasDelJuego > diasTopeJuego && !control.pantallaNegra && !gameOver)
        {
            panelPartida.SetActive(false);

            panelGameOver.SetActive(true);

            jugador.Pausar();
            control.Pausar();
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
            control.Pausar();

            string diaOdia = control.diasDelJuego == 1 ? "día" : "días";
            string horaOs = control.horasDelJuego == 1 ? "hora" : "horas";
            string minOs = control.minutosDelJuego == 1 ? "minuto" : "minutos";
            string tiempoJuego = $"¡Has logrado acabar tu juego!\r\nEn {control.diasDelJuego-1} {diaOdia}, {control.horasDelJuego:D2} {horaOs} y {control.minutosDelJuego:D2} {minOs}.\r\nCon un total de {trabajando.teclasPulsadas} acciones.";
            textoResultado.text = tiempoJuego;

            gameWin = true;
        }
    }
}
