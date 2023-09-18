using UnityEngine;

public class RippleScript : MonoBehaviour
{
    [SerializeField] private LineRenderer m_lineRenderer = null;
    [SerializeField] private float m_radius = 1;
    [SerializeField] private float m_initialLineWidth = 0.05f; // �����̐��̑���
    [SerializeField] private float m_finalLineWidth = 0; // �ŏI�I�Ȑ��̑���

    [SerializeField] private float m_duration = 2;
    [SerializeField] private float m_from = 0;
    [SerializeField] private float m_to = 4;

    private float m_elapsedTime;

    private void Reset()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    private void Awake()
    {
        InitLineRenderer();
    }

    private void FixedUpdate()
    {
        m_elapsedTime += Time.deltaTime;

        var amount = m_elapsedTime / m_duration;
        var scale = Mathf.Lerp(m_from, m_to, amount);
        var lineWidth = Mathf.Lerp(m_initialLineWidth, m_finalLineWidth, amount); // ���̑��������o

        transform.localScale = new Vector3(scale, scale, 1);
        m_lineRenderer.startWidth = lineWidth;
        m_lineRenderer.endWidth = lineWidth;

        if (amount >= 1)
        {
            Destroy(gameObject);
        }
    }

    private void InitLineRenderer()
    {
        var segments = 360;

        m_lineRenderer.startWidth = m_initialLineWidth;
        m_lineRenderer.endWidth = m_initialLineWidth;
        m_lineRenderer.positionCount = segments;
        m_lineRenderer.loop = true;
        m_lineRenderer.useWorldSpace = false;

        var points = new Vector3[segments];

        for (int i = 0; i < segments; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            var x = Mathf.Sin(rad) * m_radius;
            var y = Mathf.Cos(rad) * m_radius;
            points[i] = new Vector3(x, y, 0);
        }

        m_lineRenderer.SetPositions(points);
    }
}
