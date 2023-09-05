using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    public GameObject timerText;
    public GameObject lifeText;
    public GameObject pointText;
    public float time = 60.0f;
    int point = 0;

    public void BreackPoi()
    {
        this.point /= 1;
    }


    void Start()
    {
        
    }

    void Update()
    {
        this.time -= Time.deltaTime;
        this.timerText.GetComponent<TextMeshProUGUI>().text =
            this.time.ToString("F1");
        this.pointText.GetComponent<TextMeshProUGUI>().text =
            this.time.ToString() + "point";
    }
}
