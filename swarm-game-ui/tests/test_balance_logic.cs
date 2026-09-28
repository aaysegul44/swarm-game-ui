/*
 * Host (PC) testi: GameBalanceData ve VehicleSelectionBar mantiginin,
 * denge_tablosu.xlsx'teki gercek sayilarla dogru calistigini kanitlar.
 * Ozellikle "Oz-Dayaniklilik = Can / Hasar" hesabinin, Excel'deki
 * "Esleme Matrisi (TTK)" sekmesindeki degerlerle eslesip eslesmedigi
 * kontrol edilir.
 */
using System;
using UnityEngine;
using UnityEngine.UI;

class TestRunner
{
    static int failCount = 0;

    static void AssertNear(string desc, double actual, double expected, double tol)
    {
        bool ok = Math.Abs(actual - expected) <= tol;
        Console.WriteLine($"  [{(ok ? "OK" : "FAIL")}] {desc,-55} beklenen={expected:F4} hesaplanan={actual:F4}");
        if (!ok) failCount++;
    }

    static void Main()
    {
        Console.WriteLine("=== Test 1: GameBalanceData - denge_tablosu.xlsx ile karsilastirma ===");
        var balance = ScriptableObject.CreateInstance<GameBalanceData>();

        // denge_tablosu.xlsx "Denge Tablosu" sayfasindaki degerler
        AssertNear("IKA Can (HP)", balance.ika.maxHealth, 120, 0.001);
        AssertNear("IKA Hasar (DPS)", balance.ika.damagePerSec, 15, 0.001);
        AssertNear("IHA Can (HP)", balance.iha.maxHealth, 60, 0.001);
        AssertNear("IDA Hasar (DPS)", balance.ida.damagePerSec, 18, 0.001);

        // Excel'deki "Oz-Dayaniklilik" sutunuyla karsilastirma
        AssertNear("IKA Oz-Dayaniklilik (Can/Hasar)", balance.ika.SelfSustain, 8.0, 0.001);
        AssertNear("IHA Oz-Dayaniklilik (Can/Hasar)", balance.iha.SelfSustain, 6.0, 0.001);
        AssertNear("IDA Oz-Dayaniklilik (Can/Hasar)", balance.ida.SelfSustain, 5.5556, 0.001);

        Console.WriteLine("\n=== Test 2: VehicleSelectionBar - secim mantigi ===");
        var ikaBtn = new Button();
        var ihaBtn = new Button();
        var idaBtn = new Button();

        var bar = new VehicleSelectionBarTestable();
        bar.SetUp(balance, ikaBtn, ihaBtn, idaBtn);

        VehicleType? selected = null;
        GameBalanceData.UnitStats selectedStats = new GameBalanceData.UnitStats();
        bar.OnVehicleSelected += (type, stats) => { selected = type; selectedStats = stats; };

        bar.InvokeAwakeAndStart();
        Console.WriteLine($"  Varsayilan secim: {selected} (beklenen: IKA)");
        if (selected != VehicleType.IKA) failCount++;

        ihaBtn.onClick.Invoke();
        Console.WriteLine($"  IHA butonuna basildi -> secim: {selected}, HP={selectedStats.maxHealth}");
        if (selected != VehicleType.IHA || selectedStats.maxHealth != 60) failCount++;

        idaBtn.onClick.Invoke();
        Console.WriteLine($"  IDA butonuna basildi -> secim: {selected}, HP={selectedStats.maxHealth}");
        if (selected != VehicleType.IDA || selectedStats.maxHealth != 100) failCount++;

        Console.WriteLine();
        Console.WriteLine(failCount == 0 ? "[TUM TESTLER BASARILI]" : $"[{failCount} TEST BASARISIZ]");
        Environment.Exit(failCount == 0 ? 0 : 1);
    }
}

/* VehicleSelectionBar private [SerializeField] alanlarina test icin
   erisim saglayan, gercek davranisi degistirmeyen bir test alt sinifi. */
class VehicleSelectionBarTestable : VehicleSelectionBar
{
    public void SetUp(GameBalanceData balance, Button ika, Button iha, Button ida)
    {
        typeof(VehicleSelectionBar).GetField("balanceData",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(this, balance);
        typeof(VehicleSelectionBar).GetField("ikaButton",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(this, ika);
        typeof(VehicleSelectionBar).GetField("ihaButton",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(this, iha);
        typeof(VehicleSelectionBar).GetField("idaButton",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(this, ida);
    }

    public void InvokeAwakeAndStart()
    {
        typeof(VehicleSelectionBar).GetMethod("Awake",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(this, null);
        typeof(VehicleSelectionBar).GetMethod("Start",
            System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .Invoke(this, null);
    }
}
