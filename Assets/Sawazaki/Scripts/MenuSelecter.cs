using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
public class MenuSelecter : MonoBehaviour
{
    // Start is called before the first frame update
    public string SceneName;
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveScene()
    {
        // シーン遷移
        Debug.Log("click");
        SceneManager.LoadScene(SceneName);
    }
}
