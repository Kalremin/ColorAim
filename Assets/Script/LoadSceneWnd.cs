using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadSceneWnd : MonoBehaviour
{
    [SerializeField] Slider _loadingSlider;

    public void OpenWnd()
    {
        _loadingSlider.value = 0;
    }

    public void SetLoadingRate(float rate)
    {
        _loadingSlider.value = rate;
    }

}
