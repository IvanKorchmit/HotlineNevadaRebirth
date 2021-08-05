using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Akimbo : MonoBehaviour
{
    private Inventory inventory;
    private bool isRight;
    private Transform shellPoint;
    private void Start()
    {
        inventory = GetComponentInParent<Inventory>();
        isRight = transform.parent.name == "RightHand";
        shellPoint = transform.Find("ShellPoint");
    }
    public void Attack()
    {
        if(isRight)
        {
            inventory.SecondaryWeapon.Shoot(transform.parent.gameObject);
        }
        else
        {
            inventory.PrimaryWeapon.Shoot(transform.parent.gameObject);
        }
    }
    public void PlaySound(AudioClip audio)
    {
        SoundStatic.PlaySound(audio);
    }
    public void Eject()
    {
        if (!isRight)
        {
            if (!inventory.PrimaryWeapon.isNone() && inventory.PrimaryWeapon.WeaponBase is Firearm f)
            {
                Rigidbody2D shell = Instantiate(f.ShellType, shellPoint.position, shellPoint.rotation * Quaternion.Euler(0, 0, Random.Range(-30, 30))).GetComponent<Rigidbody2D>();
                shell.velocity = -shell.transform.up * (14 + Random.Range(-4, 12));
                shell.angularVelocity = Random.Range(0, 360);
            }
        }
        else
        {
            if (!inventory.SecondaryWeapon.isNone() && inventory.SecondaryWeapon.WeaponBase is Firearm f)
            {
                Rigidbody2D shell = Instantiate(f.ShellType, shellPoint.position, shellPoint.rotation * Quaternion.Euler(0, 0, Random.Range(-30, 30))).GetComponent<Rigidbody2D>();
                shell.velocity = -shell.transform.up * (14 + Random.Range(-4, 12));
                shell.angularVelocity = Random.Range(0, 360);
            }
        }
    }
}
