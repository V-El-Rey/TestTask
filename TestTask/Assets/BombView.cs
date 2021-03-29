using Interface;
using UnityEngine;

public class BombView : MonoBehaviour, IView
{
    public Transform objectTransform
    {
        get => gameObject.transform;
        set
        {
            
        }
    }
}
