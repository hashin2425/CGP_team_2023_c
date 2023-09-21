using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleZoom : MonoBehaviour
{   
    Transform tf; //Main CameraのTransform
    Camera cam; //Main CameraのCamera
    private bool flag=false;
    // Start is called before the first frame update
    void Start()
    {
        tf = this.gameObject.GetComponent<Transform>(); 
        cam = this.gameObject.GetComponent<Camera>();
        cam.orthographicSize = cam.orthographicSize - 2.0f;
        tf.position = tf.position + new Vector3(0.0f,2.0f,0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(!flag&&Input.GetMouseButtonDown(0)){
            cam.orthographicSize = cam.orthographicSize + 2.0f;
            tf.position = tf.position + new Vector3(0.0f,-2.0f,0.0f);
            flag=true;
        }
    }
}
