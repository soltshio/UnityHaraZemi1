using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    float moveValue;

    private void Start()
    {
        moveValue = 0.01f;

        debugText.text = "debug";
    }

    private void Update()
    {
        var current = Keyboard.current;

        if (current == null)
            return;

        if (current.rightArrowKey.isPressed)
        {
            this.transform.position += new Vector3(moveValue, 0f, 0f);
        }

        if (current.leftArrowKey.isPressed)
        {
            this.transform.position -= new Vector3(moveValue, 0f, 0f);
        }
            
    }

    
}
