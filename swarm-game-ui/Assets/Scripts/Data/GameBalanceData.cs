using UnityEngine;

/// <summary>
/// Ekibin ortak denge tablosunun (Excel) Unity icindeki tek dogru kaynagi.
/// Milestone 1: "3 temel bina ve 3 otonom arac icin Can, Hasar ve Hiz
/// istatistiklerinin belirlenmesi" gorevinin, ekibin geri kalaninin da
/// (AI/state machine, backend) okuyabilecegi paylasilan bir veri yapisina
/// donusturulmus hali.
///
/// Sayilar dogrudan "denge_tablosu.xlsx" dosyasindaki degerlerdir; tablo
/// guncellendikce buradaki degerler de elle senkronize edilmelidir.
/// </summary>
[CreateAssetMenu(fileName = "GameBalanceData", menuName = "Swarm/Game Balance Data")]
public class GameBalanceData : ScriptableObject
{
    [System.Serializable]
    public struct UnitStats
    {
        public string displayName;
        public int maxHealth;      // Can (HP)
        public float damagePerSec; // Hasar (DPS)
        public float moveSpeed;    // Hiz (birim/sn)
        public float attackRange;  // Menzil (birim)

        // Oz-Dayaniklilik = Can / Hasar (kendi hasarina gore, hizli
        // karsilastirma amacli - gercek 1v1 sure icin Esleme Matrisi kullanilir)
        public float SelfSustain => damagePerSec > 0f ? maxHealth / damagePerSec : Mathf.Infinity;
    }

    [Header("Otonom Araclar (denge_tablosu.xlsx - 'Denge Tablosu' sayfasi)")]
    public UnitStats ika = new UnitStats
    {
        displayName = "IKA",
        maxHealth = 120,
        damagePerSec = 15f,
        moveSpeed = 3f,
        attackRange = 4f
    };

    public UnitStats iha = new UnitStats
    {
        displayName = "IHA",
        maxHealth = 60,
        damagePerSec = 10f,
        moveSpeed = 7f,
        attackRange = 6f
    };

    public UnitStats ida = new UnitStats
    {
        displayName = "IDA",
        maxHealth = 100,
        damagePerSec = 18f,
        moveSpeed = 4f,
        attackRange = 5f
    };

    [Header("Binalar")]
    public UnitStats anaKale = new UnitStats
    {
        displayName = "Ana Kale",
        maxHealth = 500,
        damagePerSec = 0f,
        moveSpeed = 0f,
        attackRange = 0f
    };

    public UnitStats savunmaKulesi = new UnitStats
    {
        displayName = "Savunma Kulesi",
        maxHealth = 150,
        damagePerSec = 12f,
        moveSpeed = 0f,
        attackRange = 8f
    };

    /// <summary>Secilen arac tipine gore istatistik dondurur (UI ve AI ayni yerden okur).</summary>
    public UnitStats GetStats(VehicleType type)
    {
        switch (type)
        {
            case VehicleType.IKA: return ika;
            case VehicleType.IHA: return iha;
            case VehicleType.IDA: return ida;
            default: throw new System.ArgumentOutOfRangeException(nameof(type));
        }
    }
}

public enum VehicleType { IKA, IHA, IDA }
