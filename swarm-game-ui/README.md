# Swarm Savaş Oyunu — Tasarım, Dengeleme ve Arayüz (Görev 4)

TUSMEC Aday Mühendis Programı 2026 — Track 3 (Oyun & Simülasyon) kapsamında,
4 kişilik ekipte **Görev 4: Tasarım, Dengeleme ve Arayüz Entegratörü**
rolünde yürütülen çalışma.

## Oyun Konsepti

Karşılıklı 2 grup halinde oynanan, her grubun kendi İHA/İKA/İDA filosuyla
zamana ve mekana uygun stratejik swarm taktikleriyle savaştığı, amacı
karşı tarafın kalesini ele geçirmek olan bir oyun. Seviye arttıkça filoya
araç eklenebilir veya araçlar güçlendirilebilir; savaş alanına gelen şans
kutuları geçici avantajlar (tamir, hız artışı, hasar bonusu) sağlar.
İDA'lar deniz kontrolü, İHA'lar hava tehdidi ve koruma, İKA'lar ise
kaleyi ele geçirme rolünü üstlenir.

## Projenin Durumu

Ekibin backend (Node.js/Express/Prisma) ve AI/pathfinding (state machine)
kısımları henüz geliştirme aşamasında olduğundan, mentörümüzün talimatıyla
bu aşamada **sadece Görev 4'ün 1. ve 2. kilometre taşları** tamamlanmıştır:

- ✅ **Kilometre Taşı 1:** 3 bina + 3 araç için Can/Hasar/Hız/Menzil
  istatistiklerinin belirlenmesi (`denge_tablosu.xlsx`) ve bu tablonun
  Unity içinde ekip arkadaşlarının da (AI, backend) okuyabileceği ortak
  bir veri kaynağına (`GameBalanceData.cs`) dönüştürülmesi.
- ✅ **Kilometre Taşı 2:** Oyuncunun sahaya süreceği aracı seçebileceği
  fonksiyonel butonlar (`VehicleSelectionBar.cs`).
- ⏳ **Kilometre Taşı 3** (dinamik can barları): AI/backend tarafının
  gerçek zamanlı can verisi üretmesi beklendiğinden, bu aşamada
  gerçekleştirilememiştir; sonraki adım olarak planlanmıştır.

Bu nedenle bu klasör **tam bitmiş bir oyun değil**, ekibin iş bölümünde
bana ait olan arayüz/dengeleme katmanının bu ana kadarki halidir.

## İçerik

| Dosya | Açıklama |
|---|---|
| `Assets/Scripts/Data/GameBalanceData.cs` | Denge tablosunun Unity'deki ortak kaynağı (ScriptableObject) |
| `Assets/Scripts/UI/PauseMenuController.cs` | Duraklatma menüsü (Devam Et / Ayarlar / Ana Menü / Oyundan Çık) |
| `Assets/Scripts/UI/SettingsPanel.cs` | Ayarlar paneli (ses kaydırağı) |
| `Assets/Scripts/UI/VehicleSelectionBar.cs` | İKA/İHA/İDA araç seçim çubuğu |

## Host Doğrulama Testi

Unity Editor olmadan, C# mantığının denge tablosundaki gerçek sayılarla
tutarlı çalıştığını kanıtlayan bağımsız bir test (Mono derleyicisiyle):

```bash
cd tests
mcs -out:test_balance_logic.exe stubs/UnityStubs.cs \
    ../Assets/Scripts/Data/GameBalanceData.cs \
    ../Assets/Scripts/UI/VehicleSelectionBar.cs \
    test_balance_logic.cs
mono test_balance_logic.exe
```

Test, `GameBalanceData.cs`'teki sayıların `denge_tablosu.xlsx` ile
birebir eşleştiğini ve araç seçim butonlarının doğru istatistikleri
döndürdüğünü doğrular — tüm testler başarılı sonuçlanmıştır.

## Ekran Görüntüleri

Oyun içi duraklatma menüsü, ayarlar paneli ve araç seçim çubuğunun
gerçek Unity sahnesindeki görünümü staj defterine ayrıca eklenmiştir.

## Yazar

Ayşegül — Bilgisayar Mühendisliği, TUSMEC AMP-2026 Track 3 stajyeri
