using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [SerializeField] private float _maxtime =1.5f;
    [SerializeField] private float _heightRange =0.54f;
    [SerializeField] private GameObject _pipe;
    private float _timer;
    void Start()
    {
        SpawnPipe();

    }
    void Update()
    {
        if(_timer >= _maxtime)
        {
            SpawnPipe();
            _timer = 0;
        }
        _timer += Time.deltaTime;   
    }
    private void SpawnPipe()
    {
        Vector3 spawnPos = transform.position + Vector3.up * Random.Range(-_heightRange, _heightRange);
        GameObject pipe = Instantiate(_pipe, spawnPos, Quaternion.identity);    
        Destroy(pipe, 10f);
    }


}
