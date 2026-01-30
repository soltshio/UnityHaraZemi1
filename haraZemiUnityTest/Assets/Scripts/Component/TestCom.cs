using UnityEngine;

public class TestCom : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Selected" + PassCOMPort.selectedCOMPortName);
    }
}
