using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimeManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI timerText;
    public float timeLeft = 60.0f;
    public Rigidbody2D whiteObjectRigidbody;
    public Transform whiteObjectTransform;
    public Rigidbody2D popupObjectRigidbody;
    public Transform popupObjectTransform;

    private float ObjectGoalY = 0;
    private float whiteObjectSpeed = 16f;
    private float popupObjectSpeed = 8f;
    // private float slowDownFactor = 0.005f;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = timeLeft.ToString("F1");
        }
        else
        {
            if (whiteObjectTransform.position.y <= ObjectGoalY)
            {
                whiteObjectRigidbody.velocity = Vector3.zero;
            }
            else
            {
                Vector3 whiteObjectVec = Vector3.zero;
                whiteObjectVec.y = (ObjectGoalY - whiteObjectTransform.position.y) * whiteObjectSpeed + 0.1f;
                whiteObjectVec.z = -9.0f;
                whiteObjectRigidbody.velocity = whiteObjectVec;
            }
            if (popupObjectTransform.position.y <= ObjectGoalY)
            {
                popupObjectRigidbody.velocity = Vector3.zero;
                /*
                CatcherScript script = catcher.GetComponent<CatcherScript>();
                script.isGameEnded = true;
                catcher.SetActive(false);
                Destroy(catcher );*/
                Console.WriteLine("game ended");
            }
            else
            {
                Vector3 popupObjectVec = Vector3.zero;
                popupObjectVec.y = (ObjectGoalY - popupObjectTransform.position.y) * popupObjectSpeed + 0.1f;
                popupObjectVec.z = -9.5f;
                popupObjectRigidbody.velocity = popupObjectVec;
            }
        }
    }
}
