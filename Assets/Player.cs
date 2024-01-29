using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{

    // �ő�HP�ƌ��݂�HP
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

    // �ڒn����
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

        // Slider�𖞃^���ɂ���
        slider.value = 1;
        // ���݂�HP���ő�HP�Ɠ����ɂ���
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
        // �L�[�{�[�h���͂�i�s�����̃x�N�g���ɕϊ����ĕԂ�
        Vector3 direction = InputToDirection();

        // �i�s�����x�N�g���̑傫��
        float magnitude = direction.magnitude;

        

        // �i�s�����x�N�g�����ړ��ʂ������Ă��邩�ǂ���
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

    // �ʒu���X�V
    private void UpdatePosition(Vector3 direction)
    {
        // �J�����̕�������AX-Z���ʂ̒P�ʃx�N�g�����擾
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;

        // �����L�[�̓��͒l�ƃJ�����̌�������A�ړ�����������
        Vector3 moveForward = cameraForward * direction.z + Camera.main.transform.right * direction.x;

        // �ړ������ɃX�s�[�h���|����B�W�����v�◎��������ꍇ�́A�ʓrY�������̑��x�x�N�g���𑫂�
        rb.velocity = moveForward * Speed + new Vector3(0, rb.velocity.y, 0);

        // �L�[���͂ɂ��ړ����������܂��Ă���ꍇ�ɂ́A�L�����N�^�[�̌�����i�s�����ɍ��킹��
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

    // �ق��̃g���K�C�x���g�ɐN�������ۂɌĂ΂��
    private void OnTriggerEnter(Collider other)
    {
        
        // �A�C�e������鏈��
        if (other.gameObject.CompareTag("Item"))
        {
            Item item = other.gameObject.GetComponent<Item>();
            item.Gotten();
            OnGetItemCallback();
        }

    }

    // �I������
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

        //���݂�HP����_���[�W������
        currentHp = currentHp - damage;
        Debug.Log("After currentHp : " + currentHp);

        //�ő�HP�ɂ����錻�݂�HP��Slider�ɔ��f
        slider.value = (float)currentHp / (float)maxHp;
        Debug.Log("slider.value : " + slider.value);
    }

}
