using Assets.Scripts.Input;
using UnityEngine;
using Zenject;
using Assets.Scripts.Liquid;
using System.Linq;
using System;
using System.Collections;

namespace Assets.Scripts.Player
{
    public class PlayerPumpLiquid : MonoBehaviour, IPumpProperty
    {
        private IGamePlayInput _gamePlayInput; // Получаем управление
        #region Player
        [SerializeField] GameObject player;
        [SerializeField] private float HeightForce = 1.0f;
        [SerializeField] private float JumpForceV = 1.0f;
        private IPlayerProperty _playerProperty;
        #endregion

        #region Liquid
        [SerializeField] LayerMask LiquidLayerMask; // слои жидкости
        private ILiquid liquidInteract = null;  // интерфейс взаимодействия с жидкостью
        [SerializeField] EWaterProperty currentPropery = EWaterProperty.None; // текущее свойство полученное из жидкости
        public static Action<EWaterProperty> ActionWaterProperty; // оповещаем о изменении свойства
        private bool _isInteract = false; // происходит сейчас взаидодействие?
        [SerializeField] private float squareV = 0; // текущий объем жидкости
        [SerializeField] private int ForcePump = 1;

        public float GetSquare { get => squareV; set { /*подумать, как возвращать размер от объема*/ } }
        public EWaterProperty GetProperty { get => currentPropery; set { /* вот тут не уверен, что нужно, но пока есть */} }
        #endregion

        [Inject]
        public void Construct(IGamePlayInput gamePlayInput)
        {
            _gamePlayInput = gamePlayInput;
        }

        private void Start()
        {
            _gamePlayInput.OnInteract += SetInteract;
            _playerProperty = GetComponent<IPlayerProperty>();
            ActionWaterProperty += SetPropertyPlayer;
        }

        private void SetPropertyPlayer(EWaterProperty waterProperty)
        {
            if (waterProperty == EWaterProperty.Slime)
            {
                _playerProperty.HaveSliding = true;
            }
            else if (waterProperty == EWaterProperty.Gasoline)
            {
                _playerProperty.HaveDoubleJump = true;
            }
            else
            {
                _playerProperty.HaveDoubleJump = false;
                _playerProperty.HaveSliding = false;
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {
            LiquidPump(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            _isInteract = false;
        }


        private void SetProperty(EWaterProperty property)
        {
            currentPropery = property;
            ActionWaterProperty?.Invoke(property);
        }

        private void SetInteract(bool state)
        {
            _isInteract = state;
        }

        private void LiquidPump(Collider2D collision)
        {
            ILiquid i = collision.gameObject.GetComponent<ILiquid>();
            if (i != null && liquidInteract == null)
            {
                liquidInteract = i;
                if (!_isInteract && (currentPropery == EWaterProperty.None || currentPropery == liquidInteract.Property))
                {
                    _isInteract = true;
                    SetProperty(liquidInteract.Property);
                    StartCoroutine(Pupm());
                }
            }
        }

        IEnumerator Pupm()
        { 
            while (_isInteract)
            {
                if (liquidInteract != null && (liquidInteract.Property == currentPropery || currentPropery == EWaterProperty.None))
                {

                    float i = liquidInteract.Pump(ForcePump);
                    squareV += i;

                    if (i == 0f)
                    {
                        _isInteract = false;
                        break;
                    }
                    if (player != null)
                    {
                        int xS = player.transform.localScale.x > 0? 1: -1;
                        float x = Math.Abs(player.transform.localScale.x) + 0.01f * HeightForce;
                        player.transform.localScale = new Vector3(xS*x, player.transform.localScale.y + 0.01f * HeightForce, player.transform.localScale.z);
                        _playerProperty.ChangeJumpForce += JumpForceV;
                    }
                }
                yield return new WaitForSeconds(1f);
            }
            liquidInteract = null;
        }

        private void LiquidSqueeze()
        {
            Debug.Log("Squeeze");
        }

    }
}
