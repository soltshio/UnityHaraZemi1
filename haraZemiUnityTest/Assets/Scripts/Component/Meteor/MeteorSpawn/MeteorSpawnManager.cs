using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

//隕石をスポーンさせる機能

public class MeteorSpawnManager : MonoBehaviour
{
    [Tooltip("隕石のスポーン場所を決める機能")] [SerializeField]
    RandomPositionsInBoxes _getSpawnPosition;

    [SerializeField]
    float _interval;

    float _current=0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //最初に一気に生成しておく
    }

    // Update is called once per frame
    void Update()
    {
        _current += Time.deltaTime;

        if (_current < _interval) return;

        _current = 0;
        //生成

    }
}
