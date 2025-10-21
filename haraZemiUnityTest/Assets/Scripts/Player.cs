using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    float moveValue;

    public int[] sw=new int[3] { 1,1,1 };
    public int accX=0;

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

        string str = string.Format("acc:{0} sw:{1}{2}{3}", accX, sw[0], sw[1], sw[2]);
        debugText.text = str;
    }

    
}
