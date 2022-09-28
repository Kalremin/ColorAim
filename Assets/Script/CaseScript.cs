using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaseScript : MonoBehaviour
{
    Rigidbody _rigidbody;
    Collider _collider;

    float flow_timer;
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        flow_timer = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody.AddForce(transform.right * 100 + transform.up * 100);
        
    }

    // Update is called once per frame
    void Update()
    {
        flow_timer += Time.deltaTime;

        if (flow_timer >= 5)
            Destroy(gameObject);
    }
}
