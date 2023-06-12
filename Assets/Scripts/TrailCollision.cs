using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class TrailCollision : MonoBehaviour
{
    //Variables
    [Header("Settings")]
    [SerializeField] private int m_damage = 1;
    [SerializeField] private string m_otherTag = "Enemy";
    [SerializeField][Range(1, 10)] private int m_indexSkip = 2;
    [SerializeField] private int m_maxTrailCount = 200;

    [Header("References")]
    [SerializeField] private TrailRenderer m_trailRenderer;

    private Vector3[] m_trailPositions;

    //Methods
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        m_trailPositions = new Vector3[m_maxTrailCount];
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    private void OnDrawGizmos()
    {
        if (!m_trailRenderer) return;

        Vector3[] toUse = new Vector3[m_trailRenderer.positionCount];
        int count = m_trailRenderer.GetPositions(toUse);

        Gizmos.color = Color.red;

        int i = 1;                              //The index of the iteration
        int index = 1;                          //The index of the trail to use

        while (i < count)
        {
            index++;
            if (index % (m_indexSkip + 1) > 0) continue;
            if (index >= toUse.Length - 1) break;

            Vector3 start = toUse[index];

            int endIndex = Mathf.Clamp(index + m_indexSkip + 1, 0, toUse.Length - 1);
            Vector3 end = toUse[endIndex];

            Gizmos.DrawLine(start, end);
            i++;
        }
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if (!m_trailRenderer) return;

        Vector3[] toUse = new Vector3[m_trailRenderer.positionCount];
        int count = m_trailRenderer.GetPositions(toUse);

        int i = 1;                              //The index of the iteration
        int index = 1;                          //The index of the trail to use

        while (i < count)
        {
            index++;
            if (index % (m_indexSkip + 1) > 0) continue;
            if (index >= toUse.Length - 1) break;

            Vector3 start = toUse[index];

            int endIndex = Mathf.Clamp(index + m_indexSkip + 1, 0, toUse.Length - 1);
            Vector3 end = toUse[endIndex];

            if (Physics.Linecast(start, end, out RaycastHit hit))
            {
                if (hit.collider.CompareTag(m_otherTag))
                {
                    Debug.Log(hit.collider.gameObject.name);
                    Destroy(hit.collider.gameObject);
                    IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                    if (damageable != null)
                    {
                        
                        damageable.TakeDamage(m_damage);
                    }
                }
            }

            i++;
        }
    }
}

public interface IDamageable
{
    void TakeDamage(int damage);
}