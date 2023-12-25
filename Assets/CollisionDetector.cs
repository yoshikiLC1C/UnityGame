using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{

    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float shotSpeed = 150;
    [SerializeField]
    int shotCount = 30;
    [SerializeField]
    float shotInterval;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // 弾の発射
        //ShotBullet();
        
    }

    private void ShotBullet()
    {
        //shotInterval += 0.5f;

        //if (shotInterval % 5 == 0 && shotCount > 0)
        //{
        //    shotCount -= 1;

        //    GameObject bullet = (GameObject)Instantiate(bulletPrefab,
        //        transform.position, Quaternion.Euler(transform.parent.eulerAngles.x,
        //        transform.parent.eulerAngles.y, 0));
        //    Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        //    bulletRb.AddForce(transform.forward * shotSpeed);

        //    Destroy(bullet, 3.0f);
        //}

        //GameObject.Find("Player");

        shotInterval += 0.5f;

        if (shotInterval % 5 == 0)
        {
            GameObject bullet = (GameObject)Instantiate(bulletPrefab,
                transform.position, Quaternion.identity);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            bulletRb.AddForce(transform.forward * shotSpeed);

            Destroy(bullet, 3.0f);
        }
    }

    // 重なっていたら呼ばれる
    private void OnTriggerStay(Collider other)
    {
        //// InspectorタブのonTriggerStayで指定された処理を実行する
        //onTriggerStay.Invoke(other);

        // 敵が侵入したら撃つ処理
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.DrawLine(transform.position + Vector3.up, other.transform.position + Vector3.up, Color.blue);

            transform.LookAt(player.transform);
            ShotBullet();

        }
    }
}
