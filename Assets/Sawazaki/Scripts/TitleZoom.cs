using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleZoom : MonoBehaviour
{   
    Transform tf; //Main CameraのTransform
    Camera cam; //Main CameraのCamera
    private bool flag=false;
    //float zoomSpeed=2.0f;
    private bool isZoomingOut = false;
    private float zoomDuration = 1.0f; // ズームの所要時間
    private float zoomTimer = 0.0f;
    private Vector3 initialPosition;
    private float initialOrthographicSize;
    // Start is called before the first frame update
    void Start()
    {
        tf = this.gameObject.GetComponent<Transform>(); 
        cam = this.gameObject.GetComponent<Camera>();
        cam.orthographicSize = cam.orthographicSize - 2.0f;
        tf.position = tf.position + new Vector3(0.0f,2.0f,0.0f);
        initialPosition = tf.position;
        initialOrthographicSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (isZoomingOut)
        {
            zoomTimer += Time.deltaTime;

            if (zoomTimer < zoomDuration)
            {
                // ズームアウト中の処理
                float t = zoomTimer / zoomDuration;
                cam.orthographicSize = Mathf.Lerp(initialOrthographicSize, initialOrthographicSize + 2.0f, t);
                tf.position = Vector3.Lerp(initialPosition, initialPosition + new Vector3(0.0f, -2.0f, 0.0f), t);
            }
            else
            {
                // ズームアウト終了
                cam.orthographicSize = initialOrthographicSize + 2.0f;
                tf.position = initialPosition + new Vector3(0.0f, -2.0f, 0.0f);
                isZoomingOut = false;
            }
        }

        if(!flag&&Input.GetMouseButtonDown(0)){
            isZoomingOut=true;
            zoomTimer=0.0f;
            //cam.orthographicSize = cam.orthographicSize + 2.0f;
            //tf.position = tf.position + new Vector3(0.0f,-2.0f,0.0f);
            flag=true;
        }
    }
}
