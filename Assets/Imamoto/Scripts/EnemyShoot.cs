using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator Shoot()
    {
        while (true)
        {
            GameObject runcherBullet = GameObject.Instantiate(_bulletObj) as GameObject; //runcherbulletにbulletのインスタンスを格納
            runcherBullet.transform.position = transform.position;
            runcherBullet.transform.SetParent(GameObject.Find("EnemyBullets").transform);
            runcherBullet.GetComponent<EnemyBullet>().StartShootBullet( transform.forward, _bulletSpeed, transform.GetComponent<Enemy>().param.attackPoint);

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
