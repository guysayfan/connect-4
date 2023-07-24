# connect-4
Connect 4 game in C# with enemy AI.


# Algorithm for super AI

- Lookahead N plays
- Generate all possible boards 
- Boards with 4 in a row at play < N, continue as is until play N
- Evaluate each board after N plays
	AI wins in two columns: 1
	AI wins in two consecutive placements in the same column: 1
	AI loses in two columns: -1
	AI loses in two consecutive placements in the same column: -1
	Anything else: 0
- For each possible board keep the sequence of plays that generate the board
	- The same posible board can be arrived at with different sequence of plays when N > 2
- If there are boards with score of 1, choose randomly from the sequence of plays that lead to these boards
- Else if there noare board with score 0, choose randomly from the sequence of plays that lead to these boards
- Else, you're going to lose... choose any column


## Alternative way to find sure winners

Board is considered a winning board at turn N - 2 for player A if player A wins at turn N, regardless of which column player B plays at turn N - 1 

# Reference

[Connect 4 AI - How it works!](https://roadtolarissa.com/connect-4-ai-how-it-works)
[HackMD Design Doc](https://hackmd.io/hGzjvttQQtStH9FA595j3A)

