# conference-booking-app

1. System rezerwacji sal konferencyjnych (projekt 1-osobowy)
Cel biznesowy:
Usprawnienie procesu rezerwacji sal konferencyjnych w firmie oraz eliminacja konfliktów terminów.

Główne funkcje:
Przegląd dostępnych sal, kalendarz rezerwacji, logowanie użytkowników, panel administratora do zarządzania salami i rezerwacjami, wysyłanie powiadomień e-mail.

Technologie i architektura:
ASP.NET Core MVC + Entity Framework Core + MS SQL Server + Bootstrap 5. Architektura MVC z warstwą backendową REST API oraz responsywnym frontendem.

Miejsce instalacji i repozytorium:
Docelowo: serwer VPS Linux / Azure App Service
Repozytorium: https://github.com/viktoriatoman14/conference-booking-app

Odpowiedzialność w projekcie:
Projekt realizowany indywidualnie – pełna odpowiedzialność za frontend, backend oraz bazę danych.

# System Rezerwacji Sal Konferencyjnych

Aplikacja internetowa zrealizowana w technologii ASP.NET Core MVC (C#), służąca do zarządzania harmonogramem sal szkoleniowych i akademickich.

## Wymagania wstępne
* **.NET SDK (7.0 lub 8.0)**
* **Visual Studio 2022** (z pakietem narzędzi sieciowych i ASP.NET)
* **MS SQL Server / LocalDB**

## Instrukcja uruchomienia krok po kroku

1. **Pobranie projektu:** Rozpakuj paczkę z kodem źródłowym na dysk komputera.
2. **Otwarcie w IDE:** Uruchom Visual Studio 2022, kliknij *Otwórz projekt lub rozwiązanie* i wskaż plik `.sln` aplikacji.
3. **Konfiguracja bazy:** Otwórz plik `appsettings.json` i upewnij się, że parametr `DefaultConnection` wskazuje na Twój lokalny serwer bazodanowy (domyślnie `(localdb)\mssqllocaldb`).
4. **Utworzenie bazy danych:**
   * W menu górnym wybierz: *Narzędzia* -> *NuGet Package Manager* -> *Package Manager Console*.
   * Wpisz komendę i zatwierdź Enterem:
     ```shell
     Update-Database
     ```
     *(Baza danych oraz testowe konta użytkowników zostaną wygenerowane automatycznie).*
5. **Uruchomienie:** Naciśnij skrót klawiszowy **`Ctrl` + `F5`**, aby skompilować i włączyć aplikację w przeglądarce pod adresem `https://localhost:...`.

## Dane do logowania testowego

* **Konto Administratora (Pełny dostęp):**
  * **nazwa uzytkownika** `admin`
  * **Hasło:** `admin123!`

* **Konto Gościa (Tylko widok i rezerwacja):**
  * **nazwa uzytkownika** `gosc`
  * **Hasło:** `gosc123`

## Główne funkcjonalności
* Dynamiczny, responsywny kalendarz rezerwacji (FullCalendar).
* Automatyczne powiadomienia e-mail do wykładowców po rejestracji zajęć (MailKit).
* System ról i uprawnień (Admin / Gość).
* Interfejs dostosowany do urządzeń mobilnych (Bootstrap).
