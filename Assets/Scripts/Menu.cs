using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * Se encargará del menu inicio, juego, final, y creditos
 */

public class Menu : MonoBehaviour
{
    public MovimientoJugador jugador;

    public ControladorDeHora control;

    public GameObject menuInicial;

    public GameObject panelMensaje;

    public GameObject panelPartida;

    public GameObject panelVictoria;
    public TextMeshProUGUI textoResultado;

    public GameObject panelGameOver;
  //  public TextMeshProUGUI textResultados;

    public BarraHorizontal barraProgreso;

    public bool juegoIniciado;

    public int diasTopeJuego;

    public bool gameOver;
    public bool gameWin;

    // public InteraccionTrabajo trabajando;
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

        // Oculta el cursor y lo bloquea en el centro de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
        // Oculta el cursor
        // Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Espera que se pulse una tecla para iniciar la partida
        if (!juegoIniciado && Input.anyKeyDown)
        {
            juegoIniciado = true;
            menuInicial.SetActive(false);
            panelMensaje.SetActive(true);
            panelMensaje.GetComponentInChildren<TextMeshProUGUI>().text = "Es hora de programar ve al ordenador.\r\n\r\nUtiliza W,A,S,D para moverte y el ratón para girar.";
            jugador.Iniciar();
            control.Iniciar();            
            panelPartida.SetActive(true);
          //  trabajando.zonaControl.pausaDeteccion = true;
        }

        // cuando ya ha mostrado final bueno muestra el mensaje de pulsar espacio 
        if (gameWin && juegoIniciado)
        {
            panelMensaje.SetActive(true);
            panelMensaje.GetComponentInChildren<TextMeshProUGUI>().text = "Pulsa Espacio para volver al menú.";
        }

        // comprueba que se pulsa espacio y carga de nuevo la escena
        if (gameWin && juegoIniciado && Input.GetKeyDown(KeyCode.Space))
        {
            // Obtén el índice de la escena actual
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Carga la misma escena para reiniciarla
            SceneManager.LoadScene(currentSceneIndex);
        }

        // Se ha mostrado GameOver y se pulsa la tecla espacio reinicia la escena
        if (gameOver && juegoIniciado && Input.GetKeyDown(KeyCode.Space))
        {
            // Obtén el índice de la escena actual
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            // Carga la misma escena para reiniciarla
            SceneManager.LoadScene(currentSceneIndex);
        }

        // El juego iniciado y el tiempo llegó al limite y no se mostró la pantalla negra y gameover entonces muestra el gameover
        if (juegoIniciado && control.diasDelJuego > diasTopeJuego && !control.pantallaNegra && !gameOver)
        {
            panelPartida.SetActive(false);
            // panelMensaje.SetActive(false);

            // se acabo el tiempo
            panelGameOver.SetActive(true);
           // textResultados.text = "Tu progreso del juego llegó al :" + barraProgreso.porcentaje.ToString() + "%. ";
            jugador.Pausar();
            control.Pausar();
            gameOver = true;

            trabajando.zonaControl.pausaDeteccion = true;

            panelMensaje.SetActive(true);
            panelMensaje.GetComponentInChildren<TextMeshProUGUI>().text = $"Tu progreso del juego llegó al :{barraProgreso.porcentaje.ToString()}%\r\n\r\nPulsa Espacio para volver al menú.";
        }

        // si has ganado y no esta activo gameWin entonces pone la pantalla de victoria
        if (juegoIniciado && trabajando.haGanado && !gameWin)
        {
            panelPartida.SetActive(false);

            panelMensaje.SetActive(true);
            panelMensaje.GetComponentInChildren<TextMeshProUGUI>().text = "Pulsa Espacio para volver al menú.";
            // panelMensaje.SetActive(false);
            // se esta mostrando la victoria
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
