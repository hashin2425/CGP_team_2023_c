using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HitCounter : MonoBehaviour
{
    public int hitCount = 0;   // 初期のHIT数
    public TMP_Text hitText;   // TextMeshProUGUIを使用してHIT数を表示する変数

    public delegate void HitCountChanged(int newHitCount);
    public static event HitCountChanged OnHitCountChanged;
    public int minRand=1;
    public int maxRand=10;
    void Start()
    {
        hitCount=0;
        UpdateHitText();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            int rand=Random.Range(minRand,maxRand+1);
            if(rand<6){
                hitCount++;
                UpdateHitText();
            }
        }
        
    }

    void UpdateHitText()
    {
        hitText.text = "HIT: " + hitCount.ToString();
        // HIT数が変更されたらイベントを発火
        OnHitCountChanged?.Invoke(hitCount);
    }
}
