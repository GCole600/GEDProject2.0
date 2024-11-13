using System;
using FactoryPattern;
using SingletonPattern;
using UnityEngine;

namespace CommandPattern
{
    public class CharacterController : MonoBehaviour
    {
        public enum Direction
        {
            Left,
            Right,
            Up,
            Down
        }

        [SerializeField] private MazeGenerator maze;
        private const float Distance = 1.0f;

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (!Physics.Raycast(this.transform.position, this.transform.forward, 1)) 
                        transform.Translate(Vector3.forward * Distance);
                    break;
                case Direction.Left:
                    if (!Physics.Raycast(this.transform.position, -this.transform.right, 1))
                        transform.Translate(Vector3.left * Distance);
                    break;
                case Direction.Down:
                    if (!Physics.Raycast(this.transform.position, -this.transform.forward, 1))
                        transform.Translate(Vector3.back * Distance);
                    break;
                case Direction.Right:
                    if (!Physics.Raycast(this.transform.position, this.transform.right, 1))
                        transform.Translate(Vector3.right * Distance);
                    break;
            }
            
            AudioManager.Instance.PlaySfx("MoveSound");
            maze.ColorNode();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Key") && GameManager.Instance.runGame)
            {
                other.gameObject.SetActive(false);
                AudioManager.Instance.PlaySfx("PickUpKey");
                GameManager.Instance.hasKey = true;
            }
            else if (other.CompareTag("Trap") && GameManager.Instance.runGame)
            {
                GameManager.Instance.win = false;
                GameManager.Instance.EndGame();
            }
        }
    }
}