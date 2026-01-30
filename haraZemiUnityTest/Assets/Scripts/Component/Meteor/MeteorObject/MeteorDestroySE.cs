using System.Collections;
using UnityEngine;

//Ë¶êŒÇ™îjâÛÇ≥ÇÍÇΩâπÇñ¬ÇÁÇ∑

public class MeteorDestroySE : MonoBehaviour
{
    [SerializeField]
    AudioSource _audioSource;

    [SerializeField]
    AudioClip _destroySE;

    [SerializeField]
    float _coolTimeDuration;

    bool _isCoolTime = false;

    public void PlayOneShot()
    {
        if (_isCoolTime) return;

        _audioSource.PlayOneShot(_destroySE);
        StartCoroutine(CoolTimeCoroutine());
    }

    IEnumerator CoolTimeCoroutine()
    {
        _isCoolTime=true;

        yield return new WaitForSeconds(_coolTimeDuration);

        _isCoolTime=false;
    }
}
