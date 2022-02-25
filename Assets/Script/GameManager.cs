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


    private IEnumerator Start()
    {
        //�Q�[���̏�Ԃ���������
        currentGameState = GameState.Wait;

        //TODO ��������o�^

        //TODO ����擾�C�x���g�p�̐ݒ�

        //TODO ��������ݒ�

        //TODO ���[�g�p�̌o�H����ݒ�

        // �C�x���g�����@�\�̏���
        eventGenerator.SetUpEventGenerator(this, playerController);

        //RailMoveContrroller�̏����ݒ�
        railMoveController.SetUpRailMoveController(this);

        //�p�X�f�[�^���~�b�V�����̔����L�����擾
        SetMissionTriggers();

        //���ɍĐ����郌�[���ړ��̖ړI�n�ƌo�H�̃p�X��ݒ�
        railMoveController.SetNextRailPathData(originRailPathData);

        //�o�H�̏�������������̂�҂�(Start ���\�b�h�̖߂�l�� IEnumerator �ɕύX���ăR���[�`�����\�b�h�ɕς���)
        yield return new WaitUntil(() => railMoveController.GetMoveSetting());

        //�Q�[���̏�Ԃ��v���C���ɕύX����
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

        // TODO ����擾�C�x���g������


        // Mission���̊e�C�x���g�̐���(�G�A�M�~�b�N�A�g���b�v�A�A�C�e���Ȃǂ𐶐�)
        eventGenerator.PrepareGenerateEnemies(missionEventDetail.enemyPrefabs, missionEventDetail.eventTrans);

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

            // TODO ����擾�C�x���g���A����I���̂����ꂩ�̃{�^������������


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
}
