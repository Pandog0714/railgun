using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionEventDetail : MonoBehaviour
{

    [Header("ミッションのクリア条件")]
    public ClearConditionsType clearConditionsType;

    [Header("ミッションクリアのための敵の残数/残り時間")]
    public int missionDuration;

    [Tooltip("イベントの生成地点")]
    public Transform[] eventTrans;

    [Header("敵のプレファブ")]
    public EnemyController[] enemyPrefabs;

    [Header("発生するイベントの種類")]
    public EventType[] eventTypes;

    [Header("発生するイベントの番号")]
    public int[] eventNos;
}