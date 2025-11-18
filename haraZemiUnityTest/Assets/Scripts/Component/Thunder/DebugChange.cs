using UnityEngine;
using UnityEngine.InputSystem;

public class DebugChange : MonoBehaviour
{

    [SerializeField]
    ChangeThunderConvergence thunderConvergence;

    bool inc=false;
    bool dec=false;

    public void Inc(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            inc = true;
        }
        else if(context.canceled)
        {
            inc=false;
        }
    }

    public void Dec(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            dec = true;
        }
        else if (context.canceled)
        {
            dec = false;
        }
    }

    private void Update()
    {
        if(inc)
        {
            thunderConvergence.ConvergenceRate += 0.5f * Time.deltaTime;
        }

        if(dec)
        {
            thunderConvergence.ConvergenceRate -= 0.5f * Time.deltaTime;
        }
    }
}
