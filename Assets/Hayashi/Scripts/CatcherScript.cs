using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherScript : MonoBehaviour
{
    private Vector3 offset;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        offset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)) - transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        transform.position = mousePosition - offset;
        if (Input.GetMouseButton(0))
        {
            // クリックされている場合、色を緑にする
            spriteRenderer.color = Color.gray;
            gameObject.tag = "Catcher_sunk";
        }
        else
        {
            // "MyTag"というタグを持つ全てのオブジェクトを取得
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Fish");

            // 各オブジェクトに対して処理を行う
            foreach (GameObject obj in objects)
            {
                FishInstance script = obj.GetComponent<FishInstance>();
                script.isCaught = false;
            }
            // クリックされていない場合、色を灰色にする
            spriteRenderer.color = Color.white;
            gameObject.tag = "Catcher";
        }
    }
}