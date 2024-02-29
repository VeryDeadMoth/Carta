using UnityEngine;

public class ColliderDraw : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        GetComponent<drawManager>().numberCollider++;
    }

}
