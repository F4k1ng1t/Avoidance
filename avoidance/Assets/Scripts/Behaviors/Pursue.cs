using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursue : Seek
{
    // The maximum prediction time
    float maxPredictionTime = 5f;

    // Toggle between pursuing and evading
    public bool evade;

    // Overrides the position seek will aim for
    protected override Vector3 getTargetPosition()
    {
        // 1. Calculate direction to the target
        Vector3 directionToTarget;
        if (evade)
        {
            directionToTarget = character.transform.position - target.transform.position; // Opposite direction for evasion
        }
        else
        {
            directionToTarget = target.transform.position - character.transform.position; // Normal direction for pursuit
        }

        float distanceToTarget = directionToTarget.magnitude;
        float mySpeed = character.linearVelocity.magnitude;

        // 2. Determine appropriate prediction time
        float predictionTime;
        if (mySpeed <= distanceToTarget / maxPredictionTime)
        {
            predictionTime = maxPredictionTime;
        }
        else
        {
            predictionTime = distanceToTarget / mySpeed;
        }

        // 3. Get the target's velocity and predict its future position
        Kinematic myMovingTarget = target.GetComponent<Kinematic>();
        if (myMovingTarget == null)
        {
            return base.getTargetPosition(); // Default seek behavior if the target is not kinematic
        }

        // 4. Calculate predicted position based on pursuit or evasion
        Vector3 predictedPosition = target.transform.position + myMovingTarget.linearVelocity * predictionTime;

        // 5. Adjust for evasion
        if (evade)
        {
            // Move away from the predicted position
            Vector3 evadeDirection = (character.transform.position - predictedPosition).normalized;
            float evadeDistance = distanceToTarget; // Move based on distance
            predictedPosition = character.transform.position + evadeDirection * evadeDistance;
        }

        return predictedPosition;
    }
}
