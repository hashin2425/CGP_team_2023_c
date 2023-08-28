using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    //座標用の変数
    Vector3 mousePos, worldPos;

    private GameObject square;
    private SpriteRenderer SpriteRenderer;
    void Update()
    {
        //マウス座標の取得
        mousePos = Input.mousePosition;
        //スクリーン座標をワールド座標に変換
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        //ワールド座標を自身の座標に設定
        transform.position = worldPos;
        if (Input.GetMouseButtonDown(0))
        {
            square = GameObject.Find("Square");
            SpriteRenderer = square.GetComponent<SpriteRenderer>();
            SpriteRenderer.sortingOrder = 0;
        }
        //ボタン押した瞬間のやつ、画像変えれば行けそう
        if (Input.GetMouseButtonUp(0))
        {
            square = GameObject.Find("Square");
            SpriteRenderer = square.GetComponent<SpriteRenderer>();
            SpriteRenderer.sortingOrder = 1;
        }
        //ボタン離した瞬間のやつ、金魚プレハブの関数が実行できそう
    }
}
