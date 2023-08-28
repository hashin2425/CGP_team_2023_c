using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI timerText;
    public float time = 60.0f;

    // Update is called once per frame
    void FixedUpdate()
    {
        this.time -= Time.deltaTime;
        this.timerText.text = this.time.ToString("F1");
    }
}
