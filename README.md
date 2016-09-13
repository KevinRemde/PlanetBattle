# PlanetBattle
PlanetBattle is a multi-player strategy game, initially concieved of and built during the Microsoft DX US Hackathon on 9/12-9/13/2016.

This simple game is losely based on a strategy game I used to play on my Apple ][+ back in the day.  In a "universe" (really a 2D X,Y grid), planets start with a home planet and a certain number of ships.
Each round is made up of a turn for each player, where they send ships from a planet toward another planet.  
Once all players have entered their turn choices, the round ends by adjusting all moved ships to new coordinates.  Ships not yet at their destination are just "out there", and won't be available to move until they arrive at their destination in subsequent rounds. Any ships that do arrive at their destination will either take ownership of an un-occupied planet, or do battle with any opposing ships that already exist at that planet.  All battles are simple "to the death" random winners/losers.. and the winner (and their remaining ships) remain on the planet.  At that point the next round begins.
The game ends when all ships in the game are owned by only one remaining player.

# The Code So Far...
As of 9/13/2016, we (David Giard and myself) have designed the object models for the ships, planets, players, universe, game, and logic for battles.  Our first working game .exe will be a simple console app that walks the players through rounds and turns, showing simple text results of battles and list of planets and ownership/ship numbers.
Next version may be a UWP app, with all players working from the same PC or phone.

# Ultimate goals
Multi-device, cloud-based gameplay
Simple Graphical grid of the "universe", with planets, owners, and ship numbers shown. 
Notifications of "end of round results" whenever all players have submitted their turns/moves.
Game organizer creates the game and invites players.  
