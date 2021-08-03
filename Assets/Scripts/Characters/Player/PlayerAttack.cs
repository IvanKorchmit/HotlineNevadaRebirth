using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Inventory inventory;
    private Transform shellPoint;
    private Transform rightHand;
    private Transform leftHand;
    [SerializeField] private MagazineItem oldMagazine;
    private void Start()
    {
        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
        shellPoint = transform.Find("Visual/ShellPoint");
        rightHand = transform.Find("Visual/RightHand/Visual");
        leftHand = transform.Find("Visual/LeftHand/Visual");
    }
    private void Update()
    {
        if(!inventory.SecondaryWeapon.isNone() && !animator.GetCurrentAnimatorStateInfo(0).IsName("Akimbo"))
        {
            animator.Play("Akimbo");
        }
        else if (inventory.SecondaryWeapon.isNone() && animator.GetCurrentAnimatorStateInfo(0).IsName("Akimbo"))
        {
            animator.Play("Neutral");
        }

        if (!inventory.PrimaryWeapon.isNone())
        {
            animator.SetInteger("Weapon", inventory.PrimaryWeapon.WeaponBase.ID);
        }
        else
        {
            animator.SetInteger("Weapon", 0);
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (inventory.SecondaryWeapon.isNone())
            {
                if (inventory.PrimaryWeapon.isNone() || inventory.PrimaryWeapon.WeaponBase is Melee)
                {
                    animator.SetBool("Attack", true);
                }
                else if (inventory.PrimaryWeapon.WeaponBase is Firearm)
                {
                    if (inventory.PrimaryWeapon.Ammo > 0)
                    {
                        animator.SetBool("Attack", true);
                    }
                    else
                    {
                        Transform flame = shellPoint.Find("Flame");
                        if (flame != null)
                        {
                            flame.GetComponent<ParticleSystem>().Stop();
                            flame.SetParent(null);
                        }
                    }
                }
            }
            else if (!inventory.PrimaryWeapon.isNone())
            {
                if (inventory.PrimaryWeapon.Ammo > 0)
                {
                    rightHand.GetComponent<Animator>().SetBool("Attack", true);
                    rightHand.GetComponent<Animator>().SetInteger("Weapon", inventory.PrimaryWeapon.WeaponBase.ID);
                }
                else
                {
                    rightHand.GetComponent<Animator>().SetBool("Attack", false);
                }
            }
        }
        else
        {
            Transform flame = shellPoint.Find("Flame");
            if (flame != null)
            {
                flame.GetComponent<ParticleSystem>().Stop();
                flame.SetParent(null);
            }
            rightHand.GetComponent<Animator>().SetBool("Attack", false);
            animator.SetBool("Attack", false);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (!inventory.SecondaryWeapon.isNone())
            {
                if (inventory.SecondaryWeapon.Ammo > 0)
                {
                    rightHand.GetComponent<Animator>().SetBool("Attack", true);
                    rightHand.GetComponent<Animator>().SetInteger("Weapon", inventory.SecondaryWeapon.WeaponBase.ID);
                }
                else
                {
                    rightHand.GetComponent<Animator>().SetBool("Attack", false);
                }
            }
        }
        else
        {
            rightHand.GetComponent<Animator>().SetBool("Attack", false);
        }
        if (Input.GetKeyDown(KeyCode.R) && !inventory.PrimaryWeapon.isNone() && inventory.PrimaryWeapon.WeaponBase is Firearm)
        {
            MagazineItem magazine = inventory.FindMagazine(inventory.PrimaryWeapon.MagazineBase);

            if (!inventory.PrimaryWeapon.isNone() && inventory.PrimaryWeapon.WeaponBase is Firearm fa)
            {
                if (magazine != null && magazine.magazine != null)
                {
                    magazine = magazine.Copy();
                    oldMagazine = inventory.PrimaryWeapon.MagazineBase.Copy();
                    inventory.PrimaryWeapon.Magazine = magazine.magazine;
                    animator.SetBool("Reloading", true);
                    if (inventory.hasThisMagazine(magazine.magazine))
                    {
                        if (fa.RequiresMagazine)
                        {
                            inventory.PrimaryWeapon.Ammo = magazine.ammo;
                        }
                    }
                }
                else
                {
                    oldMagazine = inventory.PrimaryWeapon.MagazineBase.Copy();
                    magazine = inventory.FindAllowedMagazines(fa);
                    if (magazine != null)
                    {
                        magazine = magazine.Copy();
                        inventory.PrimaryWeapon.Magazine = magazine.magazine;
                        animator.SetBool("Reloading", true);
                        if (fa.RequiresMagazine)
                        {
                            inventory.PrimaryWeapon.Ammo = magazine.ammo;
                        }
                    }
                }
            }
        }
    }
    public void Attack()
    {
        inventory.PrimaryWeapon.Shoot(gameObject);
    }
    
    public void Eject()
    {
        if (!inventory.PrimaryWeapon.isNone() && inventory.PrimaryWeapon.WeaponBase is Firearm f)
        {
            Rigidbody2D shell = Instantiate(f.ShellType, shellPoint.position, shellPoint.rotation * Quaternion.Euler(0, 0, Random.Range(-30, 30))).GetComponent<Rigidbody2D>();
            shell.velocity = -shell.transform.up * (14 + Random.Range(-4, 12));
            shell.angularVelocity = Random.Range(0, 360);   
        }   
    }
    public void ResetAnimation()
    {
        Firearm firearm = inventory.PrimaryWeapon.WeaponBase as Firearm;
        if (firearm != null && inventory.PrimaryWeapon.Ammo < firearm.AmmoCapacity)
        {
            int id = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
            animator.Play(id, 0, 0);
            inventory.PrimaryWeapon.Ammo++;
            inventory.TakeItem(inventory.PrimaryWeapon.Magazine, 1);
            return;
        }
        animator.SetBool("Reloading", false);
    }
    public void EjectMagazine()
    {
        inventory.TakeItem(inventory.PrimaryWeapon.Magazine, 1);
        // Throwing magazine
        GameObject magazine = Instantiate(PrefabsStatic.Magazine, shellPoint.position, shellPoint.rotation * Quaternion.Euler(0, 0, Random.Range(-30, 30)));
        Rigidbody2D rb = magazine.GetComponent<Rigidbody2D>();
        rb.velocity = magazine.transform.up * (14 + Random.Range(-4, 12));
        magazine.GetComponent<SpriteRenderer>().sprite = oldMagazine.ammo == 0 ? oldMagazine.magazine.Empty : oldMagazine.magazine.Sprite;
        rb.angularVelocity = Random.Range(0, 360);
        magazine.GetComponent<MagazineLand>().magazine = oldMagazine;
        animator.SetBool("Reloading", false);
    }
}
