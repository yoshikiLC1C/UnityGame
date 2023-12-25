using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySooting : MonoBehaviour
{

    [SerializeField]
    GameObject Player;

    [SerializeField]
    GameObject bulletPrefab;
    float shotSpeed;
    int shotCount = 30;
    float shotInterval;

    [SerializeField]
    Transform target;
    [SerializeField]
    GameObject homingPrefab;
    [SerializeField]
    int iterationCount = 3;
    [SerializeField]
    float interval = 0.1f;

    bool isSpawning = false;
    Transform thisTransform;
    WaitForSeconds intervalWait;

    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
        intervalWait = new WaitForSeconds(interval);
    }

    // Update is called once per frame
    void Update()
    {
        // ’e‚Ì”­Ë
        //ShotBullet();
        // ’Ç”ö’e
        if (isSpawning)
            return;

        StartCoroutine(nameof(SpawnMissile));

        ShotHomingBullet();
    }

    private void ShotBullet()
    {
        shotInterval += 0.5f;

        if (shotInterval % 5 == 0 && shotCount > 0)
        {
            shotCount -= 1;

            GameObject bullet = (GameObject)Instantiate(bulletPrefab,
                transform.position, Quaternion.Euler(transform.parent.eulerAngles.x,
                transform.parent.eulerAngles.y, 0));
            Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
            bulletRb.AddForce(transform.forward * shotSpeed);

            Destroy(bullet, 3.0f);
        }
    }

    IEnumerator SpawnMissile()
    {
        isSpawning = true;

        Vector3 euler;
        Quaternion rot;
        HomingBullet homing;

        if (target == true)
        {
            for (int i = 0; i < iterationCount; i++)
            {
                homing = Instantiate(homingPrefab, thisTransform.position,
                    Quaternion.identity).GetComponent<HomingBullet>();
                homing.Target = target;
            }
        }

        yield return intervalWait;

        isSpawning = false;
    }

    private void ShotHomingBullet()
    {
        
    }

    // UŒ‚”ÍˆÍ‚ÉN“ü‚µ‚½Û‚ÉŒÄ‚Î‚ê‚é
    private void OnTriggerEnter(Collider other)
    {
        // “G‚ªN“ü‚µ‚½‚çŒ‚‚Âˆ—
        if (other.gameObject.CompareTag("Player"))
        {
            Player player = other.gameObject.GetComponent<Player>();

            ShotBullet();
        }
    }
}
