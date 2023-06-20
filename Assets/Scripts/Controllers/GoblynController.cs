using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GoblynController", menuName = "InputController/GoblynController")]
public class GoblynController : InputController
{
    public override bool RetrieveJumpInput()
    {
        return true;
    }

    public override float RetrieveMoveInput()
    {
        RaycastHit2D leftCast = Physics2D.CircleCast(transform.position, 2f, new Vector2(-1, 0), 6f, playerLayer);
        RaycastHit2D rightCast = Physics2D.CircleCast(transform.position, 2f, new Vector2(1, 0), 6f, playerLayer);

        if (leftCast != null)
        {
            return -1f;
        }
        else if (rightCast != null)
        {
            
        }
    }
}
