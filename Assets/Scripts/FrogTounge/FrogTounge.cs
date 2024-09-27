using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogTounge : MonoBehaviour
{
    [SerializeField] private GameObject BeryAudio;
   [SerializeField] private LineRenderer lineRender;

    public List<Vector3> points = new List<Vector3>();
    public List<GameObject> FrogVisitCell;
   bool isFirst = false;
    public int resolution = 10;
    public float speed = 0.5f;
    public bool shouldRemoveEmptyCells;
    [SerializeField]private int CellCounts;
    void Start()
    {
        FrogVisitCell = transform.parent.GetComponent<Frog>().visitedCell;
       
        
        lineRender = GetComponent<LineRenderer>();
        CellCounts = FrogVisitCell.Count;

    }
    void Update()
    {
        
    }
    public void StartDrawing(List<GameObject> targetPositions)
    {
        StopAllCoroutines(); 
        lineRender.positionCount = 0;
        if (targetPositions.Count == 1 || targetPositions.Count == 0)
        {
            Vector3 singlePoint = new Vector3(0, 0, 0);
            lineRender.positionCount = 1;
            lineRender.SetPosition(0, singlePoint);
        }
        else
        {
            StartCoroutine(DrawLine(targetPositions));
        }

    }
    Transform GetTopmostParent(Transform child)
    {
        Transform topmostParent = child;

        while (topmostParent.parent != null)
        {
            topmostParent = topmostParent.parent;
        }

        return topmostParent;
    }

    private IEnumerator DrawLine(List<GameObject> targetPos)
    {
        points.Clear();

        foreach (var cell in targetPos)
        {
            points.Add(new Vector3(cell.transform.position.x, cell.transform.position.y, cell.transform.position.z - 0.5f));
            
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            Vector3 startPoint = points[i];
            Vector3 endPoint = points[i + 1];
            Instantiate(BeryAudio, transform.position, Quaternion.identity);
            yield return StartCoroutine(DrawSegment(startPoint, endPoint));
            

        }
        Instantiate(BeryAudio, transform.position, Quaternion.identity);

        yield return StartCoroutine(ReverseDrawAndRemove());
        

    }

    private IEnumerator DrawSegment(Vector3 startPoint, Vector3 endPoint)
    {
        
        Vector3 direction = endPoint - startPoint;
        if (direction.magnitude < Mathf.Epsilon)
        {
            yield break;
        }

        for (int j = 0; j < resolution; j++)
        {
            float t = (j + 1) / (float)resolution;
            Vector3 newPoint = startPoint + t * direction;

            lineRender.positionCount++;
            lineRender.SetPosition(lineRender.positionCount - 1, newPoint);

            yield return new WaitForSeconds(speed / resolution);
            

        }

        
    }

    private IEnumerator ReverseDrawAndRemove()
    {
       
        bool differentColorFound = false;
        
            if (FrogVisitCell.Count != 0)
            {
                foreach (var item in FrogVisitCell)
                {
                    if (transform.GetComponentInParent<Frog>().isSeeDifferentColor)
                    {
                        differentColorFound = true;
                        break;
                    }
                }
            }


      
        if (differentColorFound)
        {
            int originalPointCount = lineRender.positionCount;

            for (int i = originalPointCount - 1; i >= 0; i--)
            {
                lineRender.positionCount--;
                yield return new WaitForSeconds(speed / resolution);
            }

            points.Clear();
            lineRender.positionCount = 0;

            transform.GetComponentInParent<Frog>().visitedCell.Clear();

           
        }
        else
        {
        
            foreach (var item in FrogVisitCell)
            {
                if (item.transform.childCount > 0)
                {
                    var childItem = item.transform.GetChild(0);
                    StartCoroutine(MoveChildItemAlongPath(childItem));
                }
            }

            yield return StartCoroutine(RetractTongue());

            if (CellCounts <= 0)
            {
                Destroy(transform.parent.gameObject);
                GameManager.Instance.FrogCount--;
            }
        }

        StartCoroutine(CallRemoveEmptyCellsOnce());
    }

    private IEnumerator MoveChildItemAlongPath(Transform childItem)
    {
        List<Vector3> pathPoints = new List<Vector3>(points);
        pathPoints.Reverse();

        foreach (var point in pathPoints)
        {
            childItem.position = point;
            yield return new WaitForSeconds(speed / resolution);

        }
       Destroy(childItem.gameObject);
       CellCounts--;
    }

    private IEnumerator RetractTongue()
    {
        int originalPointCount = lineRender.positionCount;

        for (int i = originalPointCount - 1; i >= 0; i--)
        {
            lineRender.positionCount--;
            yield return new WaitForSeconds(speed / resolution);
        }

        points.Clear();
        lineRender.positionCount = 0;

        
    }

    private IEnumerator CallRemoveEmptyCellsOnce()
    {
        
        yield return null;
        GameManager.Instance.gridManager.RemoveEmptyCells();
        
    }

}
