using Assets.Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Level.Bind
{
    public class LevelGridBinder : MonoBehaviour
    {
        [SerializeField] GridLayoutGroup _grid;
        [SerializeField] GameObject _gridItem;

        private LevelService _levelService;

        [Inject]
        public void Construct(LevelService levelService) 
        { 
            _levelService = levelService; 
        }
        private void Start()
        {
            Bind();
        }
        private void Bind()
        {
            var levels = _levelService.GetAllLevels();
            int i = 1;
            foreach (var level in levels)
            {
                var item = Instantiate(_gridItem, _grid.transform, false);
                var button = item.GetComponent<Button>();
                var tmpButton = item.GetComponentInChildren<TextMeshProUGUI>();
                tmpButton.text = i.ToString();
                i++;
                if (level.IsOpen)
                    button.interactable = true;
                
            }
        }
    }
}
