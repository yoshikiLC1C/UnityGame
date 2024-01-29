using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerHP : MonoBehaviour
{

    // 揺れ情報
    private struct ShakeInfo
    {
        public ShakeInfo(float duration, float strength, float vibrato, Vector2 randomOffset)
        {
            Duration = duration;
            Strength = strength;
            Vibrato = vibrato;
            RandomOffset = randomOffset;
        }
        public float Duration { get; } // 時間
        public float Strength { get; } // 揺れの強さ
        public float Vibrato { get; } // どのくらい振動するか
        public Vector2 RandomOffset { get; } // ランダムオフセット値
    }

    private ShakeInfo _shakeInfo;

    private Vector3 _initPosition; // 初期位置
    private bool _isDoShake;       // 揺れているか
    private float _totalShakeTime; // 揺れの経過時間



    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を保存
        _initPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_isDoShake) return;
        
        // 情報更新
        gameObject.transform.position = GetUpdateShakePosition(
            _shakeInfo,
            _totalShakeTime,
            _initPosition);

        // duration分の時間が経過したら揺らすのを止める
        _totalShakeTime += Time.deltaTime;
        if(_totalShakeTime >= _shakeInfo.Duration)
        {
            _isDoShake = false;
            _totalShakeTime = 0.0f;
            //初期値に戻す
            gameObject.transform.position = _initPosition;
        }
        
    }

    // 更新後の揺れ位置を取得
    private Vector3 GetUpdateShakePosition(ShakeInfo shakeInfo, float totalTime, Vector3 initPosition)
    {
        var strength = shakeInfo.Strength;
        var randomOffset = shakeInfo.RandomOffset;
        var randomX = GetPerlinNoiseValue(randomOffset.x, strength, totalTime);
        var randomY = GetPerlinNoiseValue(randomOffset.y, strength, totalTime);

        // -strength ～ strength の値に変換
        randomX *= strength;
        randomY *= strength;

        // -vibrato ～ vibrato の値に変換
        var vibrato = shakeInfo.Vibrato;
        var ratio = 1.0f - totalTime / shakeInfo.Duration;
        vibrato *= ratio; // フェードアウトさせるため、経過時間により揺れの量を減衰
        randomX = Mathf.Clamp(randomX, -vibrato, vibrato);
        randomY = Mathf.Clamp(randomY, -vibrato, vibrato);

        // 初期値に加える形で設定する
        var position = initPosition;
        position.x += randomX;
        position.y += randomY;
        return position;
        
    }

    // バーリンノイズ値を取得
    private float GetPerlinNoiseValue(float offset, float speed, float time)
    {
        // パーリンノイズ値を取得する
        // X: オフセット値 + 速度 * 時間 , Y: 0.0固定
        var perlinNoise = Mathf.PerlinNoise(offset + speed * time, 0.0f);
        // 0.0〜1.0 -> -1.0〜1.0に変換して返却
        return (perlinNoise - 0.5f) * 2.0f;
    }

    /// 揺れ開始
    public void StartShake(float duration, float strength, float vibrato)
    {
        // 揺れ情報を設定して開始
        var randomOffset = new Vector2(Random.Range(0.0f, 100.0f), Random.Range(0.0f, 100.0f)); // ランダム値はとりあえず0〜100で設定
        _shakeInfo = new ShakeInfo(duration, strength, vibrato, randomOffset);
        _isDoShake = true;
        _totalShakeTime = 0.0f;
    }
}
