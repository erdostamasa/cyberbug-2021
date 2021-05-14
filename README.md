# Cyberbug 2021
> First person shooter realisztikus fizikával, platforminggal, egyszerű mesterséges intelligenciával-val és low poly grafikával

### Szükséges szoftverek:
 * Unity 2019.4.21f1
 * OS: Windows 7 SP1+, 8, 10, 64-bit versions only; macOS 10.12+

### Használt technológiák:
 * Verziókezelő szoftver: git
 * Verziókezelő szolgáltalás: GitHub
 * Workflow: GitHub flow (https://guides.github.com/introduction/flow/)
 * Automatizált tesztelés:
   * GitHub Actions által futtatott unit tesztek
 * Continuous integration + deployment:
   * GitHub Actions
   * GameCI
 * Kommunikáció: 
   * aszinkron: GitHub, Codecks
   * szinkron: Discord
 * Játékmotor: Unity
 * Programozási nyelv: C#
 * Platform: Windows, WEBGL (ha a teljesítmény engedi)

## Részletes terv

### A játékos céljai:
 * Ellenségek legyőzése
 * Lőszer és gránátok gyűjtése
 * Pályák teljesítése
 * Újabb fegyverek megszerzése

### Felhasználói interakciók:
 - [x] Kamera mozgatása
 - [x] Mozgás
 - [x] Ugrás
 - [x] Lövés fegyverrel
 - [x] Fegyver újratöltése
 - [x] Fegyverek váltása
 - [x] Felhasználói felület
   - [x] Főmenü
   - [X] Pause menu
   - [X] Pálya kiválasztása
   - [X] HUD

### Szimulációk:
 - [x] Unity rigidbody szimuláció
 - [x] Reszponzív mozgás

### Környezet:
 - [X] Low poly modellek és környezet
 - [X] Több pálya
   * Egyre nehezednek
 - [x] Végtelen aréna egyre több/erősebb ellenféllel
 - [x] Ellenségek
   - [x] Követik a játékos pozícióját
   - [x] Céloznak és sebzik a játékost
   - [x] Lőszert dobnak ha elpusztulnak

### Grafika, kinézet:
 * Animációk
   * Játékos
     - [X] Lövés
     - [X] Újratöltés
     - [X] Mozgás
   * Ellenségek
     - [x] Mozgás
     - [x] Lövés
     - [x] Részecske effektek
 - [x] Robbanás
   - [ ] Golyónyomok

### Extra funkciók:
 - [ ] Futás
 - [ ] Guggolás
 - [ ] Közelharc
 - [ ] Mozgó / forgó platformok
 - [x] Procedurális animáció inverz kinematikával
 - [ ] Pénz / bolt rendszer
 - [ ] Pont / highscore rendszer
 - [ ] Kooperatív többjátékos mód
 - [ ] Játékos játékos ellen többjátékos mód
