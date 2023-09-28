using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleZoom : MonoBehaviour
{
    public Transform titleLogoTransform;
    public Transform lanternTransform;

    Transform tf; //Main CameraのTransform
    Camera cam; //Main CameraのCamera
    private bool flag = false;
    //float zoomSpeed=2.0f;
    private bool isZoomingOut = false;
    private float zoomDuration = 1.0f; // ズームの所要時間
    private float zoomTimer = 0.0f;

    private Vector3 initialCameraPosition;
    private Vector3 initialTitleLogoPosition;
    private Vector3 initialLanternPosition;
    private float initialOrthographicSize;

    private float cameraYDifference = 3.5f;
    private float cameraZoomDifference = 2.5f;
    private float titleLogoYDifference = 0.6f;
    private float lanternYDifference = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        tf = this.gameObject.GetComponent<Transform>();
        cam = this.gameObject.GetComponent<Camera>();

        cam.orthographicSize = cam.orthographicSize - cameraZoomDifference;
        tf.position = tf.position + new Vector3(0.0f, cameraYDifference, 0.0f);
        titleLogoTransform.position = titleLogoTransform.position + new Vector3(0.0f, titleLogoYDifference, 0.0f);
        lanternTransform.position = lanternTransform.position + new Vector3(0.0f, lanternYDifference, 0.0f);

        initialCameraPosition = tf.position;
        initialTitleLogoPosition = titleLogoTransform.position;
        initialLanternPosition = lanternTransform.position;
        initialOrthographicSize = cam.orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isZoomingOut)
        {
            zoomTimer += Time.deltaTime;

            if (zoomTimer < zoomDuration)
            {
                // ズームアウト中の処理
                float t = zoomTimer / zoomDuration;
                cam.orthographicSize = Mathf.Lerp(initialOrthographicSize, initialOrthographicSize + cameraZoomDifference, t);
                tf.position = Vector3.Lerp(initialCameraPosition, initialCameraPosition + new Vector3(0.0f, -cameraYDifference, 0.0f), t);
                titleLogoTransform.position = Vector3.Lerp(initialTitleLogoPosition, initialTitleLogoPosition + new Vector3(0.0f, -titleLogoYDifference, 0.0f), t);
                lanternTransform.position = Vector3.Lerp(initialLanternPosition, initialLanternPosition + new Vector3(0.0f, -lanternYDifference, 0.0f), t);
            }
            else
            {
                // ズームアウト終了
                cam.orthographicSize = initialOrthographicSize + cameraZoomDifference;
                tf.position = initialCameraPosition + new Vector3(0.0f, -cameraYDifference, 0.0f);
                titleLogoTransform.position = initialTitleLogoPosition + new Vector3(0.0f, -titleLogoYDifference, 0.0f);
                lanternTransform.position = initialLanternPosition + new Vector3(0.0f, -lanternYDifference, 0.0f);

                isZoomingOut = false;
            }
        }

        if(Input.GetMouseButton(0)) { Debug.Log("pushed"); }

        if (!flag && Input.GetMouseButton(0))
        {
            isZoomingOut = true;
            zoomTimer = 0.0f;
            //cam.orthographicSize = cam.orthographicSize + cameraZoomDifference;
            //tf.position = tf.position + new Vector3(0.0f,-cameraYDifference,0.0f);
            flag = true;
        }
    }
}