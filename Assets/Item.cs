using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum PowerUpType
    {
        None,
        Heal,
        StatusUp,
        SuperShot,
        
    }

    public PowerUpType powerUpType { get { return _powerUpType; } }

    public UnitParam itemParam { get { return _param; } }

    // Start is called before the first frame update
    void Start()
    {
        // 設定されているパラメータ設定
        SetItemParam(ref _param);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetItemParam(ref UnitParam param)
    {
        param.maxHp = _maxHp;
        param.attackPoint = _attackPoint;
        param.defensePoint = _defensePoint;
        param.moveSpeed = _moveSpeed;
        param.attackSpeed = _attackSpeed;
    }

    [SerializeField]
    private PowerUpType _powerUpType = PowerUpType.None;

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

    private UnitParam _param = new UnitParam();

}
