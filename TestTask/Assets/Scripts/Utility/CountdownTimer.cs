using UnityEngine;

namespace Utility
{
    public class CountdownTimer
    {
        public float _timeToWait;

        public CountdownTimer(float timeToWait)
        {
            _timeToWait = timeToWait;
        }

        public void Reset(float time)
        {
            _timeToWait = time;
        }

        public bool Countdown(float time)
        {
            time -= Time.deltaTime;
            return time == 0.0f;
        }

        public void Update()
        {
            Countdown(_timeToWait);
        }
    }
}
