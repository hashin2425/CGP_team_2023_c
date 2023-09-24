using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class homekingyosukui : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("kingyosukuiScene"); // メインゲームシーンに切り替える
    }
}