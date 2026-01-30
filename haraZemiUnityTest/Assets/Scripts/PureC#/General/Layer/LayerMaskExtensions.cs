using UnityEngine;

//Layer関係の汎用メソッド集
public static class LayerMaskExtensions
{

    /// <summary>
    /// 特定のオブジェクトのレイヤーが含まれているかを返す
    /// layerを直接入れる方式とGameObjectを入れる方式で好きな方を使用してください
    /// </summary>
    public static bool Contains(this LayerMask mask, int layer)
    {
        return (mask.value & (1 << layer)) != 0;
    }

    public static bool Contains(this LayerMask mask, GameObject obj)
    {
        return (mask.value & (1 << obj.layer)) != 0;
    }
}
