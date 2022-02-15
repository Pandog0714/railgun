using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RailMoveController railMoveController;

    //��قǃ��X�g�����ĕ������[�g���Ǘ��ł���悤�ɂ���
    [SerializeField, Header("�o�H�p�̃p�X�Q�̌��f�[�^")]
    private RailPathData originRailPathDate;

    //Debug�p
    [SerializeField, Header("�p�X�ɂ�����~�b�V�����̔����L��")]
    private bool[] isMissionTriggers;


    // Start is called before the first frame update
    private void Start()
    {
        //TODO �Q�[���̏�Ԃ���������

        //TODO ���[�g�p�̌o�H����ݒ�

        //RailMoveContrroller�̏����ݒ�
        railMoveController.SetUpRailMoveController(this);

        //�p�X�f�[�^���~�b�V�����̔����L�����擾
        SetMissionTriggers();

        //���ɍĐ����郌�[���ړ��̖ړI�n�ƌo�H�̃p�X��ݒ�
        railMoveController.SetNextRailPathDate(originRailPathDate);

        //TODO �o�H�̏�������������̂�҂�(Start ���\�b�h�̖߂�l�� IEnumerator �ɕύX���ăR���[�`�����\�b�h�ɕς���)

        //TODO �Q�[���̏�Ԃ��v���C���ɕύX����
    }

    /// <summary>
    /// �p�X�f�[�^���~�b�V�����̔����L�������擾
    /// </summary>
    // Update is called once per frame
    private void SetMissionTriggers()
    {
        //�z��̏�����
        isMissionTriggers = new bool[originRailPathDate.GetIsMissionTriggers().Length];

        //�~�b�V���������L���̏���o�^
        isMissionTriggers = originRailPathDate.GetIsMissionTriggers();
    }

    /// <summary>
    /// �~�b�V�����̔����L���̔���
    /// </summary>
    /// <param name="index"></param>
    public void CheckMissionTrigger(int index)
    {
        if (isMissionTriggers[index])
        {
            //TODO �~�b�V��������
            Debug.Log("�~�b�V��������");

            //Debug�p ���͂��̂܂�
            railMoveController. CountUp();
        }
        else
        {
            //�~�b�V�����Ȃ��B���̃p�X�ֈړ��ĊJ
            railMoveController.CountUp();
        }
    }
}
