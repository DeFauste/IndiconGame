using SimpleInputNamespace;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    internal class DialogShow: MonoBehaviour
    {
        [SerializeField] private string Description = "None";
        [SerializeField] private GameObject cardDialog;
        [SerializeField] private TextMeshProUGUI textUI;
        [SerializeField] private Button buttonUI;
        [SerializeField] private SpriteRenderer sprite;

        private void Start()
        {
            textUI = cardDialog.GetComponentInChildren<TextMeshProUGUI>();
            buttonUI = cardDialog.GetComponentInChildren<Button>();
            cardDialog.SetActive(false);
            buttonUI.onClick.AddListener(OnClickButton);
            if(sprite != null) sprite.enabled = false;
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            textUI.text = Description;
            cardDialog.SetActive(true);
            OnPauseGame();
        }
        private void OnPauseGame()
        {
            Time.timeScale = 0.0f;
        }
        private void OffPauseGame()
        {
            Time.timeScale = 1.0f;
        }

        private void OnClickButton()
        {
            OffPauseGame();
            cardDialog.SetActive(false);
            gameObject.SetActive(false);
        }

    }
}
