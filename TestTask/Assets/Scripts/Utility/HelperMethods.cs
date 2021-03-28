using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperMethods
{
    public static void GetRandomPosition(GameObject gameObject)
    {
        var randomPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-8.0f, 8.0f), 0);
        gameObject.transform.position = randomPosition;
    }
}
