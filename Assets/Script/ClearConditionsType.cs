/// <summary>
/// イベントのクリア条件の種類
/// </summary>
public enum ClearConditionsType　　　//　<= public enum に変更し、MonoBehaviour クラスの継承を削除します。
{
    Destroy,　　// 敵を指定数倒したらクリア。基本的には敵の全滅と同義
    TimeUp,　　 // 指定の時間が来たらクリア

    // TODO 他にもあれば追加する

}