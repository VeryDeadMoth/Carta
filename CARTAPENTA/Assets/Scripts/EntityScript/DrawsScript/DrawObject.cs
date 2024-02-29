using System;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;


public class DrawObject : MonoBehaviour
{
    [SerializeField] private float distanceVertices = .1f;
    [SerializeField] private float lengthMax = 10f;
    [SerializeField] private float lineThickness = 1f;
    [SerializeField] private float chronoBeforeDisapear = 2f;
    public Mesh mesh{get;private set;}

    private bool isDrawable = true;
    private float timeChrono = 0f;
    private Vector3 FirstPlacement;
    private Vector3 LastMousePos;
    private Vector3 VectorMovementPast;

    private void Update()
    {

        if(Input.GetMouseButtonDown(0))
        {
            // mouse pressed down
            timeChrono = 0f;
            OnMouseClicked();
        }

        //distanceVertices pour avoir une qualité de trait (+ => plus "quali" mais plus gourmand.)
        if (Input.GetMouseButton(0) && Vector3.Distance(GetMousePosition(),LastMousePos) > distanceVertices)
        {
            // mouse held down
            if(Vector3.Distance(GetMousePosition(),FirstPlacement) > lengthMax)
            {
                isDrawable = false;
            }
            if (isDrawable)
            {
                OnMouseButtonDown();
            }
        }

        //lance un chrono avant de faire disparaire le trait
        if(Input.GetMouseButtonUp(0))
        {
            timeChrono += Time.deltaTime;
        }

        if(timeChrono > 0f)
        {
            timeChrono += Time.deltaTime;
            if(timeChrono >= chronoBeforeDisapear) {
                if (!GetComponent<drawManager>().isInPlace)
                {
                    GetComponent<MeshFilter>().mesh.Clear();
                }
                timeChrono = 0f;
            }
        }

    }



    #region Mouse Function
    public void OnMouseClicked()
    {
        mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        isDrawable = true;
        FirstPlacement = GetMousePosition();
        vertices[0] = GetMousePosition();
        vertices[1] = GetMousePosition();
        vertices[2] = GetMousePosition();
        vertices[3] = GetMousePosition();

        for (int i = 0; i < 4; i++)
        {
            uv[i] = Vector2.zero;
        }


        triangles[0] = 0;
        triangles[1] = 3;
        triangles[2] = 1;

        triangles[3] = 1;
        triangles[4] = 3;
        triangles[5] = 2;

        VectorMovementPast = Vector3.zero;
        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.MarkDynamic();
        this.GetComponent<MeshFilter>().mesh = mesh;

        LastMousePos = GetMousePosition();
    }
    public void OnMouseButtonDown()
    {
        Vector3[] verticesMove = new Vector3[mesh.vertices.Length + 2];
        Vector2[] uvMove = new Vector2[mesh.uv.Length + 2];
        int[] trianglesMove = new int[mesh.triangles.Length + 6];

        mesh.vertices.CopyTo(verticesMove, 0);
        mesh.uv.CopyTo(uvMove, 0);
        mesh.triangles.CopyTo(trianglesMove, 0);

        int index = verticesMove.Length - 4;
        int index0 = index;
        int index1 = index + 1;
        int index2 = index + 2;
        int index3 = index + 3;

        Vector3 VectorMovement = (GetMousePosition() - LastMousePos).normalized;
        if (VectorMovement[0] == 0 && VectorMovement[1] == 0)
        {
            VectorMovement = VectorMovementPast;
        }
        else
        {
            VectorMovementPast = VectorMovement;
        }

        Vector3 normalVector = new Vector3(0, 0, -1f);
        // create new vertices
        Vector3 newVertexPlus = GetMousePosition() + Vector3.Cross(VectorMovement, normalVector) * lineThickness;
        Vector3 newVertexMinus = GetMousePosition() + Vector3.Cross(VectorMovement, normalVector * -1f) * lineThickness;


        // apply new vertices   
        verticesMove[index2] = newVertexPlus;
        verticesMove[index3] = newVertexMinus;

        uvMove[index2] = Vector2.zero;
        uvMove[index3] = Vector2.zero;

        // new triangles values created now
        int TriangleIndex = trianglesMove.Length - 6;
        trianglesMove[TriangleIndex] = index0;
        trianglesMove[TriangleIndex + 1] = index2;
        trianglesMove[TriangleIndex + 2] = index1;

        trianglesMove[TriangleIndex + 3] = index1;
        trianglesMove[TriangleIndex + 4] = index2;
        trianglesMove[TriangleIndex + 5] = index3;


        mesh.vertices = verticesMove;
        mesh.uv = uvMove;
        mesh.triangles = trianglesMove;

        LastMousePos = GetMousePosition();
    }

    #endregion


    public Vector3 GetMousePosition()
    {
        Vector3 CameraPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos = new Vector2(CameraPos[0], CameraPos[1]);
        return mousePos;
    }
}
