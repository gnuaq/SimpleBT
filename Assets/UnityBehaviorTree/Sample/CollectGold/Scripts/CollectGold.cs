using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class CollectGoldManager : MonoBehaviour
{
    public static CollectGoldManager Instance;

    public List<Gold> Golds;
    public List<Guard> Guards;
    public Player Player;
    public int Score;
    public bool IsEndGame = false;

    [SerializeField]
    private Gold _goldPrefab;
    [SerializeField]
    private GameObject _spannerGoldObject;
    private List<Transform> _spannerGolds;
    
    [Header("UI")]
    [SerializeField]
    private GameObject _endGamePanel;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Text _score;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Guards = FindObjectsOfType<Guard>().ToList();
        Player = FindObjectOfType<Player>();
        _spannerGolds = new List<Transform>();

        foreach (var transform in _spannerGoldObject.GetComponentsInChildren<Transform>())
        {
            _spannerGolds.Add(transform);
        }
        
        _endGamePanel.gameObject.SetActive(false);
        
        _restartButton.onClick.AddListener(RestartGame);
        _restartButton.onClick.AddListener(() => _endGamePanel.gameObject.SetActive(false));

        StartGame();
    }

    void Update()
    {
        
    }

    private void StartGame()
    {
        IsEndGame = false;
        for (int i = 0; i < 4; i++)
        {
            SpawnRandomGold();
        }
    }

    private void RestartGame()
    {
        foreach (var gold in Golds.ToList())
        {
            Golds.Remove(gold);
            Destroy(gold.gameObject);
        }

        foreach (var guard in Guards)
        {
            guard.transform.position = guard.InitPosition;
        }

        Player.transform.position = Player.InitPosition;
        
        StartGame();
    }

    private void SpawnRandomGold()
    {
        var pos = Random.Range(0, _spannerGolds.Count);
        var gold = Instantiate(_goldPrefab, _spannerGolds[pos].position, Quaternion.identity);
        Golds.Add(gold);
    }

    public void CollectGold()
    {
        Score += 10;
        SpawnRandomGold();
    }

    public void ShowEndGameUI()
    {
        _score.text = Score.ToString();
        _endGamePanel.gameObject.SetActive(true);
    }
}
