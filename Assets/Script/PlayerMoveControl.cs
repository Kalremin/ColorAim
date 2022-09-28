using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerMoveControl : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] Image bgImage;
    [SerializeField] Image stickImage;

    float mx, my;
    Vector2 pos;

    // Start is called before the first frame update
    void Start()
    {
        pos = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (InGameManager._instance.GameState == DefineEnum.eGameState.Play)
            ChangeMousePos(true);
    }

    void ChangeMousePos(bool check)
    {
        if (!check)
            return;

        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");
        
//#if UNITY_ANDROID

//#else
//        float h = Input.GetAxis("Mouse X");
//        float v = Input.GetAxis("Mouse Y");
//#endif
        
        float tempx = h * Time.deltaTime * 100;
        float tempy = v * Time.deltaTime * 100;

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
    }
    public void OnDrag(PointerEventData eventData)
    {
        

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {

            
        }

        stickImage.rectTransform.position = eventData.position;
        pos = stickImage.rectTransform.position - bgImage.rectTransform.position;

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        stickImage.rectTransform.position = eventData.position;
        pos = stickImage.rectTransform.position - bgImage.rectTransform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        stickImage.rectTransform.position = bgImage.rectTransform.position;
        pos = Vector2.zero;
    }
}
