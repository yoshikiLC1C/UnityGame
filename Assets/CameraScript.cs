using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    public Quaternion hRotation;

    private float mouseSensitivity = 200f;

    private float xRotation = 0;
    private float yRotation = 0;

    //public float mx;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // マウスの移動量を取得
        float mx = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //hRotation *= Quaternion.Euler(0, mx * 3, 0);

        xRotation = mx;
        yRotation = my;

        //yRotation = Mathf.Clamp(yRotation, -90, 90);


        // X方向に一定量移動していれば横回転
        if (Mathf.Abs(mx) > 0.001f)
        {
            // 回転軸はワールド座標のY軸
            transform.RotateAround(player.transform.position,
                Vector3.up, mx);
        }

        // Y方向に一定量移動していれば縦回転
        if (Mathf.Abs(my) > 0.001f)
        {
            // 回転軸はワールド座標のX軸
            transform.RotateAround(player.transform.position,
                transform.right, -my);
        }

        

        //player.(Vector3.up * mx);
    }
}
