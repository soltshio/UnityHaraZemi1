using System.Collections;
using UnityEngine;

//–³“GŠÔ

public class InvincibleTime : MonoBehaviour
{
    [Tooltip("–³“GŠÔ")]
    [SerializeField] float _invincibleDuration = 2f;

    bool _isInvincible = false;
    Coroutine _coroutine;

    public bool IsInvincible => _isInvincible;

    // ŠO•”‚©‚çŒÄ‚ÔƒgƒŠƒK[
    public void StartInvincible()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(InvincibleRoutine());
    }

    IEnumerator InvincibleRoutine()
    {
        _isInvincible = true;

        yield return new WaitForSeconds(_invincibleDuration);

        _isInvincible = false;
        _coroutine = null;
    }
}






