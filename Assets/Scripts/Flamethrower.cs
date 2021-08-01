using UnityEngine;

[CreateAssetMenu(fileName = "New Flamethrower", menuName = "Weapons/Firearm/Special/Flamethrower")]
public class Flamethrower : Firearm
{
    [SerializeField] private GameObject flame;

    public override void Attack(GameObject owner, Magazine magazine)
    {
        Transform shellpoint = owner.transform.Find("Visual/ShellPoint");
        if(shellpoint.Find("Flame") == null)
        {
            GameObject flame = Instantiate(this.flame, shellpoint);
            flame.transform.localPosition = Vector3.zero;
            flame.name = "Flame";
        }
    }
}