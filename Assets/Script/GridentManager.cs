using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GridentManager : BaseMeshEffect
{
    public Gradient gradient;

    public Image image;
    public Text txt;
    float timer;
    

    private void Update()
    {
        timer += Time.deltaTime;
        txt.FontTextureChanged();
    }

    public override void ModifyMesh(VertexHelper vertexhelper)
    {
        List<UIVertex> vertices = new List<UIVertex>();
        vertexhelper.GetUIVertexStream(vertices);

        float min = vertices.Min(t => t.position.x);
        float max = vertices.Max(t => t.position.x);

        
        for(int i = 0; i < vertices.Count; i++)
        {
            UIVertex temp_vertex = vertices[i];

            float curXNormalized = Mathf.InverseLerp(min, max, vertices[i].position.x);
            curXNormalized = Mathf.PingPong(curXNormalized+timer, 1f);
            
            Color gColor = gradient.Evaluate(curXNormalized);
            temp_vertex.color = new Color(gColor.r, gColor.g, gColor.b, gColor.a);

            vertices[i] = temp_vertex;
        }

        vertexhelper.Clear();
        vertexhelper.AddUIVertexTriangleStream(vertices);
    }

    
}
