using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // チョコレートの3D
    [SerializeField]
    private GameObject _chocolate3d;

    // 衝突判定用のコライダー
    private BoxCollider _collider;


    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();
    }

    public void Gotten()
    {
        _chocolate3d.SetActive(false);
        _collider.enabled = false;
    }
}
