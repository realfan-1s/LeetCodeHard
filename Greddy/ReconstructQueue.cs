using System;
using System.Collections;
using System.Collections.Generic;

public class Solution {
    public int[][] ReconstructQueue (int[][] people) {
        // 优先按照身高降序排序，倘若身高一样再按照k值升序排序
        Array.Sort (people, (x, y) => x[0].CompareTo (y[0]) * (-2) + x[1].CompareTo (y[1]));

        List<int[]> res = new List<int[]> ();

        // 逐个地把它们放在输出队列中，索引等于它们的 k 值。
        foreach (var person in people)
            res.Insert (person[1], person);
        return res.ToArray ();
    }
}