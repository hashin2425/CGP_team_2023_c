using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatcherScript : MonoBehaviour
{
    private Vector3 offset;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        offset = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f)) - transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        transform.position = mousePosition - offset;
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