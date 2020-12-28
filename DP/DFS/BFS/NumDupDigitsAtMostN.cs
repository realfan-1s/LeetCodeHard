using System.Threading;
using System.Collections.Generic;
public class Solution {
    public int NumDupDigitsAtMostN(int N) {
        List<int> digits = new List<int>();
        int total = 0;
        // 获取对应位数全满时的最大值
        for (int i = N + 1; i > 0; i /= 10)
            digits.Insert(0, i % 10);

        int length = digits.Count;
        // 首位为0
        for (int i = 1; i < length; i++)
            total += 9 * A(9, i - 1);

        // 首位不为0
        HashSet<int> ans = new HashSet<int>();
        for (int i = 0; i < length; i++)
        {
            for (int j = (i == 0 ? 1 : 0); j < digits[i]; j++)
                if (!ans.Contains(j))
                    total += A(9 - i, length - i - 1);
            // 特殊边界条件，第二位的数字与首位相同，直接跳过
            if (ans.Contains(digits[i]))
                break;
            ans.Add(digits[i]);
        }
        return N - total;
    }
    public int A(int m, int n) => n == 0 ? 1 : A(m, n - 1) * (m - n + 1);
}