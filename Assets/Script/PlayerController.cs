using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー情報の制御クラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    //現在のHP
    private int hp;

    //現在の弾数値
    private int bulletCount;

    [SerializeField, Header("最大HP値")]
    private int maxHp;

    [SerializeField, Header("最大弾数値")]
    private int maxBullet;

    [SerializeField, Header("リロード時間")]
    private float reloadTime;

    [Header("弾の攻撃力")]
    public int bulletPower;

    [Header("弾の連射速度")]
    public float shootInterval;

    [Header("弾の射程距離")]
    public float shootRange;

    [Header("リロード機能のオン/オフ")]
    public bool isReloadModeOn;

    [Header("リロード状態の制御")]
    public bool isReloading;

    /// <summary>
    /// 弾丸用のプロパティ
    /// </summary>
    public int BulletCount
    {
        set => bulletCount = Mathf.Clamp(value, 0, maxBullet);
        get => bulletCount;
    }

    /// <summary>
    /// HP用のプロパティ(のちに設定)
    /// </summary>
    public int HpProperty
    {
        set
        {
            hp = value;
            hp = Mathf.Clamp(hp, 0, maxHp);
        }
        get
        {
            return maxHp;
        }
    }
    void Start()
    {
        //デバック用
        SetUpPlayer();    
    }

    /// <summary>
    /// プレイヤー情報の初期設定
    /// </summary>
    
    //外部クラスからの呼び出しを想定してpublicで宣言
    public void SetUpPlayer()
    {
        //maxHpの設定があるかの確認、初期HPを10として設定
        hp = maxHp = maxHp == 0 ? 10 : maxHp;

        //maxBulletの設定があるかの確認、初期値を10として設定
        if(maxBullet == 0)
        {
            maxBullet = 10;
        }

        BulletCount = maxBullet;

        Debug.Log(BulletCount);
    }

    /// <summary>
    /// HPの計算と更新
    /// </summary>
    /// <param name="amout"></param>
    public void CalcHp(int amount)
    {
        hp = Mathf.Clamp(hp += amount, 0, maxHp);

        //確認でき次第コメントアウト
        Debug.Log("現在のHp :" + hp);

        //ToDo HP 表記の更新
        if (hp < 0)
        {
            Debug.Log("Game Over");
        }
    }

    /// <summary>
    /// 弾数の計算と更新
    /// </summary>
    /// <param name="amout"></param>
    public void CalcBulletCount(int amout)
    {

        BulletCount += amout;

        // UI で弾数が確認できるようになったらコメントアウトします
        Debug.Log("現在の弾数 : " + BulletCount);

        // TODO 弾数のUI表示更新

    }

    /// <summary>
    /// 弾数のリロード
    /// </summary>
    public IEnumerator ReloadBullet()
    {

        // リロード状態にして、弾の発射を制御する
        isReloading = true;

        // リロード
        BulletCount = maxBullet;

        // UI で弾数が確認できるようになったらコメントアウトします
        Debug.Log("リロード");                         


        // TODO 弾数のUI表示更新


        // TODO SE


        // リロードの待機時間
        yield return new WaitForSeconds(reloadTime);

        // 再度、弾が発射できる状態にする
        isReloading = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        // TODO 敵からの攻撃によって被弾した場合の処理

        // TODO ボスや敵の攻撃範囲を感知しないようにするためにタグでも判定するか、レイヤーを設定して回避する

    }
}
