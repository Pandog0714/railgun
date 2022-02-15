using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RailMoveController railMoveController;

    //後ほどリスト化して複数ルートを管理できるようにする
    [SerializeField, Header("経路用のパス群の元データ")]
    private RailPathData originRailPathDate;

    //Debug用
    [SerializeField, Header("パスにおけるミッションの発生有無")]
    private bool[] isMissionTriggers;


    // Start is called before the first frame update
    private void Start()
    {
        //TODO ゲームの状態を準備する

        //TODO ルート用の経路情報を設定

        //RailMoveContrrollerの初期設定
        railMoveController.SetUpRailMoveController(this);

        //パスデータよりミッションの発生有無情報取得
        SetMissionTriggers();

        //次に再生するレール移動の目的地と経路のパスを設定
        railMoveController.SetNextRailPathDate(originRailPathDate);

        //TODO 経路の準備が完了するのを待つ(Start メソッドの戻り値を IEnumerator に変更してコルーチンメソッドに変える)

        //TODO ゲームの状態をプレイ中に変更する
    }

    /// <summary>
    /// パスデータよりミッションの発生有無情報を取得
    /// </summary>
    // Update is called once per frame
    private void SetMissionTriggers()
    {
        //配列の初期化
        isMissionTriggers = new bool[originRailPathDate.GetIsMissionTriggers().Length];

        //ミッション発生有無の情報を登録
        isMissionTriggers = originRailPathDate.GetIsMissionTriggers();
    }

    /// <summary>
    /// ミッションの発生有無の判定
    /// </summary>
    /// <param name="index"></param>
    public void CheckMissionTrigger(int index)
    {
        if (isMissionTriggers[index])
        {
            //TODO ミッション発生
            Debug.Log("ミッション発生");

            //Debug用 今はそのまま
            railMoveController. CountUp();
        }
        else
        {
            //ミッションなし。次のパスへ移動再開
            railMoveController.CountUp();
        }
    }
}
