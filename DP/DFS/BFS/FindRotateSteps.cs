using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Text.RegularExpressions;
public class Solution {
    Dictionary<char, IList<int>> charInRing = new Dictionary<char, IList<int>> ();
    public int FindRotateSteps (string ring, string key) {
        int?[, ] dp = new int?[ring.Length, key.Length];
        for (int i = 0; i < ring.Length; i++) {
            if (!charInRing.ContainsKey (ring[i]))
                charInRing[ring[i]] = new List<int> ();
            charInRing[ring[i]].Add (i);
        }

        // 旋转表盘的步数+按下的步数
        return Helper (ref ring, ref key, 0, 0, dp) + key.Length;
    }

    private int Helper (ref string ring, ref string key, int ringidx, int keyidx, int?[, ] dp) {
        if (keyidx == key.Length)
            return 0;
        if (dp[ringidx, keyidx].HasValue)
            return dp[ringidx, keyidx].Value;

        int res = int.MaxValue;
        // 获得目标字符在表盘中的位置（可能有多个）
        foreach (var k in charInRing[key[keyidx]]) {
            var moves = Math.Min (ring.Length + ringidx - k, Math.Min (Math.Abs (ringidx - k), ring.Length - ringidx + k));
            res = Math.Min (res, moves + Helper (ref ring, ref key, k, keyidx + 1, dp));
        }
        dp[ringidx, keyidx] = res;
        return res;
    }
}