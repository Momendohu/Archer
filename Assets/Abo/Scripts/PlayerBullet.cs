using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour {
    public GameObject Target = null;

    public Vector3 BeginPos = Vector3.zero;

    public Vector3 EndPos = Vector3.zero;

    private float time = 0;

    void Awake () {
        BeginPos = transform.position;
    }

    void Update () {
        time += Time.deltaTime;
        transform.position = Vector3.LerpUnclamped (BeginPos, EndPos, time);
    }

    void OnTriggerStay (Collider other) {
        if (other.tag.Equals ("Enemy")) {
            Destroy (this.gameObject);
        }
    }
}