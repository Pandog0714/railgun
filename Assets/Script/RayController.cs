using System.Collections;
using UnityEngine;

/// <summary>
/// Ray による弾の発射処理の制御クラス
/// </summary>
public class RayController : MonoBehaviour
{
    [Header("発射口用のエフェクトのサイズ調整")]
    public Vector3 muzzleFlashScale;

    private bool isShooting;

    //発射時のエフェクト、被弾時のエフェクト
    private GameObject muzzleFlashObj;
    private GameObject hitEffectObj;

    // Ray で補足した対象の代入用
    private GameObject target;

    [SerializeField, Header("Ray 用のレイヤー設定")]
    private int[] layerMasks;

    //Debug用。確認できたら、SerializeField 属性を削除して private にしておく
    [SerializeField]
    private string[] layerMasksStr;

    [SerializeField]
    private PlayerController playerController;

    private EnemyController enemy;

    void Start()
    {
        // Layer の情報を文字列に変換し、Raycast メソッドで利用しやすい情報を変数として作成しておく
        layerMasksStr = new string[layerMasks.Length];
        for(int i = 0; i < layerMasks.Length; i++)
        {
            layerMasksStr[i] = LayerMask.LayerToName(layerMasks[i]);
        }
    }

    void Update()
    {
        // TODO ゲーム状態がプレイ中でない場合には処理を行わない制御をする


        // リロード判定(弾数 0 でリロード機能ありの場合)
        if(playerController.BulletCount == 0 && playerController.isReloadModeOn && Input.GetMouseButtonDown(0))
        {
            StartCoroutine(playerController.ReloadBullet());
        }

        // 発射判定(弾数が残っており、リロード実行中でない場合) 押しっぱなしで発射できる
        if(playerController.BulletCount > 0 && !playerController.isReloading && Input.GetMouseButton(0))
        {
            // 発射時間の計測
            StartCoroutine(ShootTimer());
        }
    }

    /// <summary>
    /// 継続的な弾の発射処理
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootTimer()
    {
        if(!isShooting)
        {
            isShooting = true;

            // 発射エフェクトの表示。初回のみ生成し、２回目はオンオフで切り替える
            if(muzzleFlashObj == null)
            {
                // 発射口の位置に RayController ゲームオブジェクトを配置する
                muzzleFlashObj = Instantiate(EffectManager.instance.muzzleFlashPrefab, transform.position, transform.rotation);
                muzzleFlashObj.transform.SetParent(gameObject.transform);
                muzzleFlashObj.transform.localScale = muzzleFlashScale;

            }
            else
            {
                muzzleFlashObj.SetActive(true);
            }

            // 発射
            Shoot();

            yield return new WaitForSeconds(playerController.shootInterval);

            muzzleFlashObj.SetActive(false);

            if(hitEffectObj != null)
            {
                hitEffectObj.SetActive(false);
            }

            isShooting = false;

        }
        else
        {
            yield return null;
        }
    }

    /// <summary>
    /// 弾の発射
    /// </summary>
    private void Shoot()
    {
        // カメラの位置からクリック(タップ)した地点に Ray を投射
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction, Color.red, 3.0f);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, playerController.shootRange, LayerMask.GetMask(layerMasksStr)))
        {

            Debug.Log(hit.collider.gameObject.name);

            // 同じ対象を攻撃しているか確認。対象がいなかったか、同じ対象でない場合
            if(target == null || target != hit.collider.gameObject)
            {

                target = hit.collider.gameObject;

                Debug.Log(target.name);

                // TryGetComponent の処理で敵の情報を取得できるか判定をする
                if(target.TryGetComponent(out enemy))
                {
                    //Debug
                    Debug.Log(enemy);


                    // 敵の情報を取得できた場合、ダメージの情報を敵のクラスに渡す 
                    enemy.TriggerEvent(playerController.bulletPower);

                    // 演出
                    PlayHitEffect(hit.point, hit.normal);
                }
            
            // 同じ対象の場合
            }
            else if(target == hit.collider.gameObject)
            {

                // ダメージの情報を敵のクラスに渡す 
                enemy.TriggerEvent(playerController.bulletPower);

                // 演出
                PlayHitEffect(hit.point, hit.normal);

            }
        }

        // 弾数を減らす
        playerController.CalcBulletCount(-1);
    }

    /// <summary>
    /// ヒット演出
    /// </summary>
    /// <param name="effectPos"></param>
    /// <param name="surfacePos"></param>
    private void PlayHitEffect(Vector3 effectPos, Vector3 surfacePos)
    {
        //ヒット用のエフェクトが作成されているか確認する
        if(hitEffectObj == null)
        {   
            //まだ作成されていない場合はヒット用のエフェクトを作成
            hitEffectObj = Instantiate(EffectManager.instance.hitEffectPrefab, effectPos, Quaternion.identity);
        }
        else
        {
            //生成されている場合には、エフェクトを表示する位置と開店情報を更新
            hitEffectObj.transform.position = effectPos;
            hitEffectObj.transform.rotation = Quaternion.FromToRotation(Vector3.forward, surfacePos);

            //エフェクトを表示
            hitEffectObj.SetActive(true);
        }
    }
}