# Errorline system

Az **Errorline system** egy hibajegy-kezelő és karbantartásszervező webalkalmazás. A rendszer célja, hogy hatékonyan kezelje a kollégiumban felmerülő hibajelentéseket, és támogassa a karbantartási folyamatokat.

---

## Technológiai környezet

- **Frontend:** React + TypeScript, Mantine UI
- **Backend:** C# ASP.NET Core 9.0
- **Adatbázis:** SQLite / Entity Framework Core
- **Autentikáció:** JWT alapú hitelesítés
- **Konténerizáció:** Docker + Docker Compose
- **Fejlesztői eszközök:** Visual Studio 2022, Visual Studio Code
- **Dokumentáció:** OpenAPI / Swagger

---

## Főbb funkciók

- Felhasználókezelés, regisztráció, jelszó-visszaállítás

### Kollégista funkciók

- Hibajegyek létrehozása, részletezése, szerkesztése
- Bejelentett hibajegyek állapotának nyomon követése
- Megjegyzés hozzáfűzése hibához

### Karbantartási vezető funkciók

- Hibajegyek karbantartóhoz rendelése
- Hibajegyek státuszainak módosítása
- Hibabejelentések listázása
- Eszközrendelések leadása, nyomon követése

### Karbantartó funkciók

- Hozzárendelt hibák megtekintése
- Hibák javításának visszaigazolása
- Hiba kapcsán eszköz rendelése

### Adminisztrátor funkciók

- Hibatípusok és eszközök rendszerezése
- Új eszközök, hibatípusok rögzítése
- Szoba felszereltségek rögzítése, törlése

---

## Felhasználói szerepkörök

1. Kollégista
2. Karbantartási vezető
3. Karbantartó
4. Adminisztrátor

---

## Telepítés és futtatás

### Backend indítása
```bash
cd backend\ErrorlineSystem
dotnet restore
dotnet run
```

### Frontend indítása
```bash
cd frontend
yarn install
yarn dev
```

---

## Bejelentkezés

- admin@admin.com
- resident@resident.com
- worker@worker.com
- manager@manager.com

- jelszó: 11111111

---

## Projekt specifikáció

A projekt leírása a gyökérkönyvtárban található HibaVonal.pdf fájlban érhető el.
