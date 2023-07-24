using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StyleEventController : MonoBehaviour
{
    public int slot;
    public TextMeshProUGUI textObj;
    private Vector3 eventOffset = new Vector3(618, 353, 0);

    void Update()
    {
        Vector3 targetPos = eventOffset + new Vector3(0, -50 * (slot - 1), 0);
        transform.localPosition += (targetPos - transform.localPosition) / 70;
    }
}
