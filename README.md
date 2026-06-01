# System Rezerwacji Sal Konferencyjnych

Aplikacja internetowa zrealizowana w technologii ASP.NET Core MVC (C#), służąca do zarządzania harmonogramem sal szkoleniowych i akademickich.

## 🚀 Funkcjonalności
- **Kalendarz Rezerwacji:** Interaktywny i responsywny harmonogram (FullCalendar).
- **Zarządzanie Salami:** Dodawanie, edycja i usuwanie sal konferencyjnych.
- **System CMS:** Możliwość edycji treści tekstowych strony bezpośrednio z panelu administratora.
- **Powiadomienia E-mail:** Automatyczne potwierdzenia rezerwacji wysyłane do wykładowców.
- **Bezpieczeństwo:** System ról (Admin / User) z bezpiecznym logowaniem.
- **Responsywność:** Pełne wsparcie dla urządzeń mobilnych (Bootstrap 5).

## 🛠️ Technologie
- **Backend:** ASP.NET Core 8.0, EF Core, MS SQL Server.
- **Frontend:** HTML5, CSS3, JS, Bootstrap 5.
- **Dokumentacja:** Markdown.

## 📖 Dokumentacja
- [Dokumentacja Techniczna](TECHNICAL_DOCUMENTATION.md) - Opis architektury i bazy danych.
- [Instrukcja Użytkownika](USER_MANUAL.md) - Przewodnik dla użytkowników nietechnicznych.

## ⚙️ Instrukcja uruchomienia

1. **Pobranie projektu:** Sklonuj repozytorium na dysk.
2. **Otwarcie w IDE:** Uruchom Visual Studio 2022 i otwórz plik `.sln`.
3. **Konfiguracja bazy:** Sprawdź `appsettings.json` pod kątem parametrów `DefaultConnection`.
4. **Migracja bazy danych:**
   - Otwórz *Package Manager Console*.
   - Uruchom komendę: `Update-Database`.
5. **Uruchomienie:** Naciśnij `Ctrl + F5`.

## 👤 Dane do logowania
- **Admin:** `admin` / `admin123!`
- **Użytkownik:** `gosc` / `gosc123`
