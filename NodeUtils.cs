using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace LinkChain
{
    /// <summary>
    /// אוסף פעולות עזר על שרשרת חוליות (Node<T>).
    /// שלד לתרגילים המקדימים ולתרגילים 1-23 (לפי המספור שבדף התרגילים).
    /// לכל תרגיל שנדרש בו גם מימוש רקורסיבי קיימת פעולה נוספת באותו שם עם תוספת R.
    /// אין להוסיף פרמטרים לחתימות (למעט CreateListFromArrayR שבו הפרמטר index נתון בתרגיל).
    /// </summary>
    /// 
    //  Exercises 1-10 are from part 1 of this project - handed in and fixed all problems\mistakes
    //  Exercises 11-23 are part 2 of this project
    internal class NodeUtils
    {
        //--------------------- Part 1 ---------------------

        // ============================================================
        // תרגיל מקדים א - בניית רשימת חוליות ממערך
        // ============================================================

        /// <summary>
        /// מקבלת מערך ומחזירה הפנייה לרשימת חוליות המכילה את אברי המערך לפי הסדר.
        /// מימוש איטרטיבי.
        /// </summary>
        public static Node<T> CreateListFromArray<T>(T[] arr)
        {
            Node<T> lst = null;
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                lst = new Node<T>(arr[i], lst);
            }
            return lst;
        }

        /// <summary>
        /// כמו CreateListFromArray, מימוש רקורסיבי.
        /// index - האינדקס במערך שממנו ממשיכים לבנות.
        /// </summary>
        public static Node<T> CreateListFromArrayR<T>(T[] arr, int index)//starts from end
        {
            if(arr == null || arr.Length == 0)
            {
                return null;
            }
            if (index == arr.Length - 1)//leaving exeption
            {
                return new Node<T>(arr[index]);
            }
            else
            {
                Node<T> new_node = new Node<T>(arr[index], CreateListFromArrayR(arr, index + 1));
                return new_node;
            }
        }

        // ============================================================
        // תרגיל מקדים ב - הדפסת רשימה
        // ============================================================

        /// <summary>
        /// מקבלת הפנייה לרשימת חוליות ומדפיסה את אבריה.
        /// מימוש איטרטיבי.
        /// </summary>
        public static void PrintList<T>(Node<T> l)
        {
            if(l == null)
            {
                Console.WriteLine("List is empty.");
                return;
            }
            while (l.HasNext())
            {
                Console.Write(" -> " + l.GetValue());
                l = l.GetNext();
            }
            Console.WriteLine(" -> " + l.GetValue());
        }

        /// <summary>
        /// כמו PrintList, מימוש רקורסיבי.
        /// </summary>
        public static void PrintListR<T>(Node<T> l)
        {
            if (l == null)
            {
                Console.WriteLine("List is empty.");
                return;
            }
            if (!l.HasNext())
            {
                Console.WriteLine(" -> " + l.GetValue());
                return;
            }
            else
            {
                Console.Write(" -> " + l.GetValue());
                l = l.GetNext();
                PrintListR(l);
            }
        }

        // ============================================================
        // תרגיל 1 - השוואת שתי רשימות
        // ============================================================

        /// <summary>
        /// מחזירה אמת אם שתי הרשימות זהות: אותו מספר איברים, אותם ערכים ובאותו סדר.
        /// הפעולה אינה גנרית כי נדרשת השוואת ערכים.
        /// מימוש איטרטיבי.
        /// </summary>
        public static bool CompareList(Node<int> left, Node<int> right)
        {
            while (left != null && right != null)
            {
                if (left.GetValue() != right.GetValue())
                    return false;

                left = left.GetNext();
                right = right.GetNext();
            }
            return left == null && right == null;
        }

        /// <summary>
        /// כמו CompareList, מימוש רקורסיבי.
        /// </summary>
        public static bool CompareListR(Node<int> left, Node<int> right)
        {
            if (left == null && right == null)
                return true;
            if (left == null || right == null)
                return false;
            if (left.GetValue() != right.GetValue())
                return false;

            return CompareListR(left.GetNext(), right.GetNext());
        }

        // ============================================================
        // תרגיל 2 - ספירת איברים
        // ============================================================

        /// <summary>
        /// מחזירה את מספר האיברים ברשימה. מימוש איטרטיבי.
        /// </summary>
        public static int CountList<T>(Node<T> lst)
        {
            int cnt = 1;
            if (lst == null)
                return 0;
            while (lst.HasNext())
            {
                cnt++;
                lst = lst.GetNext();
            }
            return cnt;
        }

        /// <summary>
        /// כמו CountList, מימוש רקורסיבי.
        /// </summary>
        public static int CountListR<T>(Node<T> lst)
        {
            if(lst == null) 
                return 0;
            if (!lst.HasNext())
                return 1;

            return CountListR(lst.GetNext()) + 1;
        }

        // ============================================================
        // תרגיל 3 - סכום איברי הרשימה
        // ============================================================

        /// <summary>
        /// מחזירה את סכום איברי הרשימה. מימוש איטרטיבי.
        /// </summary>
        public static int SumList(Node<int> lst)
        {
            if (lst == null)
            {
                return 0;
            }
            int sum = lst.GetValue();
            while (lst.HasNext())
            {
                lst = lst.GetNext();
                sum += lst.GetValue();
            }
            return sum;
        }

        /// <summary>
        /// כמו SumList, מימוש רקורסיבי.
        /// </summary>
        public static int SumListR(Node<int> lst)
        {
            if(lst == null)
            {
                return 0;
            }
            if (!lst.HasNext())
                return lst.GetValue();
            return SumListR(lst.GetNext()) + lst.GetValue();
        }

        // ============================================================
        // תרגיל 4 - האם ערך קיים ברשימה
        // ============================================================

        /// <summary>
        /// מחזירה אמת אם הערך val נמצא ברשימה, ושקר אחרת. מימוש איטרטיבי.
        /// </summary>
        public static bool IsExist(Node<int> ls, int val)
        {
            if (!ls.HasNext())
            {
                if (val == ls.GetValue())
                    return true;
                return false;
            }
            while (ls != null)
            {
                if (val == ls.GetValue())
                    return true;
                ls = ls.GetNext();
            }
            return false;
        }

        /// <summary>
        /// כמו IsExist, מימוש רקורסיבי.
        /// </summary>
        public static bool IsExistR(Node<int> ls, int val)
        {
            if (!ls.HasNext())
            {
                if (val == ls.GetValue())
                    return true;
                return false;
            }
            if (val == ls.GetValue())
                return true;
            return IsExistR(ls.GetNext(), val);
        }

        // ============================================================
        // תרגיל 5 - הערך המקסימלי ברשימה
        // ============================================================

        /// <summary>
        /// מקבלת רשימה שאינה ריקה ומחזירה את הערך המקסימלי שבה. מימוש איטרטיבי.
        /// </summary>
        public static int MaxList(Node<int> lst)
        {
            int max = int.MinValue;
            if (!lst.HasNext())
                return lst.GetValue();
            while (lst != null)
            {
                if (lst.GetValue() > max)
                    max = lst.GetValue();
                lst = lst.GetNext();
            }
            return max;
        }

        /// <summary>
        /// כמו MaxList, מימוש רקורסיבי.
        /// </summary>
        public static int MaxListR(Node<int> lst)
        {
            if (!lst.HasNext())
                return lst.GetValue();
            if (lst.GetValue() > MaxListR(lst.GetNext()))
                return lst.GetValue();
            else
                return MaxListR(lst.GetNext());
        }
        // ============================================================
        // תרגיל 6 - החלפת ערכים שליליים בערכם המוחלט
        // ============================================================

        /// <summary>
        /// משנה את ערכי איברי הרשימה כך שכולם יהיו חיוביים. מימוש איטרטיבי.
        /// </summary>
        public static void MakePositive(Node<int> lst)
        {
            while (lst != null)
            {
                lst.SetValue(Math.Abs(lst.GetValue()));
                lst = lst.GetNext();
            }
        }

        /// <summary>
        /// כמו MakePositive, מימוש רקורסיבי.
        /// </summary>
        public static void MakePositiveR(Node<int> lst)
        {
            if(lst == null)
            {
                return;
            }
            if (!lst.HasNext())
                lst.SetValue(Math.Abs(lst.GetValue()));
            else
            {
                lst.SetValue(Math.Abs(lst.GetValue()));
                MakePositiveR(lst.GetNext());
            }
        }

        // ============================================================
        // תרגיל 7 - קבלת החוליה שבמיקום n
        // ============================================================

        /// <summary>
        /// מחזירה הפנייה לחוליה שמיקומה n (החוליה הראשונה היא מספר 1).
        /// אם אין ברשימה n איברים מוחזר null. מימוש איטרטיבי.
        /// </summary>
        public static Node<T> GetElement<T>(Node<T> lst, int n)
        {
            if (lst == null || n < 1)
                return null;
            for (int i = 1; i < n; i++)
            {
                if (!lst.HasNext())
                    return null;
                lst = lst.GetNext();
            }
            return lst;
        }

        /// <summary>
        /// כמו GetElement, מימוש רקורסיבי.
        /// </summary>
        public static Node<T> GetElementR<T>(Node<T> lst, int n)
        {
            if (lst == null || n < 1)
                return null;
            if (n == 1)
                return lst;

            return GetElementR(lst.GetNext(), n - 1);
        }

        // ============================================================
        // תרגיל 8 - האם הרשימה ממוינת
        // ============================================================

        /// <summary>
        /// מחזירה אמת אם הרשימה ממוינת בסדר עולה, ושקר אחרת. מימוש איטרטיבי.
        /// </summary>
        public static bool IsSorted(Node<int> lst)
        {
            if (lst == null)
                return true;

            while (lst.HasNext())
            {
                if (lst.GetValue() > lst.GetNext().GetValue())
                    return false;

                lst = lst.GetNext();
            }
            return true;
        }

        /// <summary>
        /// כמו IsSorted, מימוש רקורסיבי.
        /// </summary>
        public static bool IsSortedR(Node<int> lst)
        {
            if(lst == null)
                return true;
            if (!lst.HasNext())
            { return true; }

            if (lst.GetValue() > lst.GetNext().GetValue())
                return false;
            return IsSortedR(lst.GetNext());
        }

        // ============================================================
        // תרגיל 9 - מספר הרצפים של ערך ברשימה
        // ============================================================

        /// <summary>
        /// מחזירה את מספר הרצפים של הערך val ברשימה.
        /// לדוגמה: עבור 4,5,6,6,3,4,4,4,22,3 והערך 4 - יוחזר 2.
        /// </summary>
        public static int CountSequences(Node<int> lst, int val)
        {
            int count = 0;
            while (lst != null)
            {
                if (lst.GetValue() == val)
                {
                    count++;
                    while (lst.HasNext() && lst.GetNext().GetValue() == val)
                    {
                        lst = lst.GetNext();
                    }
                }
                lst = lst.GetNext();
            }
            return count;
        }

        // ============================================================
        // תרגיל 10 - הדפסת אברי הרשימה בין שני מיקומים
        // ============================================================

        /// <summary>
        /// מדפיסה את אברי הרשימה שבין המיקומים from ו-to, כולל האיברים שבמיקומים עצמם.
        /// המיקום הראשון הוא 1. הנחה: הרשימה אינה ריקה, המיקומים תקינים ומתקיים from ≤ to.
        /// </summary>
        public static void PrintBetween(Node<int> lst, int from, int to)//asuming spots are correct
        {
            int cnt = 1;
            while (lst != null)
            {
                if (cnt >= from && cnt <= to)
                {
                    if (cnt == from)
                    {
                        Console.WriteLine("Start:");
                    }
                    Console.WriteLine(lst.GetValue());
                    if (cnt == to)
                    {
                        Console.WriteLine("End!");
                    }
                }
                lst = lst.GetNext();
                cnt++;
            }
        }
        //I fixed all the mistakes from the first part


        //--------------------- Part 2 ---------------------

        // ============================================================
        // תרגיל 11 - הסרת כפילויות מהרשימה
        // ============================================================

        /// <summary>
        /// מקבלת רשימה ומחזירה רשימה חדשה (חוליות חדשות) המכילה את ערכי הרשימה
        /// המקורית ללא כפילויות. הרשימה המקורית אינה משתנה.
        /// </summary>
        public static Node<int> RemoveDuplicates(Node<int> lst)
        {
            Node<int> newHead = null;
            Node<int> newTail = null;

            while (lst != null)
            {
                bool alreadyExists = false;
                Node<int> check = newHead;
                while (check != null)
                {
                    if (check.GetValue() == lst.GetValue())
                    {
                        alreadyExists = true;
                        break;
                    }
                    check = check.GetNext();
                }

                if (!alreadyExists)
                {
                    Node<int> newNode = new Node<int>(lst.GetValue());
                    if (newHead == null)
                    {
                        newHead = newNode;
                        newTail = newNode;
                    }
                    else
                    {
                        newTail.SetNext(newNode);
                        newTail = newNode;
                    }
                }

                lst = lst.GetNext();
            }

            return newHead;
        }

        // ============================================================
        // תרגיל 12 - רשימה מאוזנת
        // ============================================================

        /// <summary>
        /// מקבלת רשימה שאינה ריקה ומחזירה true אם מספר האיברים הגדולים מהממוצע
        /// שווה למספר האיברים הקטנים מהממוצע, ו-false אחרת.
        /// </summary>
        public static bool IsBalanced(Node<int> lst)//כתוב כי מקבלת רשימה אינה ריקה ולכן אין צורך לבדיקה (על פי הוראות העבודה)
        {
            Node<int> head = lst;
            int sum = 0;
            int cnt = 0;
            while (lst != null)
            {
                sum += lst.GetValue(); 
                cnt++;
                lst = lst.GetNext();
            }
            double avg = 1.0 * sum / cnt;
            lst = head;
            int bellow_cnt = 0;
            int above_cnt = 0;
            while (lst != null)
            {
                if(lst.GetValue() * 1.0 < avg)
                    bellow_cnt++;
                if(lst.GetValue() * 1.0 > avg)
                    above_cnt++;
                lst = lst.GetNext();
            }


            if(bellow_cnt == above_cnt)
                return true;
            else
                return false;
        }

        // ============================================================
        // תרגיל 13 - הסרת האיבר המקסימלי
        // ============================================================

        /// <summary>
        /// מקבלת רשימה שאינה ריקה ומנתקת ממנה את החולייה בעלת הערך המקסימלי
        /// (במקרה של כפילויות - המופע הראשון). מחזירה הפנייה לראש הרשימה המעודכנת.
        /// </summary>
        public static Node<int> RemoveMax(Node<int> lst)//כתוב כי מקבלת רשימה אינה ריקה ולכן אין צורך לבדיקה (על פי הוראות העבודה)
        {
            Node<int> maxNode = lst;
            Node<int> maxPrev = null;

            Node<int> prev = lst;
            Node<int> curr = lst.GetNext();

            while (curr != null)
            {
                if (curr.GetValue() > maxNode.GetValue())
                {
                    maxNode = curr;
                    maxPrev = prev;
                }
                prev = curr;
                curr = curr.GetNext();
            }

            if (maxPrev == null)
            {
                lst = lst.GetNext();
            }
            else
            {
                maxPrev.SetNext(maxNode.GetNext());
            }

            return lst;
        }

        // ============================================================
        // תרגיל 14 - הכנסת ערך לרשימה ממוינת
        // ============================================================

        /// <summary>
        /// מקבלת רשימה ממוינת בסדר עולה וערך val, יוצרת עבורו חולייה חדשה ומכניסה
        /// אותה למקום המתאים כדי לשמר את המיון. מחזירה הפנייה לראש הרשימה המעודכנת.
        /// הרשימה שהתקבלה עשויה להיות ריקה.
        /// </summary>
        public static Node<int> Insert2SortedList(Node<int> lst, int val)//כתוב כי מקבלת רשימה אינה ריקה וממוינת ולכן אין צורך לבדיקה (על פי הוראות העבודה)
        {
            Node<int> to_add = new Node<int>(val);
            if (lst == null)
                return to_add;

            Node<int> curr = lst;
            Node<int> head = lst;
            bool to_add_Ishead = false;

            if (val < lst.GetValue())
            {
                to_add.SetNext(lst);
                to_add_Ishead = true;
            }

            lst = lst.GetNext();
            while (lst != null && !to_add_Ishead)
            {
                if (val >= curr.GetValue() && val < lst.GetValue())
                {
                    to_add.SetNext(lst);
                    curr.SetNext(to_add);
                    break;
                }
                curr = lst;
                lst = lst.GetNext();
            }

            if (to_add_Ishead)
                return to_add;

            if (to_add.GetNext() == null)
                curr.SetNext(to_add);

            return head;
        }

        // ============================================================
        // תרגיל 15 - מיון הרשימה
        // ============================================================

        /// <summary>
        /// ממיינת רשימה של שלמים בסדר עולה בעזרת הפעולה Insert2SortedList, ומחזירה
        /// הפנייה לראש הרשימה הממוינת. מותר לשנות את הרשימה שהתקבלה.
        /// </summary>
        public static Node<int> SortList(Node<int> lst)
        {
            Node<int> sorted = null;

            while (lst != null)
            {
                sorted = Insert2SortedList(sorted, lst.GetValue());
                lst = lst.GetNext();
            }

            return sorted;
        }

        // ============================================================
        // תרגיל 16 - היפוך הרשימה
        // ============================================================

        /// <summary>
        /// הופכת את כיווני המצביעים של הרשימה (למשל 4,7,2,9,5 הופכת ל-5,9,2,7,4).
        /// לאחר ההיפוך ההפנייה המקורית מצביעה על החוליה האחרונה, ולכן חובה להשתמש
        /// בערך המוחזר מהפעולה.
        /// </summary>
        public static Node<T> ReverseList<T>(Node<T> lst)//this ine was pretty hard.
        {
            if(lst == null)
                return null;
            if (!lst.HasNext())
                return lst;

            Node<T> following = null;
            Node<T> ahead = lst.GetNext();

            if (!ahead.HasNext())
            {
                ahead.SetNext(lst);
                lst.SetNext(null);
                return ahead;
            }
            lst.SetNext(null);
            following = lst;
            lst = ahead;
            ahead = lst.GetNext();

            while (ahead != null)
            {

                lst.SetNext(following);
                following = lst;
                lst = ahead;
                ahead = ahead.GetNext();

            }
            lst.SetNext(following);
            return lst;
        }

        // ============================================================
        // תרגיל 17 - אורך רצף שלילי מקסימלי
        // ============================================================

        /// <summary>
        /// מקבלת רשימה ומחזירה את האורך של הרצף הרציף הארוך ביותר של מספרים
        /// שליליים ברשימה. אם אין ברשימה מספרים שליליים מוחזר 0.
        /// </summary>
        public static int MaxNegativeSeq(Node<int> lst)
        {
            if (lst == null)
                return 0;
            int current_neg_seq = 0;
            int max_neg_seq = 0;
            while (lst != null)
            {

                if (lst.GetValue() < 0)
                    current_neg_seq++;
                else
                    current_neg_seq = 0;

                if (current_neg_seq > max_neg_seq)
                    max_neg_seq = current_neg_seq;

                lst = lst.GetNext();
            }

            return max_neg_seq;
        }

        // ============================================================
        // תרגיל 18 - מיזוג שתי רשימות ממוינות (חוליות חדשות)
        // ============================================================

        /// <summary>
        /// מקבלת שתי רשימות ממוינות של שלמים ומחזירה שרשרת חדשה (חוליות חדשות)
        /// שהיא מיזוג ממוין של שתיהן. הרשימות המקוריות אינן משתנות.
        /// </summary>
        public static Node<int> MergeLists(Node<int> lst1, Node<int> lst2)//both are sorted
        {
            Node<int> head = null;
            Node<int> tail = null;

            while (lst1 != null && lst2 != null)
            {
                int val;
                if (lst1.GetValue() <= lst2.GetValue())
                {
                    val = lst1.GetValue();
                    lst1 = lst1.GetNext();
                }
                else
                {
                    val = lst2.GetValue();
                    lst2 = lst2.GetNext();
                }

                Node<int> newNode = new Node<int>(val);
                if (head == null)
                {
                    head = newNode;
                    tail = newNode;
                }
                else
                {
                    tail.SetNext(newNode);
                    tail = newNode;
                }
            }

            Node<int> remaining = (lst1 != null) ? lst1 : lst2;
            while (remaining != null)
            {
                Node<int> newNode = new Node<int>(remaining.GetValue());
                if (head == null)
                {
                    head = newNode;
                    tail = newNode;
                }
                else
                {
                    tail.SetNext(newNode);
                    tail = newNode;
                }
                remaining = remaining.GetNext();
            }

            return head;
        }

        // ============================================================
        // תרגיל 19 - מיזוג שתי רשימות ממוינות (ללא חוליות חדשות)
        // ============================================================

        /// <summary>
        /// כמו התרגיל הקודם, אך ללא יצירת חוליות חדשות - יש לנתק את החוליות
        /// הקיימות מהרשימות המקוריות ולהכניסן לשרשרת הממוזגת. לאחר הפעולה
        /// ההפניות המקוריות אינן מייצגות עוד את הרשימות המקוריות ואין להדפיסן.
        /// </summary>
        public static Node<int> MergeLists2(Node<int> lst1, Node<int> lst2)//both are sorted
        {
            if (lst1 == null)
                return lst2;
            if (lst2 == null)
                return lst1;

            Node<int> Head_to_return;
            Node<int> Following;

            if (lst1.GetValue() <= lst2.GetValue())
            {
                Head_to_return = lst1;
                Following = lst1;
                lst1 = lst1.GetNext();
            }
            else
            {
                Head_to_return = lst2;
                Following = lst2;
                lst2 = lst2.GetNext();
            }

            while (lst1 != null && lst2 != null)
            {
                if (lst1.GetValue() <= lst2.GetValue())
                {
                    Following.SetNext(lst1);
                    lst1 = lst1.GetNext();
                }
                else
                {
                    Following.SetNext(lst2);
                    lst2 = lst2.GetNext();
                }
                Following = Following.GetNext();
            }

            Following.SetNext(lst1 != null ? lst1 : lst2);

            return Head_to_return;
        }

        // ============================================================
        // תרגיל 20 - הוספת חולייה אחרי ערך נתון
        // ============================================================

        /// <summary>
        /// מקבלת שרשרת וערך val הקיים בה, ומוסיפה מיד אחרי החולייה שערכה val
        /// חולייה חדשה שערכה val+1. אם val מופיע כמה פעמים, מטפלת רק במופע
        /// הראשון. הפעולה מסוג void.
        /// </summary>
        public static void InsertAfterValue(Node<int> lst, int val)
        {
            Node<int> r = new Node<int>(val + 1);//as shown in pic we call the new Node "r".

            while (lst != null)
            {
                if (lst.GetValue() == val)
                {
                    Node<int> lst_next = lst.GetNext();
                    lst.SetNext(r);
                    r.SetNext(lst_next);
                    break;
                }
                lst = lst.GetNext();
            }
        }

        // ============================================================
        // תרגיל 21 - חיתוך שתי רשימות ממוינות
        // ============================================================

        /// <summary>
        /// מקבלת שתי רשימות ממוינות בסדר עולה ומחזירה רשימה חדשה (חוליות חדשות)
        /// עם ערכי החיתוך - כל ערך המופיע בשתי הרשימות מופיע פעם אחת בלבד.
        /// הרשימות המקוריות אינן משתנות.
        /// </summary>
        public static Node<int> Intersection(Node<int> lst1, Node<int> lst2)//Both are sorted
        {
            Node<int> head = null;
            Node<int> tail = null;

            while (lst1 != null && lst2 != null)
            {
                if (lst1.GetValue() < lst2.GetValue())
                {
                    lst1 = lst1.GetNext();
                }
                else if (lst1.GetValue() > lst2.GetValue())
                {
                    lst2 = lst2.GetNext();
                }
                else
                {
                    int val = lst1.GetValue();
                    Node<int> newNode = new Node<int>(val);
                    if (head == null)
                    {
                        head = newNode;
                        tail = newNode;
                    }
                    else
                    {
                        tail.SetNext(newNode);
                        tail = newNode;
                    }

                    while (lst1 != null && lst1.GetValue() == val)
                        lst1 = lst1.GetNext();
                    while (lst2 != null && lst2.GetValue() == val)
                        lst2 = lst2.GetNext();
                }
            }

            return head;
        }

        // ============================================================
        // תרגיל 22 - ניתוק איברי אמצע/קצוות
        // ============================================================

        /// <summary>
        /// מקבלת רשימה שאינה ריקה. אם מספר האיברים זוגי - מסירה את הגדול משני
        /// האיברים האמצעיים; אם אי-זוגי - מסירה את האיבר הראשון והאחרון (ברשימה
        /// באורך 1 הרשימה מתרוקנת). מחזירה הפנייה לראש הרשימה המעודכנת.
        /// </summary>
        public static Node<int> RemoveMiddle(Node<int> lst)//lst not empty
        {
            int count = CountList(lst);//using CountList method to get the amount of nodes there is.
            Node<int> head = lst;
            if (count == 1)
            {
                lst = null;
                return null;
            }
            else if (count == 2)
            {
                lst = lst.GetNext();
                if (head.GetValue() < lst.GetValue())
                {
                    head.SetNext(null);
                    lst = null;
                    return head;
                }
                else
                {
                    head.SetNext(null);
                    head = null;
                    return lst;
                }
            }
            else if (count % 2 == 0)
            {
                count = count / 2 - 1;
                Node<int> prev = null;
                Node<int> next = null;
                for (int i = 0; i < count; i++)
                {
                    prev = lst;
                    lst = lst.GetNext();
                    next = lst.GetNext();
                }
                if (lst.GetValue() < next.GetValue())
                {
                    prev = lst;
                    lst = lst.GetNext();
                    next = lst.GetNext();
                }
                prev.SetNext(next);
                lst.SetNext(null);
                lst = null;
            }
            else//odd
            {
                Node<int> prev = lst;
                lst = lst.GetNext();
                prev = null;
                head = lst;
                while (lst.GetNext().HasNext())
                {
                    lst = lst.GetNext();
                }
                Node<int> last = lst.GetNext();
                lst.SetNext(null);
                last = null;
            }

            return head;
        }

        // ============================================================
        // תרגיל 23 - הסרת ערכים משותפים
        // ============================================================

        /// <summary>
        /// מקבלת שתי שרשראות chain1, chain2 ומסירה מ-chain1 כל ערך שמופיע גם
        /// ב-chain2, ומוסיפה בסופה חולייה חדשה עם מספר החוליות שהוסרו. מחזירה
        /// הפנייה לראש chain1 המעודכנת. chain2 אינה משתנה.
        /// לדוגמה: chain1 = 1,5,3,20,15  ו-chain2 = 4,8,15,20,1  יהפכו ל-
        /// chain1 = 5,3,3.
        /// </summary>
        public static Node<int> RemoveCommon(Node<int> chain1, Node<int> chain2)
        {
            int num_in_common = 0;
            Node<int> head = chain1;
            Node<int> prev = null;
            Node<int> curr = chain1;

            while (curr != null)
            {
                if (chain2 != null && IsExist(chain2, curr.GetValue()))
                {
                    num_in_common++;
                    if (prev == null)
                        head = curr.GetNext();
                    else
                        prev.SetNext(curr.GetNext());
                }
                else
                {
                    prev = curr;
                }
                curr = curr.GetNext();
            }

            Node<int> countNode = new Node<int>(num_in_common);
            if (head == null)
                head = countNode;
            else
                prev.SetNext(countNode);

            return head;
        }
    }
}//YNWA//
