using UnityEngine;
using UnityEngine.UI;

public class drawManager : MonoBehaviour
{
    [SerializeField] private GameObject listGoodPlacement;
    [SerializeField] private GameObject NewMap;
    [SerializeField] private float timeInterval = 1f;
    public GameManager GameManager;
    public bool isInPlace { get; set; } = false;
    private float timeChrono = 0f;
    public int numberCollider { get; set; }


    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseRelease();
        }

        if (isInPlace)
        {

            if (NewMap != null)
            {
                this.GetComponent<DrawObject>().enabled = false;

                Color newMapColor = NewMap.GetComponent<Image>().color;
                NewMap.GetComponent<Image>().color = Fade(newMapColor, false);

                Color newLineColor = this.GetComponent<MeshRenderer>().material.color;
                this.GetComponent<Renderer>().material.color = Fade(newMapColor, true);

                if (newMapColor.a >= 1)
                {
                    numberCollider = 0;
                    GameManager.LoadNewScene("Final_scene");
                    
                }
            }
            else
            {
                numberCollider = 0;
                GameManager.LoadNewScene("Final_scene");
                
            }
        }
        isInPlace = numberCollider == listGoodPlacement.transform.childCount;

        if (!isInPlace)
        {
            
            numberCollider = 0;
        }
    }

    public void OnMouseRelease()
    {
        MeshCollider Mesh_C = GetComponent<MeshCollider>();
        ColliderDraw Collider_D = GetComponent<ColliderDraw>();
        Mesh_C.sharedMesh = null;
        if (GetComponent<MeshFilter>().mesh.vertices.Length > 4)
        {
            Mesh_C.sharedMesh = GetComponent<MeshFilter>().mesh;
        }
    }

    public Color Fade(Color co, bool fadeway)
    {
        if (!fadeway)
        {
            co.a = timeChrono; 
        }
        else
        {
            co.a = 1 - timeChrono;
        }
        timeChrono += Time.deltaTime * timeInterval;

        return co;
    }
}
