using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    public GameObject Target = null;

    public Vector3 BeginPos = Vector3.zero;

    public Vector3 EndPos = Vector3.zero;

    public UnitParam Param = new UnitParam () {
        maxHp = 99999,
        attackPoint = 1,
        defensePoint = 0,
        moveSpeed = 8,
        attackSpeed = 0
    };

    private float time = 0;

    private readonly float speed = 10;

    void Awake () {
        BeginPos = transform.position;
    }

    void Update () {
        var dist = Vector3.Distance (BeginPos, EndPos);
        time += Time.deltaTime * speed / (Mathf.Approximately (dist, 0) ? 0.000001f : dist);
        transform.position = Vector3.LerpUnclamped (BeginPos, EndPos, time);
    }

    void OnTriggerStay (Collider other) {
        if (CheckDestroyTag(other.tag)) {
            Destroy (this.gameObject);
        }
    }

    // 弾を消しても良いオブジェクトタグか確認
    private bool CheckDestroyTag(string tag)
    {
        switch (tag)
        {
            // 消さずに貫通すべきオブジェクトタグ
            case "Player":
            case "PlayerObject":
            case "EnemyBullet":
                return false;

            default:
                break;
        }

        return true;
    }
}