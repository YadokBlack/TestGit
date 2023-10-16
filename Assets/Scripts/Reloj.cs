using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Reloj : MonoBehaviour
{

    [Tooltip("Donde se muestra")]

    public TextMeshProUGUI currentTime;


    void FixedUpdate()
    {
        currentTime.color = Color.red;
        currentTime.text =
        System.DateTime.Now.Hour.ToString("00") + ":" +
        System.DateTime.Now.Minute.ToString("00");
     
    }
}
