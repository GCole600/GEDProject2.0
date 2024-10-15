using UnityEngine;
using UnityEngine.EventSystems;

namespace FactoryPattern
{
    public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            AudioManager.Instance.PlaySfx("ButtonHover");
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            // Checks if its either "Play" button, sfx varies
            AudioManager.Instance.PlaySfx(gameObject.name is "PlayMaze_Button" or "PlayButton" ? "PlayButtonClick" : "ButtonClick");
        }
    }
}