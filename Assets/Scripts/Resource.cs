using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour {

	public int value { get; private set; }
    public bool pickedUp { get; private set; }

    public void Init(int value_, Vector3 pos)
    {
        value = value_;
        transform.position = pos;
    }

    public void GetPickedUp()
    {
        pickedUp = true;
        Services.ResourceManager.DestroyResource(this);
    }
}
