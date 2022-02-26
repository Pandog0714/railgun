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
    /// WeaponDataSO �X�N���v�^�u���E�I�u�W�F�N�g�̒�����A�����Ɏw�肳�ꂽ WeaponNo ������ WeaponData �̎擾
    /// </summary>
    /// <param name="searchWeaponNo"></param>
    /// <returns></returns>
    public WeaponDataSO.WeaponData GetItemData(int searchWeaponNo)
    {
        return weaponDataSO.weaponDatasList.Find(x => x.weaponNo == searchWeaponNo);
    }
}
