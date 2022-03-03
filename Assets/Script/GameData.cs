using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    [Header("所持している武器の登録用リスト")]
    public List<WeaponDataSO.WeaponData> weaponDatasList = new List<WeaponDataSO.WeaponData>();

    void Awake()
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

    /// <summary>
    /// 武器データの登録
    /// </summary>
    /// <param name="weaponData"></param>
    public void AddWeaponData(WeaponDataSO.WeaponData weaponData)
    {
        weaponDatasList.Add(weaponData);

        Debug.Log("武器追加:" + weaponData.weaponName);
    }
}
