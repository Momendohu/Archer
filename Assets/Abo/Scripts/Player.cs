using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class Player : MonoBehaviour {
    private Rigidbody _rigidbody = null;

    private Vector2 _moveSpeed = new Vector2 (8, 8);

    void Awake () {
        _rigidbody = GetComponent<Rigidbody> ();
    }

    void Start () {

    }

    void Update () {
        //TODO:斜め移動の速度
        bool left = Input.GetKey (KeyCode.LeftArrow);
        bool right = Input.GetKey (KeyCode.RightArrow);
        bool up = Input.GetKey (KeyCode.UpArrow);
        bool down = Input.GetKey (KeyCode.DownArrow);

        _rigidbody.velocity = Vector3.zero;
        if (left) _rigidbody.velocity += new Vector3 (-_moveSpeed.x, 0, 0);
        if (right) _rigidbody.velocity += new Vector3 (_moveSpeed.x, 0, 0);
        if (up) _rigidbody.velocity += new Vector3 (0, 0, _moveSpeed.y);
        if (down) _rigidbody.velocity += new Vector3 (0, 0, -_moveSpeed.y);
    }
}