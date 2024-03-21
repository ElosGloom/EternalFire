using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.SFX
{
    [RequireComponent(typeof(Button))]
    public class ButtonSfx : MonoBehaviour
    {
        [SerializeField] private string sfxPath;
        [SerializeField] private float maxPitchRange;

        private void Awake()
        {
             GetComponent<Button>().onClick.AddListener(() => AudioManager.Instance.PlaySfx("Click"));
        }
    }
}