using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataBaseManager : MonoBehaviour
{
    public static DataBaseManager instance;

    public WeaponDataSO weaponDataSO;

    [SerializeField]
    private StagePathDataSO stagePathDataSO;


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
    /// WeaponDataSO,�X�N���v�^�u���E�I�u�W�F�N�g�̒�����A�����Ɏw�肳�ꂽWeaponNo������WeaponData�̎擾
    /// </summary>
    /// <param name="searchWeaponNo"></param>
    /// <returns></returns>
    public WeaponDataSO.WeaponData GetItemData(int searchWeaponNo)
    {
        return weaponDataSO.weaponDatasList.Find(x => x.weaponNo == searchWeaponNo);
    }


    /// <summary>
    /// �X�e�[�W�p�X�ԍ����番����RailPathData�����擾
    /// </summary>
    /// <param name="nextStagePathDataNo"></param>
    /// <param name="searchBranchDirectionType"></param>
    /// <returns></returns>
    public RailPathData GetRailPathDataFromBranchNo(int nextStagePathDataNo, BranchDirectionType searchBranchDirectionType)
    {
        return stagePathDataSO.stagePathDatasList[nextStagePathDataNo].branchDatasList.Find(x => x.branchDirectionType == searchBranchDirectionType == searchBranchDirectionType).railPathData;
    }

    /// <summary>
    /// �X�e�[�W���̃��[�g�̐��̎擾
    /// </summary>
    /// <returns></returns>
    public int GetStagePathDatasListCount()
    {
        return stagePathDataSO.stagePathDatasList.Count;
    }

    /// <summary>
    /// ����̎�ނ̎擾
    /// </summary>
    /// <param name="nextStagePathDataNo"></param>
    /// <param name="no"></param>
    /// <returns></returns>
    public BranchDirectionType GetBranchDirectionTypeFromRailPathData(int nextStagePathDataNo, int no)
    {
        return stagePathDataSO.stagePathDatasList[nextStagePathDataNo].branchDatasList[no].branchDirectionType;
    }

    /// <summary>
    /// �S������̎擾
    /// </summary>
    /// <param name="nextStagePathDataNo"></param>
    /// <returns></returns>
    public BranchDirectionType[] GetBranchDirectionTypes(int nextStagePathDataNo)
    {
        return stagePathDataSO.stagePathDatasList[nextStagePathDataNo].branchDatasList.Select(x => x.branchDirectionType).ToArray();
    }

    public int GetBranchDatasListCount(int branchNo)
    {
        return stagePathDataSO.stagePathDatasList[branchNo].branchDatasList.Count;
    }

}
