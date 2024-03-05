using UnityEngine;

namespace Effects
{
    public class fadeEffects
    {

        public fadeEffects() { }

        #region fade transition effect

        public bool EndTransitionFadeUnit(Color colToVerify)
        {
            if (colToVerify.a < 0) return true;
            if (colToVerify.a > 1) return true;
            return false;
        }

        public bool EndTransitionFadeGroup(GameObject parentOfCloud)
        {
            bool result = true;
            if (parentOfCloud.transform.childCount != 0)
            {
                for (int i = 0; i < parentOfCloud.transform.childCount; i++)
                {
                    result = result && EndTransitionFadeUnit(parentOfCloud.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color);
                }
            }
            return result;
        }


        public void FadeOutAllObject(GameObject cloudsParent, float fadeSpeed){
            for (int i = 0; i<cloudsParent.transform.childCount; i++)
            {

                // cannot use CP_Col = Fadeout(Cp_Col)
                // dunno why
                Color CP_Col = cloudsParent.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color;
                if (!EndTransitionFadeUnit(CP_Col))
                {
                    if (fadeSpeed == 0) {
                        cloudsParent.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = FadeOut(CP_Col);
                    }
                    else {
                        cloudsParent.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().color = FadeOut(CP_Col, fadeSpeed);
                    }

                }
            }
        }

        public Color FadeOut(Color objectToFade)
        {
            Color OTF_Color = objectToFade;
            OTF_Color.a -= Random.Range(0.001f,Time.deltaTime + 0.05f);

            return OTF_Color;
        }

        public Color FadeOut(Color objectToFade, float fadeSpeed)
        {
            Color OTF_Color = objectToFade;
            OTF_Color.a -= Time.deltaTime * fadeSpeed;

            return OTF_Color;
        }

        public Color FadeIn(Color objectToFade)
        {
            Color OTF_Color = objectToFade;
            OTF_Color.a += Random.Range(0.001f, Time.deltaTime + 0.05f);

            return OTF_Color;
        }

        public Color FadeIn(Color objectToFade, float fadeSpeed)
        {
            Color OTF_Color = objectToFade;
            OTF_Color.a += Time.deltaTime * fadeSpeed;

            return OTF_Color;
        }
        #endregion

        #region cloud Disperse effect
        // if cloud in parent range, then transition isn't finished
        public bool EndTransitionUnit(GameObject cloudUnit)
        {
            if (cloudUnit.GetComponentInParent<Collider2D>().bounds.Contains(cloudUnit.transform.position))
            {
                return false;
            }
            return true;
        }

        // if all clouds in parent range, then transition isn't finished (true if finished)
        public bool EndTransitionDisperse(GameObject parentOfCloud)
        {
            bool result = true;
            if (parentOfCloud.transform.childCount != 0)
            {
                for (int i = 0; i < parentOfCloud.transform.childCount; i++)
                {
                    result = result && EndTransitionUnit(parentOfCloud.transform.GetChild(i).gameObject);
                }
            }
            return result;
        }

        public void Disperse(GameObject parentOfCloud)
        {
            float speedDisperse = 0;
            GameObject cloudUnit = null;
            for (int i = 0; i < parentOfCloud.transform.childCount; i++)
            {
                cloudUnit = parentOfCloud.transform.GetChild(i).gameObject;

                if (!EndTransitionUnit(cloudUnit))
                {
                    speedDisperse = Random.Range(0.0001f, Time.deltaTime + 0.05f);

                    if ((cloudUnit.transform.position - parentOfCloud.transform.localPosition).x > 0)
                    {
                        cloudUnit.transform.Translate(Vector2.right * speedDisperse);
                    }

                    else { cloudUnit.transform.Translate(Vector2.left * speedDisperse); }
                }
            }
        }

        public void Disperse(GameObject parentOfCloud,float constantSpeed)
        {
            for (int i = 0; i < parentOfCloud.transform.childCount; i++)
            {
                GameObject cloudUnit = parentOfCloud.transform.GetChild(i).gameObject;
                if (!EndTransitionUnit(cloudUnit))
                {
                    if ((cloudUnit.transform.position - parentOfCloud.transform.localPosition).x > 0)
                    {
                        cloudUnit.transform.Translate(Vector2.right * constantSpeed);
                    }

                    else { cloudUnit.transform.Translate(Vector2.left * constantSpeed); }
                }
            }
        }
        #endregion
    }
}
