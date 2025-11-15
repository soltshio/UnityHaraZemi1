using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateThunder : MonoBehaviour
{
    [SerializeField]
    GameObject _thunderEffect;

    [SerializeField]
    HoldDownInput _holdDownInput;

    private void Awake()
    {
        _holdDownInput.OnStartHold += Activate;
        _holdDownInput.OnEndHold += DeActivate;
    }

    void Activate()
    {
        _thunderEffect.SetActive(true);
    }

    void DeActivate()
    {
        _thunderEffect.SetActive(false);
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
