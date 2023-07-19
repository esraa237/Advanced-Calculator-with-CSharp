using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace infix_to_postfix_v1
{
    public class Program
    {
        public bool IsOperator(char ch)
        {
            if (ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '(' || ch == ')')
            {
                return true;
            }
            return false;
        }
        public int priority(char ch)
        {
            if (ch == '-' || ch == '+') return 1;
            else if (ch == '*' || ch == '/') return 2;
            else return 0;
        }

        public void convert(ref string infix, out string postfix)
        {
            postfix = "";
            Stack<char> op_stack = new Stack<char>();
            for (int i = 0; i < infix.Length; i++)
            {
                char ch = infix[i];
                if (IsOperator(ch))
                {
                    if (op_stack.Count <= 0)
                    {
                        op_stack.Push(ch);
                    }
                    else
                    {
                        if (ch == '(')
                        {
                            op_stack.Push(ch);
                        }
                        else if (ch == ')')
                        {
                            while (op_stack.Peek() != '(' && op_stack.Count > 0)
                            {
                                postfix += op_stack.Pop();
                            }
                            op_stack.Pop();
                        }
                        else if (priority(ch) > priority(op_stack.Peek()))//example ch=* stack=+
                        {
                            op_stack.Push(ch);
                        }
                        else if (priority(ch) < priority(op_stack.Peek()))//example ch=+ stack=* 
                        {
                            postfix += op_stack.Pop();
                            i--;
                        }
                        else//example ch=/ stack=* or ch=- stack=+
                        {
                            postfix += op_stack.Pop();
                            op_stack.Push(ch);
                        }
                    }
                }
                else
                {
                    postfix += ch;
                }
            }
            //handle spaces
            if (op_stack.Count != 0)
            {
                for (int j = 0; j <= op_stack.Count; j++)
                    postfix += " " + op_stack.Pop();
            }
            for (int i = 1; i < postfix.Length; i++)
            {
                if ((postfix[i - 1] == '*' || postfix[i - 1] == '/') && (postfix[i] == '-' || postfix[i] == '+'))
                {
                    string a = postfix.Substring(0, i);
                    string b = postfix.Substring(i);
                    postfix = a + " " + b;

                }
            }

        }
        public int evaluate(string postfix)
        {
            postfix.Trim();
            Stack<int> num_stack = new Stack<int>();
            int temp1, temp2;
            string[] ch = postfix.Split(' ');
            for (int j = 0; j < ch.Length; j++)
            {
                if ("*+/-".Contains(ch[j]))
                {
                    if (ch[j] == "*")
                    {
                        temp1 = Convert.ToInt32(num_stack.Pop());
                        temp2 = Convert.ToInt32(num_stack.Pop());
                        num_stack.Push((temp1 * temp2));
                    }
                    else if (ch[j] == "/")
                    {
                        temp1 = Convert.ToInt32(num_stack.Pop());
                        temp2 = Convert.ToInt32(num_stack.Pop());
                        num_stack.Push((temp2 / temp1));
                    }
                    else if (ch[j] == "+")
                    {
                        temp1 = Convert.ToInt32(num_stack.Pop());
                        temp2 = Convert.ToInt32(num_stack.Pop());
                        num_stack.Push((temp2 + temp1));
                    }
                    else if (ch[j] == "-")
                    {
                        temp1 = Convert.ToInt32(num_stack.Pop());
                        temp2 = Convert.ToInt32(num_stack.Pop());
                        num_stack.Push((temp2 - temp1));
                    }
                }
                else
                {
                    num_stack.Push(Convert.ToInt32(ch[j]));
                }

            }
            return num_stack.Pop();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the Expression With Spaces Between Numbers and Operators: ");
            string infix = Console.ReadLine();
            string postfix = "";
            Program p = new Program();
            p.convert(ref infix, out postfix);
            //Console.WriteLine("InFix  :  " + infix);
            //Console.WriteLine("PostFix:  " + postfix);
            int ans = p.evaluate(postfix);
            Console.WriteLine("The Answer Of Evaluation: " + ans);
            Console.ReadKey();
        }
    }
}

