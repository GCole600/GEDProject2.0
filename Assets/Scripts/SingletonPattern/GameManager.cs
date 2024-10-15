using System;
using FactoryPattern;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SingletonPattern
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Timer")]
        [SerializeField] private TMP_Text timerText;
        private float _time;
        public bool runGame;

        [Header("Gameplay")] 
        [SerializeField] private GameObject infoScreen;
        [SerializeField] private TMP_Text playText;
        [SerializeField] private TMP_Text sizeNotSelectedText;
        private bool _isSizeSelected;
        private Camera _camera;
        
        [Header("End Game")]
        [SerializeField] private GameObject gameplayUI;
        [SerializeField] private GameObject winScreen;
        [SerializeField] private TMP_Text timeText;
        public bool hasKey;

        private void Start()
        {
            _camera = Camera.main;
            AudioManager.Instance.PlayMusic("BackgroundMusic");
        }

        private void Update()
        {
            if (Input.anyKeyDown)
            {
                infoScreen.SetActive(false);
                gameplayUI.gameObject.SetActive(true);
            }
            
            if (runGame)
            {
                _time += Time.deltaTime;
                UpdateTimer();
            }
        }

        private void UpdateTimer()
        {
            // Converts time to minutes & seconds
            float minutes = Mathf.FloorToInt(_time / 60);
            float seconds = Mathf.FloorToInt(_time % 60);

            timerText.text = "Time:" + $"{minutes:00}:{seconds:00}";
        }
        
        public void PlayGame()
        {
            if (_isSizeSelected && !runGame)
            {
                MazeGenerator.Instance.GenerateMaze();
                runGame = true;
            }
            else if (!_isSizeSelected)
            {
                sizeNotSelectedText.gameObject.SetActive(true);
            }
        }

        public void SetMazeSize(int size)
        {
            if (_isSizeSelected) return;
            
            MazeGenerator.Instance.mazeSize = new Vector2Int(size, size);
            playText.text = "Play: " + size + "x" + size;
            _isSizeSelected = true;
            sizeNotSelectedText.gameObject.SetActive(false);

            _camera.orthographicSize = size switch
            {
                10 => 6,
                15 => 8.5f,
                20 => 11,
                _ => _camera.orthographicSize
            };
        }

        public void EndGame()
        {
            if (hasKey)
            {
                // Pause timer
                runGame = false;
                
                AudioManager.Instance.PlaySfx("Win");
                
                float minutes = Mathf.FloorToInt(_time / 60);
                float seconds = Mathf.FloorToInt(_time % 60);

                timeText.text = "Completed in: " + $"{minutes:00}:{seconds:00}";

                // Display Win Screen
                gameplayUI.gameObject.SetActive(false);
                winScreen.gameObject.SetActive(true);
            }
        }
        
        public void ReloadVars()
        {
            MazeGenerator.Instance.ReloadVars();
            runGame = false;
            _time = 0;
            _isSizeSelected = false;
            hasKey = false;
            gameplayUI.gameObject.SetActive(true);
            winScreen.gameObject.SetActive(false);
        }
    }
}