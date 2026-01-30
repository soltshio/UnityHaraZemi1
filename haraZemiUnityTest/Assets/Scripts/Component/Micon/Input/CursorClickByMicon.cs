using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

//マイコン操作によるカーソルクリック

public class CursorClickByMicon : MonoBehaviour
{
    [SerializeField]
    ButtonInput _buttonInput;

    private void OnEnable()
    {
        _buttonInput.OnInputDown += OnInputDown;
        _buttonInput.OnInputUp += OnInputUp;
    }

    private void OnDisable()
    {
        _buttonInput.OnInputDown -= OnInputDown;
        _buttonInput.OnInputUp -= OnInputUp;

        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            Release();
        }
    }

    void OnInputDown()
    {
        Press();
    }

    void OnInputUp()
    {
        Release();
    }

    void Press()
    {
        if (Mouse.current == null) return;

        // ★ byte で送る
        InputSystem.QueueDeltaStateEvent(Mouse.current.leftButton, (byte)1);
        InputSystem.Update();
    }

    void Release()
    {
        if (Mouse.current == null) return;

        // ★ byte で送る
        InputSystem.QueueDeltaStateEvent(Mouse.current.leftButton, (byte)0);
        InputSystem.Update();
    }
}
