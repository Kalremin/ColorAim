using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObstacleControl : MonoBehaviour
{
    float flowTimer;
    bool _turn = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        flowTimer += Time.deltaTime;
        if (flowTimer >= 10)
        {
            flowTimer = 0;
            _turn = !_turn;

        }

        if (_turn)
            transform.position += Vector3.forward * 0.1f;
        else
            transform.position -= Vector3.forward * 0.1f;
    }
}
