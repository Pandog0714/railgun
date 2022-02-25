using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RailMoveController railMoveController;

    //後ほどリスト化して複数ルートを管理できるようにする
    [SerializeField, Header("経路用のパス群の元データ")]
    private RailPathData originRailPathData;

    //Debug用
    [SerializeField, Header("パスにおけるMissionの発生有無")]
    private bool[] isMissionTriggers;

    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private EventGenerator eventGenerator;

    [SerializeField, Header("Missionで発生している敵のリスト")]
    private List<EnemyController> enemiesList = new List<EnemyController>();

    private int currentMissionDuration;

    [Header("現在のゲームの進行状態")]
    public GameState currentGameState;


    private IEnumerator Start()
    {
        //ゲームの状態を準備する
        currentGameState = GameState.Wait;

        //TODO 初期武器登録

        //TODO 武器取得イベント用の設定

        //TODO 初期武器設定

        //TODO ルート用の経路情報を設定

        // イベント生成機能の準備
        eventGenerator.SetUpEventGenerator(this, playerController);

        //RailMoveContrrollerの初期設定
        railMoveController.SetUpRailMoveController(this);

        //パスデータよりミッションの発生有無情報取得
        SetMissionTriggers();

        //次に再生するレール移動の目的地と経路のパスを設定
        railMoveController.SetNextRailPathData(originRailPathData);

        //経路の準備が完了するのを待つ(Start メソッドの戻り値を IEnumerator に変更してコルーチンメソッドに変える)
        yield return new WaitUntil(() => railMoveController.GetMoveSetting());

        //ゲームの状態をプレイ中に変更する
        currentGameState = GameState.Play_Move;
    }

    /// <summary>
    /// パスデータよりMissionの発生有無情報を取得
    /// </summary>
    // UpData is called once per frame
    private void SetMissionTriggers()
    {
        //配列の初期化
        isMissionTriggers = new bool[originRailPathData.GetIsMissionTriggers().Length];

        //Mission発生有無の情報を登録
        isMissionTriggers = originRailPathData.GetIsMissionTriggers();
    }

    /// <summary>
    /// Missionの発生有無の判定
    /// </summary>
    /// <param name="index"></param>
    public void CheckMissionTrigger(int index)
    {
        if(isMissionTriggers[index])
        {
            //Mission発生
            PreparateMission(originRailPathData.pathDataDetails[index].missionEventDetail);
            Debug.Log("ミッション発生");

            //Debug用
            //railMoveController. CountUp();
        }
        else
        {
            //Missionなし。次のパスへ移動再開
            railMoveController.CountUp();
        }
    }

    /// <summary>
    /// Mission準備
    /// </summary>
    /// <param name="missionEventDetail"></param>
    private void PreparateMission(MissionEventDetail missionEventDetail)
    {
        // Missionの時間設定
        currentMissionDuration = missionEventDetail.missionDuration;

        // TODO 武器取得イベントか判定


        // Mission内の各イベントの生成(敵、ギミック、トラップ、アイテムなどを生成)
        eventGenerator.PrepareGenerateEnemies(missionEventDetail.enemyPrefabs, missionEventDetail.eventTrans);

        // Mission開始
        StartCoroutine(StartMission(missionEventDetail.clearConditionsType));
    }


    /// <summary>
    /// Mission開始
    /// </summary>
    /// <param name="clearConditionsType"></param>
    /// <returns></returns>
    private IEnumerator StartMission(ClearConditionsType clearConditionsType)
    {
        // Missionの監視
        yield return StartCoroutine(ObservateMission(clearConditionsType));

        // Mission終了
        EndMission();
    }

    /// <summary>
    /// Missionの監視
    /// </summary>
    /// <param name="clearConditionsType"></param>
    /// <returns></returns>
    private IEnumerator ObservateMission(ClearConditionsType clearConditionsType)
    {

        // クリア条件を満たすまで監視(currentMissionDuration 変数には、敵の数か、残り時間が入る)
        while(currentMissionDuration > 0)
        {

            // クリア条件が時間経過の場合
            if(clearConditionsType == ClearConditionsType.TimeUp)
            {

                // カウントダウン
                currentMissionDuration--;
            }

            // TODO 武器取得イベントかつ、武器選択のいずれかのボタンを押したら


            yield return null;
        }

        Debug.Log("ミッション終了");
    }

    /// <summary>
    /// Mission終了
    /// </summary>
    public void EndMission()
    {

        // TODO 武器の取得イベントの場合には武器を取得せずにポップアップを閉じる


        // 今回分の敵の情報をクリア
        ClearEnemiesList();

        // カメラの移動再開
        railMoveController.CountUp();
    }

    /// <summary>
    /// 敵のリストをクリア
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
    /// 敵の情報をリストから削除し、ミッション内の敵の残数を減らす
    /// </summary>
    /// <param name="enemy"></param>
    public void RemoveEnemyList(EnemyController enemy)
    {
        currentMissionDuration--;

        enemiesList.Remove(enemy);
    }

    /// <summary>
    /// 敵の情報をリストに追加
    /// </summary>
    /// <param name="enemy"></param>
    public void AddEnemyList(EnemyController enemy)
    {
        enemiesList.Add(enemy);
    }
}
