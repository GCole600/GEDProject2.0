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
        private float _distance = 1.0f;

        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    if (!Physics.Raycast(this.transform.position, this.transform.forward, 1)) 
                        transform.Translate(Vector3.forward * _distance);
                    break;
                case Direction.Left:
                    if (!Physics.Raycast(this.transform.position, -this.transform.right, 1))
                        transform.Translate(Vector3.left * _distance);
                    break;
                case Direction.Down:
                    if (!Physics.Raycast(this.transform.position, -this.transform.forward, 1))
                        transform.Translate(Vector3.back * _distance);
                    break;
                case Direction.Right:
                    if (!Physics.Raycast(this.transform.position, this.transform.right, 1))
                        transform.Translate(Vector3.right * _distance);
                    break;
            }

            maze.ColorNode();
        }
    }
}