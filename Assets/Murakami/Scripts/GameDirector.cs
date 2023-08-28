using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    GameObject timerText;
    GameObject lifeText;
    float time = 60.0f;
    int point = 0;

    public void BreackPoi()
    {
        this.point /= 1;
    }


    void Start()
    {
        this.timerText = GameObject.Find("Time");
        this.pointText = GameObject.Find("Life");
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
