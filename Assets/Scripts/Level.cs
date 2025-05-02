using UnityEngine;

public class Level
{
    int LevelNumber;
    string Description;

    int MoneyGot;

    public Level(int n, string d) {
        LevelNumber = n;
        Description = d;
        MoneyGot = 0;
    }

    public Level(int n, int m) {
        LevelNumber = n;
        Description = "";
        MoneyGot = m;
    }

    public int getNumber() { return LevelNumber; }
    public string getDescription() { return Description; }

    public void setMoney(int x) { MoneyGot = x; }
    public int getMoney() { return MoneyGot; }

    public bool Equals(object obj) {
        Level l = obj as Level;

        return (LevelNumber == l.LevelNumber);
    }
}
