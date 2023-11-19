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
        if (PulsadoTeclaParaIniciar())
        {
            IniciarPartida();
        }

        if (PartidaGanada())
        {
            MuestraMensaje("Pulsa Espacio para volver al menú.");
        }

        if (PartidaFinalizada() && PulsadoEspacio())
        {
            CargaEscenaActual();
        }

        if (juegoIniciado && reloj.tiempoJuego.dia > diasTopeJuego && !reloj.pantallaNegra && !gameOver)
        {
            panelPartida.SetActive(false);
            panelGameOver.SetActive(true);

            jugador.Pausar();
            reloj.Pausar();
            gameOver = true;

            trabajando.zona.control.pausaDeteccion = true;

            MuestraMensaje($"Tu progreso del juego llegó al :{barraProgreso.vida.porcentaje.ToString()}%\r\n\r\nPulsa Espacio para volver al menú.");
        }

        if (juegoIniciado && trabajando.haGanado && !gameWin)
        {
            panelPartida.SetActive(false);

            MuestraMensaje("Pulsa Espacio para volver al menú.");

            panelVictoria.SetActive(true);
            jugador.Pausar();
            reloj.Pausar();

            string diaOdia = reloj.tiempoJuego.dia == 1 ? "día" : "días";
            string horaOs = reloj.tiempoJuego.hora == 1 ? "hora" : "horas";
            string minOs = reloj.tiempoJuego.minuto == 1 ? "minuto" : "minutos";
            string tiempoJuego = $"¡Has logrado acabar tu juego!\r\nEn {reloj.tiempoJuego.dia - 1} {diaOdia}," +
                $" {reloj.tiempoJuego.hora:D2} {horaOs} y {reloj.tiempoJuego.minuto:D2} {minOs}.\r\nCon un total de {trabajando.teclasPulsadas} acciones.";
            textoResultado.text = tiempoJuego;

            gameWin = true;
        }
    }

    private static void CargaEscenaActual()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private static bool PulsadoEspacio()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }

    private void MuestraMensaje(string mensaje)
    {
        panelMensaje.SetActive(true);
        panelMensaje.GetComponentInChildren<TextMeshProUGUI>().text = mensaje;
    }

    private bool PartidaGanada()
    {
        return gameWin && juegoIniciado;
    }

    private bool PartidaPerdida()
    {
        return gameOver && juegoIniciado;
    }

    private bool PartidaFinalizada()
    {
        return PartidaGanada() || PartidaPerdida();
    }

    private void IniciarPartida()
    {
        juegoIniciado = true;
        menuInicial.SetActive(false);
        MuestraMensaje("Es hora de programar ve al ordenador.\r\n\r\nUtiliza W,A,S,D para moverte y el ratón para girar.");
        jugador.Iniciar();
        reloj.Iniciar();
        panelPartida.SetActive(true);
    }

    private bool PulsadoTeclaParaIniciar()
    {
        return !juegoIniciado && Input.anyKeyDown;
    }
}
