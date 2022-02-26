using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public WeaponDataSO weaponDataSO;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// WeaponDataSO スクリプタブル・オブジェクトの中から、引数に指定された WeaponNo を持つ WeaponData の取得
    /// </summary>
    /// <param name="searchWeaponNo"></param>
    /// <returns></returns>
    public WeaponDataSO.WeaponData GetItemData(int searchWeaponNo)
    {
        return weaponDataSO.weaponDatasList.Find(x => x.weaponNo == searchWeaponNo);
    }
}
