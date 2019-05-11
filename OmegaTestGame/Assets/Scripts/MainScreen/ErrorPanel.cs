using UnityEngine;
using UnityEngine.UI;

namespace Scripts.MainScreen
{
    public class ErrorPanel : MonoBehaviour
    {
        [SerializeField] private Text _errorText;
        [SerializeField] private Button _actionButton;
        [SerializeField] private Text _buttonText;

        public delegate void ButtonAction();

        private ButtonAction _buttonAction;

        public void CreatePanel(ButtonAction panelAction, string errorText, string buttonText)
        {
            _buttonAction = panelAction;
            _errorText.text = errorText;
            _buttonText.text = buttonText;
        }

        public void ButtonOnClick()
        {
            _buttonAction();
            Destroy(gameObject);
        }
    }
}