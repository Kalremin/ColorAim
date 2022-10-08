using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitColorItem : HitItem
{
    MeshRenderer _renderer;
    DefineEnum.eColor _itemColor;

    
    protected override void SetAwake()
    {
        _renderer = GetComponent<MeshRenderer>();
        
    }
    protected override void SetStart()
    {
        _itemColor = InGameManager._instance.SetRandomColorState();
        _renderer.material = InGameManager._instance.GetColorMaterial((int)_itemColor);
    }

    // Update is called once per frame
    void Update()
    {
        flow_time += Time.deltaTime;

        if(_isHitted)
        {
            flow_time = 0;
            _renderer.enabled = false;
            _collider.enabled = false;
            //GameObject go = Instantiate(_hitEffect, transform.position, _hitEffect.transform.rotation);
            //SoundControl._instance.PlayHitEffectSound(DefineEnum.eHitSound.HittedItem, go);
            PlayerControl._instance.ChangePistolColor(_itemColor);

            if(TryGetComponent(out PistolControl temp))
            {
                temp.ChangeGunColor(_itemColor);
            }

            Destroy(gameObject, 2f);
        }

        if (flow_time >= 10)
            Destroy(gameObject);
    }

    public override Vector3 RigidForce()
    {
        return Vector3.up * 300 + Vector3.forward * Random.Range(-200, 200) + Vector3.right * Random.Range(-200, 200);
    }

    public override void CreateItem()
    {
        throw new System.NotImplementedException();
    }
}
