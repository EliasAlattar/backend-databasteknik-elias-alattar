# Backend – Databasteknik – EducationCompany

## 📘 Om projektet

Detta projekt är en backend-applikation utvecklad i **.NET** som en del av kursen **Databasteknik**.
Syftet är att visa hur man designar och implementerar en databasdriven applikation med tydlig arkitektur, relationer och tester.

Applikationen hanterar en utbildningsverksamhet där deltagare kan registreras på kurser.

---

## 🎯 Uppfyller uppgiftskrav

Projektet uppfyller följande krav:

* Relationsdatabas med flera entiteter
* Primärnycklar och främmande nycklar
* Many-to-many-relation via kopplingstabell
* Entity Framework Core (Code First)
* Migrationer
* Lagerindelad arkitektur enligt DDD/Clean Architecture
* Enhetstester
* Publicering på GitHub
* Frontend-delen har utelämnats enligt lärarens instruktion att den inte krävs för denna inlämning

---

## 🏗️ Arkitektur

Lösningen är uppdelad i följande projekt:

* **Domain** → Entiteter och affärsmodell
* **Application** → Applikationslogik
* **Infrastructure** → Databas och EF Core
* **Presentation** → ASP.NET Core API
* **Tests** → Enhetstester (xUnit)

Denna struktur separerar ansvar och gör systemet lätt att underhålla och vidareutveckla.

---

## 🗄️ Datamodell

Systemet består av tre huvudtabeller:

### Course

Representerar en kurs.

### Participant

Representerar en deltagare.

### Enrollment

Kopplingstabell som representerar registrering av deltagare på kurser.

Relationer:

* En kurs kan ha många deltagare
* En deltagare kan gå flera kurser
* Enrollment innehåller främmande nycklar till båda

Registreringstid sätts automatiskt med UTC-tid.

---

## 🧱 Tekniker

* .NET 9
* ASP.NET Core
* Entity Framework Core
* SQLite
* xUnit

---

## ▶️ Köra projektet

1. Klona repositoryt
2. Öppna **EducationCompany.sln** i Visual Studio
3. Kör migrationer

   ```bash
   Update-Database
   ```
4. Starta projektet **EducationCompany.Presentation**

---

## 🧪 Tester

Tester finns i projektet **EducationCompany.Tests** och körs via Test Explorer eller:

```bash
dotnet test
```

---

## 📂 Repositoryt innehåller

* All källkod
* Migrationer
* Tester
* Solution-fil

Allt som krävs för att bygga och köra projektet finns inkluderat.

---

## 👤 Författare

Elias Alattar
Databasteknik – Backend

---

Detta projekt är skapat för utbildningssyfte.
