using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mousepointer : MonoBehaviour
{
    Vector3 mousePos, worldPos, posi;
    public GameObject Gun;
    public GameObject Reticle;

    void start()
    {

    }

    void Update()
    {
        Vector3 posi2 = Reticle.transform.position;
        mousePos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        posi = Gun.transform.position;

        // プレイヤーの次の位置を計算
        Vector3 nextPosition = worldPos;
        nextPosition = new Vector3(
            Mathf.Clamp(nextPosition.x, -8.65f, 8.65f),
            Mathf.Clamp(nextPosition.y, -4.75f, 4.75f),
            nextPosition.z
        );

        // プレイヤーの位置を更新
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, 30f);
          float dx = posi2.x - posi.x;
          float dy = posi2.y - posi.y;
          float rad = Mathf.Atan2(dy, dx);
          float kakudo = rad * Mathf.Rad2Deg;
        
        Gun.transform.rotation = Quaternion.Euler(0, 0, kakudo - 90);
    }
}