# Dokumentacja Techniczna - System Rezerwacji Sal Konferencyjnych

## 1. Architektura Rozwiązania

Aplikacja została zbudowana w oparciu o wzorzec projektowy **MVC (Model-View-Controller)** z wykorzystaniem platformy **ASP.NET Core 8.0**.

### Warstwy systemu:
*   **Modele (Models):** Klasy C# reprezentujące strukturę danych oraz reguły walidacji (Data Annotations).
*   **Widoki (Views):** Silnik Razor wykorzystujący HTML5, CSS3 oraz Bootstrap 5 do generowania responsywnego interfejsu użytkownika.
*   **Kontrolery (Controllers):** Logika sterująca, która przetwarza żądania użytkowników, komunikuje się z bazą danych i zwraca odpowiednie widoki lub dane JSON.
*   **Data (EF Core):** Wykorzystanie Entity Framework Core jako ORM do komunikacji z bazą MS SQL Server (LocalDB).

## 2. Struktura Bazy Danych

Baza danych składa się z trzech głównych tabel oraz tabel systemowych Microsoft Identity (do obsługi ról, choć w obecnej fazie projekt wykorzystuje uproszczony mechanizm Cookie Authentication).

### Tabele:

#### `ConferenceRooms` (Sale konferencyjne)
*   `Id` (int, PK) - Unikalny identyfikator sali.
*   `Nnumber` (string) - Numer/nazwa sali.
*   `Floor` (string) - Piętro, na którym znajduje się sala.
*   `IsAvailable` (bool) - Status dostępności sali.

#### `Professors` (Wykładowcy/Pracownicy)
*   `Id` (int, PK) - Unikalny identyfikator.
*   `AcademicTitle` (string) - Tytuł naukowy.
*   `FirstName` (string) - Imię.
*   `LastName` (string) - Nazwisko.
*   `Email` (string) - Adres e-mail do powiadomień.

#### `Bookings` (Rezerwacje)
*   `Id` (int, PK) - Unikalny identyfikator rezerwacji.
*   `StartDate` (DateTime) - Czas rozpoczęcia.
*   `EndDate` (DateTime) - Czas zakończenia.
*   `MeetingPurpose` (string) - Cel spotkania.
*   `ConferenceRoomId` (int, FK) - Powiązanie z tabelą `ConferenceRooms`.
*   `ProfessorId` (int, FK) - Powiązanie z tabelą `Professors`.
*   `UserId` (string) - Identyfikator użytkownika, który dokonał rezerwacji.

## 3. Kluczowe Funkcjonalności

### 3.1. Dynamiczny Kalendarz (FullCalendar)
Zintegrowany kalendarz w widoku rezerwacji pobiera dane asynchronicznie z kontrolera w formacie JSON (`Bookings/GetCalendarData`). Umożliwia wizualizację zajętości sal w ujęciu miesięcznym i tygodniowym.

### 3.2. Walidacja Rezerwacji
System posiada wbudowaną logikę biznesową zapobiegającą:
*   Rezerwacji sali w przeszłości.
*   Nakładaniu się terminów rezerwacji dla tej samej sali.
*   Rezerwacjom wykraczającym poza godziny pracy obiektu (08:00 - 22:15).
*   Rezerwacjom wielodniowym w ramach jednego wpisu.

### 3.3. Powiadomienia E-mail (MailKit)
Po pomyślnym utworzeniu rezerwacji, system automatycznie generuje i wysyła wiadomość e-mail do przypisanego profesora. Usługa jest zaimplementowana poprzez interfejs `IEmailSender`, co pozwala na łatwą zmianę dostawcy usług e-mail.

### 3.4. System Autoryzacji
Wykorzystano **Cookie Authentication**. Dostęp do poszczególnych akcji jest ograniczony za pomocą atrybutu `[Authorize]`:
*   `Admin`: Pełne zarządzanie salami, wykładowcami i rezerwacjami (CRUD).
*   `User`: Przeglądanie kalendarza i dodawanie nowych rezerwacji.
