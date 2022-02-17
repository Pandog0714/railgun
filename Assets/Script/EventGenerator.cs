using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventGenerator : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController playerController;

    /// <summary>
    /// ジェネレーターの初期設定(GameManager クラスから実行されるメソッド)
    /// </summary>
    /// <param name="gameManager"></param>
    /// <param name="playerController"></param>
    public void SetUpEventGenerator(GameManager gameManager, PlayerController playerController)
    {
        this.gameManager = gameManager;
        this.playerController = playerController;
    }

    /// <summary>
    /// ミッション内の各敵の生成処理(GameManager クラスから実行されるメソッド)
    /// </summary>
    /// <param name="enemyPrefabs"></param>
    /// <param name="eventTrans"></param>
    public void PrepareGenerateEnemies(EnemyController[] enemyPrefabs, Transform[] eventTrans)
    {
        for (int i = 0; i < enemyPrefabs.Length; i++)
        {
            //GenerateEvent(enemyPrefabs[i], eventTrans[i]);
        }
    }

    private void GenerateEnemy(EnemyController enemyPrefab, Transform eventTran)
    {
        EnemyController enemy = Instantiate(enemyPrefab, eventTran.position, enemyPrefab.transform.rotation);
        enemy.SetUpEnemy(playerController, gameManager);

        //gameManager.AddEnemyList(enemy);
    }
}
