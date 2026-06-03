using MoreMountains.Tools;
using MoreMountains.TopDownEngine;
using UnityEngine;

public class WeaponProceduralRecoil : MonoBehaviour, MMEventListener<MMStateChangeEvent<Weapon.WeaponStates>>
{
    [Header("Weapon Specific Recoil")] [Tooltip("Rotation force per shot")] 
    [SerializeField] private Vector3 recoilKick = new Vector3(-10f, 0f, 0f);
    [SerializeField] private float snappiness = 25f;
    [SerializeField] private float returnSpeed = 10f;

    private Weapon _weapon;
    private CharacterProceduralRecoil _characterProceduralRecoil;

    private void Awake()
    {
        _weapon = GetComponent<Weapon>();
        
        if (!_weapon) Debug.LogError("[WeaponProceduralRecoil] Couldn't get Weapon component.");
    }

    public void OnMMEvent(MMStateChangeEvent<Weapon.WeaponStates> weaponEvent)
    {
        if (weaponEvent.Target != this.gameObject) return;

        if (weaponEvent.NewState == Weapon.WeaponStates.WeaponUse)
        {
            TriggerRecoilOnOwner();
        }
    }

    private void TriggerRecoilOnOwner()
    {
        if (!_weapon || !_weapon.Owner) return;

        if (!_characterProceduralRecoil)
        {
            _characterProceduralRecoil = _weapon.Owner.GetComponentInChildren<CharacterProceduralRecoil>();
        }

        if (_characterProceduralRecoil)
        {
            _characterProceduralRecoil.ApplyWeaponRecoil(recoilKick, snappiness, returnSpeed);
        }
    }

    private void OnEnable()
    {
        this.MMEventStartListening();
    }

    private void OnDisable()
    {
        this.MMEventStopListening();
    }
}
