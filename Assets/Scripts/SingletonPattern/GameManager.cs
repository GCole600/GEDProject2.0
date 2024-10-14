using TMPro;
using UnityEngine;

namespace SingletonPattern
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Timer")]
        [SerializeField] private TMP_Text timerText;
        private float _time = 0;
        public bool timeGame = false;
        
        [Header("Gameplay")]
        [SerializeField] private TMP_Text playText;
        [SerializeField] private TMP_Text sizeNotSelectedText;
        private bool _isSizeSelected;
        private bool _hasKey;

        private void Update()
        {
            if (timeGame)
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
            if (_isSizeSelected && !timeGame)
            {
                MazeGenerator.Instance.GenerateMaze();
                timeGame = true;
            }
            else if (!_isSizeSelected)
            {
                sizeNotSelectedText.gameObject.SetActive(true);
            }
        }

        public void SetMazeSize(int size)
        {
            MazeGenerator.Instance.mazeSize = new Vector2Int(size, size);
            playText.text = "Play: " + size + "x" + size;
            _isSizeSelected = true;
            sizeNotSelectedText.gameObject.SetActive(false);
        }
    }
}