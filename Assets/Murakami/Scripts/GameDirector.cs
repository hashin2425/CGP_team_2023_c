using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameDirector : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject timerText;
    float time = 60.0f;

    void Start()
    {
        this.timerText = GameObject.Find("Time");
    }

    // Update is called once per frame
    void Update()
    {
        this.time -= Time.deltaTime;
        this.timerText.GetComponent<TextMeshProUGUI>().text =
            this.time.ToString("F1");
    }
}
