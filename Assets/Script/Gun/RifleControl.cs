using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RifleControl : GunControl
{
    bool isFiring;
    float fireDelayTime;
    float fireTime = 0.3f;

    protected override void SetAwake()
    {
        isFiring = false;
        _gunKind = DefineEnum.eGunKind.Rifle;
    }

    protected override void SetStart()
    {
    }


    // Update is called once per frame
    void Update()
    {
        if (InGameManager._instance.GameState != DefineEnum.eGameState.Play)
            return;

        if (_magazine == 0)
        {
            isFiring = false;
            PlayerControl._instance.ChangeWeapon();
            return;
        }

#if UNITY_ANDROID
        if (VirtualPadControl._instance.GetTouch())
            Shoot();

        if(!VirtualPadControl._instance.GetTouch() && _magazine != 0)
        {
            fireDelayTime = 0;
            isFiring = false;
            PlayerControl._instance.FireRifleAni(false);
        }
#else
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }

        if (Input.GetButtonUp("Fire1") && _magazine != 0)
        {
            fireDelayTime = 0;
            isFiring = false;
            PlayerControl._instance.FireRifleAni(false);
        }
#endif



    }

    public void Shoot()
    {
        if (fireDelayTime < 0)
            fireDelayTime = fireTime;
        fireDelayTime -= Time.deltaTime;

        if (fireDelayTime <= 0)
        {
            fireDelayTime = fireTime;

            Instantiate(_fireEffect, _posFireEffect.transform.position, _fireEffect.transform.rotation);
            SoundControl._instance.PlayGunEffectSound(DefineEnum.eGunSound.FireRifle, gameObject);
            PlayerControl._instance.FireRifleAni(true);
            GUIManager._instance.SetText_Magazine(--_magazine);

            Vector3 aimPoint;
#if UNITY_ANDROID
            aimPoint = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
#else
            aimPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
#endif

            Ray ray = Camera.main.ViewportPointToRay(aimPoint);
            ray.origin = _posFire.transform.position;

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject go = hit.collider.gameObject;

                switch (go.tag)
                {
                    case "Enemy":
                        Entity entity = go.GetComponent<Entity>();
                        entity.Hitted(DefineEnum.eColor.None);
                        break;
                    case "Item":
                        HitItem hitItem = go.GetComponent<HitItem>();
                        hitItem.Hitted();
                        break;
                    default:
                        InGameManager._instance.ComboClear();
                        break;
                }

            }

        }

    }


}


