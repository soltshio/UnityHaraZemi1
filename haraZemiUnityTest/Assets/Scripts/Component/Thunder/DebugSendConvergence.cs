using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class DebugSendConvergence : MonoBehaviour
{
    [SerializeField]
    ThunderConvergence _thunderConvergence;

    [SerializeField]
    SendManager _sendManager;

    bool _outputFlag=false;
    string _message;

    public bool HasMessage(out string message)
    {
        message = null;

        if (_outputFlag)
        {
            message = _message;
            _outputFlag = false;
            return true;
        }
        else
        {
            return false;
        }
    }

    private void Awake()
    {
        _thunderConvergence.OnChangedValue += SendConvergenceToMicon;
    }

    void SendConvergenceToMicon(float convergence)
    {
        // 0`1 ¨ 0`9 ‚Ì®”‚ÖilÌŒÜ“üj
        int num = Mathf.Clamp(Mathf.FloorToInt(convergence * 10f), 0, 9);

        //•¶š‚É•ÏŠ·
        string result = num.ToString();

        _outputFlag = true;
        _message = result;
    }
}
