using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private RailMoveController railMoveController;

    //後ほどリスト化して複数ルートを管理できるようにする
    [SerializeField, Header("経路用のパス群の元データ")]
    //private RailPathData or originRailPathDate;

    //Debug用
    [SerializeField, Header("パスにおけるミッションの発生有無")]
    private bool[] isMissionTriggers;


    // Start is called before the first frame update
    private void Start()
    {
        //TODO ゲームの状態を準備する

        //TODO ルート用の経路情報を設定

        //RailMoveContrrollerの初期設定
        //railMoveController.SetupRailMoveController(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
