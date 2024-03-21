using UnityEngine;

namespace Game.Scripts.SFX
{
    public class FootstepsSfx : MonoBehaviour
    {
        public void PlaySfx()
        {
           AudioManager.Instance.PlaySfx("Step");
        }
    }
}