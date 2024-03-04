using UnityEngine;

namespace fadeMethods
{

    public class FadeMethods
    {
        private Color Color;
        private bool isFading;
        private float fadeTime;
        private float fadeSpeed;

        public FadeMethods() { }

        public Color Fade(Color co, bool fadeway, float timeChronometer)
        {
            if (!fadeway)
            {
                co.a = timeChronometer;
            }
            else
            {
                co.a = 1 - timeChronometer;
            }

            return co;
        }
        public float ChronoUp(float time, float delayTime)
        {
            return time + Time.deltaTime * delayTime;
        }

    }
}
