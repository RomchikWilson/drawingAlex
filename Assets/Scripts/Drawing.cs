using SplineMesh;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Drawing : MonoBehaviour
{
    private bool StartDrawing;
    private Vector3 MousePos;
    private int CurrentIndex;

    private GameObject drawnLine_L;
    private GameObject drawnLine_R;

    private LineRenderer LR_L;
    private LineRenderer LR_R;

    private Transform LastInstantiated_Collider_L;

    [SerializeField] private Material LineMat;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform Collider_Prefab;
    [SerializeField] private GameObject line_L_old = default;
    [SerializeField] private GameObject line_R_old = default;
    [SerializeField] private Transform playerTransfrom = default;

    [SerializeField] private GameObject line_L = default; /// <НУЖНО>
    [SerializeField] private GameObject line_R = default; /// <НУЖНО>
    private Spline line_L_Scr; /// <НУЖНО>
    private Spline line_R_Scr; /// <НУЖНО>

    [SerializeField] private Image image = default;

    private List<TempNode> tempNodesR = new List<TempNode>();
    private List<TempNode> tempNodesL = new List<TempNode>();

    public void OnPointerDown()
    {
        //line_L_Scr.nodes.Clear();

        MousePos = Input.mousePosition;
        tempNodesR.Clear();
        tempNodesL.Clear();

        //Левая линия
        if (LR_L == null)
        {
            LR_L = drawnLine_L.AddComponent<LineRenderer>();
            LR_L.startWidth = 0.1f;
            LR_L.material = LineMat;
        }
        LR_L.enabled = true;

        PlayerController.FreezePlayerAction?.Invoke(true);

        ////Правая линия
        //LR_R = drawnLine_R.AddComponent<LineRenderer>();
        //LR_R.startWidth = 0.1f;
        //LR_R.material = LineMat;

        //Time.timeScale = 0f;
    }

    public void OnPointerDrag()
    {
        Vector3 Dist = MousePos - Input.mousePosition;
        if (Dist.sqrMagnitude > 2000f)
        {
            //Левая линия
            var _pos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10f));
            LR_L.SetPosition(CurrentIndex, new Vector3(_pos.x, _pos.y, 5f));


            Vector3 deltaR = new Vector3(_pos.x - playerTransfrom.position.x, _pos.y - playerTransfrom.position.y, 0);
            Vector3 deltaL = new Vector3(_pos.x - playerTransfrom.position.x, _pos.y - playerTransfrom.position.y, 0);

            //if (CurrentIndex <= 1)
            //{
            //    line_R_Scr.nodes[CurrentIndex].Position = deltaR;
            //    line_R_Scr.nodes[CurrentIndex].Direction = deltaR;

            //    line_L_Scr.nodes[CurrentIndex].Position = deltaL;
            //    line_L_Scr.nodes[CurrentIndex].Direction = deltaL;
            //}
            //else
            //{

            //if (CurrentIndex < 1)
            //{
            //    line_R_Scr.nodes[0].Position = Vector3.zero;
            //    line_R_Scr.nodes[0].Direction = Vector3.zero;
                
            //    line_L_Scr.nodes[0].Position = Vector3.zero;
            //    line_L_Scr.nodes[0].Direction = Vector3.zero;
            //}

            tempNodesR.Add(new TempNode(deltaR, deltaR));//////////////////////НУЖНО
            tempNodesL.Add(new TempNode(deltaL, deltaL));//////////////////////НУЖНО
                                                         //}

            //var _centerSegment = tempNodesR[tempNodesR.Count / 2];

            //tempNodesR.ForEach((_segment) =>
            //{
            //    _segment.Position -= _centerSegment.Position;
            //    _segment.Direction -= _centerSegment.Direction;
            //});

            //_centerSegment = tempNodesL[tempNodesL.Count / 2];
            //tempNodesL.ForEach((_segment) =>
            //{
            //    _segment.Position -= _centerSegment.Position;
            //    _segment.Direction -= _centerSegment.Direction;
            //});

            //if (LastInstantiated_Collider_L != null)
            //{
            //    Vector3 CurLinePos = LR_L.GetPosition(CurrentIndex);
            //    LastInstantiated_Collider_L.gameObject.SetActive(true);

            //    LastInstantiated_Collider_L.LookAt(CurLinePos);

            //    if (LastInstantiated_Collider_L.rotation.y == 0)
            //    {
            //        LastInstantiated_Collider_L.eulerAngles = new Vector3(LastInstantiated_Collider_L.rotation.eulerAngles.x, 90, LastInstantiated_Collider_L.rotation.eulerAngles.z);
            //    }

            //    LastInstantiated_Collider_L.localScale = new Vector3(LastInstantiated_Collider_L.localScale.x, LastInstantiated_Collider_L.localScale.y, Vector3.Distance(LastInstantiated_Collider_L.position, CurLinePos) * 0.5f);
            //}

            //LastInstantiated_Collider_L = Instantiate(Collider_Prefab, LR_L.GetPosition(CurrentIndex), Quaternion.identity, drawnLine_L.transform);
            //LastInstantiated_Collider_L.gameObject.SetActive(false);

            MousePos = Input.mousePosition;
            CurrentIndex++;

            //Левая линия
            LR_L.positionCount = CurrentIndex + 1;
            LR_L.SetPosition(CurrentIndex, new Vector3(_pos.x, _pos.y, 5f));
        }
    }
    public void OnPointerUp()
    {
        //Левая линия
        //Rigidbody rb_L = drawnLine_L.AddComponent<Rigidbody>();
        //rb_L.useGravity = false;
        //rb_L.constraints = RigidbodyConstraints.FreezeRotationX;
        //LR_L.useWorldSpace = false;

        ////Правая линия
        //Rigidbody rb_R = drawnLine_R.AddComponent<Rigidbody>();
        //rb_R.useGravity = false;
        //rb_R.constraints = RigidbodyConstraints.FreezeRotationX;
        //LR_R.useWorldSpace = false;
        

        PlayerController.FreezePlayerAction?.Invoke(false);

        for (int i = line_R_Scr.nodes.Count - 1; i > -1; i--)
        {
            line_R_Scr.nodes.RemoveAt(i);
        }
        for (int i = line_L_Scr.nodes.Count - 1; i > -1; i--)
        {
            line_L_Scr.nodes.RemoveAt(i);
        }

        CurrentIndex = 0;

        tempNodesL.ForEach((_node) =>
        {
            line_L_Scr.nodes.Add(new SplineNode(_node.Position, _node.Direction));
        });

        tempNodesR.ForEach((_node) =>
        {
            line_R_Scr.nodes.Add(new SplineNode(_node.Position, _node.Direction));
        });

        //var _centerSegment = line_R_Scr.nodes[line_R_Scr.nodes.Count / 2];
        //line_R_Scr.nodes.ForEach((_segment) =>
        //{
        //    _segment.Position -= _centerSegment.Position;
        //    _segment.Direction -= _centerSegment.Direction;
        //});
        //_centerSegment = line_L_Scr.nodes[line_L_Scr.nodes.Count / 2];
        //line_L_Scr.nodes.ForEach((_segment) =>
        //{
        //    _segment.Position -= _centerSegment.Position;
        //    _segment.Direction -= _centerSegment.Direction;
        //});

        line_L_Scr.RefreshCurves(); 
        line_R_Scr.RefreshCurves();

        PlayerController.StartMovePlayerAction?.Invoke(true);

        LR_L.enabled = false;
        //LR_R.enabled = false;

        //if (LastInstantiated_Collider_L != null)
        //{
        //    Destroy(LastInstantiated_Collider_L.gameObject);
        //}
        Start();
    }

    void Start()
    {
        if (line_L_Scr == null)
            line_L_Scr = line_L.GetComponent<Spline>();///////////////НУЖНО
        if (line_R_Scr == null)
            line_R_Scr = line_R.GetComponent<Spline>();///////////////НУЖНО

        if (drawnLine_L == null)
            drawnLine_L = new GameObject("DrawnLine_L");
        if (drawnLine_R == null)
            drawnLine_R = new GameObject("DrawnLine_R");

        drawnLine_L.transform.SetParent(line_L_old.transform);
        drawnLine_R.transform.SetParent(line_R_old.transform);
    }
}

public class TempNode
{
    public Vector3 Position { get; set; }
    public Vector3 Direction { get; set; }

    public TempNode(Vector3 pos, Vector3 dir)
    {
        Position = pos;
        Direction = dir;
    }
} 