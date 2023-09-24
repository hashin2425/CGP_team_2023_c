using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerForDev : MonoBehaviour
{
    void Update()
    {
        // もし "1" キーが押されたら SceneA を読み込む
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("MiniGameFishCatch");
        }

        // もし "2" キーが押されたら SceneB を読み込む
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("MiniGameShooting");
        }

        // もし "3" キーが押されたらゲームを終了する
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Application.Quit();
        }
    }
}
