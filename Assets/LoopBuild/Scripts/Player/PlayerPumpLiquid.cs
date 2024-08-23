using Assets.Scripts.Input;
using UnityEngine;
using Zenject;
using Assets.Scripts.Liquid;
using System;
using System.Collections;

namespace Assets.Scripts.Player
{
    public class PlayerPumpLiquid : MonoBehaviour, IPumpProperty
    {
        private IGamePlayInput _gamePlayInput; // Получаем управление
        private bool _isPressF = false;
        #region Player
        [SerializeField] GameObject player;
        [SerializeField] private float HeightForce = 1.0f;
        [SerializeField] private float JumpForceV = 1.0f;
        private IPlayerProperty _playerProperty;
        #endregion

        #region Liquid
        [SerializeField] LayerMask LiquidLayerMask; // слои жидкости
        [SerializeField] LayerMask SpinLayerMask; // слои выжимок
        private ILiquid liquidInteract = null;  // интерфейс взаимодействия с жидкостью
        [SerializeField] EWaterProperty currentPropery = EWaterProperty.None; // текущее свойство полученное из жидкости
        public static Action<EWaterProperty> ActionLiquidProperty; // оповещаем о изменении свойства
        private bool _isInteract = false; // происходит сейчас взаидодействие?
        [SerializeField] private float squareV = 0; // текущий объем жидкости
        [SerializeField] private int ForcePump = 1;

        public float GetSquare { get => squareV; set { /*подумать, как возвращать размер от объема*/ } }
        public EWaterProperty GetProperty { get => currentPropery; set { /* вот тут не уверен, что нужно, но пока есть */} }
        #endregion
        [SerializeField] private bool isNull = false;
        [Inject]
        public void Construct(IGamePlayInput gamePlayInput)
        {
            _gamePlayInput = gamePlayInput;
        }

        private void Start()
        {
            _gamePlayInput.OnInteract += PressF;
            _playerProperty = GetComponent<IPlayerProperty>();
            ActionLiquidProperty += SetPropertyPlayer;
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
            if(LiquidLayerMask.value == (1 << collision.gameObject.layer)) LiquidPump(collision);
            if(_isPressF && SpinLayerMask.value == (1 << collision.gameObject.layer)) LiquidSqueeze(collision);
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            _isInteract = false;
        }
        private void FixedUpdate()
        {
            isNull = liquidInteract == null;
        }

        private void SetProperty(EWaterProperty property)
        {
            currentPropery = property;
            ActionLiquidProperty?.Invoke(property);
        }

        private void PressF(bool state)
        {
            _isPressF = state;
        }

        private void LiquidPump(Collider2D collision)
        {
            ILiquid i = collision.gameObject.GetComponent<ILiquid>();
            if (i != null && liquidInteract == null)
            {
                liquidInteract = i;
                if (!_isInteract && (currentPropery == EWaterProperty.None || currentPropery == liquidInteract.Property))
                {
                    SetProperty(liquidInteract.Property);
                    StartCoroutine(Pupm());
                } else
                {
                    liquidInteract = null;
                }
            }
        }

        IEnumerator Pupm()
        {
            _isInteract = true;
            Debug.Log($"Pump Start {liquidInteract == null}");
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
            Debug.Log($"Pump End {liquidInteract == null}");
        }

        private void LiquidSqueeze(Collider2D collision)
        {
            ILiquid i = collision.gameObject.GetComponent<ILiquid>();
            if (i != null && liquidInteract == null)
            {
                liquidInteract = i;
                if (!_isInteract && (liquidInteract.Property == EWaterProperty.None || currentPropery == liquidInteract.Property))
                {
                    StartCoroutine(Squeeze());
                } else
                {
                    liquidInteract = null;
                }
            }
        }
        IEnumerator Squeeze()
        {
            _isInteract = true;
            Debug.Log($"Sqeezy Start {liquidInteract == null}");
            while (squareV > 0 && _isInteract)
            {
                if (liquidInteract != null)
                {
                    liquidInteract.Property = currentPropery;
                    if (player != null && squareV > 0 && liquidInteract.SetPropertyWater(currentPropery))
                    {
                        int xS = player.transform.localScale.x > 0 ? 1 : -1;
                        float x = Math.Abs(player.transform.localScale.x) - 0.01f * HeightForce;
                        player.transform.localScale = new Vector3(xS * x, player.transform.localScale.y - 0.01f * HeightForce, player.transform.localScale.z); _playerProperty.ChangeJumpForce -= JumpForceV;
                        squareV -= liquidInteract.Squeeze(ForcePump, squareV);
                    }
                    else
                    {
                        _isInteract = false;
                    }
                    if (Math.Abs(player.transform.localScale.x) < 1 || Math.Abs(player.transform.localScale.y) < 1)
                    {
                        int xS = player.transform.localScale.x > 0 ? 1 : -1;
                        player.transform.localScale = new Vector3(xS * 1,1,1);
                    }
                    if (squareV <= 0f)
                    {
                        squareV = 0f;
                        SetProperty(EWaterProperty.None);
                    }
                }
                yield return new WaitForSeconds(0.5f);
            }
            liquidInteract = null;
            Debug.Log($"Sqeezy End {liquidInteract == null}");
        }
    }
}
