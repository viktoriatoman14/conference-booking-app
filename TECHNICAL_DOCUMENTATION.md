# Dokumentacja Techniczna - System Rezerwacji Sal Konferencyjnych

## 1. Architektura Systemu
Aplikacja została zbudowana w oparciu o wzorzec projektowy **Model-View-Controller (MVC)** przy użyciu frameworka **ASP.NET Core 8.0**.

### Warstwy aplikacji:
- **Models:** Definiują strukturę danych (Sale, Rezerwacje, Profesorowie, Treść strony).
- **Views:** Pliki Razor (.cshtml) odpowiedzialne za interfejs użytkownika.
- **Controllers:** Obsługują logikę biznesową i komunikację między widokiem a bazą danych.
- **Data:** Zawiera `ApplicationDbContext` oraz migracje Entity Framework Core.
- **Services:** Dodatkowe usługi, takie jak system wysyłki powiadomień e-mail (`EmailSender`).

## 2. Stos Technologiczny
- **Backend:** C#, ASP.NET Core 8.0, Entity Framework Core.
- **Baza danych:** Microsoft SQL Server (LocalDB w środowisku deweloperskim).
- **Frontend:** HTML5, CSS3 (Bootstrap 5), JavaScript.
- **Biblioteki zewnętrzne:**
  - `FullCalendar` - interaktywny kalendarz rezerwacji.
  - `MailKit` / `MimeKit` - obsługa wysyłki e-maili.
  - `Bootstrap Icons` - zestaw ikon interfejsu.

## 3. Struktura Bazy Danych
System korzysta z następujących tabel:
- **AspNetUsers / Roles:** (Identity) Zarządzanie użytkownikami i uprawnieniami.
- **ConferenceRooms:** Informacje o salach (numer, piętro, dostępność).
- **Professors:** Dane osób rezerwujących (imię, nazwisko, tytuł naukowy, e-mail).
- **Bookings:** Powiązanie sali z profesorem oraz czasem trwania rezerwacji.
- **PageContents:** Słownik klucz-wartość do dynamicznego zarządzania treścią strony.

## 4. Kluczowe Funkcjonalności
- **System Rezerwacji:** Zapobieganie konfliktom terminów (walidacja nakładających się rezerwacji).
- **Powiadomienia:** Automatyczne wysyłanie e-maili z potwierdzeniem rezerwacji.
- **Panel Administratora:** Pełne zarządzanie salami, użytkownikami oraz treścią tekstową strony (CMS).
- **Kalendarz:** Wizualizacja obłożenia sal w czasie rzeczywistym.
