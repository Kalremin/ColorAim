using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    //static OptionManager _unique;
    //public static OptionManager _instance { get { return _unique; } }

    public Slider _effectSoundSlider;
    public Slider _BGMSoundSlider;

    bool isOpen;
    public bool EnableOpen { get { return isOpen; }}

    private void Awake()
    {
        isOpen = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        _effectSoundSlider.minValue = 0f;
        _effectSoundSlider.maxValue = 1f;
        _effectSoundSlider.value = SoundControl._instance.GetEffectVol();

        _BGMSoundSlider.minValue = 0f;
        _BGMSoundSlider.maxValue = 1f;
        _BGMSoundSlider.value = SoundControl._instance.GetBGMVol();
    }

    // Update is called once per frame
    

    public void BtnClickOptionOK()
    {
        SoundControl._instance.ChangeVolume(_effectSoundSlider.value, _BGMSoundSlider.value);

        //TitleUIManager._instance.BtnClickOptionOK();
        Destroy(gameObject);
    }

}
