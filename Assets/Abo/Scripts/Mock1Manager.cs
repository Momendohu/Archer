using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mock1Manager : MonoBehaviour {
    void Awake () {
        AudioManager.Instance.PlayBGM ("sample1");
    }

    void Start () {

    }

    void Update () {

    }
}