using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace data_mining_similarity
{
    class Program
    {
        ///get content from pages
        static string get_content(string input)
        {
            string output = "";
            //remove scripts
            Regex removecript = new Regex(@"<script[^>]*>[\s\S]*?</script>");
            string result = removecript.Replace(input, "");
            //remove tags
            string pattern = @"<[^>].+?>";
            string result1 = Regex.Replace(result, pattern, "");
            //remove  space
            string result2 = Regex.Replace(result1, "\\n", "");
            string result3 = Regex.Replace(result2, "\\r", "");
            output = Regex.Replace(result3, "\\t", "");


            return output;
        }

        ///seprate words form each others
        static string[] get_words(string[] input)
        {

            string[] output = new string[input.Length];

            int j = 0;
            for (int i = 0; i < input.Length; i++)
            {
                input[i] = input[i].ToLower();
                if (input[i].Length < 11 && input[i] != "!" && input[i] != "?" && input[i] != ";" && input[i] != ":" && input[i] != "/" && input[i] != "," && input[i] != "<!--" && input[i] != "-->" && input[i] != "//" && input[i] != "-" && input[i] != "before" && input[i] != "after" && input[i] != "over" && input[i] != "her" && input[i] != "him" && input[i] != "she" && input[i] != "if" && input[i] != "on" && input[i] != "often" && input[i] != "his" && input[i] != "he" && input[i] != "be" && input[i] != "" && input[i] != " " && input[i] != "</br>" && input[i] != "</a>" && input[i] != "</p>" && input[i] != "<a>" && input[i] != "<p>" && input[i] != "." && input[i] != "a" && input[i] != "the" && input[i] != "as" && input[i] != "than" && input[i] != "this" && input[i] != "or" && input[i] != "and" && input[i] != "am" && input[i] != "I" && input[i] != "to" && input[i] != "not" && input[i] != "you" && input[i] != "we" && input[i] != "your" && input[i] != "our" && input[i] != "they" && input[i] != "that" && input[i] != "what" && input[i] != "why" && input[i] != "while" && input[i] != "who" && input[i] != "is" && input[i] != "were" && input[i] != "such" && input[i] != "much" && input[i] != "but" && input[i] != "have" && input[i] != "has" && input[i] != "only" && input[i] != "since" && input[i] != "off" && input[i] != "by" && input[i] != "an" && input[i] != "are" && input[i] != "had" && input[i] != "having" && input[i] != "who" && input[i] != "!" && input[i] != "?" && input[i] != "of" && input[i] != "want" && input[i] != "do" && input[i] != "does" && input[i] != "you" && input[i] != "at" && input[i] != "can" && input[i] != "shoud" && input[i] != "for" && input[i] != "it" && input[i] != "will" && input[i] != "this" && input[i] != "in" && input[i] != "non" && input[i] != "how")
                {
                    output[j] = input[i].ToString();
                    j++;
                }

            }


            return output;
        }

        //calculate arrays length
        static int get_Array_length(string[] input)
        {
            int length = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != null)
                {
                    length++;
                }
            }
            return length;

        }

        //create words and calculate count of them
        static word[] create_words(string[] input)
        {
            int length = get_Array_length(input);
            word[] output = new word[length + 1];
            int k = 0;
            bool cheak = false;
            for (int i = 0; i < length; i++)
            {
                output[k] = new word();
                for (int j = 0; j < k; j++)
                {



                    if (input[i] == output[j].name)
                    {
                        cheak = true;
                        output[j].count += 1;
                        break;
                    }



                }
                if (cheak == false)
                {
                    output[k].name = input[i];
                    output[k].count += 1;
                    k++;

                }
                cheak = false;
            }
            return output;
        }
        static int get_word_Array_length(word[] input)
        {
            int length = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] != null)
                {
                    length++;
                }
            }
            return length;

        }

        static void Main(string[] args)
        {
            //input html page
            string input1 = File.ReadAllText("input1.html");
            string input2 = File.ReadAllText("input2.html");

            string output1 = get_content(input1);
            string output2 = get_content(input2);

            string[] words_1 = output1.Split(' ');
            string[] words_2 = output2.Split(' ');

            string[] content1 = new string[words_1.Length];
            string[] content2 = new string[words_1.Length];

            content1 = get_words(words_1);
            content2 = get_words(words_2);

            int length1 = get_Array_length(content1);
            int length2 = get_Array_length(content2);


            word[] page1 = new word[length1 + 1];
            word[] page2 = new word[length2 + 1];


            page1 = create_words(content1);
            page2 = create_words(content2);



            int page1_length = get_word_Array_length(page1);
            int page2_length = get_word_Array_length(page2);
            word[] final1 = new word[page1_length];
            word[] final2 = new word[page2_length];

            ///this for compare becasue information of top arrays  is change and unsueful
            word[] final11 = new word[page1_length];
            word[] final22 = new word[page2_length];
            ///////////

            for (int i = 0; i < page1_length; i++)
            {
                final1[i] = new word();
                final1[i].name = page1[i].name;
                final1[i].count = page1[i].count;

                final11[i] = new word();
                final11[i].name = page1[i].name;
                final11[i].count = page1[i].count;

            }
            for (int i = 0; i < page2_length; i++)
            {
                final2[i] = new word();
                final2[i].name = page2[i].name;
                final2[i].count = page2[i].count;

                final22[i] = new word();
                final22[i].name = page2[i].name;
                final22[i].count = page2[i].count;

            }



            // insert content and words to array 
            string[] sum_array = new string[final1.Length + final2.Length];

            int k = 0;
            for (int i = 0; i < final1.Length; i++)
            {
                for (int j = 0; j < final2.Length; j++)
                {
                    if (final1[i].name == final2[j].name && final1[i].name != "" && final2[j].name != "")
                    {
                        sum_array[k] = final1[i].name;
                        k++;
                        final1[i].name = "";
                        final2[j].name = "";

                    }
                }
            }
            for (int i = 0; i < final1.Length; i++)
            {
                if (final1[i].name != "")
                {
                    sum_array[k] = final1[i].name;
                    k++;
                }
            }
            for (int i = 0; i < final2.Length; i++)
            {
                if (final2[i].name != "")
                {
                    sum_array[k] = final2[i].name;
                    k++;
                }
            }


            ////insert count of each word to array
            string[,] similarity_array = new string[k, 3];
            for (int i = 0; i < k; i++)
            {
                similarity_array[i, 0] = sum_array[i];
            }
            bool flag1 = true;
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < final11.Length; j++)
                {
                    if (similarity_array[i, 0] == final11[j].name)
                    {
                        similarity_array[i, 1] = final11[j].count.ToString();
                        flag1 = true;
                        break;



                    }
                    else
                    {
                        flag1 = false;
                    }
                }
                if (flag1 == false)
                {
                    similarity_array[i, 1] = "0";
                    flag1 = true;
                }
            }
            bool flag2 = true;
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < final22.Length; j++)
                {
                    if (similarity_array[i, 0] == final22[j].name)
                    {
                        similarity_array[i, 2] = final22[j].count.ToString();
                        flag2 = true;
                        break;

                    }
                    else
                    {
                        flag2 = false;
                    }
                }
                if (flag2 == false)
                {
                    similarity_array[i, 2] = "0";
                    flag2 = true;
                }
            }


            ///compute similarity

            double d1d2 = 0;
            for (int i = 0; i < k; i++)
            {
                d1d2 = d1d2 + (int.Parse(similarity_array[i, 1]) * int.Parse(similarity_array[i, 2]));

            }

            double d1length = 0;
            for (int i = 0; i < k; i++)
            {
                d1length = d1length + (int.Parse(similarity_array[i, 1]) * int.Parse(similarity_array[i, 1]));

            }
            d1length = System.Math.Sqrt(d1length);

            double d2length = 0;
            for (int i = 0; i < k; i++)
            {
                d2length = d2length + (int.Parse(similarity_array[i, 2]) * int.Parse(similarity_array[i, 2]));

            }
            d2length = System.Math.Sqrt(d2length);

            double similarity = (d1d2) / (d1length * d2length);



            Console.WriteLine("similarity between this webpages is : " + similarity.ToString());
            Console.ReadKey();
        }
    }
}

class word
{
    public string name { get; set; }

    public int count { get; set; }

    public word() { }


}









