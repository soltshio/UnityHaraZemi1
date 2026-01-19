using UnityEngine;

public class CursorInitPos : MonoBehaviour
{
    [SerializeField]
    Vector2Int pos;
    
    void Start()
    {
        MouseCursorHandler.Move(pos.x, pos.y);
    }
}
