using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RailMoveController railMoveController;

    //��قǃ��X�g�����ĕ������[�g���Ǘ��ł���悤�ɂ���
    [SerializeField, Header("�o�H�p�̃p�X�Q�̌��f�[�^")]
    //private RailPathData or originRailPathDate;

    //Debug�p
    [SerializeField, Header("�p�X�ɂ�����~�b�V�����̔����L��")]
    private bool[] isMissionTriggers;


    // Start is called before the first frame update
    private void Start()
    {
        //TODO �Q�[���̏�Ԃ���������

        //TODO ���[�g�p�̌o�H����ݒ�

        //RailMoveContrroller�̏����ݒ�
        //railMoveController.SetupRailMoveController(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
