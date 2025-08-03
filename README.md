# Knightmare Chess Engine

**Knightmare** is an open source chess engine. It currently supports the [UCI protocol](https://en.wikipedia.org/wiki/Universal_Chess_Interface)

---

## Techniques so far

The engine currently implements:

- **Alpha-Beta Pruning** – Optimized minimax algorithm for deeper search trees.
- **Bitboard Representation** – Efficient board state representation using 64-bit integers.
- **Magic Bitboards** – Fast sliding piece move generation for bishops, rooks and queens.
- **Move Ordering: MVV-LVA** – "Most Valuable Victim - Least Valuable Attacker" heuristic to improve pruning efficiency.

---

## UCI commands

Knightmare supports standard UCI commands:

### Set up a position:
```bash
position startpos
position startpos moves e2e4 e7e5
```
### Start search for best move:
```bash
go
```
### Enable CLI Debug UI:
```bash
debug on
```

---

## Useful resources:
- https://www.chessprogramming.org
- https://www.chessprogramming.org/Alpha-Beta
- https://www.chessprogramming.org/Bitboards
- https://www.chessprogramming.org/Magic_Bitboards
- https://www.chessprogramming.org/Transposition_Table
