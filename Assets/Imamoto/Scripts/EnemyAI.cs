using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // プレイヤー自機取得
        _playerObj = GameObject.FindGameObjectWithTag("Player");

        _targetTrs = _playerObj.transform;

        _agent = GetComponent<NavMeshAgent>();

        _speed = transform.GetComponent<Enemy>().param.moveSpeed;
        Debug.Log(_speed);
        _enemyShoot = GetComponent<EnemyShoot>();

        StartCoroutine("UpdateTarget", _waitTime);

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();

        EnemyShootExecute();
    }

    // 敵のプレイヤーへの移動
    private void EnemyMove()
    {
        // プレイヤーとの距離ベクトル
        var v = transform.position - _targetTrs.position;

        // プレイヤー位置にたどり着いたら、移動しない
        if (v.sqrMagnitude <= 0.1f)
        {
            return;
        }

        // 位置更新
        transform.position += transform.forward * _speed * Time.deltaTime;
    }

    // プレイヤーの位置情報更新
    private IEnumerator UpdateTarget(float waitTime)
    {
        while (true)
        {
            if (_playerObj == null)
                yield break;

            transform.LookAt(_playerObj.transform);

            yield return new WaitForSeconds(waitTime);
        }
    }

    // 敵の攻撃処理
    private void EnemyShootExecute()
    {
        // プレイヤーとの距離ベクトル
        var v = transform.position - _targetTrs.position;

        if (!_isAttackState && v.sqrMagnitude <= _attackStartPlayerDistence)
        {
            Debug.Log("攻撃開始");
            StartCoroutine(_enemyShoot.Shoot());
            _isAttackState = true;
        }
        else if(_isAttackState && v.sqrMagnitude > _attackStartPlayerDistence)
        {
            Debug.Log("攻撃終了");
            StopCoroutine(_enemyShoot.Shoot());
            _isAttackState = false;
        }
    }

    private GameObject _playerObj;

    private NavMeshAgent _agent;

    private Transform _targetTrs;

    private float _speed;

    [SerializeField]
    private float _waitTime = 0f;

    [SerializeField]
    private float _attackStartPlayerDistence = 0f;

    private bool _isAttackState;

    private EnemyShoot _enemyShoot;
}
