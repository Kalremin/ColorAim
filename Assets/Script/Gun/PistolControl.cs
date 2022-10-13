using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolControl : GunControl
{
    [SerializeField] MeshRenderer _renderer;    // 색 변경할 렌더러
    DefineEnum.eColor _gunColor;
    public DefineEnum.eColor GunColor { get { return _gunColor; } }

    protected override void SetAwake()
    {
        _gunKind = DefineEnum.eGunKind.Pistol;
    }
    protected override void SetStart()
    {
        ChangeGunColor(GUIManager._instance.ColorState);
        GUIManager._instance.ChangeNextColorMat();
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_ANDROID
        if (VirtualPadControl._instance.GetTouch())
        {
            Shoot();
            VirtualPadControl._instance.TouchOff();
        }
#else
        if (Input.GetButtonDown("Fire1"))
            Shoot();
#endif

    }

    public void Shoot()
    {

        if (InGameManager._instance.GameState != DefineEnum.eGameState.Play
            || _eGunState == DefineEnum.eGunState.Reload
            || _eGunState == DefineEnum.eGunState.Fire)
            return;

        if (_magazine == 0)
        {
            ChangeGunColor(GUIManager._instance.ColorState);
            GUIManager._instance.ChangeNextColorMat();
            ResetMagazine();
            return;
        }

        _eGunState = DefineEnum.eGunState.Fire;
        _animator.SetTrigger("Fire");

        Vector3 aimPoint;

#if UNITY_ANDROID
        aimPoint = Camera.main.ScreenToViewportPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
#else
        aimPoint = Camera.main.ScreenToViewportPoint(Input.mousePosition);
#endif


        Ray ray = Camera.main.ViewportPointToRay(aimPoint);
        ray.origin = _posFire.transform.position;

        GameObject go = Instantiate(_bulletPref, _posFire.transform.position, _posFire.transform.rotation);
        BulletScript bull = go.GetComponent<BulletScript>();
        bull.launch(this, ray.direction, _gunColor);
        Instantiate(_fireEffect, _posFireEffect.transform.position, _fireEffect.transform.rotation);
        SoundControl._instance.PlayGunEffectSound(DefineEnum.eGunSound.FirePistol, gameObject, 0.3f * SoundControl._instance.GetEffectVol());
    }

    public void ChangeGunColor(DefineEnum.eColor color)
    {
        _gunColor = color;
        _renderer.material = InGameManager._instance.GetColorMaterial((int)_gunColor);
    }



    
}
