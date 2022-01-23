# Cyberbug 2021
> First person shooter realisztikus fizikával, platforminggal, egyszerű mesterséges intelligenciával-val és low poly grafikával

A projekt az ELTE Informatika Kar **Tools of Software Projects** tárgya keretében valósult meg.


### Projekt megnyitásához szükséges rendszerkövetelmények
 * Unity 2019.4.21f1
 * OS: Windows 7 SP1+, 8, 10, 64-bit versions only; macOS 10.12+

### Használt technológiák
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

### A játékos céljai
 * Ellenségek legyőzése
 * Lőszer gyűjtése
 * Pályák teljesítése
 * Újabb fegyverek megszerzése

### Felhasználói interakciók
 - [x] Kamera mozgatása
 - [x] Mozgás
 - [x] Ugrás
 - [x] Lövés fegyverrel
 - [x] Fegyver újratöltése
 - [x] Fegyverek váltása
 - [x] Felhasználói felület
   - [x] Főmenü
   - [x] Pause menu
   - [x] Pálya kiválasztása
   - [x] HUD

### Szimulációk
 - [x] Unity rigidbody szimuláció
 - [x] Reszponzív mozgás

### Környezet
 - [x] Low poly modellek és környezet
 - [x] Több pálya
   * Egyre nehezednek
 - [x] Végtelen aréna egyre több/erősebb ellenféllel
 - [x] Ellenségek
   - [x] Követik a játékos pozícióját
   - [x] Céloznak és sebzik a játékost
   - [x] Lőszert dobnak ha elpusztulnak

### Grafika, kinézet
 * Animációk
   * Játékos
     - [x] Lövés
     - [x] Újratöltés
     - [x] Mozgás
   * Ellenségek
     - [x] Mozgás
     - [x] Lövés
     - [x] Részecske effektek
 - [x] Robbanás
   - [x] Golyónyomok

### Extra funkciók
 - [x] Procedurális animáció inverz kinematikával
 - [x] Pont / highscore rendszer
 - [ ] ~~Guggolás~~
 - [ ] ~~Közelharc~~
 - [ ] ~~Mozgó / forgó platformok~~
 - [ ] ~~Pénz / bolt rendszer~~
 - [ ] ~~Kooperatív többjátékos mód~~
 - [ ] ~~Játékos játékos ellen többjátékos mód~~
