#include <vector>

using namespace std;

// 我们需要将二维转化为一维，对于矩阵的每一列，我们将其加在一起，成为了一维上的一个数，二维矩阵的和转化为了一维数组的和
class Solution
{
public:
    vector<int> getMaxMatrix(vector<vector<int>> &matrix)
    {
        int col = matrix[0].size();
        int row = matrix.size();
        vector<int> ans(4, 0);
        vector<int> dp(col, 0);
        int sum;
        // 用于记录最大值
        int maxn = INT32_MIN;
        int bestr1, bestc1;

        // 边的上界
        for (int i = 0; i < row; ++i)
        {
            for (int t = 0; t < col; ++t)
                dp[t] = 0;

            // 边的下界
            for (int j = i; j < row; ++j)
            {
                sum = 0;
                for (int k = 0; k < col; ++k)
                {
                    dp[k] += matrix[j][k];
                    if (sum > 0)
                    {
                        sum += dp[k];
                    }
                    else
                    {
                        sum = dp[k];
                        bestr1 = i;
                        bestc1 = k;
                    }

                    if (sum > maxn)
                    {
                        maxn = sum;
                        ans[0] = bestr1;
                        ans[1] = bestc1;
                        ans[2] = j;
                        ans[3] = k;
                    }
                }
            }
        }

        return ans;
    }
};