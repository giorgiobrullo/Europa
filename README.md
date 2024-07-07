# Europa
Europa è un platform in 2D con elementi di combattimento, sviluppato come progetto di Sviluppo di Giochi Digitali. Ambientato sul pianeta Europa, una delle lune di Giove, il gioco consiste in quattro livelli distinti, ciascuno con ambientazioni e sfide uniche. Tutti i diritti degli asset utilizzati sono riservati ai rispettivi autori.

Il giocatore controlla un pinguino eroico dotato di abilità speciali come il doppio salto e attacchi. Durante il gioco, il pinguino può raccogliere power-up che lo aiuteranno a superare le sfide più difficili. Oltre al combattimento, il gioco include sezioni di platforming impegnative, con ostacoli come thwomp, palle rotanti e piattaforme che cadono.
## Download
- [Codice sorgente](https://github.com/giorgiobrullo/Europa/archive/refs/heads/main.zip)
- [Build del gioco (OSX, Linux64, Windows32/64)](https://github.com/giorgiobrullo/Europa/releases/latest)

## Nota
- Le piattaforme cadenti, una volta precipitate, non ritornano nella loro posizione originale. Questo comportamento è intenzionale, costringendo il giocatore a morire (perdendo tempo) per resettarle.
- Nell'ultimo livello, le piattaforme cadenti finiscono sulle punte, garantendo comunque che un giocatore sopra di esse non subisca danni. Questo comportamento è voluto; l'obiettivo delle piattaforme cadenti in quella zona è rendere più difficoltosi i salti.

## Tabella di Punteggi

### Impostazioni generali 
| Elemento       | Punteggio | Note |
|----------------|-----------|------|
| Build mobile   | ❌ NO     |      |
| Splash Screen  | ✅ 0.5    |      |
| Icona gioco    | ✅ 0.5    |      |

### Main menu
| Elemento                  | Punteggio | Note                                                     |
|---------------------------|-----------|----------------------------------------------------------|
| Load Game                 | ✅ 2      | Checkpoint e dal menù principale                         |
| Options -> Sound/Music    | ✅ 1      |                                                          |
| Options -> Controls       | ✅ 2      | Possibile cambiare tra WASD e <- ->Frecciette nel menù opzioni |
| Options -> Resolution/Quality | ✅ 0.5 |                                                          |
| Credits Screen            | ✅ 0.5    |                                                          |
| Classifica (non ordinata/ordinata) | ❌ NO     |                                                          |

### Tutorial
| Elemento                         | Punteggio | Note                                   |
|----------------------------------|-----------|----------------------------------------|
| Schermata statica (3*0.5)        | ✅ 1.5    | Primo livello Tutorial, schermate statiche volanti |

### GamePlay
| Elemento                            | Punteggio | Note                              |
|-------------------------------------|-----------|-----------------------------------|
| Score                               | ✅ 1      | Mostrato alla fine nei crediti    |
| Powerup (3*1.0)                     | ✅ 3      | Powerup vita, attacco e difesa    |
| Gioco a tempo                      | ❌ NO     |                                   |
| Presenza di nemici/sfida            | ✅ 1      |                                   |
| Presenza di nemici/sfida->Livelli di difficoltà | ✅ 1      | Cute, Normal, Hardcore            |
| Presenza di nemici/sfida->Difficoltà crescente | ❌ NO     |                                   |
| Presenza di nemici/sfida->AI Base| ✅ 1     |   Bat, Slime (GenericEnemy)       |
| Presenza di nemici/sfida->AI Complicata| ❌ NO    |                                   |
| Presenza di nemici/sfida->Multiplayer Locale| ❌ NO    |                                   |

### Strutture
| Elemento                            | Punteggio | Note                              |
|-------------------------------------|-----------|-----------------------------------|
| PlayerPrefs                               | ✅ 0.5      | e.g. [1](https://github.com/giorgiobrullo/Europa/blob/e9d3f18855ff750e567a1dec6bd14d06ece1c7c5/Assets/Scripts/Menu/Menu.cs#L20-L30) [2](https://github.com/giorgiobrullo/Europa/blob/e9d3f18855ff750e567a1dec6bd14d06ece1c7c5/Assets/Scripts/Other/CreditScroller.cs#L95-L101)    |
| Singleton (4*1.0)                     | ✅ 4      | Powerup vita, attacco e difesa    |
| Coroutines (4*0.5)                   | ✅ 2     |  e.g. [1](https://github.com/giorgiobrullo/Europa/blob/b7dcb85ac2d21f484087f8309e1e1dd15e6b3202/Assets/Scripts/Volume/VolumeController.cs#L63) [2](https://github.com/giorgiobrullo/Europa/blob/b7dcb85ac2d21f484087f8309e1e1dd15e6b3202/Assets/Scripts/Traps/FireTrap.cs#L33) [3](https://github.com/giorgiobrullo/Europa/blob/b7dcb85ac2d21f484087f8309e1e1dd15e6b3202/Assets/Scripts/Enemies/Generic/EnemyMovement.cs#L106) [4](https://github.com/giorgiobrullo/Europa/blob/e9d3f18855ff750e567a1dec6bd14d06ece1c7c5/Assets/Scripts/Other/Portal.cs#L47)                                 |
| Enums           | ❌ NO     |                                   |
| Classi statiche | ❌ NO      |             |
| Generics | ❌ NO     |                                   |
| Method overload | ❌ NO     |                                |
| Presenza di ereditarietà| ✅ 1    |  e.g. [1](https://github.com/giorgiobrullo/Europa/blob/e9d3f18855ff750e567a1dec6bd14d06ece1c7c5/Assets/Scripts/Items/DroppedCoin.cs#L6)                                 |
| Presenza di ereditarietà->Overriding| ✅ 0.5    | e.g. [1](https://github.com/giorgiobrullo/Europa/blob/e9d3f18855ff750e567a1dec6bd14d06ece1c7c5/Assets/Scripts/Items/DroppedCoin.cs#L8)                                  |
| Interfacce| ❌ NO    |                                   |
| ExtensionsMethods| ❌ NO    |                                   |
| Delegates| ❌ NO    |                                   |

### EXTRA
| Elemento                            | Punteggio | Note                              |
|-------------------------------------|-----------|-----------------------------------|
| Animazioni originali (2*0.5)| ✅ 1    |  Bat, Slime, Traps                                 |
| Sound->Soundtrack| ✅ 0.5    |  Soundtrack ogni livello e menù                                 |
| Sound->Altri suoni (3*0.5)| ✅ 1.5    |   [Cartella suoni](https://github.com/giorgiobrullo/Europa/tree/main/Assets/Sounds)                                |
| Raycast| ✅ 1    |   e.g. [1](https://github.com/giorgiobrullo/Europa/blob/e9d3f18855ff750e567a1dec6bd14d06ece1c7c5/Assets/Scripts/Traps/Rockhead.cs#L117) [2](https://github.com/giorgiobrullo/Europa/blob/e9d3f18855ff750e567a1dec6bd14d06ece1c7c5/Assets/Scripts/Enemies/Generic/EnemyMovement.cs#L45)                                |
| User interface (6*0.5)| ✅ 3    |  Barra Vita, Testo Shield, Testo Attack, Testo difficoltà, GameOver, MenuInGame, Testo tutorial, Dialoghi NPC                                 |
| Particelle (3*0.5)                             | ✅ 1.5      |  Rockhead (quanto hitta il muro), Big Coin, Player   |

Totale: 32.0
## Integrazione con LiveSplit

**Europa** è ottimizzato per gli appassionati di speedrun e offre un'integrazione semplice con LiveSplit, un popolare timer di speedrun. LiveSplit è un'applicazione di temporizzazione altamente personalizzabile, utilizzata dai giocatori per cronometrarsi durante le speedrun.

Per utilizzare LiveSplit con **Europa**, segui questi passaggi:

1. **Scarica LiveSplit** dal [sito ufficiale](https://livesplit.org/) o da altre fonti come l'App store o package manager.
2. **Configura il Server TCP**:
    - Apri LiveSplit.
    - Fai clic con il tasto destro sulla finestra di LiveSplit e seleziona `Control` -> `Start TCP Server`.
3. **Carica gli Splits di Europa**:
    - Fai nuovamente clic con il tasto destro sulla finestra di LiveSplit.
    - Seleziona `Open Splits` -> `From URL...` e inserisci il seguente URL: `https://raw.githubusercontent.com/giorgiobrullo/Europa/main/Europa_Split.lss`.
4. **Avvia Europa**:
    - Lancia il gioco **Europa**.
    - Goditi le tue speedrun con il supporto di LiveSplit.

Questa integrazione permette al gioco di comunicare direttamente con LiveSplit, impostando automaticamente i tempi al cambio di livello e terminando il timer alla fine.

WR speedrun Any% Normal:

[![Youtube video preview](http://img.youtube.com/vi/sOLZ3imw2ZA/0.jpg)](http://www.youtube.com/watch?v=sOLZ3imw2ZA "Europa - Any% Normal - WR 2:14.90")


