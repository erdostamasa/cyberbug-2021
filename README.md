# Cyberbug 2021
> First person shooter realisztikus fizikával, platforminggal, egyszerű mesterséges intelligenciával-val és low poly grafikával

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

### Felhasználói interakciók:
 * Kamera mozgatása
 * Mozgás
 * Ugrás
 * Lövés fegyverrel
 * Fegyver újratöltése
 * Fegyverek váltása
 * Gránát dobása
 * Felhasználói felület
   * Főmenü
   * Pause menu
   * Pálya kiválasztása
   * HUD

### Szimulációk:
 * Unity rigidbody szimuláció
 * Reszponzív mozgás
 * Realisztikus lövedék szimuláció

### Környezet:
 * Low poly modellek és környezet
 * Több pálya
   * Egyre nehezednek
 * Végtelen aréna egyre több/erősebb ellenféllel
 * Ellenségek
   * Követik a játékos pozícióját
   * Céloznak és lőnek a játékosra
   * Lőszert és gránátot dobnak ha elpusztulnak
 * Elpusztítható dobozokból lőszer esik

### A játékos céljai:
 * Ellenségek legyőzése
 * Lőszer és gránátok gyűjtése
 * Pályák teljesítése
 * Újabb fegyverek megszerzése

### Grafika, kinézet:
 * Animációk
   * Játékos
     * Lövés
     * Újratöltés
     * Fegyver váltás
     * Mozgás
     * Veszítés
   * Ellenségek
     * Mozgás
     * Lövés
     * Részecske effektek
 * Robbanás
   * Golyónyomok

### Extra funkciók:
 * Futás
 * Guggolás
 * Közelharc
 * Mozgó / forgó platformok
 * Procedurális animáció inverz kinematikával
 * Pénz / bolt rendszer
 * Pont / highscore rendszer
 * Kooperatív többjátékos mód
 * Játékos játékos ellen többjátékos mód
