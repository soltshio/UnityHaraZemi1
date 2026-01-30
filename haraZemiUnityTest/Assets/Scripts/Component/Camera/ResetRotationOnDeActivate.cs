using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

//非アクティブ化した時に向きをリセットする

public class ResetRotationOnDeActivate : MonoBehaviour
{
    [SerializeField]
    Transform _origin;

    [SerializeField]
    CinemachineCamera _mine;

    Quaternion _originRot;

    void Awake()
    {
        _originRot = _origin.rotation;
    }

    private void Update()
    {
        if (_mine.gameObject.activeSelf) return;

        ResetRot();
    }

    public void ResetRot()
    {
        _mine.transform.rotation = _originRot;
    }
}
