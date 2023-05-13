using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
public class DrawLine : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent EnterExitPointEvent = new UnityEvent();
    public EndDrawLineEvent EndDrawLineEvent = new EndDrawLineEvent();

    private LineRenderer _line;
    private Collider2D _colliderOfPerson;
    private Vector3 _previoisPosition;
    private Vector3[] _positions;
    private Vector3[] _pos;
    public Vector3[] Pos { get { return _pos; } }
    private float _minDistance = 0.1f;

    private bool _canDraw = false;
    private bool _lineOnExit = false;
    

    private PersonController _personController;


    private void Awake()
    {
        _personController = GetComponent<PersonController>();
        _line = GetComponent<LineRenderer>();
        _colliderOfPerson = GetComponent<Collider2D>();
    }
    private void OnEnable()
    {
        _personController.OnPersonClickedEvent.AddListener(StartDraw);

        _personController.OnPersonUpEvent.AddListener(EndDraw);
    }
    private void OnDisable()
    {
        _personController.OnPersonClickedEvent.RemoveAllListeners();

        _personController.OnPersonUpEvent.RemoveAllListeners();
    }
    private void Start()
    {
        _line.positionCount = 1;
        _previoisPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && _canDraw == true && _lineOnExit == false)
        {
            Drawing();
        }
    }

    private void StartDraw(TypeOfPerson typeOfPerson)
    {
        _canDraw = true;
    }
    private void EndDraw(TypeOfPerson typeOfPerson)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.TryGetComponent(out Exit exit))
            {
                if (_personController.PersonType == exit.ExitType || exit.ExitType == TypeOfPerson.Both )
                {
                    if (exit._isFilled == false)
                    {
                        EnterExitPointEvent?.Invoke();
                        _canDraw = false;
                        _lineOnExit = true;

                        exit._isFilled = true;

                    }
                    else
                    {
                        ClearLine();
                    }
                }
                else
                {
                    ClearLine();
                }
            }
            else
            {
                if (_lineOnExit == false)
                {
                    ClearLine();
                }
            }
        }
        else
        {
            if (_lineOnExit == false)
            {
                ClearLine();
            }
        }
    }

    private void Drawing()
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
        _pos = GetLinePointsInWorldSpace();

        EndDrawLineEvent?.Invoke(_pos);
    }
    private void ClearLine()
    {
        _line.positionCount = 1;
        _canDraw = false;
    }
    private Vector3[] GetLinePointsInWorldSpace()
    {
        _line.GetPositions(_positions);
        return _positions;
    }

}

public class EndDrawLineEvent : UnityEvent<Vector3[]> { }



