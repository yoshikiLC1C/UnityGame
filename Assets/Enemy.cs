using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    int DamageCount = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // CollisionDetector.cs��onTriggerStay�ɃZ�b�g���A�Փ˒��Ɏ��s�����
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("playerBullet"))
        //{
        //    FireBullet bullet =  other.gameObject.GetComponent<FireBullet>();
        //    Destroy(bullet);
        //}
    }

    // �I������
    public void Finish()
    {
        // ���o���j
    }

    public void Damage()
    {
        DamageCount++;
        if(DamageCount >= 30)
        {
            Destroy(gameObject);
        }
    }
}
