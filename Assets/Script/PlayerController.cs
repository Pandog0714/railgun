using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���̊Ǘ��N���X
/// </summary>
public class PlayerController : MonoBehaviour
{
    private int hp;                // ���݂�Hp�l
    private int bulletCount;       // ���݂̒e���l

    [SerializeField, Header("�ő�Hp�l")]
    private int maxHp;

    [SerializeField, Header("�ő�e���l")]
    private int maxBullet;

    [SerializeField, Header("�����[�h����")]
    private float reloadTime;

    [Header("�e�̍U����")]
    public int bulletPower;

    [Header("�e�̘A�ˑ��x")]
    public float shootInterval;

    [Header("�e�̎˒�����")]
    public float shootRange;

    [Header("�����[�h�@�\�̃I��/�I�t")]
    public bool isReloadModeOn;

    [Header("�����[�h��Ԃ̐���")]
    public bool isReloading;

    [SerializeField]
    private UIManager uiManagar;

    public int currentWeaponNo;

    /// <summary>
    /// �e���p�̃v���p�e�B
    /// </summary>
    public int BulletCount
    {
        set => bulletCount = Mathf.Clamp(value, 0, maxBullet);
        get => bulletCount;
    }



    void Start()
    {
        // Debug �p
        SetUpPlayer();
    }

    /// <summary>
    /// �v���C���[���̏����ݒ�
    /// </summary>
    public void SetUpPlayer()
    {

        // maxHp �̐ݒ肪���邩�m�F�B�Ȃ���Ώ����l 10 �ŃZ�b�g���� hp ��ݒ�
        hp = maxHp = maxHp == 0 ? 10 : maxHp;

        // maxBullet �̐ݒ肪���邩�m�F�B�Ȃ���Ώ����l 10 �ŃZ�b�g���� �e����ݒ�
        BulletCount = maxBullet = maxBullet == 0 ? 10 : maxBullet;

        // UI�Ƀ��C�t�p�A�C�R�����ő�HP��������������
        uiManagar.SetPlayerInfo(maxHp);

    }

    /// <summary>
    /// HP�̌v�Z�ƍX�V
    /// </summary>
    public void CalcHp(int amount)
    {
        hp = Mathf.Clamp(hp += amount, 0, maxHp);

        Debug.Log("���݂�Hp : " + hp);

        // HP �\���̍X�V
        uiManagar.UpdateDisplayLife(hp);

        if(hp <= 0)
        {
            Debug.Log("Game Over");
        }
    }

    /// <summary>
    /// �e���̌v�Z�ƍX�V
    /// </summary>
    /// <param name="amout"></param>
    public void CalcBulletCount(int amout)
    {

        BulletCount += amout;

        Debug.Log("���݂̒e�� : " + BulletCount);

        // TODO �e����UI�\���X�V

    }

    /// <summary>
    /// �e���̃����[�h
    /// </summary>
    public IEnumerator ReloadBullet()
    {

        // �����[�h��Ԃɂ��āA�e�̔��˂𐧌䂷��
        isReloading = true;

        // �����[�h
        BulletCount = maxBullet;

        Debug.Log("�����[�h");


        // TODO �e����UI�\���X�V


        // TODO SE


        // �����[�h�̑ҋ@����
        yield return new WaitForSeconds(reloadTime);

        // �ēx�A�e�����˂ł����Ԃɂ���
        isReloading = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        // TODO �G����̍U���ɂ���Ĕ�e�����ꍇ�̏���

        // TODO �{�X��G�̍U���͈͂����m���Ȃ��悤�ɂ��邽�߂Ƀ^�O�ł����肷�邩�A���C���[��ݒ肵�ĉ������

    }

    /// <summary>
    /// ����(�o���b�g)�̏���ύX���ĕ���؂�ւ�
    /// </summary>
    /// <param name="weaponData"></param>
    public void ChangeBulletData(WeaponData weaponData)
    {

        // ���ݎg�p���Ă��镐��̔ԍ���ێ�
        currentWeaponNo = weaponData.weaponNo;

        // ����̏����e�ϐ��ɐݒ�
        bulletPower = weaponData.bulletPower;
        maxBullet = weaponData.maxBullet;

        reloadTime = weaponData.reloadTime;
        shootInterval = weaponData.shootInterval;
        shootRange = weaponData.shootRange;

        bulletCount = maxBullet;

        // TODO ���łɎg�p�������Ƃ̂��镐��ł���ꍇ


        // TODO �e����O��̎c�e���ɂ���

    }
}
