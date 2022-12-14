using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    public GameObject[] loadout;
    public Transform weaponParent;
    public GameObject currentEquipment;
    public int aimSpeed = 20;
    public int damage;
    public int maxAmmo;
    public int currentAmmo;
    public float reloadTime;
    
    public TextMeshProUGUI ammoText;

    AudioSource _audioSource;
    public AudioClip gunSound;
    public AudioClip reloadSound;
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        GameObject t_newEquipment = Instantiate(loadout[0], weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        t_newEquipment.transform.localPosition = Vector3.zero;
        t_newEquipment.transform.localEulerAngles = Vector3.zero;
        currentEquipment = t_newEquipment;
        if(currentEquipment.CompareTag("Pistol")){
            maxAmmo = 10;
            currentAmmo = maxAmmo;
            damage = 5;
            reloadTime = 1f;
        }
        else if(currentEquipment.CompareTag("LMG")){
            maxAmmo = 25;
            currentAmmo = maxAmmo;
            damage = 15;
            reloadTime = 2f;
        }
        ammoText.text = currentAmmo + "/" + maxAmmo;


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)){
            Equip(0);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            Equip(1);
        }
        Aim(Input.GetMouseButton(1));
    }

    public void Shoot()
    {
        if(currentAmmo > 0){
            _audioSource.PlayOneShot(gunSound);
            currentAmmo--;
            ammoText.text = currentAmmo + "/" + maxAmmo;
        }
        else{

            StartCoroutine(Reload());
        }
    }

    IEnumerator Reload()
    {           
        _audioSource.PlayOneShot(reloadSound, 2);
        ammoText.text = "RELOADING";
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }
    void Aim(bool p_isAiming)
    {
        Transform t_anchor = currentEquipment.transform.Find("Anchor");
        Transform t_state_ads = currentEquipment.transform.Find("State/ADS/Anchor");
        Transform t_state_hip = currentEquipment.transform.Find("State/Hip");
        if(p_isAiming){
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_ads.position, Time.deltaTime * aimSpeed);
        }
        else{
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_state_hip.position, Time.deltaTime * aimSpeed);            
        }
    }
    void Equip(int p_ind)
    {   
        if(currentEquipment != null){
            Destroy(currentEquipment);
        }
        GameObject t_newEquipment = Instantiate(loadout[p_ind], weaponParent.position, weaponParent.rotation, weaponParent) as GameObject;
        t_newEquipment.transform.localPosition = Vector3.zero;
        t_newEquipment.transform.localEulerAngles = Vector3.zero;
        currentEquipment = t_newEquipment;        
        if(currentEquipment.CompareTag("Pistol")){
            maxAmmo = 10;
            currentAmmo = maxAmmo;
            damage = 5;
            reloadTime = 1f;
        }
        else if(currentEquipment.CompareTag("LMG")){
            maxAmmo = 25;
            currentAmmo = maxAmmo;
            damage = 15;
            reloadTime = 2f;
        }
        ammoText.text = currentAmmo + "/" + maxAmmo;
    }
}
