using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LineGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _linePrefab;
    private LineRenderer _line;
    //private Line _activeLine;
    private Vector3 _previoisPosition;
    private Vector3[] _positions;
    private float _minDistance = 0.1f;

    private bool _canDraw = false;

    [SerializeField] private PersonController _personController;
    private void OnEnable()
    {
        _personController.OnPersonClickedEvent.AddListener(PersonClicked);

        //_personController.OnPersonUp.AddListener();
    }
    private void Start()
    {
        _line = GetComponent<LineRenderer>();
        _line.positionCount = 1;
        _previoisPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && _canDraw == true)
        {
            Draw();
        }

        /*if (Input.GetMouseButtonDown(0) && _personController._canDraw == true)
        {
            GameObject newLine = Instantiate(_linePrefab);
            _activeLine = newLine.GetComponent<Line>();
        }

        if (Input.GetMouseButtonUp(0))
        {
            _activeLine = null;
        }

        if (_activeLine != null)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _activeLine.UpdateLine(mousePos);
        }*/
    }

    private void PersonClicked(TypeOfPerson typeOfPerson)
    {
        _canDraw = true;
    }

    private void Draw()
    {
        Vector3 currentPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currentPosition.z = 0f;

        if (Vector3.Distance(currentPosition, _previoisPosition) > _minDistance)
        {

            if (_previoisPosition == transform.position)
            {
                _line.SetPosition(0, currentPosition);
            }
            else
            {
                _line.positionCount++;
                _line.SetPosition(_line.positionCount - 1, currentPosition);
            }

            _previoisPosition = currentPosition;
        }

        _positions = new Vector3[_line.positionCount];
    }
          
}
  


