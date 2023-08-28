using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherScript : MonoBehaviour
{
    private Vector3 mousePos, worldPos;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        //マウス座標の取得
        mousePos = Input.mousePosition;
        //スクリーン座標をワールド座標に変換
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        worldPos.z = 0.01f;
        //ワールド座標を自身の座標に設定
        transform.position = worldPos;
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