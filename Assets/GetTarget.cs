using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class GetTarget : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    protected GameObject GetTargetClosestPlayer()
    {
        float search_radius = 30f;

        var hits = Physics.SphereCastAll(
            player.transform.position,
            search_radius,
            player.transform.forward,
            0.01f
            ).Select(h => h.transform.gameObject).ToList();

        if(0 < hits.Count())
        {
            float min_target_distance = float.MaxValue;
            GameObject target = null;

            foreach (var hit in hits)
            {
                float target_distance = Vector3.Distance(player.transform.position,
                    hit.transform.position);
                if(target_distance < min_target_distance)
                {
                    min_target_distance = target_distance;
                    target = hit.transform.gameObject;
                }
            }

            return target;
        }
        else
        {
            return null;
        }
    }

    protected List<GameObject> FilterTargetObject(List<GameObject> hits)
    {
        return hits.Where(h =>
        {
            Vector3 screenPoint = Camera.main.WorldToViewportPoint(h.transform.position);
            return screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
        })
        .Where(h => h.tag == "Enemy")
        .ToList();
    }

}
