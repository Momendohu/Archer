using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {
    public struct RoomParam {
        public bool IsActive;
    };

    public RoomParam Param = new RoomParam () {
        IsActive = true,
    };

    void Awake () { }

    void Start () { }

    void Update () { }
}