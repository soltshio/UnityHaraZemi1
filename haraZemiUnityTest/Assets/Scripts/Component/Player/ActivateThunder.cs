using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateThunder : MonoBehaviour
{
    [SerializeField]
    GameObject _thunderEffect;

    public void Activate(InputAction.CallbackContext context)
    {
        if(context.performed)//•\Ž¦‰»
        {
            _thunderEffect.SetActive(true);
        }
        else if(context.canceled)//”ñ•\Ž¦‰»
        {
            _thunderEffect.SetActive(false);
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _thunderEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
