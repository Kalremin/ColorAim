using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HitItem : MonoBehaviour
{
    [SerializeField] protected GameObject _hitEffect;

    protected Rigidbody _rigidbody;
    protected Collider _collider;

    protected bool _isHitted;
    protected int _remainTime;
    protected float flow_time;

    protected void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();

        _isHitted = false;
        _remainTime = 10;
        flow_time = 0;

        SetAwake();
    }

    protected void Start()
    {
        _rigidbody.AddForce(RigidForce());
        SetStart();
    }



    protected abstract void SetAwake();

    protected abstract void SetStart();

    public abstract void CreateItem();

    public abstract Vector3 RigidForce();

    public void Hitted()
    {
        _isHitted = true;
    }
}
