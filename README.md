# ComSniffer

Sniffer bidirectionnel de lignes série / TCP — port sécurisé de `slsnif 0.4.4` en C#/.NET 10.

Le trafic entre le port de l'application et celui du périphérique réel est relayé dans les deux sens et journalisé, avec horodatage, affichage hexadécimal et captures brutes (tee) optionnelles.

## Projets

| Projet | Type | Description |
|---|---|---|
| **ComSniffer.Core** | bibliothèque | Moteur `SnifferEngine` (forwarding + événements `PacketCaptured`/`StatusChanged`/`ErrorOccurred`), orchestrateur `SnifferSession` partagé par la GUI et les hôtes (état, compteurs, messages d'erreur), `SnifferOptions`, endpoints série/TCP, formatage, persistance des réglages (`GuiSettings`). |
| **ComSniffer** | console | Interface en ligne de commande, compatible slsnif. |
| **ComSniffer.Gui** | WinForms | GUI : deux panneaux temps réel (Device→Host / Host→Device), hex + ASCII, compteurs de débit, log/tee, thème sombre suivi du système. |
| **ComSniffer.Tests** | xUnit | Tests du parsing, du formatage et du forwarding TCP end-to-end. |

`ComSniffer.Core` est aussi référencé par `Plugins.ComSniffer` (dépôt `ToolsBox` voisin) qui expose le sniffer comme module de l'application hôte.

## Endpoints

| Syntaxe | Signification |
|---|---|
| `COMx`, `\\.\COMx` | port série local |
| `tcp:hôte:port` | connexion TCP sortante (ex. périphérique ser2net) |
| `listen:port`, `listen:ip:port` | écoute TCP (côté application uniquement) |

Une paire virtuelle **com0com** permet d'intercepter une application existante : l'application se connecte à `COM10`, le sniffer sur `COM11` relaye vers le vrai périphérique. Le baudrate n'est pas propagé par la paire — réglez-le avec `-s`.

Le sniffer est un relais (MITM) : il ouvre **les deux** endpoints, qui doivent donc être libres — un port série n'accepte qu'un seul processus à la fois. Pour tester sans matériel réel, deux options :

- une seconde paire com0com côté `device`, avec un terminal série (ou un script) comme faux périphérique :

  ```
  [Application]--COM10 ↔ COM11--[ComSniffer]--COM20 ↔ COM21--[Faux périphérique]
                      paire #1                      paire #2
  ```

- ou un endpoint TCP côté `device` (`-d listen:4001` puis un client TCP — netcat, telnet… — sur `localhost:4001`) : une seule paire com0com suffit.

Une surveillance purement passive (sans interposition) n'est pas possible en mode utilisateur : elle requiert un driver filtre noyau ou un multiplexeur externe (hub4com).

## Utilisation — CLI

```bash
dotnet run --project ComSniffer -- -d COM1 -a COM11 -s 115200 -x -t
```

```
  -d, --device <ep>     périphérique réel : COMx | tcp:hôte:port
  -a, --appport <ep>    côté application  : COMx | tcp:... | listen:...
  -s, --speed <baud>    baudrate (défaut : 9600)
  --databits <5-8>      bits de données (défaut : 8)
  --parity <p>          none | even | odd | mark | space
  --stopbits <s>        1 | 1.5 | 2
  --handshake <h>       none | xonxoff | rts | rtsxonxoff
  -x, --hex             affichage hexadécimal
  -t, --timestamp       horodatage par paquet
  -b, --bytes           nombre d'octets par paquet
  --log <fichier>       sortie formatée vers fichier
  -i, --in-tee <fich.>  octets bruts device → host (répétable)
  -o, --out-tee <fich.> octets bruts host → device (répétable)
  -l, --list            liste les ports série détectés
```

Fichier de configuration : `%USERPROFILE%\.slsnifrc` (une option par ligne, ex. `SPEED 9600`, `TIMESTAMP ON`).

## Utilisation — GUI

```bash
dotnet run --project ComSniffer.Gui
```

Réglages persistés dans `%AppData%\ComSniffer\settings.json` (partagés avec le plugin ToolsBox). À chaque capture, le log formaté et les tee bruts (device→host / host→device) sont écrits automatiquement dans le sous-dossier `Logs\` de l'application, avec un nom horodaté.

## Build et tests

```bash
dotnet build ComSniffer.sln
dotnet test ComSniffer.sln
```
