using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class baketsucontroller : MonoBehaviour
{

    // Start is called before the first frame update
    public TextMeshProUGUI potatoScore;
    [SerializeField]
    float SPEED = 1.0f;
    private Rigidbody2D rigidBody;
    // Update is called once per frame
    //Vector3 mousePos, worldPos;

    private GameObject square;
    private SpriteRenderer SpriteRenderer;
    private int score; // スコa
    private Vector2 inputAxis;


    void Start()
    {
        // オブジェクトに設定しているRigidbody2Dの参照を取得する
        this.rigidBody = GetComponent<Rigidbody2D>();
        potatoScore.text = "potato: ";
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            inputAxis.y = 1;

        }
        else if (Input.GetKey(KeyCode.S))
        {
            inputAxis.y = -1;

        }
       
        if (gameObject.transform.position.x < -8 || gameObject.transform.position.x > 8)
        {
            inputAxis.x = 0;
        }
        if (Input.GetKey(KeyCode.D)&& gameObject.transform.position.x <8)
        {
            inputAxis.x = 1;

        }
        else if (Input.GetKey(KeyCode.A)&& gameObject.transform.position.x>-8)
        {
            inputAxis.x = -1;

        }
        


        //float Score = GameObject.FindGameObjectsWithTag("potato").Length;
        //potatoScore.text = Score.ToString();

        //マウス座標の取得
        // mousePos = Input.mousePosition;
        //スクリーン座標をワールド座標に変換
        //worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        //ワールド座標を自身の座標に設定
        // transform.position = worldPos;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // ぶつかったオブジェクトが収集アイテムだった場合
        if (other.gameObject.CompareTag("potato"))
        {
            // その収集アイテムを非表示にします
            other.gameObject.SetActive(false);

            // スコアを加算します
            score = score + 1;

            // UI の表示を更新します
            SetCountText();
        }
    }
    void SetCountText()
    {
        // スコアの表示を更新
        potatoScore.text = "potato: " + score.ToString();
    }
    private void FixedUpdate()
    {
        // 速度を代入する

        rigidBody.velocity = inputAxis.normalized * SPEED;
        Vector3 newPosition = new Vector3(8, -4, 0);
        //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPosition, Time.fixedDeltaTime * 0.5f);
    }
}
   

