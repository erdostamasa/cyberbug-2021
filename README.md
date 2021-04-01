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
 - [ ] Fegyver újratöltése
 - [x] Fegyverek váltása
 - [ ] Gránát dobása
 - [ ] Felhasználói felület
   - [x] Főmenü
   - [ ] Pause menu
   - [ ] Pálya kiválasztása
   - [ ] HUD

### Szimulációk:
 - [x] Unity rigidbody szimuláció
 - [x] Reszponzív mozgás

### Környezet:
 - [ ] Low poly modellek és környezet
 - [ ] Több pálya
   * Egyre nehezednek
 - [ ] Végtelen aréna egyre több/erősebb ellenféllel
 - [ ] Ellenségek
   - [x] Követik a játékos pozícióját
   - [ ] Céloznak és lőnek a játékosra
   - [ ] Lőszert és gránátot dobnak ha elpusztulnak
 - [ ] Elpusztítható dobozokból lőszer esik

### Grafika, kinézet:
 * Animációk
   * Játékos
     - [ ] Lövés
     - [ ] Újratöltés
     - [ ] Fegyver váltás
     - [ ] Mozgás
     - [ ] Veszítés
   * Ellenségek
     - [x] Mozgás
     - [ ] Lövés
     - [ ] Részecske effektek
 - [ ] Robbanás
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
