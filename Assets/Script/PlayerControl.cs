using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    static PlayerControl _unique;
    public static PlayerControl _instance => _unique;

    [SerializeField] GameObject _pistolHand;
    [SerializeField] GameObject _rifleHand;
    [SerializeField] GameObject _spineBody;

    Animator _animator;

    bool isShootingRifle = false;
    float mx, my, h, v;
    public Animator PlayerAnimator { get { return _animator; } }
    public bool IsShootingRifle { get { return isShootingRifle; } }

    public Camera minimapCam;


    private void Awake()
    {
        _unique = this;
        _animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _pistolHand.SetActive(true);
        _rifleHand.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(InGameManager._instance.GameState == DefineEnum.eGameState.Play)
            ChangeMousePos();

    }

    // 마우스 이동으로 화면 전환
    void ChangeMousePos()
    {

#if UNITY_ANDROID
        h = VirtualPadControl._instance.GetHorizontal();
        v = VirtualPadControl._instance.GetVertical();
#endif


#if UNITY_STANDALONE
        h = Input.GetAxis("Mouse X");
        v = Input.GetAxis("Mouse Y");
#endif

        mx += h * Time.deltaTime * 100;
        my += v * Time.deltaTime * 100;

        if (my >= 60)
        {
            my = 60;
        }
        else if (my <= -60)
        {
            my = -60;
        }

        transform.eulerAngles = new Vector3(-my, mx, 0);
        minimapCam.transform.eulerAngles = new Vector3(90, mx, 0);

    }
    public void PlayHitSound()
    {
        SoundControl._instance.PlayHitEffectSound(DefineEnum.eHitSound.HittedPlayer, gameObject, SoundControl._instance.GetEffectVol());
    }

    public void ChangePistolColor(DefineEnum.eColor toColor)
    {
        if (isShootingRifle)
            return;

        PistolControl pic=_pistolHand.transform.GetChild(0).GetComponent<PistolControl>();
        pic.ChangeGunColor(toColor);

    }
    public void ChangeWeapon()
    {
        _animator.SetTrigger("Swap");
        
        if (isShootingRifle)
        {
            _pistolHand.SetActive(true);
            _rifleHand.SetActive(false);
            isShootingRifle = false;
            _animator.SetBool("IsFireRifle", false);
        }
        else
        {
            _pistolHand.SetActive(false);
            _rifleHand.SetActive(true);
            isShootingRifle = true;
            
        }
        
    }
    public void ResetRifleMagazine() => _rifleHand.transform.GetChild(0).GetComponent<RifleControl>().ResetMagazine();

    public void FirePistolAni()
    {
        _animator.SetTrigger("");
    }

    public void FireRifleAni(bool fire)
    {
        _animator.SetBool("IsFireRifle", fire);
    }
    public void ReloadPistolAni()
    {
        _animator.SetTrigger("");
    }

    public void ReloadRifleAni()
    {
        _animator.SetTrigger("");
    }
}
