using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Player : MonoBehaviour {
    private readonly string path_prefab_bullet = "Prefabs/PlayerBullet";

    private Rigidbody _rigidbody = null;

    private bool _isSearching = false;

    private bool _isMoving = false;

    private int _nowHp = 0;

    private Vector3 _moveVelocity = Vector3.zero;

    private float _totalFall = 0;

    private bool _isJump = false;

    public UnitParam Param = new UnitParam () {
        maxHp = 10,
        attackPoint = 0,
        defensePoint = 0,
        moveSpeed = 8,
        attackSpeed = 0
    };

    void Awake () {
        _rigidbody = GetComponent<Rigidbody> ();

        _nowHp = Param.maxHp;
    }

    void Start () {

    }

    void Update () {
        //TODO:斜め移動の速度の調整
        bool left = Input.GetKey (KeyCode.LeftArrow);
        bool right = Input.GetKey (KeyCode.RightArrow);
        bool up = Input.GetKey (KeyCode.UpArrow);
        bool down = Input.GetKey (KeyCode.DownArrow);

        _isMoving = (left || right || up || down);

        _rigidbody.velocity = Vector3.zero;
        _moveVelocity = Vector3.zero;

        _isJump = CheckGrounded();

        if (left) {
            _moveVelocity += new Vector3 (-Param.moveSpeed, 0, 0);
        }

        if (right) {
            _moveVelocity += new Vector3 (Param.moveSpeed, 0, 0);
        }

        if (up) {
            _moveVelocity += new Vector3 (0, 0, Param.moveSpeed);
        }

        if (down) {
            _moveVelocity += new Vector3 (0, 0, -Param.moveSpeed);
        }

        if (_isJump)
        {
            _totalFall += Time.deltaTime;
            _moveVelocity.y = Physics.gravity.y * _totalFall;
        }
        else
        {
            _totalFall = 0;
        }

        if (!_isJump && Input.GetKeyDown(KeyCode.Space))
        {
            //_moveVelocity.y += 100f;
            _rigidbody.AddForce(Vector3.up * 500, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _moveVelocity;
    }

    private IEnumerator ShotBullet (GameObject target) {
        while (true) {
            if (_isMoving) {
                yield return new WaitForSeconds (0.5f);
                continue;
            }

            if (!target) {
                _isSearching = false;
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

    private bool CheckGrounded()
    {
        var ray = new Ray(transform.position + Vector3.up * 0.1f, Vector3.down);
        var distance = 0.6f;
        return !Physics.Raycast(ray, distance);
    }

    void OnTriggerEnter (Collider other) {
        switch (other.tag) {
            case "EnemyBullet":
                _nowHp -= other.gameObject.GetComponent<EnemyBullet> ().attackPoint;

                if (_nowHp <= 0) {
                    print ("GAME OVER");
                }
                break;

            case "Item":
                var item = other.gameObject.GetComponent<Item> ();
                UpdateParam (item);
                Destroy (other.gameObject);
                break;
        }
    }

    // アイテム取得によるステータス更新処理
    void UpdateParam (Item item) {
        switch (item.powerUpType) {
            // ステータス上昇
            case Item.PowerUpType.StatusUp:
                Param.maxHp += item.itemParam.maxHp > 0 ? item.itemParam.maxHp : Param.maxHp;
                Param.attackPoint += item.itemParam.attackPoint > 0 ? item.itemParam.attackPoint : Param.attackPoint;
                Param.defensePoint += item.itemParam.defensePoint > 0 ? item.itemParam.defensePoint : Param.defensePoint;
                Param.moveSpeed += item.itemParam.moveSpeed > 0 ? item.itemParam.moveSpeed : Param.moveSpeed;
                Param.attackSpeed += item.itemParam.attackSpeed > 0 ? item.itemParam.attackSpeed : Param.attackSpeed;
                break;

            default:
                Debug.LogAssertion ("存在しないパワーアップをしているよ！ : " + item.powerUpType);
                break;
        }

    }
}