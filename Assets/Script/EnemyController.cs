using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の制御クラス
/// </summary>
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject lookTarget;

    [SerializeField]
    private OffMeshLinkType enemyNo;

    [SerializeField]
    private int hp;

    [SerializeField]
    private int attakPower;

    [SerializeField]
    private float moveSpeed;

    private Animator anim;

    private bool isAttack;

    private float attackInterval = 3.0f;

    private PlayerController player;

    //private GameManager gameManager;

    private IEnumerator attackCoroutine;

    private bool isDead;

    private NavMeshAgent agent;

    private float originMoveSpeed;

    public EnemyMoveType enemyMoveType;

    /// <summary>
    /// エネミーの初期設定
    /// </summary>
    /// <param name="playerController"></param>
    /// <param name="gameManager"></param>
    public void SetUpEnemy(PlayerController playerController, GameManager gameManager)
    {
        lookTarget = playerController.gameObject;
        this gameManager = gameManager;

        TryGetComponent()
    }
}
