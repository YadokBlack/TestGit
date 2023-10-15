using UnityEngine;
using UnityEngine.UI;


public class ColorGradientScript : MonoBehaviour
{
    public float maxValue;     // El valor m�ximo.

    public Image colorImage;   // La imagen que representar� el color.


    [Header("Estados")]

    public Condicion condicion;
    [SerializeField]
    private Estados estados; // Enum para los estados

    private enum Estados
    {
        Sed,
        Comida,
        Cansancio,
        Estres
    }

    private void Update()
    {
        // Calcula la proporci�n actual entre el valor actual y el valor m�ximo.
        float proportion = 0;  

        switch (estados)
        {
            case Estados.Sed:
                // Haz algo relacionado con la sed.
                proportion = Mathf.Clamp01(condicion.sed / maxValue);
                break;
            case Estados.Comida:
                // Haz algo relacionado con la comida.
                proportion = Mathf.Clamp01(condicion.hambre / maxValue);
                break;
            case Estados.Cansancio:
                // Haz algo relacionado con el cansancio.
                proportion = Mathf.Clamp01(condicion.cansancio / maxValue);
                break;
            case Estados.Estres:
                // Haz algo relacionado con el estr�s.
                proportion = Mathf.Clamp01(condicion.estres / maxValue);
                break;
        }

        Color color = new Color(.39f, 0.78f, .39f);

        // Interpola entre el verde y el rojo basado en la proporci�n.
        Color lerpedColor = Color.Lerp(color, Color.red, proportion);

        // Asigna el color a la imagen.
        colorImage.color = lerpedColor;
    }
}
