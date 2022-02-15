using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RailMoveController : MonoBehaviour
{
    [SerializeField]
    private Transform railMoveTarget;             // ���[���ړ�������ΏہB�J�����A���邢�� AR �p�̃J�������w��

    [SerializeField]
    private RailPathData currentRailPathData;     // RailPathData �N���X�̃A�^�b�`����Ă��� RailPathData �Q�[���I�u�W�F�N�g���A�T�C���B���ƂŎ����A�T�C���ɕύX

    private Tween tweenMove;
    private Tween tweenRotation;

    private Vector3[] paths;
    private float[] moveDurations;
    private int pathCount;

    private GameManager gameManager;

    /// <summary>
    /// RailMoveController�̏����ݒ�
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpRailMoveController(GameManager gameManager)         //<= GameManager���Ăяo�����
    {
        this.gameManager = gameManager;

        //TODO ���ɂ���ꍇ�͒ǋL�B�K�v�ɉ����Ĉ�����ʂ��ĊO������������炤�悤�ɂ���
    }

    /// <summary>
    /// ���ɍĐ����郌�[���ړ��̖ړI�n�ƌo�H�̃p�X���擾���Đݒ�
    /// </summary>
    /// <param name="nextRailPathData"></param>
    public void SetNextRailPathDate(RailPathData nextRailPathData)        //<= GameManager���Ăяo�����
    {

        //�ړI�n�擾
        currentRailPathData = nextRailPathData;

        //�ړ��J�n
        StartCoroutine(StartRailMove());
    }

    //void Start()
    //{
        // Debug �p  ���[���ړ��̊J�n
    //    StartCoroutine(StartRailMove());
    //}

    /// <summary>
    /// ���[���ړ��̊J�n
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartRailMove()
    {

        yield return null;

        // �p�X�̃J�E���g��������
        pathCount = 0;

        // �ړ�����n�_���擾���邽�߂̔z��̏�����
        paths = new Vector3[currentRailPathData.GetPathTrans().Length];
        float totalTime = 0;

        // �ړ�����ʒu���Ǝ��Ԃ����Ԃɔz��Ɏ擾
        for (int i = 0; i < currentRailPathData.GetPathTrans().Length; i++)
        {
            paths[i] = currentRailPathData.GetPathTrans()[i].position;
            totalTime += currentRailPathData.GetRailMoveDurations()[i];
        }

        // �e�p�X���Ƃ̈ړ����Ԃ��擾
        moveDurations = currentRailPathData.GetRailMoveDurations();

        // �p�X�ɂ��ړ��J�n
        RailMove();


        // TODO �ړ����ꎞ��~


        // TODO �Q�[���̐i�s��Ԃ��ړ����ɂȂ�܂őҋ@


        // TODO �ړ��J�n

        Debug.Log("�ړ��J�n");
    }

    /// <summary>
    /// ���[���ړ��̈ꎞ��~
    /// </summary>
    public void PauseMove()
    {
        // �ꎞ��~
        tweenMove.Pause();
        tweenRotation.Pause();
    }

    /// <summary>
    /// ���[���ړ��̍ĊJ
    /// </summary>
    public void ResumeMove()
    {
        // �ړ��ĊJ
        tweenMove.Play();
        tweenRotation.Play();
    }

    /// <summary>
    /// �p�X�̃J�E���g�A�b�v(�p�X���Ƃɓ������ꍇ)
    /// </summary>
    public void CountUp()
    {
        pathCount++;
        Debug.Log(pathCount);

        // �ړ�
        RailMove();
    }

    /// <summary>
    /// 2�_�Ԃ̃p�X�̖ڕW�n�_��ݒ肵�Ĉړ�
    /// </summary>
    public void RailMove()
    {

        // �c���Ă���p�X���Ȃ��ꍇ
        if (pathCount >= currentRailPathData.GetPathTrans().Length)
        {

            // DOTween ���~
            tweenMove.Kill();

            tweenMove = null;
            tweenRotation = null;


            // TODO �ړ������ɔ����A�ړ��񐔂��J�E���g�A�b�v


            // TODO �ړ���̊m�F�B�ړ��悪�c���Ă��Ȃ��ꍇ�ɂ́A�Q�[���}�l�[�W���[���ŕ���̊m�F(���̃��[�g�I��A�ړ���̕���A�{�X�A�N���A�̂����ꂩ)


            Debug.Log("����m�F");

            return;
        }

        // �p�X�̐ݒ�p�̔z���錾
        Vector3[] targetPaths;

        // �p�X�̃J�E���g���ɉ����ĕ��򂵂ăp�X��ݒ�
        if (pathCount == 0)
        {
            targetPaths = new Vector3[2] { railMoveTarget.position, paths[pathCount] };
        }
        else
        {
            targetPaths = new Vector3[2] { paths[pathCount - 1], paths[pathCount] };
        }

        // �p�X�̈ړ����Ԃ�ݒ�
        float duration = moveDurations[pathCount];

        Debug.Log("�X�^�[�g�n�_ :" + targetPaths[0]);
        Debug.Log("�ڕW�n�_ :" + targetPaths[1]);
        Debug.Log("�ړ��ɂ����鎞�� :" + duration);

        // �p�X�̈ړ�
        tweenMove = railMoveTarget.transform.DOPath(targetPaths, duration).SetEase(Ease.Linear).OnWaypointChange((waypointIndex) => CheckArrivalDestination(waypointIndex));

        // �J�����̉�]
        tweenRotation = railMoveTarget.transform.DORotate(currentRailPathData.pathDataDetails[pathCount].pathTran.eulerAngles, duration).SetEase(Ease.Linear);
        Debug.Log($" ��]�p�x :  { currentRailPathData.pathDataDetails[pathCount].pathTran.eulerAngles } ");
    }

    /// <summary>
    /// �p�X�̖ڕW�n�_�ɓ������邽�тɎ��s�����
    /// </summary>
    /// <param name="waypointIndex"></param>
    private void CheckArrivalDestination(int waypointIndex)
    {

        if (waypointIndex == 0)
        {
            return;
        }

        Debug.Log("�ڕW�n�_ ���� : " + pathCount + " �Ԗ�");

        // �ړ��̈ꎞ��~
        PauseMove();

        // TODO �~�b�V���������邩�m�F(�~�b�V�������������邩�Q�[���}�l�[�W���[���Ŕ�����s��)
        gameManager.CheckMissionTrigger(pathCount);


        // Debug�p  ���̃p�X���Z�b�g���Ĉړ������s
        //CountUp();
    }
}