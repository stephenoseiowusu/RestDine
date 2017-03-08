using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace RestDine.Controllers
{
    public class Security
    {
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = MD5.Create();  //or use SHA1.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString + "lol"));
        }
      
        public static string GetHashString(string inputString)
        {
            String which = getHashType(inputString);
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString(which));

            return sb.ToString();
        }
        public static String getHashType(String inputString)
        {
            String result = "";
            int index = char.ToUpper(inputString.ToList().ToArray()[0]) - 64;
            index = index % 9;
            switch(index)
            {
                case 0: 
                    result = "C3";
                    break;
                case 1: 
                    result = "D4";
                    break;
                case 2:
                    result = "e1";
                    break;
                case 3:
                    result = "E2";
                    break;
                case 4:
                    result = "F1";
                    break;
                case 5:
                    result = "G";
                    break;
                case 6:
                    result = "N1";
                    break;
                case 7:
                    result = "P0";
                    break;
                case 8:
                    result = "X4";
                    break;
                case 9:
                    result = "0000.0000";
                    break;
 

                       
            }
            return result;
        }
    }
}