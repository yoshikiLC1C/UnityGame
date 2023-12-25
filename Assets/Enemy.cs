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

    // CollisionDetector.csのonTriggerStayにセットし、衝突中に実行される
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("playerBullet"))
        //{
        //    FireBullet bullet =  other.gameObject.GetComponent<FireBullet>();
        //    Destroy(bullet);
        //}
    }

    // 終了処理
    public void Finish()
    {
        // 演出爆破
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
