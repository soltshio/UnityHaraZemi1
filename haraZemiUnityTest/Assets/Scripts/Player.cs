using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public TextMeshProUGUI debugText;
    float moveValue;
    float jumpValue;

    public int accX=0;

    public int[] swPre;
    public int[] sw;

    public bool[] jklPress;
    public bool[] jklToggle;

    public float startTime;

    void Awake()
    {
        swPre = new int[5];

        sw = new int[5];

        for(int i=0; i<sw.Length ;i++)
        {
            sw[i] = 1;
            swPre[i] = 1;
        }


        jklPress = new bool[3];
        jklToggle = new bool[3];

        for(int i=0; i<jklPress.Length ;i++)
        {
            jklPress[i] = false;
            jklToggle[i] = false;
        }
    }

    private void Start()
    {
        moveValue = 0.01f;
        jumpValue = 5f;

        debugText.text = "debug";

        startTime = -1;
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




        if (current.jKey.wasPressedThisFrame)
        {
            jklPress[0] = true;
            jklToggle[0] = !jklToggle[0];
        }
        if (current.kKey.wasPressedThisFrame)
        {
            jklPress[1] = true;
            jklToggle[1] = true;
        }
        if (current.kKey.wasReleasedThisFrame)
        {
            jklPress[1] = true;
            jklToggle[1] = false;
        }
        if (current.lKey.wasPressedThisFrame)
        {
            jklPress[2] = true;
            jklToggle[2] = true;
            startTime = 0;
        }

        if (startTime >= 0f)
        {
            startTime += Time.deltaTime;
            if (startTime > 3f)
            {
                jklPress[2] = true;
                jklToggle[2] = false;
                startTime = -1;
            }
        }



        string str = string.Format("acc:{0} sw:{1}{2}{3}", accX, sw[0], sw[1], sw[2]);
        debugText.text = str;




        for(int i=0; i<swPre.Length ;i++)
        {
            swPre[i] = sw[i];
        }
    }

    
}
