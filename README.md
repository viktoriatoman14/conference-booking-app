# System Rezerwacji Sal Konferencyjnych

Aplikacja internetowa służąca do zarządzania rezerwacjami sal konferencyjnych, szkoleniowych i akademickich, zrealizowana w technologii ASP.NET Core MVC.

## Cel projektu
Usprawnienie procesu rezerwacji sal w firmie lub na uczelni oraz eliminacja konfliktów terminów poprzez centralny system zarządzania harmonogramem.

## Główne funkcjonalności
* **Dynamiczny kalendarz rezerwacji:** Integracja z biblioteką FullCalendar umożliwia przejrzysty podgląd zajętości sal.
* **Zarządzanie salami i rezerwacjami:** Panel administratora do dodawania, edycji i usuwania zasobów.
* **System powiadomień e-mail:** Automatyczne wysyłanie potwierdzeń rezerwacji do wykładowców/pracowników (MailKit).
* **System ról i uprawnień:** Podział na administratora (pełny dostęp) oraz użytkownika (przegląd i rezerwacja).
* **Responsywny interfejs:** Wykorzystanie Bootstrap 5 zapewnia poprawne działanie na urządzeniach mobilnych.

## Technologie
* **Backend:** ASP.NET Core 8.0 (C#)
* **Baza danych:** Entity Framework Core + MS SQL Server / LocalDB
* **Frontend:** Razor Views + Bootstrap 5 + JavaScript (FullCalendar)
* **Inne:** MailKit (obsługa e-mail)

## Struktura projektu
Główny kod aplikacji znajduje się w katalogu `/ConferenceBookingApp`. Repozytorium zawiera również folder `/conference_booking`, który jest pustym szablonem projektu.

## Wymagania wstępne
* **.NET SDK 8.0**
* **Visual Studio 2022** (z pakietem narzędzi ASP.NET i web development)
* **MS SQL Server / LocalDB**

## Instrukcja uruchomienia

1. **Pobranie projektu:** Pobierz lub sklonuj repozytorium na dysk.
2. **Otwarcie w IDE:** Uruchom Visual Studio 2022 i otwórz plik rozwiązania `ConferenceBookingApp/ConferenceBookingApp.sln`.
3. **Konfiguracja bazy:** Sprawdź plik `appsettings.json` w projekcie `ConferenceBookingApp`. Upewnij się, że parametr `DefaultConnection` wskazuje na Twój serwer SQL (domyślnie `(localdb)\mssqllocaldb`).
4. **Migracja bazy danych:**
   * Otwórz *Package Manager Console* (Narzędzia -> NuGet Package Manager).
   * Wykonaj komendę:
     ```shell
     Update-Database
     ```
     Baza danych oraz testowe konta zostaną utworzone automatycznie.
5. **Uruchomienie:** Naciśnij **`Ctrl + F5`**, aby uruchomić aplikację.

## Dane do logowania testowego

* **Konto Administratora:**
  * **Login:** `admin`
  * **Hasło:** `admin123!`

* **Konto Użytkownika:**
  * **Login:** `gosc`
  * **Hasło:** `gosc123`

---
*Projekt realizowany indywidualnie. Pełna odpowiedzialność za frontend, backend oraz bazę danych.*
