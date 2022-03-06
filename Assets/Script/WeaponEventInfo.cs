using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WeaponEventInfo : MonoBehaviour
{
    [SerializeField] //でなっぐ用
    private WeaponDataSO.WeaponData weaponData;

    //決定ボタン
    [SerializeField]
    private Button btnSubmit;

    //キャンセルボタン用
    [SerializeField]
    private Button btnCancel;

    //武器の名称
    [SerializeField]
    private Text txtWeaponName;

    //武器のアイコン画像表示用
    [SerializeField]
    private Image imgWeaponicon;

    [SerializeField]
    private CanvasGroup canvasGroup;

    public bool isChooseWeapon;

    
    /// <summary>
    /// 武器取得イベントの初期設定
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
    /// 武器情報の初期化
    /// </summary>
    private void IntializeWeaponData()
    {
        weaponData = null;

        txtWeaponName.text = string.Empty;
        imgWeaponicon.sprite = null;

        isChooseWeapon = false;
    }

    /// <summary>
    /// 武器の情報セット
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
    /// 決定ボタンを押した際の処理
    /// </summary>
    private void OnClickSubmit()
    {
        //武器登録
        GameData.instance.AddWeaponData(weaponData);
        isChooseWeapon = true;
    }

    /// <summary>
    /// キャンセルボタンを押した際の処理
    /// </summary>
    private void OnClickCancel()
    {
        //キャンセルした際の処理
        isChooseWeapon = true;
    }

    /// <summary>
    /// 各ボタンの活性化/非活性化の切り替え
    /// </summary>
    /// <param name="isSwitch"></param>
    private void SwitchActivateButtons(bool isSwitch)
    {
        btnSubmit.interactable = isSwitch;
        btnCancel.interactable = isSwitch;
    }

    /// <summary>
    /// 表示
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
