[System.Serializable]
public class MagazineItem
{
    public Magazine magazine;
    public int ammo;
    public int quantity;
    public MagazineItem Copy()
    {
        return new MagazineItem(ammo,quantity,magazine);
    }
    public MagazineItem(int ammo, int quantity, Magazine magazine)
    {
        this.magazine = magazine;
        this.ammo = ammo;
        this.quantity = quantity;
    }
    public void Take(int quantity, bool isAmmo = false)
    {
        if (!isAmmo)
        {
            this.quantity -= quantity;
            if (this.quantity <= 0)
            {
                magazine = null;
                this.quantity = 0;
                ammo = 0;
            }
        }
        else
        {
            ammo -= quantity;
            if (ammo <= 0)
            {
                ammo = magazine.AmmoCapacity;
                Take(1, false);
            }
        }
    }
    public bool Equals(MagazineItem magazine)
    {
        return this.magazine == magazine.magazine && ammo == magazine.ammo;
    }
    public static MagazineItem Empty => new MagazineItem(0, 0, null);
}