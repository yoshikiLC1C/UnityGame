using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    // 最大HPと現在のHP
    private int maxHp = 100;
    public int currentHp;

    [SerializeField]
    private Slider slider;
    [SerializeField]
    private PlayerHP playerHp;

    private float Speed = 5f;

    private const float RotateSpeed = 720f;

    private const float jumpPower = 5f;

    private const float QBPower = 80;

    private Rigidbody rb;

    [SerializeField]
    private Transform _unityChan;

    private Animator _unityChanAnimator;

    public Action OnGetItemCallback;

    private bool _isFinished = false;

    [SerializeField]
    private CameraScript refCamera;

    // 接地判定
    [SerializeField]
    float groundCheckRadius = 0.4f;
    [SerializeField]
    float groundCheckOffsetY = 0.45f;
    [SerializeField]
    float groundCheckDistance = 0.2f;
    [SerializeField]
    LayerMask groundLayers = 0;

    RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        _unityChanAnimator = _unityChan.GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

        // Sliderを満タンにする
        slider.value = 1;
        // 現在のHPを最大HPと同じにする
        currentHp = maxHp;
        Debug.Log("Start currentHp : " + currentHp);
    }



    // Update is called once per frame
    void Update()
    {
        if (_isFinished)
        {
            return;
        }
        // キーボード入力を進行方向のベクトルに変換して返す
        Vector3 direction = InputToDirection();

        // 進行方向ベクトルの大きさ
        float magnitude = direction.magnitude;

        

        // 進行方向ベクトルが移動量を持っているかどうか
        if (Mathf.Approximately(magnitude, 0f) == false)
        {
            _unityChanAnimator.SetBool("running", true);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                Speed *= 1.75f;
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Speed /= 1.75f;
            }
            //UpdateRotation(direction);
        }
        else
        {
            _unityChanAnimator.SetBool("running", false);
        }

        UpdatePosition(direction);

        Jump();
        QuickBoost();
    }

    

    private Vector3 InputToDirection()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, 0f, z);

        return direction.normalized;
    }

    // 位置を更新
    private void UpdatePosition(Vector3 direction)
    {
        // カメラの方向から、X-Z平面の単位ベクトルを取得
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // 方向キーの入力値とカメラの向きから、移動方向を決定
        Vector3 moveForward = cameraForward * direction.z + Camera.main.transform.right * direction.x;

        // 移動方向にスピードを掛ける。ジャンプや落下がある場合は、別途Y軸方向の速度ベクトルを足す
        rb.velocity = moveForward * Speed + new Vector3(0, rb.velocity.y, 0);

        // キー入力により移動方向が決まっている場合には、キャラクターの向きを進行方向に合わせる
        if (moveForward != Vector3.zero)
        {
            Quaternion from = _unityChan.rotation;
            _unityChan.rotation = Quaternion.RotateTowards(from, Quaternion.LookRotation(moveForward),
                RotateSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.AddForce(moveForward * QBPower, ForceMode.Impulse);
        }
    }


    private void Jump()
    {
        if (CheckGroundStatus() == false) return;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * jumpPower, ForceMode.Impulse);
            //jumpNow = true;
        }
    }

    private void QuickBoost()
    {
        
    }

    bool CheckGroundStatus()
    {
        return Physics.SphereCast(transform.position + groundCheckOffsetY
            * Vector3.up, groundCheckRadius, Vector3.down, out hit,
            groundCheckDistance, groundLayers, QueryTriggerInteraction.Ignore);
    }

    // ほかのトリガイベントに侵入した際に呼ばれる
    private void OnTriggerEnter(Collider other)
    {
        
        // アイテムを取る処理
        if (other.gameObject.CompareTag("Item"))
        {
            Item item = other.gameObject.GetComponent<Item>();
            item.Gotten();
            OnGetItemCallback();
        }

    }

    // 終了処理
    public void Finish()
    {
        _isFinished = true;
        _unityChan.rotation = Quaternion.Euler(0f, 180f, 0f);
        if (currentHp > 0)
        {
            _unityChanAnimator.SetBool("finish", true);
        }
    }
    public void Damage()
    {
        playerHp.StartShake(1.0f,10.0f,1.0f);
        int damage = 10;
        Debug.Log("damage : " + damage);

        //現在のHPからダメージを引く
        currentHp = currentHp - damage;
        Debug.Log("After currentHp : " + currentHp);

        //最大HPにおける現在のHPをSliderに反映
        slider.value = (float)currentHp / (float)maxHp;
        Debug.Log("slider.value : " + slider.value);
    }

}
