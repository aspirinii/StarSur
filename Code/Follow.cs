using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        Vector3 pos = Camera.main.WorldToScreenPoint(GameManager.instance.player.transform.position);
        rect.position = pos;
    }
}
