using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponDataSO", menuName = "Create WeaponDataSO")]
public class WeaponDataSO : ScriptableObject
{
    [System.Serializable]
    public class WeaponData
    {
        public string weaponName;
        public int weaponNo;
        public int maxBullet;
        public float reloadTime;
        public int bulletPower;
        public float shootInterval;
        public float shootRange;
        public Sprite weaponIcon;
    }

    public List<WeaponData> weaponDatasList = new List<WeaponData>();
}
