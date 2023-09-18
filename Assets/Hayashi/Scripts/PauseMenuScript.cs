using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject noticeBar;
    public GameObject menuBox;
    public bool canMouseBeClicked = true;

    private float moveSpeed = 0.2f;
    private Transform noticeBarTransform;
    private BoxCollider2D noticeBarCollider;
    private bool isNowMouseEntered;
    private bool isNowMenuDisplayed;
    private float targetXStart = 1170;
    private float targetYEnd = 685;

    void Start()
    {
        noticeBarTransform = noticeBar.transform;
        noticeBarCollider = noticeBar.GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        isNowMouseEntered = noticeBarCollider.bounds.Contains(mousePos);

        float newX = noticeBarTransform.localPosition.x;
        if (isNowMouseEntered && Input.GetMouseButton(0))
        {
            menuBox.SetActive(true);
            isNowMenuDisplayed = true;
        }
        else if (isNowMouseEntered && !isNowMenuDisplayed)
        {
            canMouseBeClicked = false;
            newX += (targetYEnd - newX) * moveSpeed;
        }
        else if (!isNowMouseEntered && !isNowMenuDisplayed)
        {
            canMouseBeClicked = true;
            newX += (targetXStart - newX) * moveSpeed;
        }
        noticeBarTransform.localPosition = new Vector3(newX, 480, 0);
    }

    public void OnRestartButtonClicked()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void OnTitleButtonClicked()
    {

    }

    public void OnCloseButtonClicked()
    {
        menuBox.SetActive(false);
        isNowMenuDisplayed = false;

    }
}
