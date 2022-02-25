using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData
{
    public string weaponName;　　　// 武器の名称
    public int weaponNo;　　　　　 // 武器の通し番号
    public int maxBullet;　　　　　// 弾の最大装填弾数
    public float reloadTime;　　　 // 弾の再装填にかかる時間
    public int bulletPower;　　　　// 弾の攻撃力
    public float shootInterval;　　// 連続で弾を発射する際の間隔
    public float shootRange;　　　 // 弾の射程距離
    public Sprite weaponIcon;　　　// 武器のアイコン画像
}
