using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Player : MonoBehaviour {
    private readonly string path_prefab_bullet = "Prefabs/PlayerBullet";

    private Rigidbody _rigidbody = null;

    private PlayerSearchField _searchField = null;

    private bool isSearing = false;

    private int _nowHp = 0;

    public UnitParam Param = new UnitParam() {
        maxHp = 10,
        attackPoint = 0,
        defensePoint = 0,
        moveSpeed = 8,
        attackSpeed = 0
    };

    void Awake () {
        _rigidbody = GetComponent<Rigidbody> ();
        _searchField = transform.Find ("SearchField").GetComponent<PlayerSearchField> ();

        _nowHp = Param.maxHp;
    }

    void Start () { }

    void Update () {
        //TODO:斜め移動の速度の調整
        bool left = Input.GetKey (KeyCode.LeftArrow);
        bool right = Input.GetKey (KeyCode.RightArrow);
        bool up = Input.GetKey (KeyCode.UpArrow);
        bool down = Input.GetKey (KeyCode.DownArrow);

        _rigidbody.velocity = Vector3.zero;
        if (left) {
            _rigidbody.velocity += new Vector3 (-Param.moveSpeed, 0, 0);
        }

        if (right) {
            _rigidbody.velocity += new Vector3 (Param.moveSpeed, 0, 0);
        }

        if (up) {
            _rigidbody.velocity += new Vector3 (0, 0, Param.moveSpeed);
        }

        if (down) {
            _rigidbody.velocity += new Vector3 (0, 0, -Param.moveSpeed);
        }

        if (_searchField.SearchedObject && !isSearing) {
            isSearing = true;
            StartCoroutine (ShotBullet (_searchField.SearchedObject));
        }
    }

    private IEnumerator ShotBullet (GameObject target) {
        while (true) {
            if (!target) {
                isSearing = false;
                break;
            }
            createBullet (target);
            yield return new WaitForSeconds (0.5f);
        }
    }

    private GameObject createBullet (GameObject target) {
        GameObject obj = null;
        if (target) {
            obj = Instantiate (Resources.Load (path_prefab_bullet), transform.position, Quaternion.identity) as GameObject;
            obj.GetComponent<PlayerBullet> ().EndPos = target.transform.position;
        }

        return obj;
    }

    void OnTriggerEnter (Collider other) {
        switch (other.tag)
        {
            case "EnemyBullet":
                _nowHp -= other.gameObject.GetComponent<EnemyBullet>().attackPoint;

                if (_nowHp <= 0)
                {
                    print("GAME OVER");
                }
                break;

            case "Item":
                var item = other.gameObject.GetComponent<Item>();
                UpdateParam(item);
                Destroy(other.gameObject);
                break;
        }
    }

    // アイテム取得によるステータス更新処理
    void UpdateParam(Item item){
        switch (item.powerUpType)
        {
            // ステータス上昇
            case Item.PowerUpType.StatusUp:
                Param.maxHp = item.itemParam.maxHp > 0 ? item.itemParam.maxHp : Param.maxHp;
                Param.attackPoint = item.itemParam.attackPoint > 0 ? item.itemParam.attackPoint : Param.attackPoint;
                Param.defensePoint = item.itemParam.defensePoint > 0 ? item.itemParam.defensePoint : Param.defensePoint;
                Param.moveSpeed = item.itemParam.moveSpeed > 0 ? item.itemParam.moveSpeed : Param.moveSpeed;
                Param.attackSpeed = item.itemParam.attackSpeed > 0 ? item.itemParam.attackSpeed : Param.attackSpeed;
                break;

            default:
                Debug.LogAssertion("存在しないパワーアップをしているよ！ : " + item.powerUpType);
                break;
        }

    }
}