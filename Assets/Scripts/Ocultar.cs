using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocultar : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        // Obtenemos el componente MeshRenderer del objeto
        meshRenderer = GetComponent<MeshRenderer>();

        // Comprobamos si el objeto tiene un MeshRenderer
        if (meshRenderer == null)
        {
            Debug.LogWarning("El objeto no tiene un MeshRenderer.");
        }
        else
        {
            // Ocultamos el objeto desactivando el componente MeshRenderer
            meshRenderer.enabled = false;
        }
    }
}
