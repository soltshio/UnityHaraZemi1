using UnityEngine;

public class MathfExtension
{
    // 円内の均等ランダム点生成（Z=0 の平面）
    public static Vector3 RandomPointInCircle(float radius)
    {
        Vector2 p = Random.insideUnitCircle * radius;
        return new Vector3(p.x, p.y, 0);
    }

    // 円錐内部の方向を均等ランダムに生成
    public static Vector3 RandomDirectionInCone(Vector3 forward, float angle)
    {
        float rad = angle * Mathf.Deg2Rad;

        float cosMax = Mathf.Cos(rad);
        float cosTheta = Random.Range(cosMax, 1f);
        float sinTheta = Mathf.Sqrt(1f - cosTheta * cosTheta);

        float phi = Random.Range(0f, Mathf.PI * 2);

        Vector3 localDir = new Vector3(
            sinTheta * Mathf.Cos(phi),
            sinTheta * Mathf.Sin(phi),
            cosTheta
        );

        return Quaternion.LookRotation(forward) * localDir;
    }

    /// <summary>
    /// maxよりもminの方が大きければ、自動的に入れ替える(int型)
    /// </summary>
    public static void NormalizeRange(ref int min, ref int max)
    {
        if (min > max)
        {
            (min, max) = (max, min);
        }
    }

    /// <summary>
    /// maxよりもminの方が大きければ、自動的に入れ替える(float型)
    /// </summary>
    public static void NormalizeRange(ref float min, ref float max)
    {
        if (min > max)
        {
            (min, max) = (max, min);
        }
    }


    /// <summary>
    /// 値(int型)が範囲内か(min以上、max以下)を返す
    /// </summary>
    public static bool IsInRange(int value, int min, int max)
    {
        NormalizeRange(ref min, ref max);
        return value >= min && value <= max;
    }

    /// <summary>
    /// 値(float型)が範囲内か(min以上、max以下)を返す
    /// </summary>
    public static bool IsInRange(float value, float min, float max)
    {
        NormalizeRange(ref min, ref max);
        return value >= min && value <= max;
    }
}
