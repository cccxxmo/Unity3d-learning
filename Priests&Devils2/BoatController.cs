using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController {
    readonly GameObject boat;
    public readonly float speed = 10;
    readonly Vector3 right = new Vector3(3.5f, 0, 0);
    readonly Vector3 left = new Vector3(-3.5f, 0, 0);
    readonly Vector3[] rightPositions;
    readonly Vector3[] leftPositions;

    int status;// left(0)/right(1)
    ICharacterController[] characterOnBoat = new ICharacterController[2];

    public BoatController()
    {
        status = 1;
        leftPositions = new Vector3[] { new Vector3(-4.5F, 0.2F, 0), new Vector3(-2.5F, 0.2F, 0) };
        rightPositions = new Vector3[] { new Vector3(2.5F, 0.2F, 0), new Vector3(4.5F, 0.2F, 0) };
        boat = Object.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject)), right, Quaternion.identity, null) as GameObject;
        boat.name = "boat";
        boat.AddComponent(typeof(ClickGUI));
    }

	public Vector3 getTarget()
	{
		if (status == 1)
		{
			return left;
		}
		else
		{
			return right;
		}
	}

	public void toggle() 
	{
		status = 1 - status;
	}


    public int getEmptyIndex()
    {
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] == null)
            {
                return i;
            }
        }
        return -1;
    }

    public Vector3 getEmptyPosition()
    {
        int index = getEmptyIndex();
        if (status == 0)
            return leftPositions[index];
        else
            return rightPositions[index];
    }

    public bool isEmpty()
    {
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    public void getOnBoat(ICharacterController character)
    {
        characterOnBoat[getEmptyIndex()] = character;
    }

    public ICharacterController getOffBoat(string name)
    {
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] != null && characterOnBoat[i].getName() == name)
            {
                ICharacterController character = characterOnBoat[i];
                characterOnBoat[i] = null;
                return character;
            }
        }
        return null;
    }

    public GameObject getBoat()
    {
        return boat;
    }

    public int getBoatPos()
    {
        return status;
    }

    public int[] getBoatStatus()
    {
        int[] boatStatus = { 0, 0 };
        for (int i = 0; i < characterOnBoat.Length; i++)
        {
            if (characterOnBoat[i] == null)
                continue;
            boatStatus[characterOnBoat[i].getType()]++;
        }
        return boatStatus;
    }// 0: Priests, 1: Demon

	public bool available () 
	{
		return ((status == 0) ? left : right) == boat.transform.position;
	}


    public void reset()
    {
        if (status == 0)
        {
            toggle();
        }
		boat.transform.position = right;
        characterOnBoat = new ICharacterController[2];
    }
}
