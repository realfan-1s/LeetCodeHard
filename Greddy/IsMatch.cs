public class Solution {
    public bool IsMatch(string s, string p) {
        int s_Length = s.Length;
        int p_Length = p.Length;
        int fast = 0, slow = 0, istar = -1, jstar = -1;

        while (fast < s_Length)
        {
            if (slow < p_Length && (s[fast] == p[slow] || p[slow] == '?'))
            {
                fast++;
                slow++;
            }
            else if (slow < p_Length && p[slow] == '*') //记录如果之后序列匹配不成功时， i和j需要回溯到的位置
            {
                istar = fast;
                jstar = slow++;
            }
            else if (istar >= 0)//发现字符不匹配且没有星号出现 但是istar>0 说明可能是*匹配的字符数量不对 这时回溯
            {
                fast = ++istar;
                // fast回溯到istar+1 因为上次从s串istar开始对*的尝试匹配已经被证明是不成功的（不然不会落入此分支） 
                // 所以需要从istar+1再开始试 同时inc istar 更新回溯位置
                slow = jstar + 1;
                // slow回溯到jstar+1 重新使用p串*后的部分开始对s串istar（这个istar在上一行已经inc过了）位置及之后字符的匹配 
            }
            else
                return false;
        }

        while (slow < p_Length && p[slow] == '*')
            slow++;

        return slow == p_Length;
    }
}