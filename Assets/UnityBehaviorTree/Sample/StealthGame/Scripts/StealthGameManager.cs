using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class StealthGameManager : MonoBehaviour
{
    public static StealthGameManager Instance;

    public List<Gold> Golds;
    public List<Guard> Guards;
    public Player Player;

    [Header("UI")]
    [SerializeField]
    private GameObject _endGamePanel;
    [SerializeField]
    private Button _restartButton;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Golds = FindObjectsOfType<Gold>().ToList();
        Guards = FindObjectsOfType<Guard>().ToList();
        Player = FindObjectOfType<Player>();
        _restartButton.onClick.AddListener(() => _endGamePanel.gameObject.SetActive(false));
        _restartButton.onClick.AddListener(RestartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void RestartGame()
    {
    }

    public void CollectGold()
    {
        if (Golds.Count == 0)
        {
            ShowEndGameUI();
        }
    }

    public void ShowEndGameUI()
    {
        _endGamePanel.gameObject.SetActive(true);
    }
}
