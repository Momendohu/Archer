using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearchField : MonoBehaviour {
    public GameObject SearchedObject = null;

    void OnTriggerStay (Collider other) {
        if (other.tag.Equals ("Enemy")) {
            SearchedObject = other.gameObject;
        }
    }
}