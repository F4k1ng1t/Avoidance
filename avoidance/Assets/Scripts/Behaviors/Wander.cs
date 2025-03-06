using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : Seek
{
    float delay = 1.0f;
    float elapsedTime = 4.0f;
    Vector3 result;

    protected override Vector3 getTargetPosition()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime > delay)
        {
            int offset = 100;
            Vector3 circlePos = Random.insideUnitCircle * offset;
            Vector3 adjustedCirclePos = new Vector3(circlePos.x, 0, circlePos.y);
            result = character.transform.position + (character.transform.forward * 10) + adjustedCirclePos;
            elapsedTime = 0f;
        }
        return result;
    }
}
