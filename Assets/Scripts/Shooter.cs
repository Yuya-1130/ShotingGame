using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class Shooter : MonoBehaviour
{
    // カウント用
    [Header("破壊カウント")]
    [SerializeField] public TextMeshProUGUI scoreText;
    [Header("壊す回数")]
    [SerializeField] public float braekCount = 0;

    public float clearTargetCount = 10f;
    private bool isCleared = false; // クリアしたかどうかを記録する

    public string nextSceneName = "ClearScene";
    void Update()
    {
        if (isCleared) return;

        // 0 = 左クリックされた瞬間
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            // マウスの位置を取得
            Vector2 mousePosition = Mouse.current.position.ReadValue();
            // カメラからマウスのクリック位置に向かう光線を作る
            Ray ray = Camera.main.ScreenPointToRay(mousePosition);
            RaycastHit hit;

            // 光線が何かのコライダーに当たったか判定（距離100以内）
            if (Physics.Raycast(ray, out hit, 100f))
            {
                // 当たったオブジェクトに「TargetMove」スクリプトがついているか確認
                TargetMove target = hit.collider.GetComponent<TargetMove>();

                if (target != null)
                {
                    // ついていたら、その的を消す関数を実行！
                    target.Extinguish();
                    Debug.Log("破壊カウントを増やす");
                    braekCount++;

                    UpdateScoreText();

                    CheckClear();
                }
            }
        }
    }
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            // 魔法科のスコアボードっぽく「SCORE: 001」のように3桁で表示します
            // 整数にするために (int) をつけています
            scoreText.text = "SCORE: " + ((int)braekCount).ToString("D3");
        }
    }

    void CheckClear()
    {
        if (braekCount >= clearTargetCount)
        {
            isCleared = true;
            Debug.Log("ゲームクリア！シーンを移動します。");

            //指定した名前のシーンへ移動する命令
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
