Welcome in Mow Jow !

Projet de développement applicatif objet en C# avec Visual Studio 2015.

Il s'agit de la replication du jeu de cartes Mow, qui mets en scène des fermiers s'affrontant via des vaches !
Le jeu est accessible à un ou plusieurs joueurs, humains ou contrôlés par l'ordinateur.

4 personnes font partie du développement : 

  * Adrien : Lead, Interfaces, MVVM
  
  * Charles : Algorithmes, IA
  
  * Nicolas : Déroulement du jeu
  
  * Guillaume : Système de statistiques des parties
    

Aspect :

  * Résolution de l'application : 1024x768
  
  * Plateau de jeu au centre
  
  * Joueurs adverses en haut de l'écran
  
  * Main en bas de l'écran
  
  * Etable personnelle en bas à droite
  
  * Pioche sur la gauche, sens de jeu sur la droite
  
  * Retour menu principal en haut à gauche
  
  * Statistiques en haut à droite


Lancement de l'application :

  * Possibilités :
  
    - Jouer 
    
        -> Nouvelle partie
        
          + Réglages partie
              
        -> Charger ancienne partie
            
    - Statistiques -> Voir les statistiques
    
    - Regles -> Voir les règles
    
    - Crédits -> Voir les crédits
    
Déroulement d'une partie :

  * Chaque joueur commence par recevoir 5 cartes
  
  * Le jeu se déroule dans le sens désigné par la carte sens
  
  * A chaque tour, le joueur pioche une carte et regarde s'il peut/décide ou non de jouer
  
    - S'il joue et pose une carte, le joueur suivant prends la suite
    
    - S'il ne peut ou choisit de ne pas jouer, toutes les cartes présentes dans le troupeau se retrouve dans son étable
    
  * Lorsque la pioche est vide, on termine la manche de jeu, cette dernière étant perdue par le dernier joueur ne pouvant pas jouer
  
  * A chaque nouvelle manche, les cartes sont mélangées avant d'être distribuées
  
  * La partie se termine lorsqu'un joueur atteint 100 mouches présentes dans son étable


Règles du jeu :

* Nombre de joueurs : 1 à 5, dont au moins 1 joueur humain (5 joueurs humains -  max 4 joueurs IA)

* Objectif : terminer la partie avec le moins possible de mouches présentes auprès des vaches.

Lorsqu'un joueur atteint 100 mouches, le jeu prend fin. Celui qui a le moins de points l'emporte !

Les joueurs conservent 5 cartes dans leur main. 

Ils ont pour but de se débarasser de leurs cartes dans le troupeau (cartes posées par les joueurs sur la table).
Lorsqu'un joueur ne peut ni piocher ni jouer, il doit ramasser le troupeau qui termine dans son étable. 
Les cartes réprésentent des vaches et peuvent être accompagnées de 0 à 5 mouches. 
C'est le nombre de mouches récupérées par le joueur qui détermine son score final.

Cartes du jeu :

 * 15 cartes vaches (numérotées de 1 à 15), avec 0 mouche
 
 * 13 cartes vaches (numérotées de 2 à 14), avec 1 mouche
 
 * 11 cartes vaches (numérotées de 3 à 13), avec 2 mouches
 
 * 3 cartes vaches (numérotées 7, 8, 9), avec 3 mouches
 
 * 6 cartes vaches spéciales, avec 5 mouches

Cartes spéciales :

  * Vaches serre file : 0 et 16 
  
    - Bloquent chacune une extrémité du troupeau
    
  * Vaches acrobates : 7 et 9
  
    - Se placent au dessus d'une vache portant le même numéro
    
  * Vaches retardataires : 2 cartes
  
    - S'insèrent entre 2 cartes dont la valeur est au moins éloignée de 2 

Lorsqu'un joueur utilise une carte spéciale, il peut décider de faire tourner le sens du jeu.
