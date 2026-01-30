using UnityEngine;

public class SwitchCurcorInvisible_UseMicon : MonoBehaviour
{
    private void OnEnable()
    {
        var serialHandlerInstance = SerialHandler.Instance;

        if (serialHandlerInstance == null) return;

        Cursor.visible = serialHandlerInstance.DontUseMicon;
    }

    private void OnDisable()
    {
        Cursor.visible = true;
    }
}
