using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAvoider : Kinematic
{
    CollisionAvoidance myMoveType;

    public Kinematic[] myTargets = new Kinematic[4];
    public GameObject targetGroup;

    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new CollisionAvoidance();
        myMoveType.character = this;

        // Get all Kinematic components from the children of targetGroup
        List<Kinematic> targetsList = new List<Kinematic>();

        foreach (Transform child in targetGroup.transform)
        {
            Kinematic potentialTarget = child.GetComponent<Kinematic>();
            if (potentialTarget != null && potentialTarget != this)
            {
                targetsList.Add(potentialTarget);
            }
        }

        myTargets = targetsList.ToArray(); // Convert list to array
        myMoveType.targets = myTargets;
    }

    // Update is called once per frame
    protected override void Update()
    {
        steeringUpdate = myMoveType.getSteering();
        base.Update();
    }
}
