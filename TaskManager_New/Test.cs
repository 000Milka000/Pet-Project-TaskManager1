namespace TaskManager_New
{
        public class Solution
        {
            public bool IsSubsequence(string s, string t)
            {
                var numpoz = new List<int>();

                for(int a = 0; a < s.Length; a++)
                {
                    char ch = s[a];
                    if (t.Contains(ch)){
                        numpoz.Add(t.IndexOf(ch));
                    }
                    else
                    {
                        return false;
                    }
                }

                bool result = true;
                foreach(var r in numpoz)
                {
                    int indexR = numpoz.IndexOf(r);
                    int num2 = numpoz[indexR - 1];
                    if (num2 == -1)
                    {
                        continue;
                }
                else
                {
                    if(r < num2)
                    {
                        result = false;
                    }
                }
                }
                return result;
            }
        }
    }


//s = "abc", t = "ahbgdc" 0, 2, 5