using UnityEngine;

public class TargetMove : MonoBehaviour
{
    void Update()
    {
        // 画面の下（例えばY座標が -10 以下）に落ちたら自動で消滅
        if (transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }

    // 右クリックで消去
    public void Extinguish()
    {
        Destroy(gameObject);
    }
}
