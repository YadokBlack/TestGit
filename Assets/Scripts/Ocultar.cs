using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ocultar : MonoBehaviour
{
    private MeshRenderer meshRenderer;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer == null)
        {
            Debug.LogWarning("El objeto no tiene un MeshRenderer.");
        }
        else
        {
            meshRenderer.enabled = false;
        }
    }
}
