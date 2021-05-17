using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Controller : MonoBehaviour
{
    private GameObject _objWeapon;
    private Animator _weapAnimator;

    private bool _usingWeapon = false;
    private string _weaponName;

    private bool _isTuching = false;



    private void Start()
    {
        _weapAnimator = GetComponent<Animator>();


    }

    private void FixedUpdate()
    {
        if (_usingWeapon && Input.GetKeyDown(KeyCode.G))
        {
            _weapAnimator.SetTrigger("Put Down");
        }

        else if (!_usingWeapon && Input.GetKeyDown(KeyCode.F) && _isTuching)
        {
            _weapAnimator.SetTrigger("Pick Up");
        }

    }
    // Animation 
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _usingWeapon && _objWeapon.name == "Shield")
        {
            _weapAnimator.SetTrigger("Shield Attack");
        }
        else if (Input.GetMouseButtonDown(0) && _usingWeapon && _objWeapon.name == "Sword")
        {
            _weapAnimator.SetTrigger("Sword Attack");
        }
        else if (Input.GetMouseButtonDown(0) && _usingWeapon && _objWeapon.name == "Axe")
        {
            _weapAnimator.SetTrigger("Axe Attack");
        }
    }


    // Invoked in animation
    private void TakeWeapon()
    {
        _objWeapon = GameObject.Find(_weaponName);
        _objWeapon.transform.parent = _objWeapon.GetComponent<WeaponPossition>().Hand.transform;
        _objWeapon.transform.localPosition = _objWeapon.GetComponent<WeaponPossition>().PickPosition;
        _objWeapon.transform.localEulerAngles = _objWeapon.GetComponent<WeaponPossition>().PickRotation;
        _usingWeapon = true;
        _isTuching = false;
        _weapAnimator.ResetTrigger("Pick Up");
    }
    // Invoked in animation
    private void DropWeapon()
    {

        _objWeapon.transform.parent = null;
        _objWeapon.transform.localPosition = new Vector3(_objWeapon.transform.position.x, 1, _objWeapon.transform.position.z);
        _objWeapon.transform.localEulerAngles = new Vector3(-90, _objWeapon.transform.position.y, _objWeapon.transform.position.z);
        _usingWeapon = false;

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            _isTuching = true;
            _weaponName = other.gameObject.name;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            _isTuching = false;
        }
    }

}
