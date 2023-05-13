using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _levelCountPanel;

    [SerializeField] private GameObject _HelpAnim;
    private TextMeshProUGUI _countOfLevel;
    [SerializeField] private Button _startbButton;
    private PersonController[] _persons;
    private void OnDisable()
    {
        foreach (var item in _persons)
        {
            item.OnWinGame.RemoveAllListeners();
            item.OnLoseGame.RemoveAllListeners();
        }
    }
    private void Awake()
    {
        CheckCountOfPerson();
    }
    private void Start()
    {
        _countOfLevel = _levelCountPanel.GetComponentInChildren<TextMeshProUGUI>();
        _countOfLevel.text = "Уровень " + (SceneManager.GetActiveScene().buildIndex).ToString();

    }
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_HelpAnim != null)
            {
                _HelpAnim?.SetActive(false);
            }
            
        }
    }

    private void CheckCountOfPerson()
    {

        _persons = FindObjectsOfType(typeof(PersonController)) as PersonController[];
        foreach (PersonController controller in _persons) 
        {
            controller.OnWinGame.AddListener(OpenWinPanel);
            controller.OnLoseGame.AddListener(OpenLosePanel);
        }
    }

    private void OpenWinPanel()
    {
        Invoke("OpenPanel", 1f);
    }
    private void OpenLosePanel()
    {
        _losePanel.SetActive(true);

        Time.timeScale = 0f;
    }
    private void OpenPanel()
    {
        _winPanel.SetActive(true);
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
    public void OnRestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    } 
    public void OnNextLevelButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1f;
    }
}
