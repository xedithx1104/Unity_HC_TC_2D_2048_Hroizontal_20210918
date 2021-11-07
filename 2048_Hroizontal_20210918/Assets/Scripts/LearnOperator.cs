using UnityEngine;

/// <summary>
/// 認識運算子
/// 1. 指定
/// 2. 數學
/// 3. 比較
/// 4. 邏輯
/// </summary>
public class LearnOperator : MonoBehaviour
{
    public float a = 10;
    public float b = 3;
    public int c = 30;
    public int hp = 100;

    private void Start()
    {
        #region 指定運算子
        // 指定 =
        // 先執行指定運算子右邊再指定給左邊
        #endregion

        #region 數學運算子
        // 加減乘除餘
        // + - * / %
        print("加法：" + (a + b));     // 結果 13
        print("減法：" + (a - b));     // 結果 7
        print("乘法：" + (a * b));     // 結果 30
        print("除法：" + (a / b));     // 結果 3.3333
        print("餘法：" + (a % b));     // 結果 1

        c = c + 1;                      // 原始寫法
        print("C 結果：" + c);
        c++;                            // 遞增 ++，遞減 -- 
        print("C 結果：" + c);

        hp = hp + 10;
        print("HP 結果：" + hp);
        hp += 10;                       // 適用數學運算子 += -= *= /= %=
        print("HP 結果：" + hp);
        #endregion

        #region 比較運算子
        // 大於、小於、大等於、小等於、等於、不等於
        // > < >= <= == !=
        // 比較兩個值，並且得到布林值結果

        print("a > b " + (a > b));      // t
        print("a < b " + (a < b));      // f
        print("a >= b " + (a >= b));    // t
        print("a <= b " + (a <= b));    // f
        print("a == b " + (a == b));    // f
        print("a != b " + (a != b));    // t
        #endregion

        #region 邏輯運算子
        // 並且、或者、顛倒
        // && || !
        // 並且、或者
        // 比較兩個布林值，並且得到布林值結果
        // 並且：只要有一個 F 結果就是 F
        print("T && T " + (true && true));       // T
        print("F && T " + (false && true));      // F
        print("T && F " + (true && false));      // F
        print("F && T " + (false && false));     // F
        // 或者：只要有一個 T 結果就是 F
        print("T || T " + (true || true));       // T
        print("F || T " + (false || true));      // T
        print("T || F " + (true || false));      // T
        print("F || T " + (false || false));     // F
                                                 // Alt + Shift + > 快速選取
        // 顛倒：只能加在布林值前面
        print(!true);               // F
        print(!(a > b));            // F
        print(!(true && true));     // F
        #endregion
    }
} 
