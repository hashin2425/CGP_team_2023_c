using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
    //���W�p�̕ϐ�
    Vector3 mousePos, worldPos;

    private GameObject square;
    private SpriteRenderer SpriteRenderer;
    void Update()
    {
        //�}�E�X���W�̎擾
        mousePos = Input.mousePosition;
        //�X�N���[�����W�����[���h���W�ɕϊ�
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        //���[���h���W�����g�̍��W�ɐݒ�
        transform.position = worldPos;
        if (Input.GetMouseButtonDown(0))
        {
            square = GameObject.Find("Square");
            SpriteRenderer = square.GetComponent<SpriteRenderer>();
            SpriteRenderer.sortingOrder = 0;
        }
        //�{�^���������u�Ԃ̂�A�摜�ς���΍s������
        if (Input.GetMouseButtonUp(0))
        {
            square = GameObject.Find("Square");
            SpriteRenderer = square.GetComponent<SpriteRenderer>();
            SpriteRenderer.sortingOrder = 1;
        }
        //�{�^���������u�Ԃ̂�A�����v���n�u�̊֐������s�ł�����
    }
}
