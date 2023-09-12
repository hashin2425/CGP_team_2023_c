using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem2 : MonoBehaviour
{
    //�A�C�e���v���n�u
    public GameObject itemPrefab;
    //�G�������ԊԊu
    private float interval;
    //�o�ߎ���
    private float time = 0f;
    // Start is called before the first frame update
    void Start()
    {
        //���ԊԊu�����肷��
        interval = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //���Ԍv��
        time += Time.deltaTime;

        //�o�ߎ��Ԃ��������ԂɂȂ����Ƃ�(�������Ԃ��傫���Ȃ����Ƃ�)
        if (time > interval)
        {
            //enemy���C���X�^���X������(��������)
            GameObject item = Instantiate(itemPrefab);
            //���������G�̍��W�����肷��(����X=0,Y=10,Z=20�̈ʒu�ɏo��)
            item.transform.position = new Vector2(10, -1);
            //�o�ߎ��Ԃ����������čēx���Ԍv�����n�߂�
            time = 0f;
        }
    }
}
