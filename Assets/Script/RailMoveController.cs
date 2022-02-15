using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class RailMoveController : MonoBehaviour
{
    [SerializeField]
    private Transform railMoveTarget;             // レール移動させる対象。カメラ、あるいは AR 用のカメラを指定

    [SerializeField]
    private RailPathData currentRailPathData;     // RailPathData クラスのアタッチされている RailPathData ゲームオブジェクトをアサイン。あとで自動アサインに変更

    private Tween tweenMove;
    private Tween tweenRotation;

    private Vector3[] paths;
    private float[] moveDurations;
    private int pathCount;

    private GameManager gameManager;

    /// <summary>
    /// RailMoveControllerの初期設定
    /// </summary>
    /// <param name="gameManager"></param>
    public void SetUpRailMoveController(GameManager gameManager)         //<= GameManagerより呼び出される
    {
        this.gameManager = gameManager;

        //TODO 他にある場合は追記。必要に応じて引数を通じて外部から情報をもらうようにする
    }

    /// <summary>
    /// 次に再生するレール移動の目的地と経路のパスを取得して設定
    /// </summary>
    /// <param name="nextRailPathData"></param>
    public void SetNextRailPathDate(RailPathData nextRailPathData)        //<= GameManagerより呼び出される
    {

        //目的地取得
        currentRailPathData = nextRailPathData;

        //移動開始
        StartCoroutine(StartRailMove());
    }

    //void Start()
    //{
        // Debug 用  レール移動の開始
    //    StartCoroutine(StartRailMove());
    //}

    /// <summary>
    /// レール移動の開始
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartRailMove()
    {

        yield return null;

        // パスのカウントを初期化
        pathCount = 0;

        // 移動する地点を取得するための配列の初期化
        paths = new Vector3[currentRailPathData.GetPathTrans().Length];
        float totalTime = 0;

        // 移動する位置情報と時間を順番に配列に取得
        for (int i = 0; i < currentRailPathData.GetPathTrans().Length; i++)
        {
            paths[i] = currentRailPathData.GetPathTrans()[i].position;
            totalTime += currentRailPathData.GetRailMoveDurations()[i];
        }

        // 各パスごとの移動時間を取得
        moveDurations = currentRailPathData.GetRailMoveDurations();

        // パスによる移動開始
        RailMove();


        // TODO 移動を一時停止


        // TODO ゲームの進行状態が移動中になるまで待機


        // TODO 移動開始

        Debug.Log("移動開始");
    }

    /// <summary>
    /// レール移動の一時停止
    /// </summary>
    public void PauseMove()
    {
        // 一時停止
        tweenMove.Pause();
        tweenRotation.Pause();
    }

    /// <summary>
    /// レール移動の再開
    /// </summary>
    public void ResumeMove()
    {
        // 移動再開
        tweenMove.Play();
        tweenRotation.Play();
    }

    /// <summary>
    /// パスのカウントアップ(パスごとに動かす場合)
    /// </summary>
    public void CountUp()
    {
        pathCount++;
        Debug.Log(pathCount);

        // 移動
        RailMove();
    }

    /// <summary>
    /// 2点間のパスの目標地点を設定して移動
    /// </summary>
    public void RailMove()
    {

        // 残っているパスがない場合
        if (pathCount >= currentRailPathData.GetPathTrans().Length)
        {

            // DOTween を停止
            tweenMove.Kill();

            tweenMove = null;
            tweenRotation = null;


            // TODO 移動完了に伴い、移動回数をカウントアップ


            // TODO 移動先の確認。移動先が残っていない場合には、ゲームマネージャー側で分岐の確認(次のルート選定、移動先の分岐、ボス、クリアのいずれか)


            Debug.Log("分岐確認");

            return;
        }

        // パスの設定用の配列を宣言
        Vector3[] targetPaths;

        // パスのカウント数に応じて分岐してパスを設定
        if (pathCount == 0)
        {
            targetPaths = new Vector3[2] { railMoveTarget.position, paths[pathCount] };
        }
        else
        {
            targetPaths = new Vector3[2] { paths[pathCount - 1], paths[pathCount] };
        }

        // パスの移動時間を設定
        float duration = moveDurations[pathCount];

        Debug.Log("スタート地点 :" + targetPaths[0]);
        Debug.Log("目標地点 :" + targetPaths[1]);
        Debug.Log("移動にかかる時間 :" + duration);

        // パスの移動
        tweenMove = railMoveTarget.transform.DOPath(targetPaths, duration).SetEase(Ease.Linear).OnWaypointChange((waypointIndex) => CheckArrivalDestination(waypointIndex));

        // カメラの回転
        tweenRotation = railMoveTarget.transform.DORotate(currentRailPathData.pathDataDetails[pathCount].pathTran.eulerAngles, duration).SetEase(Ease.Linear);
        Debug.Log($" 回転角度 :  { currentRailPathData.pathDataDetails[pathCount].pathTran.eulerAngles } ");
    }

    /// <summary>
    /// パスの目標地点に到着するたびに実行される
    /// </summary>
    /// <param name="waypointIndex"></param>
    private void CheckArrivalDestination(int waypointIndex)
    {

        if (waypointIndex == 0)
        {
            return;
        }

        Debug.Log("目標地点 到着 : " + pathCount + " 番目");

        // 移動の一時停止
        PauseMove();

        // TODO ミッションがあるか確認(ミッションが発生するかゲームマネージャー側で判定を行う)
        gameManager.CheckMissionTrigger(pathCount);


        // Debug用  次のパスをセットして移動を実行
        //CountUp();
    }
}