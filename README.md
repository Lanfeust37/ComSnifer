# ComSnifer

Sniffer bidirectionnel de lignes série / TCP — port sécurisé de `slsnif 0.4.4` en C#/.NET 10.

Le trafic entre le port de l'application et celui du périphérique réel est relayé dans les deux sens et journalisé, avec horodatage, affichage hexadécimal et captures brutes (tee) optionnelles.

## Projets

| Projet | Type | Description |
|---|---|---|
| **ComSnifer.Core** | bibliothèque | Moteur `SnifferEngine` (forwarding + événements `PacketCaptured`/`StatusChanged`/`ErrorOccurred`), `SnifferOptions`, endpoints série/TCP, formatage, persistance des réglages (`GuiSettings`). |
| **ComSnifer** | console | Interface en ligne de commande, compatible slsnif. |
| **ComSnifer.Gui** | WinForms | GUI : deux panneaux temps réel (Device→Host / Host→Device), hex + ASCII, compteurs de débit, log/tee, thème sombre suivi du système. |
| **ComSnifer.Tests** | xUnit | Tests du parsing, du formatage et du forwarding TCP end-to-end. |

`ComSnifer.Core` est aussi référencé par `Plugins.ComSnifer` (dépôt `ToolsBox` voisin) qui expose le sniffer comme module de l'application hôte.

## Endpoints

| Syntaxe | Signification |
|---|---|
| `COMx`, `\\.\COMx` | port série local |
| `tcp:hôte:port` | connexion TCP sortante (ex. périphérique ser2net) |
| `listen:port`, `listen:ip:port` | écoute TCP (côté application uniquement) |

Une paire virtuelle **com0com** permet d'intercepter une application existante : l'application se connecte à `COM10`, le sniffer sur `COM11` relaye vers le vrai périphérique. Le baudrate n'est pas propagé par la paire — réglez-le avec `-s`.

## Utilisation — CLI

```bash
dotnet run --project ComSnifer -- -d COM1 -a COM11 -s 115200 -x -t
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
dotnet run --project ComSnifer.Gui
```

Réglages persistés dans `%AppData%\ComSnifer\settings.json` (partagés avec le plugin ToolsBox).

## Build et tests

```bash
dotnet build ComSnifer.sln
dotnet test ComSnifer.sln
```
