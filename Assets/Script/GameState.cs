/// <summary>
/// ゲームの進行状態の種類
/// </summary>
public enum GameState
{
    Wait,            // ゲームの準備前
    Play_Move,       // ゲームプレイ中。移動時
    Play_Mission,    // ゲージプレイ中。ミッション時
    GameUp,          // ゲーム終了(ゲームオーバー、ゲームクリア)
}
