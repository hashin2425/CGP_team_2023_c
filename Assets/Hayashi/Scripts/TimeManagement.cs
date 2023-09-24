using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class TimeManagement : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI timerText;
    public float timeLeft = 60.0f;
    public Rigidbody2D whiteObjectRigidbody;
    public Transform whiteObjectTransform;
    public Rigidbody2D popupObjectRigidbody;
    public Transform popupObjectTransform;
    public bool isGameEnd;
    public AudioClip seGameEnd;
    public RectTransform[] valueDisplays;
    public GameObject buttonHolder;

    private float ObjectGoalY = 0;
    private float whiteObjectSpeed = 16f;
    private float popupObjectSpeed = 8f;
    private float valueDisplaySpeed = 0.5f;
    private float valueDisplayHeight = 130f;
    private float intervalSec = 0;
    private enum GameEndState
    {
        playSound,
        appendStopText,
        stayIntervalBeforeScoreResult,
        moveStopTextAndValueDisplay,
        stayIntervalBeforeButtonAppend,
        appendButtons
    }
    private GameEndState nowGameState = GameEndState.playSound;

    void FixedUpdate()
    {
        bool isGameContinue = !isGameEnd && timeLeft >= 0;
        if (isGameContinue)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = timeLeft.ToString("F1");
        }
        else
        {
            switch (nowGameState)
            {
                case GameEndState.playSound:
                    gameObject.GetComponent<AudioSource>().PlayOneShot(seGameEnd);
                    nowGameState = GameEndState.appendStopText;
                    ObjectGoalY = 0;
                    break;

                case GameEndState.appendStopText:
                    if (popupObjectTransform.position.y < ObjectGoalY)
                    {
                        nowGameState = GameEndState.stayIntervalBeforeScoreResult;
                        ObjectGoalY = 2.5f;
                        intervalSec = 1f;
                    }
                    whiteObjectTransform.position = Vector3.Lerp(whiteObjectTransform.position, new Vector3(0, -0.2f, -9.0f), Time.fixedDeltaTime * whiteObjectSpeed);
                    popupObjectTransform.position = Vector3.Lerp(popupObjectTransform.position, new Vector3(0, -0.1f, -9.0f), Time.fixedDeltaTime * popupObjectSpeed);
                    break;

                case GameEndState.stayIntervalBeforeScoreResult:
                    intervalSec -= Time.deltaTime;
                    if (intervalSec < 0)
                    {
                        nowGameState = GameEndState.moveStopTextAndValueDisplay;
                    }
                    break;

                case GameEndState.moveStopTextAndValueDisplay:
                    popupObjectTransform.position = Vector3.Lerp(popupObjectTransform.position, new Vector3(popupObjectTransform.position.x, ObjectGoalY, popupObjectTransform.position.z), Time.fixedDeltaTime * popupObjectSpeed);
                    bool isMovementEnd = true;
                    for (int i = 0; i < valueDisplays.Length; i++)
                    {
                        float newX = (0 - valueDisplays[i].localPosition.x) * valueDisplaySpeed / Time.fixedDeltaTime;
                        float newY = (valueDisplayHeight * -i - valueDisplays[i].localPosition.y) * valueDisplaySpeed / Time.fixedDeltaTime;
                        valueDisplays[i].localPosition = Vector3.Lerp(valueDisplays[i].localPosition, new Vector3(newX, newY, valueDisplays[i].localPosition.z), Time.fixedDeltaTime * valueDisplaySpeed / (i + 1)); //new Vector3(newX, newY, valueDisplays[i].localPosition.z);
                        if (Vector3.Distance(valueDisplays[i].localPosition, new Vector3(newX, newY, valueDisplays[i].localPosition.z)) < 0.1f)
                        {
                            isMovementEnd = isMovementEnd && true;
                        }
                        else
                        {
                            isMovementEnd = false;
                        }
                    }
                    if (isMovementEnd)
                    {
                        nowGameState = GameEndState.stayIntervalBeforeButtonAppend;
                        intervalSec = 0.25f;
                    }

                    break;

                case GameEndState.stayIntervalBeforeButtonAppend:
                    intervalSec -= Time.deltaTime;
                    if (intervalSec < 0)
                    {
                        nowGameState = GameEndState.appendButtons;
                    }
                    break;

                case GameEndState.appendButtons:
                    buttonHolder.SetActive(true);
                    Vector3 newPosition = buttonHolder.transform.localPosition;
                    newPosition.y = valueDisplayHeight * -valueDisplays.Length + 1;
                    buttonHolder.transform.localPosition = Vector3.Lerp(buttonHolder.transform.localPosition, newPosition, Time.fixedDeltaTime * popupObjectSpeed);
                    break;
            }
        }
    }

    public void OnRestartButtonClicked()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void OnTitleButtonClicked()
    {
        SceneManager.LoadScene("Menu");
    }
}
