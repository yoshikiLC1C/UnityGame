using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // 最大HPと現在のHP
    private int maxHp = 100;
    private int currentHp;

    [SerializeField]
    private Slider slider;

    public Canvas canvas;

    //public Action OnCallback;

    //public bool deathFrag;

    [SerializeField]
    private Game game;

    // Start is called before the first frame update
    void Start()
    {
        // Sliderを満タンにする
        slider.value = 1;
        // 現在のHPを最大HPと同じにする
        currentHp = maxHp;
        Debug.Log("Start currentHp : " + currentHp);
    }

    // Update is called once per frame
    void Update()
    {
        //HPバーをプレイヤーの方に向ける
        canvas.transform.rotation = 
            Camera.main.transform.rotation;
    }

    // CollisionDetector.csのonTriggerStayにセットし、衝突中に実行される
    private void OnTriggerEnter(Collider other)
    {
       
    }

    // 終了処理
    public void Finish()
    {
        // 演出爆破
    }

    public void Damage()
    {
        int damage = 10;
        Debug.Log("damage : " + damage);

        //現在のHPからダメージを引く
        currentHp = currentHp - damage;
        Debug.Log("After currentHp : " + currentHp);

        //最大HPにおける現在のHPをSliderに反映
        slider.value = (float)currentHp / (float)maxHp;
        Debug.Log("slider.value : " + slider.value);

        if(currentHp <= 0)
        {

            Destroy(gameObject);
            game.OnEnemyCount();

        }
    }
}
