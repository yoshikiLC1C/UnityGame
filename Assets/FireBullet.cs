using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float shotSpeed;
    public int shotCount = 30;
    private float shotInterval;

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
        // íeÇÃî≠éÀ
        ShotBullet();
        // í«îˆíe
        if (isSpawning)
            return;

        StartCoroutine(nameof(SpawnMissile));

        ShotHomingBullet();
    }

    private void ShotBullet()
    {
        if (Input.GetKey(KeyCode.Mouse0))
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
        else if (Input.GetKeyDown(KeyCode.R))
        {
            shotCount = 30;
        }
    }

    IEnumerator SpawnMissile()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            isSpawning = true;

            Vector3 euler;
            Quaternion rot;
            HomingBullet homing;

            for (int i = 0; i < iterationCount; i++)
            {
                homing = Instantiate(homingPrefab, thisTransform.position,
                    Quaternion.identity).GetComponent<HomingBullet>();
                homing.Target = target;
            }

            yield return intervalWait;

            isSpawning = false;
        }
    }

    private void ShotHomingBullet()
    {
        if(Input.GetKey(KeyCode.Mouse1))
        {
            
        }
        

    }
}



//í«îˆíe
//if (target == null)
//    return;

//GameObject bullet = (GameObject)Instantiate(bulletPrefab,
//        transform.position, Quaternion.Euler(transform.parent.eulerAngles.x,
//        transform.parent.eulerAngles.y, 0));
//Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

//var acceleration = Vector3.zero;
//var diff = target.transform.position - bulletRb.transform.position;

//// ë¨ìxvelocityÇÃï®ëÃÇ™periodïbå„Ç…diffêiÇﬁÇΩÇﬂÇÃâ¡ë¨ìx
//acceleration += (diff - velocity * period) * 2f / (period * period);

//if (0 < randomPeriod)
//{
//    var xr = Random.Range(-randomPower, randomPower);
//    var yr = Random.Range(-randomPower, randomPower);
//    var zr = Random.Range(-randomPower, randomPower);
//    acceleration += new Vector3(xr, yr, zr);
//}

//if (acceleration.magnitude > maxAcceleration)
//{
//    acceleration = acceleration.normalized * maxAcceleration;
//}

//period -= Time.deltaTime;
//randomPeriod -= Time.deltaTime;
//if (period < 0f)
//    return;

//velocity += acceleration * Time.deltaTime;
//bulletRb.transform.position += velocity * Time.deltaTime;
//bulletRb.AddForce(bulletRb.transform.position);


//Destroy(bullet, 3.0f);