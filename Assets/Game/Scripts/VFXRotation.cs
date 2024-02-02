using UnityEngine;

public class VFXRotation : MonoBehaviour
{
    [SerializeField] private ParticleSystem _marks;
    [SerializeField] private Transform _heroTransform;

    private void Update()
    {
        UpdateParticleRotation();
    }

    private void UpdateParticleRotation()
    {
        var currentRotation = _heroTransform.eulerAngles.y;
        currentRotation *= Mathf.Deg2Rad;
        var particleMain = _marks.main;
        particleMain.startRotation = currentRotation;
    }
}