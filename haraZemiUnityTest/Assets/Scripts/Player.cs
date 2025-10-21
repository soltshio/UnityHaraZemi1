using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    float moveValue;
    float jumpValue;

    public int[] sw=new int[3] { 1,1,1 };
    int[] swPre = new int[3];
    public int accX=0;

    private void Start()
    {
        moveValue = 0.01f;
        jumpValue = 5f;

        debugText.text = "debug";
    }

    private void Update()
    {
        var current = Keyboard.current;

        if (current == null)
            return;

        Vector3 curentPos = this.transform.position;
        float zPos = (float)accX / 4000;
        curentPos.z = zPos;
        this.transform.position = curentPos;


        if (current.rightArrowKey.isPressed || sw[2]==0)
        {
            this.transform.position += new Vector3(moveValue, 0f, 0f);
        }

        if (current.leftArrowKey.isPressed || sw[0]==0)
        {
            this.transform.position -= new Vector3(moveValue, 0f, 0f);
        }

        if (current.zKey.wasPressedThisFrame || (sw[1] == 0 && swPre[2]==1))
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.up * jumpValue;
        }

        string str = string.Format("acc:{0} sw:{1}{2}{3}", accX, sw[0], sw[1], sw[2]);
        debugText.text = str;

        for(int i=0; i<swPre.Length ;i++)
        {
            swPre[i] = sw[i];
        }
    }

    
}
