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

    bool _preEnabled = false;

    void Awake()
    {
        _originRot = _origin.rotation;

    }

    public void ResetRot()
    {
        _mine.transform.rotation = _originRot;
        Debug.Log("呼ばれてる？");
    }

    private void Update()
    {
        bool enabled = _mine.enabled;

        if(enabled==false && _preEnabled==true)
        {
            ResetRot();
        }

        _preEnabled = enabled;
    }
}
