using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float time = 1;
    [SerializeField]
    float lifeTime = 2;
    [SerializeField]
    bool limitAcceleration = false;
    [SerializeField]
    float maxAcceleration = 100;
    [SerializeField]
    Vector3 minInitVelocity;
    [SerializeField]
    Vector3 maxInitVelocity;

    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    Transform thisTransform;

    public Transform Target
    {
        set
        {
            target = value;
        }
        get
        {
            return target;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        thisTransform = transform;
        position = thisTransform.position;
        velocity = new Vector3(
                Random.Range(minInitVelocity.x, maxInitVelocity.x),
                Random.Range(minInitVelocity.y, maxInitVelocity.y),
                Random.Range(minInitVelocity.z, maxInitVelocity.z));

        StartCoroutine(nameof(Timer));
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        acceleration =
            2f / (time * time) * (target.position - position - time * velocity);

        if (limitAcceleration && acceleration.sqrMagnitude
            > maxAcceleration * maxAcceleration)
        {
            acceleration = acceleration.normalized * maxAcceleration;
        }

        time -= Time.deltaTime;
        if(time < 0f)
        {
            return;
        }

        velocity += acceleration * Time.deltaTime;
        position += velocity * Time.deltaTime;
        thisTransform.position = position;
        thisTransform.rotation = Quaternion.LookRotation(velocity);
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(lifeTime);

        Destroy(gameObject);
    }
}
