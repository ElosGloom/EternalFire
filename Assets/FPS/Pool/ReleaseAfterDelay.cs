using System.Collections;
using UnityEngine;

namespace FPS.Pool
{
    public class ReleaseAfterDelay : MonoBehaviour
    {
        [SerializeField] private Poolable poolable;
        [SerializeField] private float secondsBeforeDelay;

        public void OnEnable()
        {
            StartCoroutine(ReleaseRoutine());
           
        }
        
        private IEnumerator ReleaseRoutine()
        {
            yield return new WaitForSeconds(secondsBeforeDelay);
            poolable.Release();
        }
    }
}