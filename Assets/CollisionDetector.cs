using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{

    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    float shotSpeed = 150;
    [SerializeField]
    int shotCount = 30;
    [SerializeField]
    float shotInterval;

    [SerializeField]
    private SphereCollider searchArea = default;
    [SerializeField]
    private float searchAngle = 45f;
    [SerializeField]
    private bool editor;

    [SerializeField]
    private GameObject Target;
    [SerializeField]
    private GameObject LaunchPoint;

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
                LaunchPoint.transform.position, Quaternion.identity);
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

            bulletRb.AddForce((LaunchPoint.transform.forward) * shotSpeed);

            Destroy(bullet, 3.0f);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            var playerDirection = other.transform.position - transform.position;

            playerDirection.y += 5;

            var angle = Vector3.Angle(transform.forward, playerDirection);

            if (angle <= searchAngle)
            {
                //Target = other.gameObject;
                Target.transform.position = Vector3.Lerp(Target.transform.position, other.transform.position, 0.1f);
                
                Debug.DrawLine(transform.position + Vector3.up, other.transform.position + Vector3.up, Color.blue);
                //transform.LookAt(other.transform);
                ShotBullet();
            }
            else// if (angle > searchAngle)
            {
                Target.transform.position = Vector3.Lerp(Target.transform.position, LaunchPoint.transform.position + Vector3.forward, 0.1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Target = null;
            Target.transform.position = LaunchPoint.transform.position;
        }
    }

#if UNITY_EDITOR
    // サーチ角度表示
    private void OnDrawGizmos()
    {
        if (editor)
        {
            Handles.color = Color.red;
            Handles.DrawSolidArc(transform.position, Vector3.up,
                Quaternion.Euler(0f, -searchAngle, 0f) * transform.forward,
                searchAngle * 2f, searchArea.radius * 1f);
        }
    }
#endif
}
