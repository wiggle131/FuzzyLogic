using System;
using System.Collections.Generic;
public class StockList
{
    public static string stockSummary(string[] lstOfCode, string[] lstOfCategory)
    {
        //code here
        string[] temp;
        IDictionary<string, int> num = new Dictionary<string, int>();
        int i = 0;
        string firstLetter;
        foreach (var x in lstOfCode)
        {
            temp = x.Split(' ');
            firstLetter = Convert.ToString(temp[0][0]);
            if (Contains(Convert.ToString(temp[0][0]), lstOfCategory))
            {
                if (num.ContainsKey(firstLetter))
                {
                    num[firstLetter] = Convert.ToInt32(temp[1]) + num[firstLetter];
                }
                else
                {
                    num.Add(firstLetter, Convert.ToInt32(temp[1]));
                }
            }

            lstOfCode[i] = String.Concat(temp[0][0], ' ', temp[1]);
            i++;
        }
        string output = "";
        foreach (var x in lstOfCategory)
        {
            output = ("({0} : {1})", x, num[x]);
        }

        return Convert.ToString(num['B']);
    }
    private static bool Contains(string test, string[] lstOfCategory)
    {
        bool flag = false;
        foreach (var x in lstOfCategory)
        {
            if (test == x)
            {
                flag = true;
                break;
            }
        }
        return flag;
    }
}


