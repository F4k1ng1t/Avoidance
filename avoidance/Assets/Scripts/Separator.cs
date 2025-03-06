using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

public class Separator : Kinematic
{
    Separation myMoveType;
    Face myRotateType;
    GameObject[] separators;

    public Transform separatorGroup;
    // Start is called before the first frame update
    void Start()
    {
        myMoveType = new Separation();
        myMoveType.character = this;

        Transform[] childTransforms = separatorGroup.GetComponentsInChildren<Transform>();

        // Use a temporary list to collect valid separators
        List<Kinematic> separatorList = new List<Kinematic>();

        foreach (Transform child in childTransforms)
        {
            Separator separator = child.gameObject.GetComponent<Separator>();
            if (separator != null && separator != this) // Ensure it exists and isn't self-referencing
            {
                //Debug.Log("Found Separator: " + separator.gameObject.name);
                separatorList.Add(separator); // Add to the list
            }
        }
        // Convert the list to an array before assigning it
        myMoveType.targets = separatorList.ToArray();
        
        //Debugging Stuff
        
        //Debug.Log($"{myMoveType.targets}");
        //foreach (Kinematic target in myMoveType.targets)
        //{
        //    Debug.Log(target.gameObject.name);
        //}

        myRotateType = new Face();
        myRotateType.character = this;
        myRotateType.target = myTarget;
    }
    protected override void Update()
    {
        steeringUpdate = new SteeringOutput();
        steeringUpdate.linear = myMoveType.getSteering().linear;
        steeringUpdate.angular = myRotateType.getSteering().angular;
        base.Update();
    }
}

