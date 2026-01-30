using UnityEngine;
using UnityEngine.EventSystems;

public class DeselectOnDecide :
    MonoBehaviour,
    IPointerClickHandler,
    ISubmitHandler
{
    // マウスクリック時
    public void OnPointerClick(PointerEventData eventData)
    {
        Deselect();
    }

    // キーボード / ゲームパッド決定時
    public void OnSubmit(BaseEventData eventData)
    {
        Deselect();
    }

    void Deselect()
    {
        if (EventSystem.current != null)
        {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
