# Europa
Europa è un platform in 2D con elementi di combattimento, sviluppato come progetto di Sviluppo di Giochi Digitali. Ambientato sul pianeta Europa, una delle lune di Giove, il gioco consiste in quattro livelli distinti, ciascuno con ambientazioni e sfide uniche. Tutti i diritti degli asset utilizzati sono riservati ai rispettivi autori.

Il giocatore controlla un pinguino eroico dotato di abilità speciali come il doppio salto e attacchi. Durante il gioco, il pinguino può raccogliere power-up che lo aiuteranno a superare le sfide più difficili. Oltre al combattimento, il gioco include sezioni di platforming impegnative, con ostacoli come thwomp, palle rotanti e piattaforme che cadono.
## Download
- [Codice sorgente](https://github.com/giorgiobrullo/Europa/archive/refs/heads/main.zip)
- [Build del gioco (OSX, Linux64, Windows32/64)](https://github.com/giorgiobrullo/Europa/releases/latest)

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
| Presenza di nemici/sfida->AI Base| ✅ 1     |                                   |
| Presenza di nemici/sfida->AI Complicata| ❌ NO    |                                   |
| Presenza di nemici/sfida->Multiplayer Locale| ❌ NO    |                                   |

### Strutture
| Elemento                            | Punteggio | Note                              |
|-------------------------------------|-----------|-----------------------------------|
| PlayerPrefs                               | ✅ 0.5      | e.g. inserire link    |
| Singleton (4*1.0)                     | ✅ 4      | Powerup vita, attacco e difesa    |
| Coroutines (4*0.5)                   | ✅ 2     |                                   |
| Enums           | ❌ NO     |                                   |
| Classi statiche | ❌ NO      |             |
| Generics | ❌ NO     |                                   |
| Method overload | ✅ 0.5     |  e.g.                                 |
| Presenza di ereditarietà| ✅ 1    |                                   |
| Presenza di ereditarietà->Overriding| ✅ 0.5    |                                   |
| Interfacce| ❌ NO    |                                   |
| ExtensionsMethods| ❌ NO    |                                   |
| Delegates| ❌ NO    |                                   |

### EXTRA
| Elemento                            | Punteggio | Note                              |
|-------------------------------------|-----------|-----------------------------------|
| Animazioni originali (2*0.5)| ✅ 1    |                                   |
| Sound->Soundtrack| ✅ 0.5    |                                   |
| Sound->Altri suoni (3*0.5)| ✅ 1.5    |                                   |
| Raycast| ✅ 1    |                                   |
| User interface (6*0.5)| ✅ 3    |                                   |
| Particelle (3*0.5)                             | ✅ 1.5      |     |

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
