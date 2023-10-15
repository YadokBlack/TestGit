using UnityEngine;

public class SalirConEsc : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Verificar si la plataforma de ejecución es PC
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.LinuxPlayer)
        {
            // Verificar si se presiona la tecla ESC
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Salir del juego
                Application.Quit();
            }
        }
    }
}
