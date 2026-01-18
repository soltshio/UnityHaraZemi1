using UnityEngine;

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

        //押してる最中だったらボタンを無理やり離させる
        if (MouseCursorHandler.IsLeftButtonDown()) MouseCursorHandler.LeftUp();
    }

    void OnInputDown()
    {
        MouseCursorHandler.LeftDown();
    }

    void OnInputUp()
    {
        MouseCursorHandler.LeftUp();
    }
}
