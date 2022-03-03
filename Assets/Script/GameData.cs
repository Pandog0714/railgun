using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData instance;

    [Header("�������Ă��镐��̓o�^�p���X�g")]
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
    /// ����f�[�^�̓o�^
    /// </summary>
    /// <param name="weaponData"></param>
    public void AddWeaponData(WeaponDataSO.WeaponData weaponData)
    {
        weaponDatasList.Add(weaponData);

        Debug.Log("����ǉ�:" + weaponData.weaponName);
    }
}
