using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class baketsucontroller : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI potatoScore;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float Score = GameObject.FindGameObjectsWithTag("potato").Length;
        potatoScore.text = Score.ToString();
    }
}
