using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    [SerializeField]
    private GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        var dir = (Target.transform.position - transform.position).normalized;

        dir.y = 0;
        Quaternion setRotation = Quaternion.LookRotation(dir);

        //transform.rotation = Quaternion.Slerp(transform.rotation,setRotation, 1f * Time.deltaTime);

    }
}
