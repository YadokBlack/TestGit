using UnityEngine;
using UnityEngine.UI;

public class ColorGradientScript : MonoBehaviour
{
    public float maxValue;     

    public Image colorImage;   

    [Header("Estados")]

    public Condicion condicion;
    [SerializeField]
    private PosiblesEstados estados; 

    /*
    private enum Estados
    {
        Sed,
        Comida,
        Cansancio,
        Estres
    }
    */

    private void Update()
    {
        float proportion = 0;  

        switch (estados)
        {
            case PosiblesEstados.Sed:
                proportion = Mathf.Clamp01(condicion.listadoEstados.estados[(int)PosiblesEstados.Sed].valor / maxValue);
                break;
            case PosiblesEstados.Hambre:
                proportion = Mathf.Clamp01(condicion.listadoEstados.estados[(int)PosiblesEstados.Hambre].valor / maxValue);
                break;
            case PosiblesEstados.Cansancio:
                proportion = Mathf.Clamp01(condicion.listadoEstados.estados[(int)PosiblesEstados.Cansancio].valor / maxValue);
                break;
            case PosiblesEstados.Estres:
                proportion = Mathf.Clamp01(condicion.listadoEstados.estados[(int)PosiblesEstados.Estres].valor / maxValue);
                break;
        }

        Color color = new Color(.39f, 0.78f, .39f);
        Color lerpedColor = Color.Lerp(color, Color.red, proportion);
        colorImage.color = lerpedColor;
    }
}
