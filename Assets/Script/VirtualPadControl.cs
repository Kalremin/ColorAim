using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VirtualPadControl : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    static VirtualPadControl _unique;

    public static VirtualPadControl _instance => _unique;

    public int temp = 3;

    [SerializeField] Image bgImage;
    [SerializeField] Image stickImage;
    [SerializeField] Image shotImage;
    Vector2 inputVector;

    bool isTouching = false;
    float horizontalVal=0;
    float verticalVal=0;

    public float GetHorizontal() => horizontalVal;
    public float GetVertical() => verticalVal;

    private void Awake()
    {
        _unique = this;

#if !UNITY_ANDROID
        gameObject.SetActive(false);
#endif
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / bgImage.rectTransform.rect.width);
            pos.y = (pos.y / bgImage.rectTransform.rect.height);
            
            inputVector = new Vector2(pos.x * 2 , pos.y * 2 );
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            
            stickImage.rectTransform.localPosition = new Vector3(inputVector.x * (bgImage.rectTransform.rect.width / 3), inputVector.y * (bgImage.rectTransform.rect.height / 3));
            horizontalVal = inputVector.x * (bgImage.rectTransform.rect.width / 3) / temp;
            verticalVal = inputVector.y * (bgImage.rectTransform.rect.height / 3) / (temp * 2);
        }


    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        horizontalVal = 0;
        verticalVal = 0;
        inputVector = Vector3.zero;
        stickImage.rectTransform.position = bgImage.rectTransform.position;
    }

    public bool GetTouch()
    {
        return isTouching;
    }

    public void TouchOn()
    {
        isTouching = true;
        Debug.Log("TouchOn");
    }

    public void TouchOff()
    {
        isTouching = false;
        Debug.Log("TouchOFF");
    }

}
