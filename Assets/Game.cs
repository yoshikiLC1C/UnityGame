using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // �A�C�e���̐�
    public const int Total = 10;

    // �c��
    private int _restCount;

    // �c���e�L�X�g
    [SerializeField]
    private Text _restCountText;

    // �v���C���[
    [SerializeField]
    private Player _player;

    // �G��Prefab
    [SerializeField]
    private GameObject _EnemyPrefab;

    // �N���A�̉摜
    [SerializeField]
    private Image _clearImage;

    //�u������x�v�{�^��
    [SerializeField]
    private Button _restartButton;

    // Start is called before the first frame update
    void Start()
    {
        SetRestCount(Total);
        //CreateItems();
        _player.OnGetItemCallback = OnGetItem;

        //�}�E�X�J�[�\�����\���ɂ��A�ʒu���Œ�
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // �G�̐���
    private void CreateEnemys()
    {
        for(int i = 0; i < Total; i++)
        {
            GameObject Enemy = Instantiate(_EnemyPrefab);
            Enemy.transform.position = GetRandomItemPosition();
        }
    }

    // �G�@�����_�����W
    private Vector3 GetRandomItemPosition()
    {
        // -20.0 �` 20.0�̊ԂŃ����_����X���W������
        var x = UnityEngine.Random.Range(-20f, 20f);
        // 1/2�̊m���Ŕ��]
        if(UnityEngine.Random.Range(0, 2) % 2 == 0)
        {
            x += -1f;
        }

        // -20.0 �` 20.0�̊ԂŃ����_����Z���W������
        var z = UnityEngine.Random.Range(-20f, 20f);
        // 1/2�̊m���Ŕ��]
        if (UnityEngine.Random.Range(0, 2) % 2 == 0)
        {
            z += -1f;
        }

        return new Vector3(x, 2f, z);
    }

    // �c��G�̐���ݒ�
    private void SetRestCount(int value)
    {
        _restCount = value;
        _restCountText.text = string.Format("�c��{0}��", _restCount);
    }

    // �A�C�e���擾���̏���
    private void OnGetItem()
    {
        SetRestCount(_restCount - 1);
        if(_restCount == 0)
        {
            Finish();
        }
    }

    // �I������
    private void Finish()
    {
        _player.Finish();
        _clearImage.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }

    // ������x�V�ԏ���
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
