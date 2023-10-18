using UnityEngine;
using UnityEngine.UI;

public class ColorGradientScript : MonoBehaviour
{
    public float maxValue;     

    public Image colorImage;   

    [Header("Estados")]

    public Condicion condicion;
    [SerializeField]
    private Estados estados; 

    private enum Estados
    {
        Sed,
        Comida,
        Cansancio,
        Estres
    }

    private void Update()
    {
        float proportion = 0;  

        switch (estados)
        {
            case Estados.Sed:
                proportion = Mathf.Clamp01(condicion.sed / maxValue);
                break;
            case Estados.Comida:
                proportion = Mathf.Clamp01(condicion.hambre / maxValue);
                break;
            case Estados.Cansancio:
                proportion = Mathf.Clamp01(condicion.cansancio / maxValue);
                break;
            case Estados.Estres:
                proportion = Mathf.Clamp01(condicion.estres / maxValue);
                break;
        }

        Color color = new Color(.39f, 0.78f, .39f);
        Color lerpedColor = Color.Lerp(color, Color.red, proportion);
        colorImage.color = lerpedColor;
    }
}
