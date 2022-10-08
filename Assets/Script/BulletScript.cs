using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody _rigidbody;
    GunControl _gun;
    DefineEnum.eColor _hitColor;
    float flow_timer;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Destroy(gameObject, 7);
    }

    public void launch(GunControl gun, Vector3 posFire, DefineEnum.eColor gunColor)
    {
        _gun = gun;
        _hitColor = gunColor;
        transform.forward = posFire;
        _rigidbody.AddForce(transform.forward * 1000);
    }

    private void OnCollisionEnter(Collision collision)
    {
        bool isNotHit = true;

        switch (collision.transform.tag)
        {
            case "Enemy":
                Entity entity = collision.gameObject.GetComponent<Entity>();

                if (entity.Hitted(_hitColor))
                    isNotHit = false;

                break;

            case "Item":
                collision.gameObject.GetComponent<HitItem>().Hitted();
                isNotHit = false;
                break;

        }

        if (isNotHit)
            InGameManager._instance.ComboClear();
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        bool isNotHit = true;

        switch (other.transform.tag)
        {
            case "Enemy":
                Entity entity = other.gameObject.GetComponent<Entity>();

                if (entity.Hitted(_hitColor))
                    isNotHit = false;

                break;

            case "Item":
                other.gameObject.GetComponent<HitItem>().Hitted();
                isNotHit = false;
                break;

        }

        if (isNotHit)
            InGameManager._instance.ComboClear();

        Destroy(gameObject);
    }



}
