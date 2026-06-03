using UnityEngine;

public class CharacterProceduralRecoil : MonoBehaviour
{
    [Header("Bones")] 
    [SerializeField] private Transform spineBone;

    private Vector3 _currentRecoil;
    private Vector3 _targetRecoil;

    private float _currentSnappiness;
    private float _currentReturnSpeed;

    private void Start()
    {
        if (!spineBone) Debug.LogError("[CharacterProceduralRecoil] No Spine bone found! Assign in editor");
    }

    private void LateUpdate()
    {
        float returnSpeed = _currentReturnSpeed > 0 ? _currentReturnSpeed : 10f;
        float snappiness = _currentSnappiness > 0 ? _currentSnappiness : 25f;
        
        _targetRecoil = Vector3.Lerp(_targetRecoil, Vector3.zero, returnSpeed * Time.deltaTime);
        _currentRecoil = Vector3.Lerp(_currentRecoil, _targetRecoil, snappiness * Time.deltaTime);

        spineBone.localRotation *= Quaternion.Euler(_currentRecoil);
    }

    public void ApplyWeaponRecoil(Vector3 recoilKick, float snappiness, float returnSpeed)
    {
        _targetRecoil += recoilKick;
        _currentSnappiness = snappiness;
        _currentReturnSpeed = returnSpeed;
    }
}
