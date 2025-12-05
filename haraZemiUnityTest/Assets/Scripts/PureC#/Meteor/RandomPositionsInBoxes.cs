using UnityEngine;

//•¡”‚Ì” Œ^‚Ì—Ìˆæ‚Ì’†‚©‚çƒ‰ƒ“ƒ_ƒ€‚ÈˆÊ’u‚ğ•Ô‚·

[System.Serializable]
public class RandomPositionsInBoxes
{
    [SerializeField]
    Transform[] _spawnZones;

    public Vector3 GetRandomPos()
    {
        Transform spawnZone = _spawnZones[Random.Range(0,_spawnZones.Length)];

        Vector3 localPos = MathfExtension.RandomPositionInNormalzeBox();

        return spawnZone.TransformPoint(localPos);
    }
}
