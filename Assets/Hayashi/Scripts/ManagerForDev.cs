using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerForDev : MonoBehaviour
{
    void Update()
    {
        // ���� "1" �L�[�������ꂽ�� SceneA ��ǂݍ���
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("MiniGameFishCatch");
        }

        // ���� "2" �L�[�������ꂽ�� SceneB ��ǂݍ���
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("MiniGameShooting");
        }

        // ���� "3" �L�[�������ꂽ��Q�[�����I������
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Application.Quit();
        }
    }
}
