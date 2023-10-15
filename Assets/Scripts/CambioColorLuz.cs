using UnityEngine;

public class CambioColorLuz : MonoBehaviour
{
    public Light luzDireccional;

    public PasoDelTiempo tiempo;

    const int horaCambioTardeNoche = 17;
    const int horasEnUnDia = 24;

    [SerializeField]
    private Color colorManyana = new Color(0.741f, 0.925f, 0.964f); 
    [SerializeField]
    private Color colorTarde = new Color(0.898f, 0.824f, 0.608f); 
    [SerializeField]
    private Color colorNoche = new Color(0.098f, 0.180f, 0.416f);  

    private void Update()
    {
        CambiarColorDeLuzDireccional();
    }

    private void CambiarColorDeLuzDireccional()
    {
        float horaInterpolacion = Mathf.InverseLerp(0, horaCambioTardeNoche+1, tiempo.horasDelJuego);
        Color colorActual = Color.Lerp(colorManyana, colorTarde, horaInterpolacion);

        if (tiempo.horasDelJuego > horaCambioTardeNoche)
        {
            horaInterpolacion = Mathf.InverseLerp(horaCambioTardeNoche, horasEnUnDia, tiempo.horasDelJuego);

            colorActual = Color.Lerp(colorTarde, colorNoche, horaInterpolacion);
        }

        luzDireccional.color = colorActual;
    }
}
