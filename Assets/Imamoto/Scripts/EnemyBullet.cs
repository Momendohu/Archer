using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public int attackPoint { get { return _attackPoint; } }

    // Start is called before the first frame update
    void Start()
    {
        _attackPoint = transform.root.gameObject.GetComponent<Enemy>().param.attackPoint;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            Destroy(this.gameObject);
        }
    }

    private int _attackPoint;
}
