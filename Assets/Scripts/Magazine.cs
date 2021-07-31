using UnityEngine;

[CreateAssetMenu(fileName = "New Generic Magazine", menuName = "Ammo/Generic")]
public class Magazine : ScriptableObject
{
    [SerializeField] private int stack;
    [SerializeField] private new string name;
    [SerializeField] private int damage;
    [SerializeField] private Sprite sprite;
    [SerializeField] private Sprite empty;
    [SerializeField] private int ammoCapacity;
    public int AmmoCapacity => ammoCapacity;
    public int Damage => damage;
    public string Name => name;
    public int Stack => stack;
    public Sprite Sprite => sprite;
    public Sprite Empty => empty;
}
