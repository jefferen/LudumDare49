using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthMeter : MonoBehaviour
{
    public Transform Player;
    public float Offset;
    TMPro.TextMeshProUGUI DepthText;

    private void Start()
    {
        DepthText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update()
    {
        float depth = (Player.position.y/5) * -1 - Offset;
        DepthText.text = "-" + depth.ToString("F2");
    }
}
