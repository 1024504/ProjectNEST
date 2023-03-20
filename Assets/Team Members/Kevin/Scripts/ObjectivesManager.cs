using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectivesManager : MonoBehaviour
{
    public GameObject objectivesMarker;
    public Transform proceduralPanel;
    public void Start()
    {
        /*Image go = Instantiate(objectivesMarker, proceduralPanel.position, Quaternion.identity);
        go.transform.parent = proceduralPanel.transform;*/
    }
}

