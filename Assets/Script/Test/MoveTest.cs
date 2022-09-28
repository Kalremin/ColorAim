using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour
{
    float mx, my;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveControl();
    }

    void MoveControl()
    {
        //float h = Input.GetAxis("Mouse X");
        //float v = Input.GetAxis("Mouse Y");
        float h = VirtualPadControl._instance.GetHorizontal();
        float v = VirtualPadControl._instance.GetVertical();

        float tempx = h * Time.deltaTime * 100;
        float tempy = v * Time.deltaTime * 100;

        mx += h * Time.deltaTime * 100;
        my += v * Time.deltaTime * 100;

        Debug.Log("mx: " + mx + " /my: " + my);
        Debug.Log("tempx: " + tempx + " /tempy: " + tempy);

        transform.eulerAngles += new Vector3(-tempy, tempx, 0);
        //transform.eulerAngles = new Vector3(-my, mx, 0);
    }
}
