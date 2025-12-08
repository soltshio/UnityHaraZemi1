using System.Collections.Generic;
using UnityEngine;

public class ExplosionPool : MonoBehaviour
{
    [SerializeField]
    GameObject _explosionPrefab;

    [SerializeField]
    int _maxSize = 20;

    Queue<GameObject> _explosionQueue=new();

    public void Spawn(Vector3 pos)
    {
        if(_explosionQueue.Count<=_maxSize)//キューに空きがあったら生成
        {
            GameObject instance = Instantiate(_explosionPrefab, pos, Quaternion.identity);
            _explosionQueue.Enqueue(instance);
        }
        else//無かったら、一番古いのを取り出して使う
        {

        }
    }
}
