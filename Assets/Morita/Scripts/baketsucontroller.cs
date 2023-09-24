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
    private int score; // �X�Ra
    private Vector2 inputAxis;


    void Start()
    {
        // �I�u�W�F�N�g�ɐݒ肵�Ă���Rigidbody2D�̎Q�Ƃ��擾����
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

        //�}�E�X���W�̎擾
        // mousePos = Input.mousePosition;
        //�X�N���[�����W�����[���h���W�ɕϊ�
        //worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        //���[���h���W�����g�̍��W�ɐݒ�
        // transform.position = worldPos;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        // �Ԃ������I�u�W�F�N�g�����W�A�C�e���������ꍇ
        if (other.gameObject.CompareTag("potato"))
        {
            // ���̎��W�A�C�e�����\���ɂ��܂�
            other.gameObject.SetActive(false);

            // �X�R�A�����Z���܂�
            score = score + 1;

            // UI �̕\�����X�V���܂�
            SetCountText();
        }
    }
    void SetCountText()
    {
        // �X�R�A�̕\�����X�V
        potatoScore.text = "potato: " + score.ToString();
    }
    private void FixedUpdate()
    {
        // ���x��������

        rigidBody.velocity = inputAxis.normalized * SPEED;
        Vector3 newPosition = new Vector3(8, -4, 0);
        //gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, newPosition, Time.fixedDeltaTime * 0.5f);
    }
}
   

