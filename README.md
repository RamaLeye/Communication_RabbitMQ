# TP4_PatronsModeles_Communication

Cette solution est composé de 3 projets consoles :
- LogEmitter : un Sender
- FileDump et LogAnalysis : des Receivers

Pour le projet LogEmitter, il suffit de l'exécuter et d'appuyer sur la touche entrée à chaque fois que vous souhaitez créer un log. Les paramétres de ce log ( level et message) sont crées aleatoirement.
Lancer les 3 projets simultanément et la consommation des logs s'affichera sur les différentes consoles de FileDump et LogAnalysis.

Le fichier .log où seront écrits les differents logs de FileDump se situe dans le dossier où se trouve le .exe du projet. Il n'est pas écrasé afin de garder l'historique des derniers logs.

Pour LogAnalysis, j'ai choisi de créer une classe TimerLog où on retrouvera la fonction d'affichage du nombre de logs, un timer de 10 secondes et une fonction qui va aller chercher les logs dans les queues en fontion de la clé de routage.  