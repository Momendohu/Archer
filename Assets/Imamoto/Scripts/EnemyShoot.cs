using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Shoot");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            GameObject runcherBullet = GameObject.Instantiate(_bulletObj) as GameObject; //runcherbulletにbulletのインスタンスを格納
            runcherBullet.transform.SetParent(this.transform);
            runcherBullet.GetComponent<Rigidbody>().velocity = transform.forward * _bulletSpeed; //アタッチしているオブジェクトの前方にbullet speedの速さで発射
            runcherBullet.transform.position = transform.position;

            yield return new WaitForSeconds(_shootInterval);
        }
    }

    [SerializeField]
    private float _bulletSpeed = 0f;

    [SerializeField]
    private float _shootInterval = 0f;

    [SerializeField]
    private GameObject _bulletObj = null;

    
}
