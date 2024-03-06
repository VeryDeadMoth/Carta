using UnityEngine;

namespace Effects
{
    public class fadeEffects
    {

        public fadeEffects() { }

        #region fade transition effect

        // ---------------------- //
        // BOOLEAN METHODS //
        // ---------------------- //


        // check if one object finished his transition
        public bool EndTransitionFadeUnit(Color colToVerify)
        {
            if (colToVerify.a < 0) return true;
            if (colToVerify.a > 1) return true;
            return false;
        }

        // check if all object of a parent finished their transition
        public bool EndTransitionFadeGroup(GameObject parentOfCloud)
        {
            bool result = true;
            if (parentOfCloud.transform.childCount != 0)
            {
                for (int i = 0; i < parentOfCloud.transform.childCount; i++)
                {
                    result = result && EndTransitionFadeUnit(parentOfCloud.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color);
                }
            }
            return result;
        }

        // ---------------------- //
        // BOOLEAN METHODS //
        // ---------------------- //






        // Fade out all object of the parent cloudsParent
        public void FadeOutAllObject(GameObject cloudsParent, float fadeSpeed){
            for (int i = 0; i<cloudsParent.transform.childCount; i++)
            {

                // cannot use CP_Col = Fadeout(Cp_Col)
                // dunno why
                Color CP_Col = cloudsParent.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color;
                if (!EndTransitionFadeUnit(CP_Col))
                {
                    cloudsParent.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = FadeOut(CP_Col, fadeSpeed);
                }
            }
        }

        // Fade out all object of the parent cloudsParent, with a random speed
        public void FadeOutAllObject(GameObject cloudsParent)
        {
            for (int i = 0; i < cloudsParent.transform.childCount; i++)
            {

                // cannot use CP_Col = Fadeout(Cp_Col)
                // dunno why
                Color CP_Col = cloudsParent.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color;
                if (!EndTransitionFadeUnit(CP_Col))
                {
                    cloudsParent.transform.GetChild(i).gameObject.GetComponent<Renderer>().material.color = FadeOut(CP_Col);
                }
            }
        }

        public Color FadeOut(Color objectToFade)
        {
            Color OTF_Color = objectToFade;
            OTF_Color.a -= Random.Range(0.01f,Time.deltaTime + 0.01f);
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

        // ---------------------- //
        // BOOLEAN METHODS //
        // ---------------------- //


        // check if transition of one object is finished
        // if cloud in parent range, then transition isn't finished
        public bool EndTransitionUnit(GameObject cloudUnit)
        {
            if (cloudUnit.GetComponentInParent<Collider2D>().bounds.Contains(cloudUnit.transform.position))
            {
                return false;
            }
            return true;
        }

        // check if transition of all object are finished
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

        // ---------------------- //
        // BOOLEAN METHODS //
        // ---------------------- //






        public void Disperse(GameObject parentOfCloud)
        {
            float speedDisperse = 0;
            GameObject cloudUnit = null;
            for (int i = 0; i < parentOfCloud.transform.childCount; i++)
            {
                cloudUnit = parentOfCloud.transform.GetChild(i).gameObject;

                if (!EndTransitionUnit(cloudUnit))
                {
                    speedDisperse = Random.Range(0.001f, Time.deltaTime + 0.5f);

                    if ((cloudUnit.transform.position - parentOfCloud.transform.localPosition).x > 0)
                    {
                        cloudUnit.transform.Translate(Vector2.right * speedDisperse);
                    }

                    else { cloudUnit.transform.Translate(Vector2.left * speedDisperse); }
                }
            }
        }

        public void Disperse(GameObject parentOfCloud,float speedDisperse)
        {
            for (int i = 0; i < parentOfCloud.transform.childCount; i++)
            {
                GameObject cloudUnit = parentOfCloud.transform.GetChild(i).gameObject;
                if (!EndTransitionUnit(cloudUnit))
                {
                    if ((cloudUnit.transform.position - parentOfCloud.transform.localPosition).x > 0)
                    {
                        cloudUnit.transform.Translate(Vector2.right * speedDisperse);
                    }

                    else { cloudUnit.transform.Translate(Vector2.left * speedDisperse); }
                }
            }
        }
        #endregion
    }
}
