using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public static EffectManager instance;

    //Player用
    [Header("発射口用のエフェクト")]
    public GameObject muzzleFlashPrefab;

    [Header("敵に弾が当たったときのエフェクト")]
    public GameObject hitEffectPrefab;

    //TODO 追加

    //Enemy用

    //共通

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
