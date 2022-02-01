using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ray �ɂ��e�̔��ˏ����̐���N���X
/// </summary>
public class RayController : MonoBehaviour
{
    [Header("���ˌ��p�̃G�t�F�N�g�̃T�C�Y����")]
    public Vector3 muzzleFlashScale;

    private bool isShooting;

    //���ˎ��̃G�t�F�N�g�A��e���̃G�t�F�N�g
    private GameObject muzzleFlashObj;
    private GameObject hitEffectObj;

    // Ray �ŕ⑫�����Ώۂ̑���p
    private GameObject target;

    [SerializeField, Header("Ray �p�̃��C���[�ݒ�")]
    private int[] layerMasks;

    //Debug�p�B�m�F�ł�����ASerializeField �������폜���� private �ɂ��Ă���
    [SerializeField]
    private string[] layerMasksStr;

    [SerializeField]
    private PlayerController playerController;


    void Start()
    {
        // Layer �̏��𕶎���ɕϊ����ARaycast ���\�b�h�ŗ��p���₷������ϐ��Ƃ��č쐬���Ă���
        layerMasksStr = new string[layerMasks.Length];
        for (int i = 0; i < layerMasks.Length; i++)
        {
            layerMasksStr[i] = LayerMask.LayerToName(layerMasks[i]);
        }
    }

    void Update()
    {
        // TODO �Q�[����Ԃ��v���C���łȂ��ꍇ�ɂ͏������s��Ȃ����������


        // �����[�h����(�e�� 0 �Ń����[�h�@�\����̏ꍇ)
        if (playerController.BulletCount == 0 && playerController.isReloadModeOn && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(playerController.ReloadBullet());
        }

        // ���˔���(�e�����c���Ă���A�����[�h���s���łȂ��ꍇ)�@�������ςȂ��Ŕ��˂ł���
        if (playerController.BulletCount > 0 && !playerController.isReloading && Input.GetMouseButton(0))
        {

            // ���ˎ��Ԃ̌v��
            StartCoroutine(ShootTimer());
        }
    }

    /// <summary>
    /// �p���I�Ȓe�̔��ˏ���
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootTimer()
    {
        if (!isShooting)
        {
            isShooting = true;

            // ���˃G�t�F�N�g�̕\���B����̂ݐ������A�Q��ڂ̓I���I�t�Ő؂�ւ���
            if (muzzleFlashObj == null)
            {
                // ���ˌ��̈ʒu�� RayController �Q�[���I�u�W�F�N�g��z�u����
                muzzleFlashObj = Instantiate(EffectManager.instance.muzzleFlashPrefab, transform.position, transform.rotation);
                muzzleFlashObj.transform.SetParent(gameObject.transform);
                muzzleFlashObj.transform.localScale = muzzleFlashScale;

            }
            else
            {
                muzzleFlashObj.SetActive(true);
            }

            // ����
            Shoot();

            yield return new WaitForSeconds(playerController.shootInterval);

            muzzleFlashObj.SetActive(false);

            if (hitEffectObj != null)
            {
                hitEffectObj.SetActive(false);
            }

            isShooting = false;

        }
        else
        {
            yield return null;
        }
    }

    /// <summary>
    /// �e�̔���
    /// </summary>
    private void Shoot()
    {
        // �J�����̈ʒu����N���b�N(�^�b�v)�����n�_�� Ray �𓊎�
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, playerController.shootRange, LayerMask.GetMask(layerMasksStr))==true)
        {

            Debug.Log(hit.collider.gameObject.name);

            // �����Ώۂ��U�����Ă��邩�m�F�B�Ώۂ����Ȃ��������A�����ΏۂłȂ��ꍇ
            if (target == null || target != hit.collider.gameObject)
            {

                target = hit.collider.gameObject;


                // TODO TryGetComponent �̏����œG���Q���Ȃǂ̏����擾���A���������


                // TODO ���o


                //�@�����Ώۂ̏ꍇ
            }
            else if (target == hit.collider.gameObject)
            {
                // TODO ���łɏ�񂪂���̂ōĎ擾�͂����ɔ���݂̂���

                // TODO ���o

            }
        }

        // �e�������炷
        playerController.CalcBulletCount(-1);
    }
}