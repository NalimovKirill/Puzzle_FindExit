using UnityEngine;
using DG.Tweening;

public class MovePersons : MonoBehaviour
{
    private DrawLine[] _personsArray;

    private bool _doOneTime = false;
    private Vector3[] _positionToMove;

    private int _counterToFinishLevel;
    [SerializeField] private float _timeToMove = 3;
    private void OnDisable()
    {
        foreach (var item in _personsArray) 
        {
            item.EnterExitPointEvent.RemoveAllListeners();
            item.EndDrawLineEvent.RemoveAllListeners();
        }
    }
    
    private void Start()
    {
        _personsArray = FindObjectsOfType(typeof(DrawLine)) as DrawLine[];
        foreach (var item in _personsArray)
        {
            item.EnterExitPointEvent.AddListener(AddPersonToMove);
            item.EndDrawLineEvent.AddListener(FillPathWay);
        }
    }
    private void FillPathWay(Vector3[] positions)
    {
        _positionToMove = positions;
    }
    private void AddPersonToMove()
    {
        _counterToFinishLevel++;
    }
    private void Update()
    {
        if (!_doOneTime)
        {
            if (_counterToFinishLevel == _personsArray.Length)
            {
                foreach (var item in _personsArray)
                {
                    item.transform.DOPath(item.Pos, _timeToMove, PathType.Linear);
                }
                _doOneTime = true;
            }
        }
    }
}
