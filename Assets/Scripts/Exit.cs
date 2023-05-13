using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class Exit : MonoBehaviour
{
    [SerializeField] private TypeOfPerson _typeOfExit;
    public TypeOfPerson ExitType { get { return _typeOfExit; } }

    private TMPro.TextMeshPro _textMeshPro;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> sprites;
    [HideInInspector]
    public bool _isFilled = false;

    private void Awake()
    {
        _textMeshPro = GetComponentInChildren<TMPro.TextMeshPro>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        
    }
    private void Start()
    {
        transform.DORotate(new Vector3(0, 0, 360f), 5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1);
        _isFilled = false;
        switch (_typeOfExit)
        {
            case TypeOfPerson.Boy:
                _textMeshPro.text = "1";
                _spriteRenderer.sprite = sprites[0];
                break;
            case TypeOfPerson.Girl:
                 _textMeshPro.text = "2";
                _spriteRenderer.sprite = sprites[1];
                break;
            case TypeOfPerson.Both:
                _textMeshPro.text = "1/2";
                _spriteRenderer.sprite = sprites[2];
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        _textMeshPro.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

}
