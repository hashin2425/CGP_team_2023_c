using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject noticeBar;
    public GameObject menuBox;
    public bool canMouseBeClicked = true;
    public Slider seVolumeSlider;
    public Slider bgmVolumeSlider;
    public AudioVolumeManager volumeManager;

    private bool isSoundInitialized = false;
    private float moveSpeed = 0.2f;
    private Transform noticeBarTransform;
    private BoxCollider2D noticeBarCollider;
    private bool isNowMouseEntered;
    private bool isNowMenuDisplayed;
    private float targetXStart = 1170;
    private float targetYEnd = 685;
    private string seVolumeKey = "seVolume";
    private string bgmVolumeKey = "bgmVolume";

    void Start()
    {
        bgmVolumeSlider.value = PlayerPrefs.GetFloat(bgmVolumeKey);
        seVolumeSlider.value = PlayerPrefs.GetFloat(seVolumeKey);
        isSoundInitialized = true;
        Time.timeScale = 1;
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
            Time.timeScale = 0;
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
        OnSettingsChanged();
    }

    public void OnTitleButtonClicked()
    {
        SceneManager.LoadScene("Menu");
        OnSettingsChanged();
    }

    public void OnCloseButtonClicked()
    {
        menuBox.SetActive(false);
        isNowMenuDisplayed = false;
        Time.timeScale = 1;

        OnSettingsChanged();
    }

    public void OnSettingsChanged()
    {
        Debug.Log("on setting changed");
        if (isSoundInitialized)
        {
            PlayerPrefs.SetFloat(seVolumeKey, seVolumeSlider.value);
            PlayerPrefs.SetFloat(bgmVolumeKey, bgmVolumeSlider.value);
            PlayerPrefs.Save();

            Debug.Log(PlayerPrefs.GetFloat(bgmVolumeKey));

            volumeManager.updateAudioVolumes();
        }
    }
}
