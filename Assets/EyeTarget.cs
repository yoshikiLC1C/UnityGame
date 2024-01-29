using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EyeTarget : MonoBehaviour
{

    [SerializeField]
    private SphereCollider searchArea = default;
    [SerializeField]
    private float searchAngle = 45f;
    [SerializeField]
    private bool editor;

    [SerializeField] 
    private GameObject Target;

    // Start is called before the first frame update
    void Start()
    {
        //Destroy(this.gameObject, time);
    }

    // Update is called once per frame
    void Update()
    {
       //if(Target)
       //{
       //    transform.LookAt(Target.transform);
       //}

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            var playerDirection = other.transform.position - transform.position;

            var angle = Vector3.Angle(transform.forward, playerDirection);

            if (angle <= searchAngle)
            {
                //Target = other.gameObject;
                Target.transform.position = Vector3.Lerp(Target.transform.position, other.transform.position, 0.1f);
                
            }
            else if(angle > searchAngle)
            {
                Target.transform.position = Vector3.Lerp(Target.transform.position, transform.position, 0.1f);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //Target = null;
        Target.transform.position = transform.position;
    }

#if UNITY_EDITOR
    // サーチ角度表示
    private void OnDrawGizmos()
    {
        if(editor)
        {
            Handles.color = Color.red;
            Handles.DrawSolidArc(transform.position, Vector3.up,
                Quaternion.Euler(0f, -searchAngle, 0f) * transform.forward,
                searchAngle * 2f, searchArea.radius * 0.5f);
        }
    }
#endif
}
