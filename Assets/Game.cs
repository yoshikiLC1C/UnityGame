using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    // アイテムの数
    public const int Total = 10;

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

    // クリアの画像
    [SerializeField]
    private Image _clearImage;

    //「もう一度」ボタン
    [SerializeField]
    private Button _restartButton;

    // Start is called before the first frame update
    void Start()
    {
        SetRestCount(Total);
        //CreateItems();
        _player.OnGetItemCallback = OnGetItem;

        //マウスカーソルを非表示にし、位置を固定
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        
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
    private void OnGetItem()
    {
        SetRestCount(_restCount - 1);
        if(_restCount == 0)
        {
            Finish();
        }
    }

    // 終了処理
    private void Finish()
    {
        _player.Finish();
        _clearImage.gameObject.SetActive(true);
        _restartButton.gameObject.SetActive(true);
    }

    // もう一度遊ぶ処理
    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
