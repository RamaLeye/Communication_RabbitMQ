# TP4_PatronsModeles_Communication

Cette solution est composé de 3 projets consoles :
- LogEmitter : un Sender
- FileDump et LogAnalysis : des Receivers


Exécution depuis Visual Studio : 
Pour le projet LogEmitter, il suffit de l'exécuter et d'appuyer sur la touche entrée à chaque fois que vous souhaitez créer un log. Les paramétres de ce log ( level et message) sont crées aleatoirement dans la classe RandomObjects.
Executer les 3 projets simultanément et la consommation des logs s'affichera sur les différentes consoles de FileDump et LogAnalysis.


Execution depuis le PowerShell : 
Ouvrir 3 shells et pour chaque projet, se positionner dans le dossier correspondant. 

- cd LogEmitter 

- cd FileDump 

- cd LogAnalysis 

Et faire un dotnet run dans chaque shell.
De la meme manière que sur visual studio, la création de logs dans le shell LogEmitter se fait en appuyant sur la touche entrée.


Le fichier .log où seront écrits les differents logs de FileDump se situe dans le dossier où se trouve le .exe du projet s'il est lancé depuis Visual studio et est dans le dossier FileDump s'il est lancé depuis le PowerShell. Il n'est pas écrasé afin de garder l'historique des derniers logs.
Pour LogAnalysis, j'ai choisi de créer une classe TimerLog où on retrouvera la fonction d'affichage du nombre de logs, un timer de 10 secondes et une fonction qui va aller chercher les logs dans les queues en fontion de la clé de routage.  