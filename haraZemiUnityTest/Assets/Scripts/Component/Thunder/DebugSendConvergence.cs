using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DebugSendConvergence : MonoBehaviour
{
    [SerializeField]
    ThunderConvergence _thunderConvergence;

    [SerializeField]
    SendManager _sendManager;

    private void Awake()
    {
        _thunderConvergence.OnChangedValue += SendConvergenceToMicon;
    }

    void SendConvergenceToMicon(float convergence)
    {
        // 0`1 ¨ 0`1000 ‚Ì®”‚ÖilÌŒÜ“üj
        int num = Mathf.RoundToInt(convergence * 1000f);

        // 4 Œ…ƒ[ƒ–„‚ß‚Ì•¶š—ñ‚É•ÏŠ·
        string result = num.ToString("D4");

        _sendManager.Send(result);
    }
}
