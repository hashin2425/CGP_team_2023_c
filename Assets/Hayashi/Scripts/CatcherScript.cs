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
        //�}�E�X���W�̎擾
        mousePos = Input.mousePosition;
        //�X�N���[�����W�����[���h���W�ɕϊ�
        worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f));
        worldPos.z = 0.01f;
        //���[���h���W�����g�̍��W�ɐݒ�
        transform.position = worldPos;
        if (Input.GetMouseButton(0))
        {
            // �N���b�N����Ă���ꍇ�A�F��΂ɂ���
            spriteRenderer.color = Color.gray;
            gameObject.tag = "Catcher_sunk";
        }
        else
        {
            // "MyTag"�Ƃ����^�O�����S�ẴI�u�W�F�N�g���擾
            GameObject[] objects = GameObject.FindGameObjectsWithTag("Fish");

            // �e�I�u�W�F�N�g�ɑ΂��ď������s��
            foreach (GameObject obj in objects)
            {
                FishInstance script = obj.GetComponent<FishInstance>();
                script.isCaught = false;
            }
            // �N���b�N����Ă��Ȃ��ꍇ�A�F���D�F�ɂ���
            spriteRenderer.color = Color.white;
            gameObject.tag = "Catcher";
        }
    }
}