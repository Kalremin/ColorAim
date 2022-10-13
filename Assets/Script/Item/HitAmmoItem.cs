using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAmmoItem : HitItem
{
    [SerializeField] GameObject _actEffect;

    BoxCollider _boxCollider;

    bool isAct = false;


    protected override void SetAwake()
    {
        _rigidbody = GetComponent<Rigidbody>();

        _collider = GetComponent<Collider>();
    }
    protected override void SetStart()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        flow_time += Time.deltaTime;

        if (flow_time >= 10)
        {
            ItemAct();
            
        }

        if(flow_time >= 11)
        {
            Destroy(gameObject);
        }

        if (_isHitted)
        {
            if (PlayerControl._instance.IsShootingRifle)
            {
                PlayerControl._instance.ResetRifleMagazine();
            }
            else
            {
                PlayerControl._instance.ChangeWeapon();
            }

            Destroy(gameObject);
        }
        
        transform.Rotate(Vector3.up);

    }

    private void OnTriggerStay(Collider other)
    {
        if (isAct)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Entity>().SetBarrior(true);
                Instantiate(_actEffect, transform.position, _actEffect.transform.rotation);
            }
            Destroy(gameObject);
        }
    }

    void ItemAct()
    {
        _boxCollider.size = new Vector3(10, 0, 10);
        _boxCollider.isTrigger = true;
        isAct = true;

    }

    


    public override void CreateItem()
    {
        
    }

    public override Vector3 RigidForce()
    {
        return Vector3.zero;
    }
}
