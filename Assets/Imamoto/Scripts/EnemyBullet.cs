using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int attackPoint { get { return _attackPoint; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = transform.position;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    public void StartShootBullet(Vector3 forwardVec, float bulletSpeed, int attackPoint)
    {
        _bulletForwardVec = forwardVec;

        _bulletSpeed = bulletSpeed;

        _attackPoint = attackPoint;

        GetComponent<Rigidbody>().velocity = _bulletForwardVec * _bulletSpeed; //アタッチしているオブジェクトの前方にbullet speedの速さで発射
    }

    private int _attackPoint;

    private Vector3 _bulletForwardVec = Vector3.zero;

    private float _bulletSpeed;
}
