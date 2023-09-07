using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherScript : MonoBehaviour
{
    private Vector3 mousePos, worldPos;
    private SpriteRenderer spriteRenderer;
    public bool isGameEnded = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (!isGameEnded)
        {
            //マウス座標の取得
            mousePos = Input.mousePosition;
            //スクリーン座標をワールド座標に変換
            worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
            if (Input.GetMouseButton(0))
            {
                worldPos.z = 2.0f;
                // クリックされている場合、色を緑にする
                spriteRenderer.color = Color.gray;
                gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -1.0f);
                gameObject.tag = "Catcher_sunk";
            }
            else
            {
                worldPos.z = -1.0f;
                GameObject[] objects = GameObject.FindGameObjectsWithTag("Fish");

                // 各オブジェクトに対して処理を行う
                foreach (GameObject obj in objects)
                {
                    FishInstance script = obj.GetComponent<FishInstance>();
                    script.isCaught = false;
                    obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, 0.0f);
                }
                // クリックされていない場合、色を灰色にする
                spriteRenderer.color = Color.white;
                gameObject.tag = "Catcher";
            }
            //ワールド座標を自身の座標に設定
            transform.position = worldPos;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}