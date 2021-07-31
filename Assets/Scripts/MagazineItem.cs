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
    public void Take(int quantity)
    {
        this.quantity -= quantity;
        if (this.quantity <= 0)
        {
            magazine = null;
            this.quantity = 0;
            ammo = 0;
        }
    }
    public bool Equals(MagazineItem magazine)
    {
        return this.magazine == magazine.magazine && ammo == magazine.ammo;
    }
    public static MagazineItem Empty => new MagazineItem(0, 0, null);
}