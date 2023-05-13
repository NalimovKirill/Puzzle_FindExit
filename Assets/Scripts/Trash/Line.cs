using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    private List<Vector2> _points;
    
    private void SetPoint(Vector2 point)
    {
        _points.Add(point);

        _lineRenderer.positionCount = _points.Count;
        _lineRenderer.SetPosition(_points.Count - 1, point);
    }

    public void UpdateLine(Vector2 position)
    {
        if (_points == null)
        {
            _points = new List<Vector2>();
            SetPoint(position);
            return;
        }

        if (Vector2.Distance(_points.Last(), position) > .1f)
        {
            SetPoint(position);
        }
    }
}
