using System;
using UnityEngine;

public class ColliderDraw : MonoBehaviour
{
    public event EventHandler<EventArgsCollider> ColliderGoodPlace;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ColliderGoodPlace?.Invoke(this, new EventArgsCollider(collision.gameObject));
    }


}

public class EventArgsCollider : EventArgs
{
    public GameObject GO_ColliderLine;
    public EventArgsCollider(GameObject go)
    {
        this.GO_ColliderLine = go;
    }
}
