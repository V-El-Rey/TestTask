using UnityEngine;

public class CountdownTimer
{
    
    public static bool CountdownEnded(float time)
    {
        var timeToWait = time;
        timeToWait -= Time.deltaTime;
        if (timeToWait <= 0.0f)
        {
            return true;
        }
        return false;
    }
}
