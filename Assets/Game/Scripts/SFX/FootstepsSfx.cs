using UnityEngine;

namespace Game.Scripts.SFX
{
    public class FootstepsSfx : MonoBehaviour
    {
        public void PlaySfx()
        {
            SfxController.PlaySfx("event:/Step", 0.2f);
        }
    }
}