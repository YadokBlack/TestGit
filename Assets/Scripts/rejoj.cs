using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class rejoj : MonoBehaviour
{

    [Tooltip("Donde se muestra")]
    // public Text currentTime;
    public TextMeshProUGUI currentTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentTime.color = Color.red;
        currentTime.text =
        System.DateTime.Now.Hour.ToString("00") + ":" +
        System.DateTime.Now.Minute.ToString("00");
     
    }
}
