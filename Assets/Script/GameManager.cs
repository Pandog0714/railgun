using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RailMoveController railMoveController;

    //��قǃ��X�g�����ĕ������[�g���Ǘ��ł���悤�ɂ���
    [SerializeField, Header("�o�H�p�̃p�X�Q�̌��f�[�^")]
    private RailPathData originRailPathData;

    //Debug�p
    [SerializeField, Header("�p�X�ɂ�����Mission�̔����L��")]
    private bool[] isMissionTriggers;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private EventGenerator eventGenerator;

    [SerializeField, Header("Mission�Ŕ������Ă���G�̃��X�g")]
    private List<EnemyController> enemiesList = new List<EnemyController>();

    private int currentMissionDuration;

    [Header("���݂̃Q�[���̐i�s���")]
    public GameState currentGameState;

    [SerializeField]
    private WeaponEventInfo weaponEventInfo;


    private IEnumerator Start()
    {
        // �Q�[���̏�Ԃ���������
        currentGameState = GameState.Wait;

        // ��������o�^
        GameData.instance.AddWeaponData(DataBaseManager.instance.GetItemData(0));

        // ����擾�C�x���g�p�̐ݒ�
        weaponEventInfo.IntializeWeaponEventInfo();

        // ��������ݒ�
        playerController.ChangeBulletData(GameData.instance.weaponDatasList[0]);

        // ���[�g�p�̌o�H����ݒ�
        originRailPathData = DataBaseManager.instance.GetRailPathDataFromBranchNo(0, BranchDirectionType.NoBranch);

        // �C�x���g�����@�\�̏���
        eventGenerator.SetUpEventGenerator(this, playerController);

        // RailMoveContrroller�̏����ݒ�
        railMoveController.SetUpRailMoveController(this);

        // �p�X�f�[�^���~�b�V�����̔����L�����擾
        SetMissionTriggers();

        // ���ɍĐ����郌�[���ړ��̖ړI�n�ƌo�H�̃p�X��ݒ�
        railMoveController.SetNextRailPathData(originRailPathData);

        // �o�H�̏�������������̂�҂�(Start ���\�b�h�̖߂�l�� IEnumerator �ɕύX���ăR���[�`�����\�b�h�ɕς���)
        yield return new WaitUntil(() => railMoveController.GetMoveSetting());

        // �Q�[���̏�Ԃ��v���C���ɕύX����
        currentGameState = GameState.Play_Move;
    }

    /// <summary>
    /// �p�X�f�[�^���Mission�̔����L�������擾
    /// </summary>
    // UpData is called once per frame
    private void SetMissionTriggers()
    {
        //�z��̏�����
        isMissionTriggers = new bool[originRailPathData.GetIsMissionTriggers().Length];

        //Mission�����L���̏���o�^
        isMissionTriggers = originRailPathData.GetIsMissionTriggers();
    }

    /// <summary>
    /// Mission�̔����L���̔���
    /// </summary>
    /// <param name="index"></param>
    public void CheckMissionTrigger(int index)
    {
        if(isMissionTriggers[index])
        {
            //Mission����
            PreparateMission(originRailPathData.pathDataDetails[index].missionEventDetail);
            Debug.Log("�~�b�V��������");

            //Debug�p
            //railMoveController. CountUp();
        }
        else
        {
            //Mission�Ȃ��B���̃p�X�ֈړ��ĊJ
            railMoveController.CountUp();
        }
    }

    /// <summary>
    /// Mission����
    /// </summary>
    /// <param name="missionEventDetail"></param>
    private void PreparateMission(MissionEventDetail missionEventDetail)
    {
        // Mission�̎��Ԑݒ�
        currentMissionDuration = missionEventDetail.missionDuration;

        // ����擾�C�x���g������
        if(missionEventDetail.eventTypes[0] == EventType.Weapon)
        {
            // ����̏����擾���ăZ�b�g
            weaponEventInfo.SetWeaponData(DataBaseManager.instance.GetItemData(missionEventDetail.eventNos[0]));
            weaponEventInfo.Show();
        }
        else
        {
            // Mission���̊e�C�x���g�̐���(�G�A�M�~�b�N�A�g���b�v�A�A�C�e���Ȃǂ𐶐�)
            eventGenerator.PrepareGenerateEnemies(missionEventDetail.enemyPrefabs, missionEventDetail.eventTrans);
        }

        // Mission�J�n
        StartCoroutine(StartMission(missionEventDetail.clearConditionsType));
    }


    /// <summary>
    /// Mission�J�n
    /// </summary>
    /// <param name="clearConditionsType"></param>
    /// <returns></returns>
    private IEnumerator StartMission(ClearConditionsType clearConditionsType)
    {
        // Mission�̊Ď�
        yield return StartCoroutine(ObservateMission(clearConditionsType));

        // Mission�I��
        EndMission();
    }

    /// <summary>
    /// Mission�̊Ď�
    /// </summary>
    /// <param name="clearConditionsType"></param>
    /// <returns></returns>
    private IEnumerator ObservateMission(ClearConditionsType clearConditionsType)
    {

        // �N���A�����𖞂����܂ŊĎ�(currentMissionDuration �ϐ��ɂ́A�G�̐����A�c�莞�Ԃ�����)
        while(currentMissionDuration > 0)
        {

            // �N���A���������Ԍo�߂̏ꍇ
            if(clearConditionsType == ClearConditionsType.TimeUp)
            {

                // �J�E���g�_�E��
                currentMissionDuration--;
            }

            //  ����擾�C�x���g���A����I���̂����ꂩ�̃{�^������������
            if(weaponEventInfo.gameObject.activeSelf && weaponEventInfo.isChooseWeapon)
            {
                //�C�x���g�I��
                currentMissionDuration = 0;

                weaponEventInfo.Hide();

                Debug.Log("����擾�C�x���g�I��");
                yield break;
            }

            yield return null;
        }

        Debug.Log("�~�b�V�����I��");
    }

    /// <summary>
    /// Mission�I��
    /// </summary>
    public void EndMission()
    {

        // TODO ����̎擾�C�x���g�̏ꍇ�ɂ͕�����擾�����Ƀ|�b�v�A�b�v�����


        // ���񕪂̓G�̏����N���A
        ClearEnemiesList();

        // �J�����̈ړ��ĊJ
        railMoveController.CountUp();
    }

    /// <summary>
    /// �G�̃��X�g���N���A
    /// </summary>
    private void ClearEnemiesList()
    {
        if(enemiesList.Count > 0)
        {
            for (int i = 0; i < enemiesList.Count; i++)
            {
                Destroy(enemiesList[i]);
            }
        }
    }

    /// <summary>
    /// �G�̏������X�g����폜���A�~�b�V�������̓G�̎c�������炷
    /// </summary>
    /// <param name="enemy"></param>
    public void RemoveEnemyList(EnemyController enemy)
    {
        currentMissionDuration--;

        enemiesList.Remove(enemy);
    }

    /// <summary>
    /// �G�̏������X�g�ɒǉ�
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemyList(EnemyController enemy)
    {
        enemiesList.Add(enemy);
    }

    /// <summary>
    /// ���[�g����m�F�̏���
    /// </summary>
    /// <param name="newtbranchNo"></param>
    public void PreparateCheckNextBranch(int newtbranchNo)
    {
        StartCoroutine(CheckNextBranch(newtbranchNo));
    }


    /// <summary>
    /// ���[�g����̔���
    /// </summary>
    /// <param name="nextStagePathDataNo"></param>
    /// <returns></returns>
    private IEnumerator CheckNextBranch(int nextStagePathDataNo)
    {
        if(DataBaseManager.instance.GetBranchDatasListCount(nextStagePathDataNo) == 1)
        {
            Debug.Log("����Ȃ��Ŏ��̃��[�g��");

            // ����Ȃ��̏ꍇ�A���̌o�H��o�^
            originRailPathData = DataBaseManager.instance.GetRailPathDataFromBranchNo(nextStagePathDataNo, BranchDirectionType.NoBranch);
        }
        else
        {
            // TODO ���򂪂���ꍇ�A����C�x���g�𔭐������āA��ʏ�ɖ��̃{�^���\��
            
            // TODO �����I������܂őҋ@

            // TODO �I���������򃋁[�g�̐ݒ�

        }

        //���[�g���̃~�b�V��������ݒ�
        SetMissionTriggers();

        //�o�H���ړ���ɐݒ�
        railMoveController.SetNextRailPathData(originRailPathData);

        //���[���ړ��̌o�H�ƈړ��o�^����������܂őҋ@
        yield return new WaitUntil(() => railMoveController.GetMoveSetting());

        //�Q�[���̐i�s��Ԃ��ړ����ɕύX����
        currentGameState = GameState.Play_Move;
    }
}
