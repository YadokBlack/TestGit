using UnityEngine;

public class CambioColorLuz : MonoBehaviour
{
    public Light luzDireccional;

    public PasoDelTiempo tiempo;

    [SerializeField]
    private Color colorManyana = new Color(0.741f, 0.925f, 0.964f); // Azul claro para la mañana
    [SerializeField]
    private Color colorTarde = new Color(0.898f, 0.824f, 0.608f);  // Naranja para la tarde
    [SerializeField]
    private Color colorNoche = new Color(0.098f, 0.180f, 0.416f);  // Azul oscuro para la noche

    private void Update()
    {
        CambiarColorDeLuzDireccional();
    }

    private void CambiarColorDeLuzDireccional()
    {

        float horaInterpolacion = Mathf.InverseLerp(0, 18, tiempo.horasDelJuego);
        Color colorActual = Color.Lerp(colorManyana, colorTarde, horaInterpolacion);

        if (tiempo.horasDelJuego > 17)
        {
            horaInterpolacion = Mathf.InverseLerp(17, 24, tiempo.horasDelJuego);

            colorActual = Color.Lerp(colorTarde, colorNoche, horaInterpolacion);
        }

        luzDireccional.color = colorActual;
    }
}
