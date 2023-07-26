using System.Collections.Generic;
using UnityEngine;

public class ChainLightning : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public float maxChainDistance = 5.0f;
    public int maxChainCount = 5;
    public LayerMask targetLayer;
    
    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cast();
        }
    }

    public void Cast()
    {
        List<Transform> hitTargets = new List<Transform>();
        Transform currentTarget = transform;

        for (int i = 0; i < maxChainCount; i++)
        {
            RaycastHit hit;
            if (Physics.Raycast(currentTarget.position, currentTarget.forward, out hit, maxChainDistance, targetLayer))
            {
                if (hitTargets.Contains(hit.transform))
                    break;

                hitTargets.Add(hit.transform);
                currentTarget = hit.transform;
            }
            else
            {
                break;
            }
        }

        DrawLightning(hitTargets);
    }

    void DrawLightning(List<Transform> targets)
    {
        lineRenderer.positionCount = targets.Count;
        for (int i = 0; i < targets.Count; i++)
        {
            lineRenderer.SetPosition(i, targets[i].position);
        }
    }
}