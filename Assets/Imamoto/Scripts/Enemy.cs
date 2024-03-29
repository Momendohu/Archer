﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public UnitParam param { get { return _param; } }

    // Start is called before the first frame update
    void Awake()
    {
        SetEnemyUnitParam(ref _param);

        _nowHp = _param.maxHp;
        _mainGameManager = GodGameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("PlayerBullet"))
        {
            _nowHp -= other.gameObject.GetComponent<PlayerBullet>().Param.attackPoint;

            if (_nowHp <= 0)
            {
                DeadEnemy();
            }
        }
    }

    private void SetEnemyUnitParam(ref UnitParam param)
    {
        param.maxHp = _maxHp;
        param.attackPoint = _attackPoint;
        param.defensePoint = _defensePoint;
        param.moveSpeed = _moveSpeed;
        param.attackSpeed = _attackSpeed;
    }

    private void DeadEnemy()
    {
        Debug.Log("敵死亡");

        if (_isBoss)
        {
            _mainGameManager.GameClear();
        }
        Destroy(this.gameObject);
    }

    [SerializeField]
    private int _maxHp = 0;

    [SerializeField]
    private int _attackPoint = 0;

    [SerializeField]
    private int _defensePoint = 0;

    [SerializeField]
    private float _moveSpeed = 0f;

    [SerializeField]
    private float _attackSpeed = 0f;

    [SerializeField]
    private bool _isBoss = false;

    private UnitParam _param = new UnitParam();

    private int _nowHp = 0;

    private GodGameManager _mainGameManager;
}
