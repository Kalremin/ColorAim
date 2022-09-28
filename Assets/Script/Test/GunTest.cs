using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunTest : MonoBehaviour
{
    public GameObject _posFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {


            Debug.Log(
            Camera.main.ScreenToViewportPoint(Input.mousePosition));
            Vector3 temp = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            Ray r1 = Camera.main.ViewportPointToRay(temp);
            r1.origin = _posFire.transform.position;
            Debug.Log(r1.direction);
            Debug.DrawRay(r1.origin, r1.direction, Color.black, 5f);
            

        }
    }
}
