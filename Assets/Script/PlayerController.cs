using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���̐���N���X
/// </summary>
public class PlayerController : MonoBehaviour
{
    //���݂�HP
    private int hp;

    //���݂̒e���l
    private int bulletCount;

    [SerializeField, Header("�ő�HP�l")]
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

    /// <summary>
    /// �e�ۗp�̃v���p�e�B
    /// </summary>
    public int BulletCount
    {
        set => bulletCount = Mathf.Clamp(value, 0, maxBullet);
        get => bulletCount;
    }

    /// <summary>
    /// HP�p�̃v���p�e�B(�̂��ɐݒ�)
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
        //�f�o�b�N�p
        SetUpPlayer();    
    }

    /// <summary>
    /// �v���C���[���̏����ݒ�
    /// </summary>
    
    //�O���N���X����̌Ăяo����z�肵��public�Ő錾
    public void SetUpPlayer()
    {
        //maxHp�̐ݒ肪���邩�̊m�F�A����HP��10�Ƃ��Đݒ�
        hp = maxHp = maxHp == 0 ? 10 : maxHp;

        //maxBullet�̐ݒ肪���邩�̊m�F�A�����l��10�Ƃ��Đݒ�
        if(maxBullet == 0)
        {
            maxBullet = 10;
        }

        BulletCount = maxBullet;

        Debug.Log(BulletCount);
    }

    /// <summary>
    /// HP�̌v�Z�ƍX�V
    /// </summary>
    /// <param name="amout"></param>
    public void CalcHp(int amount)
    {
        hp = Mathf.Clamp(hp += amount, 0, maxHp);

        //�m�F�ł�����R�����g�A�E�g
        Debug.Log("���݂�Hp :" + hp);

        //ToDo HP �\�L�̍X�V
        if (hp < 0)
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

        // UI �Œe�����m�F�ł���悤�ɂȂ�����R�����g�A�E�g���܂�
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

        // UI �Œe�����m�F�ł���悤�ɂȂ�����R�����g�A�E�g���܂�
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
}
