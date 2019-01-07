using UnityEngine;
using System.Collections.Generic;

public class GrassParent : MonoBehaviour {

    List<Transform> children = new List<Transform>();

	void Start () {
        foreach (Transform child in transform)
            children.Add(child);
        Invoke("CheckChildren", 30);
    }
	
	void CheckChildren()
    {
        foreach (Transform child in children)
            child.gameObject.SetActive(true);
        Invoke("CheckChildren", 30);
    }
}
