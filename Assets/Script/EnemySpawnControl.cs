using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnControl : MonoBehaviour
{

    static EnemySpawnControl _unique;
    public static EnemySpawnControl _instance { get { return _unique; } }

    List<Vector3> _posSpawn = new List<Vector3>();

    private void Awake()
    {
        _unique = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform child in transform)
        {
            _posSpawn.Add(child.position);
        }
    }

    public void Spawn(GameObject entity)
    {
        Instantiate(entity, _posSpawn[Random.Range(0, _posSpawn.Count)], entity.transform.rotation);
    }
}
