/*
OUTLINE:
---------
Features:
1. On start menu with play & saved game button
2. Load grid when play button is pressed (generates tiles and places the pieces on the board)
3. Game timer at the top middle of the screen
4. Menu & Save Game button
5. Turn indicator on the top right
6. Two player chess game, NOT AI
7. !** No special moves : No castling, no pawn promotion, no en passant capture,
     no auto checkmate but there is a check indicator.
8. When a piece is selected highlights possible tiles to move to if its their turn.
9. Pieces can only move to those highlighted places (no illegal moves)
-----------
Scripts:
(Main Camera) AdjustCamera -> adjust camera rect according to screen resolution
(Game Manager) GameMaster -> responsible for game phase changes, (ui)turn
                            *TurnChangeEvent
(Game Manager) JsonManager -> save game, load game
(Tile Manager) PieceMovement -> contains the methods for events such as
                         *PieceSelectedEvent, *PieceMovedEvent
Piece -> abstract class containing methods that are to be used by every piece type:
                Pawn, Rook, Knight, Bishop, Queen, King : Piece,
                Bool isFirstMove,
                [12]GameObject PiecePrefabs, Piece pieceType, HighlightSelectableTiles method
                invokes event that uses HighlightTile(Tile), IntersectsKingSelectable
(Tile Manager) TileManager -> (GenerateGrid) spawn tiles method triggered by play button on click,
                             keeps tiles and  pieces in 8x8 int array ;
                             1st digit = W: 1, B: 2 
                             0:Empty, 1Pawn, 2:Rook, 3:Knight, 4:Bishop, 5:Queen, 6:King
                             StartArray :
                                  0   1   2   3   4   5   6   7
                             0 [ 22, 23, 24, 25, 26, 24, 23, 22 
                             1   21, 21, 21, 21, 21, 21, 21, 21
                             2   0 , 0 , 0 , 0 , 0 , 0 , 0 , 0  
                             3   0 , 0 , 0 , 0 , 0 , 0 , 0 , 0  
                             4   0 , 0 , 0 , 0 , 0 , 0 , 0 , 0  
                             5   0 , 0 , 0 , 0 , 0 , 0 , 0 , 0  
                             6   11, 11, 11, 11, 11, 11, 11, 11
                             7   12, 13, 14, 15, 16, 14, 13, 12 ]
                             
                             , UpdateBoard method
(Tile Manager) TileManager -> Tile[] tileArray, Tile currentlySelectedTile (onmousedown checks if there is a selected tile and acts accordingly)
                              ConvertToTileArray method, UpdateTileArray,
                              DeselectAllTiles, SelectableAllFalse
(Tile Prefab) Tile -> Piece holdedPiece, Color baseColor/offsetColor,
                    GameObject hover/selected/selectable(setActive), SpriteRenderer sr,
                    Bool isSelectable(when a tile is highlighted => true),
                    HighlightTile method, OnMouseEnter, OnMouseDown, OnMouseExit,
                    SelectTile method, DeselectTile method (invokes event to deselect all tiles)
                    *PieceMovedEvent, *DeselectTileEvent
(UI) -> UIManager -> change turn method, canvas enable/disable methods, on click events
(UI/Timer) -> TimerScript -> updates ui for game timer
-------------
Events:
1. *TurnChangeEvent -> changes game turn, makes appropriate ui changes
2. *PieceSelectedEvent -> checks if tile has a piece, checks piece color with the game turn,
                          highlight appropriate tiles for selected piece
3. *DeselectTileEvent -> Tile manager, DeselectAllTiles
4. *PieceMovedEvent -> change turn event invoked, checks if the clicked tile has a piece, 
                        moves piece o clicked tile, if clicked tile has the king *GameEndEvent
5. *GameEndEvent -> end game canvas opens, shows who won the game and replay button
6. *NoSavedGameEvent -> disabled saved game button, and displays warning message
*/