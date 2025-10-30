using UnityEngine;

public class LineBetween_HeadAndSolderingIron : MonoBehaviour
{
    [SerializeField]
    LineRenderer _lineRenderer;

    [SerializeField]
    Rigidbody _lastPoint;

    [Tooltip("ヒモの質点\n最初と最後はヒモの端の質点を入れる")] [SerializeField]
    ConfigurableJoint[] _massPoints;

    void Start()
    {
        SetConnectBody();
        DeactiveMeshRenderer();
    }

    void Update()
    {
        RenderLine();
    }

    void SetConnectBody()//HingeJointに繋げるオブジェクトの設定
    {
        for (int i = 0; i < _massPoints.Length - 1; i++)
        {
            Rigidbody connectBody = _massPoints[i + 1].GetComponent<Rigidbody>();

            _massPoints[i].connectedBody = connectBody;
        }

        _massPoints[_massPoints.Length-1].connectedBody = _lastPoint;
    }

    void DeactiveMeshRenderer()//メッシュ(球)を非表示
    {
        for (int i = 0; i < _massPoints.Length; i++)
        {
            MeshRenderer mesh = _massPoints[i].GetComponent<MeshRenderer>();

            mesh.enabled = false;
        }

        MeshRenderer lastMesh = _lastPoint.GetComponent<MeshRenderer>();

        lastMesh.enabled = false;
    }

    void RenderLine()//線の描画
    {
        _lineRenderer.positionCount = _massPoints.Length+1;

        for (int i = 0; i < _massPoints.Length; i++)
        {
            _lineRenderer.SetPosition(i, _massPoints[i].transform.position);
        }

        _lineRenderer.SetPosition(_massPoints.Length, _lastPoint.transform.position);
    }
}
