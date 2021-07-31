using UnityEngine;

[CreateAssetMenu(fileName = "New Buckshot", menuName = "Ammo/Shells/Buckshot")]
public class Buckshot : Magazine
{
    [SerializeField] private int pellets;
    public int Pellets => pellets;
}