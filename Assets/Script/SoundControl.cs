using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    [SerializeField] AudioClip[] _hitEffectClips;
    [SerializeField] AudioClip[] _gunEffectClips;
    [SerializeField] AudioSource _playerBGM;
    [SerializeField] AudioClip[] _BGMClips;

    static SoundControl _unique;

    public static SoundControl _instance { get { return _unique; } }

    float _effectSoundVol;
    float _BGMSoundVol;

    public float GetEffectVol() => _effectSoundVol;
    public float GetBGMVol() => _BGMSoundVol;

    private void Awake()
    {
        _unique = this;
        _effectSoundVol = 1f;
        _BGMSoundVol = 1f;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHitEffectSound(DefineEnum.eHitSound eEffect, GameObject parentObj, float volume =1, bool isLooping = false)
    {
        GameObject go = new GameObject(eEffect.ToString());
        go.transform.parent = parentObj.transform;

        AudioSource soundPlayer = go.AddComponent<AudioSource>();
        soundPlayer.clip = _hitEffectClips[(int)eEffect];
        soundPlayer.volume = volume;
        soundPlayer.loop = isLooping;
        soundPlayer.Play();

        Destroy(go, 2f);
    }

    public void PlayGunEffectSound(DefineEnum.eGunSound eEffect, GameObject parentObj, float volume = 1, bool isLooping = false)
    {
        GameObject go = new GameObject(eEffect.ToString());
        go.transform.parent = parentObj.transform;

        AudioSource soundPlayer = go.AddComponent<AudioSource>();
        soundPlayer.clip = _gunEffectClips[(int)eEffect];
        soundPlayer.volume = volume;
        soundPlayer.loop = isLooping;
        soundPlayer.Play();

        Destroy(go, 2f);
    }

    public void PlayBGMSound(DefineEnum.eScenesState scenesState, float volume = 1, bool isLooping = true)
    {
        _playerBGM.clip = _BGMClips[(int)scenesState];
        _playerBGM.volume = volume;
        _playerBGM.loop = isLooping;
        _playerBGM.Play();
    }

    public void ChangeVolume(float effectVol, float bgmVol)
    {
        _effectSoundVol = effectVol;
        _BGMSoundVol = bgmVol;
        _playerBGM.volume = _BGMSoundVol;
    }
}
