using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoOpen : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseEnter() {
        panel.SetActive(true);
    }
    private void OnMouseExit() {       
        panel.SetActive(false);
    }
}
