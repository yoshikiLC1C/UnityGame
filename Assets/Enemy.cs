using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // �ő�HP�ƌ��݂�HP
    private int maxHp = 100;
    private int currentHp;

    [SerializeField]
    private Slider slider;

    public Canvas canvas;

    //public Action OnCallback;

    //public bool deathFrag;

    [SerializeField]
    private Game game;

    // Start is called before the first frame update
    void Start()
    {
        // Slider�𖞃^���ɂ���
        slider.value = 1;
        // ���݂�HP���ő�HP�Ɠ����ɂ���
        currentHp = maxHp;
        Debug.Log("Start currentHp : " + currentHp);
    }

    // Update is called once per frame
    void Update()
    {
        //HP�o�[���v���C���[�̕��Ɍ�����
        canvas.transform.rotation = 
            Camera.main.transform.rotation;
    }

    // CollisionDetector.cs��onTriggerStay�ɃZ�b�g���A�Փ˒��Ɏ��s�����
    private void OnTriggerEnter(Collider other)
    {
       
    }

    // �I������
    public void Finish()
    {
        // ���o���j
    }

    public void Damage()
    {
        int damage = 10;
        Debug.Log("damage : " + damage);

        //���݂�HP����_���[�W������
        currentHp = currentHp - damage;
        Debug.Log("After currentHp : " + currentHp);

        //�ő�HP�ɂ����錻�݂�HP��Slider�ɔ��f
        slider.value = (float)currentHp / (float)maxHp;
        Debug.Log("slider.value : " + slider.value);

        if(currentHp <= 0)
        {

            Destroy(gameObject);
            game.OnEnemyCount();

        }
    }
}
