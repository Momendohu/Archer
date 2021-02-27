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

        _agent = GetComponent<NavMeshAgent>();

        _speed = 1f;

        StartCoroutine("UpdateTarget", _waitTime);

    }

    // Update is called once per frame
    void Update()
    {
        EnemyMove();
    }

    // 敵のプレイヤーへの移動
    private void EnemyMove()
    {
        // プレイヤーとの距離ベクトル
        var v = transform.position - _targetTrs.position;

        // プレイヤー位置にたどり着いたら、移動しない
        if (v.sqrMagnitude <= 0.1f)
            return;

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
            _targetTrs = _playerObj.transform;

            yield return new WaitForSeconds(waitTime);
        }
    }

    private GameObject _playerObj;

    private NavMeshAgent _agent;

    private Transform _targetTrs;

    private float _speed;

    [SerializeField]
    private float _waitTime = 0f;
}
