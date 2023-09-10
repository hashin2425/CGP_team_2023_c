using UnityEngine;
using TMPro;

public class BulletCounter : MonoBehaviour
{
    public int bulletCount = 10; // 初期の弾の数
    public TMP_Text bulletText;  // TextMeshProUGUIを使用して弾の数を表示する変数
    public string endMessage="Game Over.";
    public delegate void BulletCountChanged(int newBulletCount);
    public static event BulletCountChanged OnBulletCountChanged;
    public GameObject message;
    void Start()
    {
        UpdateBulletText();
        message.SetActive(false);
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)){
            bulletCount--;
            UpdateBulletText();
            if (bulletCount==0){
                message.SetActive(true);
            }
        }
    }

    void UpdateBulletText(){
        bulletText.text = "Bullet: " + bulletCount.ToString();
        // 弾の数が変更されたらイベントを発火
        OnBulletCountChanged?.Invoke(bulletCount);
    }
}
