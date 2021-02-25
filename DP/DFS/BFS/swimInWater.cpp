#include <vector>
#include <numeric>

using namespace std;

// 并查集
class UnionFind
{
private:
    vector<int> parent;
    vector<int> size;
    int n;
    int setcount;

public:
    UnionFind(int _n) : parent(_n), size(_n, 1), n(_n), setcount(_n)
    {
        iota(parent.begin(), parent.end(), 0);
    }
    int Find(int x)
    {
        while (parent[x] != x)
        {
            parent[x] = parent[parent[x]];
            x = parent[x];
        }
        return x;
    }
    bool IsConnected(int x, int y)
    {
        return Find(x) == Find(y);
    }
    void Merge(int x, int y)
    {
        x = Find(x);
        y = Find(y);
        if (x == y)
            return;
        if (size[x] < size[y])
        {
            swap(x, y);
        }
        parent[y] = x;
        size[x] += y;
        --setcount;
    }
    int GetSetCount()
    {
        return setcount;
    }
};

class Solution
{
private:
    constexpr static int direction[4][2] = {{1, 0}, {-1, 0}, {0, 1}, {0, -1}};
    int n;

public:
    int swimInWater(vector<vector<int>> &grid)
    {
        n = grid.size();
        int len = n * n;
        UnionFind uf(len);
        vector<int> length(len);
        for (int i = 0; i < n; ++i)
            for (int j = 0; j < n; ++j)
            {
                length[grid[i][j]] = getindex(i, j);
            }

        for (int i = 0; i < len; ++i)
        {
            int x = length[i] / n;
            int y = length[i] % n;
            for (const auto &dir : direction)
            {
                int nx = x + dir[0];
                int ny = y + dir[1];
                if (nx >= 0 && nx < n && ny >= 0 && ny < n && grid[nx][ny] <= i)
                {
                    uf.Merge(length[i], getindex(nx, ny));
                }
                if (uf.IsConnected(0, len - 1))
                    return i;
            }
        }

        return -1;
    }

private:
    int getindex(int x, int y)
    {
        return x * n + y;
    }
};

// 二分搜索+DFS
class Solution
{
private:
    int n;
    constexpr static int direction[4][2] = {{1, 0}, {-1, 0}, {0, 1}, {0, -1}};

public:
    int swimInWater(vector<vector<int>> &grid)
    {
        n = grid.size();
        int len = n * n;
        int left = 0, right = n * n - 1;
        while (left < right)
        {
            int mid = (left + right) / 2;
            vector<vector<bool>> visited(n, vector<bool>(n, false));
            if (grid[0][0] <= mid && DFS(grid, 0, 0, visited, mid))
                right = mid;
            else
                left = mid + 1;
        }
        return left;
    }

private:
    int InArea(int x, int y)
    {
        return x >= 0 && x < n && y >= 0 && y < n;
    }
    bool DFS(vector<vector<int>> &grid, int c, int r, vector<vector<bool>> &visited, int target)
    {
        if (c == n - 1 && r == n - 1)
            return true;
        visited[c][r] = true;
        for (const auto &dir : direction)
        {
            int x = c + dir[0];
            int y = r + dir[1];
            if (InArea(x, y) && !visited[x][y] && grid[x][y] <= target)
            {
                if (DFS(grid, x, y, visited, target))
                    return true;
            }
        }

        return false;
    }
};
