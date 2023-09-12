using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem2 : MonoBehaviour
{
    //アイテムプレハブ
    public GameObject itemPrefab;
    //敵生成時間間隔
    private float interval;
    //経過時間
    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //時間間隔を決定する
        interval = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //時間計測
        time += Time.deltaTime;

        //経過時間が生成時間になったとき(生成時間より大きくなったとき)
        if (time > interval)
        {
            //enemyをインスタンス化する(生成する)
            GameObject item = Instantiate(itemPrefab);
            //生成した敵の座標を決定する(現状X=0,Y=10,Z=20の位置に出力)
            item.transform.position = new Vector2(10, -1);
            //経過時間を初期化して再度時間計測を始める
            time = 0f;
        }
    }
}
