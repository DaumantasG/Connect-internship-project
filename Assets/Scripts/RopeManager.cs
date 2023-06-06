using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UIElements;

public class RopeManager : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Transform parent;
    [SerializeField] private GameObject linePrefab;

    public LineRenderer lineRenderer;
    public bool isAnimating;

    public Transform startObj;
    public Transform previousObj;
    public Transform nextObj;
    public Transform savedObj;  

    //Adds Line Renderer object to GameObject and gives it material, texture and size
    public void CreateLine()
    {
        GameObject lineObject = Instantiate(linePrefab, parent);
        lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.material = Resources.Load<Material>("rope");
        lineRenderer.textureMode = LineTextureMode.Tile;
        lineRenderer.startWidth = 7f;
        lineRenderer.endWidth = 7f;

        StartCoroutine(AnimateLine());

        //Sets third object as second object
        previousObj = nextObj;
    }
    public void LastLine()
    {
        //Sets first object as last object
        nextObj = savedObj;
        CreateLine();
    }

    private IEnumerator AnimateLine()
    {
        isAnimating = true;

        lineRenderer.positionCount = 2;

        //Sets coordinates for both objects
        lineRenderer.SetPosition(0, startObj.position);
        lineRenderer.SetPosition(1, previousObj.position);

        float duration = 1f;
        float elapsedTime = 0f;

        //Draws line from first object to second over time
        while (elapsedTime < duration)
        {
            Vector3 currentPosition = Vector3.Lerp(startObj.position, previousObj.position, elapsedTime / duration);

            lineRenderer.SetPosition(1, currentPosition);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        //Sets second object as first
        startObj = previousObj;
        isAnimating = false;
    }
}
