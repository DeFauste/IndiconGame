
using System.ComponentModel;

namespace Assets.Scripts.Player
{
    public interface IPlayerProperty
    {
        [DefaultValue(false)] public bool HaveDoubleJump {get; set;}
        [DefaultValue(false)] public bool HaveSliding {get; set;}
        public float ChangeJumpForce {get; set;}

        public float ChangeMoveSpeed {get; set;}
    }
}
