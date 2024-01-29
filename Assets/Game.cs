using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // 敵の数
    public int Total = 0;

    // 残数
    private int _restCount;


    // 残数テキスト
    [SerializeField]
    private Text _restCountText;

    // プレイヤー
    [SerializeField]
    private Player _player;

    // 敵のPrefab
    [SerializeField]
    private GameObject _EnemyPrefab;

    private GameObject[] enemyObjects;
    private int enemyNum;

    // クリアの画像
    [SerializeField]
    private Image _clearImage;

    [SerializeField]
    private Text _text;

    //「もう一度」ボタン
    [SerializeField]
    private Button _restartButton;

    //「タイトル」ボタン
    [SerializeField]
    private Button _titleButton;

    // Start is called before the first frame update
    void Start()
    {

        enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        Total = enemyObjects.Length;

        SetRestCount(Total);
        //CreateEnemys();
        //_enemy. = OnEnemyCount;

        

        //マウスカーソルを非表示にし、位置を固定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //OnEnemyCount();
        if(_player.currentHp <= 0)
        {
            Finish();
        }
    }

    // 敵の生成
    private void CreateEnemys()
    {
        for(int i = 0; i < Total; i++)
        {
            GameObject Enemy = Instantiate(_EnemyPrefab);
            Enemy.transform.position = GetRandomItemPosition();
        }
    }

    // 敵　ランダム座標
    private Vector3 GetRandomItemPosition()
    {
        // -20.0 ～ 20.0の間でランダムにX座標を決定
        var x = UnityEngine.Random.Range(-20f, 20f);
        // 1/2の確率で反転
        if(UnityEngine.Random.Range(0, 2) % 2 == 0)
        {
            x += -1f;
        }

        // -20.0 ～ 20.0の間でランダムにZ座標を決定
        var z = UnityEngine.Random.Range(-20f, 20f);
        // 1/2の確率で反転
        if (UnityEngine.Random.Range(0, 2) % 2 == 0)
        {
            z += -1f;
        }

        return new Vector3(x, 2f, z);
    }

    // 残り敵の数を設定
    private void SetRestCount(int value)
    {
        _restCount = value;
        _restCountText.text = string.Format("残り{0}体", _restCount);
    }

    // アイテム取得時の処理
    public void OnEnemyCount()
    {
        SetRestCount(_restCount - 1);
        if (_restCount == 0)
        {
            Finish();
        }
    }

    // 終了処理
    private void Finish()
    {
        if (_player.currentHp > 0)
        {
            _player.Finish();
            _clearImage.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(true);
            _titleButton.gameObject.SetActive(true);
        }
        else
        {
            _player.Finish();
            _text.gameObject.SetActive(true);
            _restartButton.gameObject.SetActive(true);
            _titleButton.gameObject.SetActive(true);
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // もう一度遊ぶ処理
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
    // タイトルに戻る処理
    public void Title()
    {
        SceneManager.LoadScene("Title");
    }
}
