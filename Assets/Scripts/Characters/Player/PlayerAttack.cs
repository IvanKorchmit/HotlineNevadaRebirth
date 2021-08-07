using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private Inventory inventory;
    private Transform shellPoint;
    private Animator rightHand;
    private Animator leftHand;
    [SerializeField] private MagazineItem oldMagazine;
    private bool reloadAkimbo;
    private bool isSecondReloading;
    private void Start()
    {
        animator = GetComponent<Animator>();
        inventory = GetComponent<Inventory>();
        shellPoint = transform.Find("Visual/ShellPoint");
        rightHand = transform.Find("Visual/RightHand/Visual").GetComponent<Animator>();
        leftHand = transform.Find("Visual/LeftHand/Visual").GetComponent<Animator>();
    }
    private void Update()
    {
        if ((!inventory.SecondaryWeapon.isNone() && !inventory.PrimaryWeapon.isNone()) && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Reload"))
        {
            rightHand.SetInteger("Weapon", inventory.SecondaryWeapon.WeaponBase.ID);
            leftHand.SetInteger("Weapon", inventory.PrimaryWeapon.WeaponBase.ID);
            rightHand.gameObject.SetActive(true);
            leftHand.gameObject.SetActive(true);
        }
        else
        {
            rightHand.gameObject.SetActive(false);
            leftHand.gameObject.SetActive(false);
        }
        if (!inventory.SecondaryWeapon.isNone() && !animator.GetCurrentAnimatorStateInfo(0).IsName("Akimbo") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Reload"))
        {
            if (!animator.GetBool("Reloading") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Reload"))
            {
                animator.Play("Akimbo");
            }
        }
        else if (inventory.SecondaryWeapon.isNone() && animator.GetCurrentAnimatorStateInfo(0).IsName("Akimbo") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Reload"))
        {
            if (!animator.GetBool("Reloading") && !animator.GetCurrentAnimatorStateInfo(0).IsTag("Reload"))
            {
                animator.Play("Neutral");
            }
        }

        if (!inventory.PrimaryWeapon.isNone() && !reloadAkimbo)
        {
            animator.SetInteger("Weapon", inventory.PrimaryWeapon.WeaponBase.ID);
        }
        else if (inventory.PrimaryWeapon.isNone())
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
                    leftHand.SetBool("Attack", true);
                }
                else
                {
                    leftHand.SetBool("Attack", false);
                    Transform flame = leftHand.transform.Find("ShellPoint/Flame");
                    if (flame != null)
                    {
                        flame.GetComponent<ParticleSystem>().Stop();
                        flame.SetParent(null);
                    }
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
            flame = leftHand.transform.Find("ShellPoint/Flame");
            if (flame != null)
            {
                flame.GetComponent<ParticleSystem>().Stop();
                flame.SetParent(null);
            }
            leftHand.SetBool("Attack", false);
            animator.SetBool("Attack", false);
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (!inventory.SecondaryWeapon.isNone())
            {
                if (inventory.SecondaryWeapon.Ammo > 0)
                {
                    rightHand.SetBool("Attack", true);
                }
                else
                {
                    rightHand.SetBool("Attack", false);
                    Transform flame = rightHand.transform.Find("ShellPoint/Flame");
                    if (flame != null)
                    {
                        flame.GetComponent<ParticleSystem>().Stop();
                        flame.SetParent(null);
                    }
                }
            }
        }
        else
        {
            rightHand.SetBool("Attack", false);
            Transform flame = rightHand.transform.Find("ShellPoint/Flame");
            if (flame != null)
            {
                flame.GetComponent<ParticleSystem>().Stop();
                flame.SetParent(null);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    public void Attack()
    {
        inventory.PrimaryWeapon.Shoot(gameObject);
    }
    private void Reload()
    {
        if (inventory.SecondaryWeapon.isNone())
        {
            if (!inventory.PrimaryWeapon.isNone() && inventory.PrimaryWeapon.WeaponBase is Firearm)
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
                        if (!fa.RequiresMagazine && inventory.PrimaryWeapon.Ammo > 0)
                        {
                            return;
                        }
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
        else
        {
            reloadAkimbo = true;
            isSecondReloading = false;
            transform.Find("Visual").GetComponent<SpriteRenderer>().flipY = isSecondReloading;
            oldMagazine = inventory.PrimaryWeapon.MagazineBase.Copy();
            animator.SetBool("Reloading", true);
            animator.Play("Neutral");
            if (inventory.PrimaryWeapon.WeaponBase is Firearm fPrimary && !fPrimary.RequiresMagazine && inventory.PrimaryWeapon.Ammo == 0)
            {
                MagazineItem magazine = inventory.FindAllowedMagazines(fPrimary);
                if (magazine != null)
                {
                    inventory.PrimaryWeapon.Magazine = magazine.magazine;
                }
            }
        }
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
        if (!reloadAkimbo)
        {
            Firearm firearm = inventory.PrimaryWeapon.WeaponBase as Firearm;
            if (firearm != null && inventory.PrimaryWeapon.Ammo < firearm.AmmoCapacity && 
                ( inventory.hasThisMagazine(inventory.PrimaryWeapon.Magazine) || inventory.FindAllowedMagazines(inventory.PrimaryWeapon.WeaponBase as Firearm) != null))
            {
                int id = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
                animator.Play(id, 0, 0);
                inventory.PrimaryWeapon.Ammo++;
                inventory.TakeItem(inventory.PrimaryWeapon.Magazine, 1, true);
                return;
            }
            animator.SetBool("Reloading", false);
        }
        else
        {
            if(!isSecondReloading)
            {
                Firearm firearm = inventory.PrimaryWeapon.WeaponBase as Firearm;
                if (firearm != null && inventory.PrimaryWeapon.Ammo < firearm.AmmoCapacity &&
                inventory.hasThisMagazine(inventory.PrimaryWeapon.Magazine))
                {
                    int id = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
                    animator.Play(id, 0, 0);
                    inventory.PrimaryWeapon.Ammo++;
                    inventory.TakeItem(inventory.PrimaryWeapon.Magazine, 1, true);
                    return;
                }
            }
            else
            {
                Firearm firearm = inventory.SecondaryWeapon.WeaponBase as Firearm;
                if (firearm != null && inventory.SecondaryWeapon.Ammo < firearm.AmmoCapacity &&
                inventory.hasThisMagazine(inventory.SecondaryWeapon.Magazine))
                {
                    int id = animator.GetCurrentAnimatorStateInfo(0).fullPathHash;
                    animator.Play(id, 0, 0);
                    inventory.SecondaryWeapon.Ammo++;
                    inventory.TakeItem(inventory.SecondaryWeapon.Magazine, 1, true);
                    return;
                }
            }
        }
    }
    public void EjectMagazine()
    {
        inventory.TakeItem(!isSecondReloading ? inventory.PrimaryWeapon.Magazine : inventory.SecondaryWeapon.Magazine, 1);
        // Throwing magazine
        GameObject magazine = Instantiate(PrefabsStatic.Magazine, shellPoint.position, shellPoint.rotation * Quaternion.Euler(0, 0, Random.Range(-30, 30)));
        Rigidbody2D rb = magazine.GetComponent<Rigidbody2D>();
        rb.velocity = !isSecondReloading ? magazine.transform.up * (14 + Random.Range(-4, 12)) : -magazine.transform.up * (14 + Random.Range(-4, 12));
        magazine.GetComponent<SpriteRenderer>().sprite = oldMagazine.ammo == 0 ? oldMagazine.magazine.Empty : oldMagazine.magazine.Sprite;
        rb.angularVelocity = Random.Range(0, 360);
        magazine.GetComponent<MagazineLand>().magazine = oldMagazine;
        animator.SetBool("Reloading", reloadAkimbo);
    }
    public void CheckReloading()
    {
        if (reloadAkimbo)
        {
            isSecondReloading = !isSecondReloading;
            transform.Find("Visual").GetComponent<SpriteRenderer>().flipY = isSecondReloading;
            if (!isSecondReloading)
            {
                reloadAkimbo = false;
                animator.SetBool("Reloading", false);
                if (inventory.SecondaryWeapon.WeaponBase is Firearm fSecondary && !fSecondary.RequiresMagazine && inventory.SecondaryWeapon.Ammo == 0)
                {
                    MagazineItem magazine = inventory.FindAllowedMagazines(fSecondary);
                    if (magazine != null)
                    {
                        inventory.SecondaryWeapon.Magazine = magazine.magazine;
                    }
                }
            }
            else
            {
                animator.SetInteger("Weapon", inventory.SecondaryWeapon.WeaponBase.ID);
                animator.Play("Neutral");
            }
        }
    }
}
