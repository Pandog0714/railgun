using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionEventDetail : MonoBehaviour
{

    [Header("�~�b�V�����̃N���A����")]
    public ClearConditionsType clearConditionsType;

    [Header("�~�b�V�����N���A�̂��߂̓G�̎c��/�c�莞��")]
    public int missionDuration;

    [Tooltip("�C�x���g�̐����n�_")]
    public Transform[] eventTrans;

    [Header("�G�̃v���t�@�u")]
    public EnemyController[] enemyPrefabs;
}