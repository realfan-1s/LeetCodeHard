# 基于集合的回溯
class Solution:
    def solveNQueens(self, n: int):
        def GenerateQueens():
            board = list()
            for i in range(n):
                row[queens[i]] = "Q"
                board.append("".join(row))
                row[queens[i]] = "."
            return board

        def TraceBack(row: int):
            if row == n:
                # 当每一列都搜索完毕后，初始化棋盘
                board = GenerateQueens()
                solution.append(board)
            else:
                # 横向遍历
                for i in range(n):
                    if i in cols or row - i in diagonal1 or row + i in diagonal2:
                        continue
                    queens[row] = i
                    cols.add(i)
                    diagonal1.add(row - i)
                    diagonal2.add(row + i)
                    # 递归调用，枚举下一行时的情况
                    TraceBack(row + 1)
                    cols.remove(i)
                    diagonal1.remove(row - i)
                    diagonal2.remove(row + i)

        solution = list()
        queens = [-1] * n
        row = ["."] * n
        cols = set()
        # 保存皇后在斜线上的位置
        diagonal1 = set()
        diagonal2 = set()
        TraceBack(0)
        return solution
