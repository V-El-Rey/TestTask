using System.Collections;
using System.Collections.Generic;
using Interface;
using UnityEngine;

public class InfantryView : MonoBehaviour, IView
{
    public Transform objectTransform
    {
        get => gameObject.transform;
        set
        {
        }
    }

}
