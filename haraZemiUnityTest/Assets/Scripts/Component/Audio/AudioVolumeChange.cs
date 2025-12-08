using UnityEngine;

public class AudioVolumeChange : MonoBehaviour
{
    [SerializeField]
    AudioSource _audioSource;

    [SerializeField] [Range(0f, 1f)]
    float _maxVolume;

    [SerializeField] [Range(0f, 1f)]
    float _minVolume;

    public void SwitchPlay(bool isPlay)
    {
        if(isPlay)
        {
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
    }

    public void ChangeVolume(float rate)//0Å`1ÇÃîÕàÕì‡Ç≈éwíË
    {
        rate = Mathf.Clamp01(rate);

        if (_audioSource == null) return;

        float volume = Mathf.Lerp(_minVolume,_maxVolume,rate);

        _audioSource.volume = volume;
    }
}
