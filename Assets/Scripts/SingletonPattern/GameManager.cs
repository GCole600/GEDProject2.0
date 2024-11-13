using System.Collections;
using FactoryPattern;
using ObjectPool;
using TMPro;
using UnityEngine;

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
        [SerializeField] private GameObject loseScreen;
        [SerializeField] private TMP_Text timeText;
        public bool hasKey;
        public bool win;

        private void Start()
        {
            _camera = Camera.main;
            StartCoroutine(LoadMusic());
        }

        private IEnumerator LoadMusic()
        {
            yield return new WaitForEndOfFrame();
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

            timerText.text = "Time: " + $"{minutes:00}:{seconds:00}";
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
            if (runGame) return;
            
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

            switch (size)
            {
                case 10:
                    TrapSpawner.Instance.maxPoolSize = 5;
                    TrapSpawner.Instance.stackDefaultCapacity = 5;
                    break;
                case 15:
                    TrapSpawner.Instance.maxPoolSize = 10;
                    TrapSpawner.Instance.stackDefaultCapacity = 10;
                    break;
                case 20:
                    TrapSpawner.Instance.maxPoolSize = 15;
                    TrapSpawner.Instance.stackDefaultCapacity = 15;
                    break;
            }
        }

        public void EndGame()
        {
            // Pause timer
            runGame = false;
            
            gameplayUI.gameObject.SetActive(false);
            
            switch (win)
            {
                case true:
                    AudioManager.Instance.PlaySfx("Win");
                
                    float minutes = Mathf.FloorToInt(_time / 60);
                    float seconds = Mathf.FloorToInt(_time % 60);

                    timeText.text = "Completed in: " + $"{minutes:00}:{seconds:00}";

                    // Display Win Screen
                    winScreen.gameObject.SetActive(true);
                    break;
                
                case false:
                    //AudioManager.Instance.PlaySfx("Win");

                    // Display Lose Screen
                    loseScreen.gameObject.SetActive(true);
                    break;
            }
        }
        
        public void ReloadVars()
        {
            MazeGenerator.Instance.ReloadVars();
            runGame = false;
            _time = 0;
            _isSizeSelected = false;
            hasKey = false;
            win = false;
            gameplayUI.gameObject.SetActive(true);
            winScreen.gameObject.SetActive(false);
            loseScreen.gameObject.SetActive(false);
        }
    }
}