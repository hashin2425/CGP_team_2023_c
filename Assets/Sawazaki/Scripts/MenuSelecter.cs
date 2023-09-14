using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private void OnMouseDown()
    {
        // シーン遷移
        SceneManager.LoadScene(SceneName);
    }
}
