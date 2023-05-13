using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PersonController : MonoBehaviour
{

    [SerializeField] private TypeOfPerson _typeOfPerson;
    public TypeOfPerson PersonType { get { return _typeOfPerson; } }

    private TextMeshPro _textMeshPro;
    private SpriteRenderer _spriteRenderer;
    [SerializeField] private List<Sprite> sprites;
    private LineRenderer _lineRenderer;
    private AudioSource _explosionSound;

    public OnPersonClickedEvent OnPersonClickedEvent = new OnPersonClickedEvent();
    public OnPersonUpEvent OnPersonUpEvent = new OnPersonUpEvent();
    [HideInInspector]
    public UnityEvent OnWinGame = new UnityEvent();
    [HideInInspector]
    public UnityEvent OnLoseGame = new UnityEvent();

    

    private void Awake()
    {
        _textMeshPro = GetComponentInChildren<TextMeshPro>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();
        _explosionSound = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        
        switch (_typeOfPerson)
        {
            case TypeOfPerson.Boy:
                _textMeshPro.text = "1";
                _spriteRenderer.sprite = sprites[0];

                _lineRenderer.startColor = Color.gray;
                _lineRenderer.endColor = Color.gray;
                break;
            case TypeOfPerson.Girl:
                _textMeshPro.text = "2";
                _spriteRenderer.sprite = sprites[1];
                _spriteRenderer.color = Color.green;

                _lineRenderer.startColor = Color.green;
                _lineRenderer.endColor = Color.green;
                break;
            case TypeOfPerson.Both:
                _textMeshPro.text = "1/2";
                _spriteRenderer.sprite = sprites[2];
                break;
            default:
                break;
        }

    }
    
    private void OnMouseDown()
    {
        OnPersonClickedEvent?.Invoke(_typeOfPerson);
       
    }
    private void OnMouseUp()
    {
        OnPersonUpEvent?.Invoke(_typeOfPerson);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Exit>(out Exit exit))
        {
            if (exit.ExitType == _typeOfPerson)
            {
                OnWinGame?.Invoke();
            }
        }
        
        if (collision.gameObject.TryGetComponent<PersonController>(out PersonController person))
        {
            OnLoseGame?.Invoke();
            _spriteRenderer.sprite = sprites[3];
            _textMeshPro.text = "";
            _explosionSound.Play();
        }
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
        {
            _spriteRenderer.sprite = sprites[3];
            _textMeshPro.text = "";
            _explosionSound.Play();
            OnLoseGame?.Invoke();
        }
    }

}
public enum TypeOfPerson { Boy , Girl, Both }
public class OnPersonClickedEvent: UnityEvent<TypeOfPerson> { }
public class OnPersonUpEvent: UnityEvent<TypeOfPerson> { }
