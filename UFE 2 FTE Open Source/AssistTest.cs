using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UFE2FTE;

public class AssistTest : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.N))
        {
            UFE.GetPlayer1ControlsScript().assists[0].Physics.MoveX(UFE.GetPlayer1ControlsScript().assists[0].GetDirection(), UFE.GetPlayer1ControlsScript().assists[0].GetDirection());
        }

        if (Input.GetKey(KeyCode.M))
        {
            UFE2FTE.UFE2FTE.CastMoveByMoveName(
                 UFE.GetPlayer1ControlsScript().assists[0],
                 "Heavy Kick");
                 //UFE2FTE.GetMoveInfoByMoveNameFromMoveInfoCollection("Heavy Kick", UFE.GetPlayer1ControlsScript().assists[0].MoveSet.attackMoves));
            //UFE.GetPlayer1ControlsScript().assists[0].Physics.MoveX(1, 1);
        }
    }
}
