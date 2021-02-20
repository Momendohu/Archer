using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour {
    private readonly string path_prefab_wall_cube = "Prefabs/WallCube";

    private void createWalls () {
        //NOTE:仮置き
        for (int i = -20; i < 20; i++) {
            this.createWall (new Vector3Int (-7, 1, i));
            this.createWall (new Vector3Int (7, 1, i));
        }

        for (int j = -6; j < 7; j++) {
            this.createWall (new Vector3Int (j, 1, -20));
            this.createWall (new Vector3Int (j, 1, 19));
        }
    }

    private GameObject createWall (Vector3Int pos) {
        GameObject obj = Instantiate (Resources.Load (path_prefab_wall_cube), pos, Quaternion.identity) as GameObject;
        obj.transform.SetParent (this.transform);
        return obj;
    }

    void Awake () {
        this.createWalls ();
    }

    void Start () {

    }

    void Update () {

    }
}