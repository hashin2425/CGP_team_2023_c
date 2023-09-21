using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class homeshateki : MonoBehaviour
{
    public void shatekiScene()
    {
        SceneManager.LoadScene("shatekiScene"); // メインゲームシーンに切り替える
    }
}