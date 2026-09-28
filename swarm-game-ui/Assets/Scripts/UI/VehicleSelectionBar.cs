using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ekranin altindaki IKA / IHA / IDA secim cubugu (Milestone 2:
/// "Oyuncunun sahaya surecegi araci secebilecegi basit, fonksiyonel
/// butonlarin eklenmesi").
///
/// Secilen arac tipini disariya (Cekirdek Oynanis / AI gelistiricisinin
/// kodlayacagi spawn ve state machine sistemine) bildirir. Su an o
/// sistemler henuz gelistirme asamasinda oldugundan, secim sadece
/// gorsel olarak vurgulanip Console'a yazdiriliyor; ekip arkadaslarim
/// backend/AI tarafini tamamladiginda OnVehicleSelected event'ine
/// kendi fonksiyonlarini baglayacaklar.
/// </summary>
public class VehicleSelectionBar : MonoBehaviour
{
    [Header("Veri Kaynagi")]
    [SerializeField] private GameBalanceData balanceData;

    [Header("Butonlar (sirasiyla: IKA, IHA, IDA)")]
    [SerializeField] private Button ikaButton;
    [SerializeField] private Button ihaButton;
    [SerializeField] private Button idaButton;

    [Header("Secim Vurgusu")]
    [SerializeField] private Color normalColor = Color.gray;
    [SerializeField] private Color selectedColor = new Color(0.95f, 0.85f, 0.2f); // sari vurgu

    /// <summary>Diger sistemlerin abone olabilecegi secim olayi.</summary>
    public event System.Action<VehicleType, GameBalanceData.UnitStats> OnVehicleSelected;

    public VehicleType CurrentSelection { get; private set; } = VehicleType.IKA;

    private void Awake()
    {
        ikaButton.onClick.AddListener(() => SelectVehicle(VehicleType.IKA));
        ihaButton.onClick.AddListener(() => SelectVehicle(VehicleType.IHA));
        idaButton.onClick.AddListener(() => SelectVehicle(VehicleType.IDA));
    }

    private void Start()
    {
        // Varsayilan olarak ilk arac (IKA) secili baslar
        SelectVehicle(VehicleType.IKA);
    }

    private void SelectVehicle(VehicleType type)
    {
        CurrentSelection = type;
        HighlightSelectedButton(type);

        GameBalanceData.UnitStats stats = balanceData.GetStats(type);
        Debug.Log($"[VehicleSelectionBar] Secilen arac: {stats.displayName} " +
                  $"(HP={stats.maxHealth}, DPS={stats.damagePerSec}, Hiz={stats.moveSpeed})");

        OnVehicleSelected?.Invoke(type, stats);
    }

    private void HighlightSelectedButton(VehicleType selected)
    {
        SetButtonColor(ikaButton, selected == VehicleType.IKA);
        SetButtonColor(ihaButton, selected == VehicleType.IHA);
        SetButtonColor(idaButton, selected == VehicleType.IDA);
    }

    private void SetButtonColor(Button button, bool isSelected)
    {
        var colors = button.colors;
        colors.normalColor = isSelected ? selectedColor : normalColor;
        button.colors = colors;
    }
}
