using Assets.Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using R3;
using System.Linq;
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
            _levelService.LoadLevelFromManagerScene();
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
                var imgLock = item.GetComponentsInChildren<Image>().First(x => x.name == "Lock");
                tmpButton.text = i.ToString();
                i++;
                if (level.IsOpen)
                {
                    button.interactable = true;
                    imgLock.enabled = false;
                }

                button.OnClickAsObservable().Subscribe(_ => 
                {
                    _levelService.LoadLevelById(level.Id);
                }
                ).AddTo(tmpButton);
            }
        }
    }
}
