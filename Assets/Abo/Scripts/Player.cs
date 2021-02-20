using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Player : MonoBehaviour {
    private readonly string path_prefab_bullet = "Prefabs/Bullet";

    private Rigidbody _rigidbody = null;

    private PlayerSearchField _searchField = null;

    private Vector2 _moveSpeed = new Vector2 (8, 8);

    private bool isSearing = false;

    void Awake () {
        _rigidbody = GetComponent<Rigidbody> ();
        _searchField = transform.Find ("SearchField").GetComponent<PlayerSearchField> ();
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
            _rigidbody.velocity += new Vector3 (-_moveSpeed.x, 0, 0);
        }

        if (right) {
            _rigidbody.velocity += new Vector3 (_moveSpeed.x, 0, 0);
        }

        if (up) {
            _rigidbody.velocity += new Vector3 (0, 0, _moveSpeed.y);
        }

        if (down) {
            _rigidbody.velocity += new Vector3 (0, 0, -_moveSpeed.y);
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
}