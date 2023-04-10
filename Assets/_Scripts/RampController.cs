using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class RampController : MonoBehaviour
{

    [SerializeField] private SpriteShapeController _shape;
    [SerializeField] LineRenderer _lR;

    public int PointIndex;

    public bool RandomHeightAtStart;
    public float SelectedHeightAtStart;

    public Transform SlidingPoint;

    public float MaxHeight, MinHeight;

    public LayerMask PointLayerMask;

    private float _firstMouseHeight;
    private float _firstPointHeight;

    private bool _canMove = false; 
   
    

    // Start is called before the first frame update
    void Start()
    {
        SetStartPosition();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,Mathf.Infinity,PointLayerMask);

            if (hit.transform != SlidingPoint) return;

            _canMove = true;
            _lR.enabled = true;

            _lR.SetPosition(0, _shape.spline.GetPosition(PointIndex));
            _firstMouseHeight = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            _firstPointHeight = _shape.spline.GetPosition(PointIndex).y;
        }

        UpdateDistanceWithMousePos();

        if (Input.GetMouseButtonUp(0))
        {
            _canMove = false;
            _lR.enabled = false;
        }
    }


    void UpdateDistanceWithMousePos()
    {
        if (Input.GetMouseButton(0))
        {
            if (!_canMove) return;

            float distance = -_firstMouseHeight + Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            ChangeCornerPosition(distance);
        }

       
        
    }

    void ChangeCornerPosition(float y)
    {
        Vector2 position = _shape.spline.GetPosition(PointIndex);

        _shape.spline.SetPosition(PointIndex,new Vector2(position.x, Mathf.Clamp(_firstPointHeight + y, MinHeight,MaxHeight)));
        SlidingPoint.localPosition = new Vector3(_shape.spline.GetPosition(PointIndex).x, _shape.spline.GetPosition(PointIndex).y,SlidingPoint.localPosition.z);
        _lR.SetPosition(1, SlidingPoint.localPosition);
    }

    void SetStartPosition()
    {
        if (RandomHeightAtStart)
        {
            Vector2 position = _shape.spline.GetPosition(PointIndex);
            _shape.spline.SetPosition(PointIndex, new Vector2(position.x, Random.Range(MinHeight, MaxHeight)));
            SlidingPoint.localPosition = new Vector3(_shape.spline.GetPosition(PointIndex).x, _shape.spline.GetPosition(PointIndex).y, SlidingPoint.localPosition.z);
        }
        else
        {
            Vector2 position = _shape.spline.GetPosition(PointIndex);
            _shape.spline.SetPosition(PointIndex, new Vector2(position.x, SelectedHeightAtStart));
            SlidingPoint.localPosition = new Vector3(_shape.spline.GetPosition(PointIndex).x, _shape.spline.GetPosition(PointIndex).y, SlidingPoint.localPosition.z);
        }
       
    }
}
