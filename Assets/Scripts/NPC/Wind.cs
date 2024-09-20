using UnityEngine;

namespace Assets.LoopBuild.Scripts.NPC
{
    public class Wind : MonoBehaviour, IInteract
    {
        private readonly int onOfhashAnim = Animator.StringToHash("OnOff"); 
        [SerializeField] private Vector2 velocity = Vector2.zero;
        [SerializeField] private float scalePush = 1.3f;
        [SerializeField] private Animator animator;
        Collider2D _collision;
        Rigidbody2D _rb;
        public bool OnOff { get; private set; } = true;
        private void Start()
        {
            OnWind();
        }
        private void OnWind()
        {
            if (animator != null)
            {
                animator.SetBool(onOfhashAnim, OnOff);
            }
        }
        private void OnTriggerStay2D(Collider2D collision)
        {


            if (_collision == null && _collision != collision)
            {
                _rb = collision.gameObject.GetComponent<Rigidbody2D>();

            }
            if (_rb != null)
            {
                float influence = collision.gameObject.transform.localScale.x;

                if (influence < scalePush)
                {
                    _rb.AddForce(velocity, ForceMode2D.Force);
                }
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            _collision = null;
            _rb = null;
        }

        public void Interact()
        {
            OnOff = !OnOff;
            OnWind();
        }
    }
}
