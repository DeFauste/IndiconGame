using Assets.Scripts.Input;
using UnityEngine;
using Zenject;
using Assets.Scripts.Liquid;

namespace Assets.Scripts.Player
{
    public class PlayerPumpLiquid : MonoBehaviour
    {
        [SerializeField] LayerMask LiquidLayerMask;
        private IGamePlayInput _gamePlayInput;
        private IPlayerProperty _playerProperty;
        ContactFilter2D contactFilter = new ContactFilter2D();
        private bool _isInteract = false;
        [SerializeField] private Collider2D _playerCollider;

        [Inject]
        public void Construct(IGamePlayInput gamePlayInput)
        {
            _gamePlayInput = gamePlayInput;
        }

        private void Start()
        {
            _playerProperty = GetComponent<IPlayerProperty>();
            _gamePlayInput.OnInteract += SetInteract;
            contactFilter.SetLayerMask(LiquidLayerMask);
            contactFilter.useTriggers = true;
        }


        private void FixedUpdate()
        {
            if (_isInteract)
            {
                LiquidSqueeze();
            }
            LiquidPump();

        }
        private void SetInteract(bool state)
        {
            _isInteract = state;
        }

        private void LiquidPump()
        {
            Collider2D[] collider2Ds = new Collider2D[1];
            int count = _playerCollider.OverlapCollider(contactFilter, collider2Ds);
            if (count > 0)
            {
                Debug.Log("Pump");
                ILiquid i = collider2Ds[0].gameObject.GetComponent<ILiquid>();
                if (i != null)
                {

                }
            }

        }

        private void LiquidSqueeze()
        {
            Debug.Log("Squeeze");
        }

    }
}
