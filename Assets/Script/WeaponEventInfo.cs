using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponEventInfo : MonoBehaviour
{
    [SerializeField] //�łȂ����p
    private WeaponDataSO.WeaponData weaponData;

    //����{�^��
    [SerializeField]
    private Button btnSubmit;

    //�L�����Z���{�^���p
    [SerializeField]
    private Button btnCancel;

    //����̖���
    [SerializeField]
    private Text txtWeaponName;

    //����̃A�C�R���摜�\���p
    [SerializeField]
    private Image imgWeaponicon;

    [SerializeField]
    private CanvasGroup canvasGroup;

    public bool isChooseWeapon;

    
    /// <summary>
    /// ����擾�C�x���g�̏����ݒ�
    /// </summary>
    public void IntializeWeaponEventInfo()
    {
        SwitchActivateButtons(false);

        btnSubmit.onClick.AddListener(OnClickSubmit);
        btnSubmit.onClick.AddListener(() => SwitchActivateButtons(false));

        btnCancel.onClick.AddListener(OnClickCancel);
        btnCancel.onClick.AddListener(() => SwitchActivateButtons(false));

        SwitchActivateButtons(true);

        Hide(0);
    }

    /// <summary>
    /// ������̏�����
    /// </summary>
    private void IntializeWeaponData()
    {
        weaponData = null;

        txtWeaponName.text = string.Empty;
        imgWeaponicon.sprite = null;

        isChooseWeapon = false;
    }

    /// <summary>
    /// ����̏��Z�b�g
    /// </summary>
    /// <param name="weaponData"></param>
    private void SetWeaponData(WeaponDataSO.WeaponData weaponData)
    {
        this.weaponData = weaponData;

        txtWeaponName.text = weaponData.weaponName;
        imgWeaponicon.sprite = null;

        SwitchActivateButtons(true);
    }

    /// <summary>
    /// ����{�^�����������ۂ̏���
    /// </summary>
    private void OnClickSubmit()
    {
        //����o�^
        GameData.instance.AddWeaponData(weaponData);
        isChooseWeapon = true;
    }

    /// <summary>
    /// �L�����Z���{�^�����������ۂ̏���
    /// </summary>
    private void OnClickCancel()
    {
        //�L�����Z�������ۂ̏���
        isChooseWeapon = true;
    }

    /// <summary>
    /// �e�{�^���̊�����/�񊈐����̐؂�ւ�
    /// </summary>
    /// <param name="isSwitch"></param>
    private void SwitchActivateButtons(bool isSwitch)
    {
        btnSubmit.interactable = isSwitch;
        btnCancel.interactable = isSwitch;
    }

    /// <summary>
    /// �\��
    /// </summary>
    /// <param name="duration"></param>
    public void Show(float duration = 0.5f)
    {
        gameObject.SetActive(true);
        canvasGroup.DOFade(1.0f, duration);
    }

    public void Hide(float duration = 0.5f)
    {
        canvasGroup.DOFade(0, duration).OnComplete(() => gameObject.SetActive(false));

        IntializeWeaponData();
    }
}
