#include <stdc++.h>

using namespace std;

class Solution {
public:
    bool line[9][9];
    bool column[9][9];
    bool block[3][3][9];
    bool validate;
    vector<pair<int, int>> space;

    void solveSudoku(vector<vector<char>>& board) {
        memset(line, false, sizeof(line));
        memset(column, false, sizeof(column));
        memset(block, false, sizeof(block));
        validate = false;

        for (int i = 0; i < 9; i++)
            for (int j = 0; j < 9; j++){
                if (board[i][j] == '.')
                    space.emplace_back(i ,j);
                else{
                    int digit = board[i][j] - '0' - 1;
                    line[i][digit] = column[j][digit] = block[i / 3][j / 3][digit] = true;
                }
            }

        DFS(board, 0);
    }

    void DFS(vector<vector<char>>& board, int index){
        // 一行填完
        if (index == space.size()){
            validate = true;
            return;
        }

        auto [i, j] = space[index];
        for (int digit = 0; digit < 9 && !validate; digit++){
            if (!line[i][digit] && !column[j][digit] && !block[i / 3][j / 3][digit]){
                line[i][digit] = column[j][digit] = block[i / 3][j / 3][digit] = true;
                board[i][j] = digit + '0' + 1;
                DFS(board, index + 1);
                line[i][digit] = column[j][digit] = block[i / 3][j / 3][digit] = false;
            }
        }
    }
};