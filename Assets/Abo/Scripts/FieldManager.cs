using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : SingletonMonoBehaviour<MonoBehaviour> {
    public struct FieldParam {
        
    };

    public FieldParam Param = new FieldParam () {
    };

    [SerializeField] private RoomManager _roomManager = null;
}