using System.Collections;
using DLL_Plugin;
using FactoryPattern;
using ObjectPool;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using Invoker = CommandPattern.Invoker;

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
        [SerializeField] private GameObject listUI;
        [SerializeField] private GameObject winScreen;
        [SerializeField] private GameObject loseScreen;
        [SerializeField] private TMP_Text timeText;
        public bool hasKey;
        public bool win;

        private bool _playing;

        private void Start()
        {
            _camera = Camera.main;
            StartCoroutine(LateLoad());
        }

        private static IEnumerator LateLoad()
        {
            yield return new WaitForEndOfFrame();
            AudioManager.Instance.PlayMusic("BackgroundMusic");
            
            // Load the CSV file
            CSVLoader.Instance.LoadCSV(Application.streamingAssetsPath + "/Maze Data - Sheet1.csv");
        }

        private void Update()
        {
            if (!_playing)
            {
                if (Keyboard.current.anyKey.wasPressedThisFrame)
                {
                    infoScreen.SetActive(false);
                    gameplayUI.gameObject.SetActive(true);
                    listUI.gameObject.SetActive(true);
                    _playing = true;
                }
            }
        }

        private IEnumerator UpdateTimer()
        {
            while (runGame)
            {
                _time += 1.0f;
                
                // Converts time to minutes & seconds
                float minutes = Mathf.FloorToInt(_time / 60);
                float seconds = Mathf.FloorToInt(_time % 60);

                timerText.text = "Time: " + $"{minutes:00}:{seconds:00}";
                
                yield return new WaitForSeconds(1.0f);
            }
        }
        
        public void PlayGame()
        {
            switch (_isSizeSelected)
            {
                case true when !runGame:
                    MazeGenerator.Instance.GenerateMaze();
                    runGame = true;
                    StartCoroutine(UpdateTimer());
                    break;
                case false:
                    sizeNotSelectedText.gameObject.SetActive(true);
                    break;
            }
        }

        public void SetMazeSize(int row)
        {
            if (runGame) return; // Can't change size if playing game

            // Get maze size from CSV file
            var mazeSize = CSVLoader.Instance.GetValueAt(row, "Maze Size");
            MazeGenerator.Instance.mazeSize = new Vector2Int(mazeSize, mazeSize);
            playText.text = "Play: " + mazeSize + "x" + mazeSize;
            
            _isSizeSelected = true;
            sizeNotSelectedText.gameObject.SetActive(false);

            _camera.orthographicSize = mazeSize switch
            {
                10 => 6,
                15 => 8.5f,
                20 => 11,
                _ => _camera.orthographicSize
            };

            var trapCount = CSVLoader.Instance.GetValueAt(row, "Trap Count");
            TrapSpawner.Instance.maxPoolSize = trapCount;
            TrapSpawner.Instance.stackDefaultCapacity = trapCount;
        }

        public void EndGame()
        {
            // Pause timer
            runGame = false;
            
            gameplayUI.gameObject.SetActive(false);
            listUI.gameObject.SetActive(false);
            
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
                    // Display Lose Screen
                    loseScreen.gameObject.SetActive(true);
                    break;
            }
        }
        
        public void ReloadVars()
        {
            MazeGenerator.Instance.ReloadVars();
            Invoker.Instance.ClearList();
            runGame = false;
            _time = 0;
            _isSizeSelected = false;
            hasKey = false;
            win = false;
            gameplayUI.gameObject.SetActive(true);
            listUI.gameObject.SetActive(true);
            winScreen.gameObject.SetActive(false);
            loseScreen.gameObject.SetActive(false);
        }
    }
}