using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.IO;
using System.Security;
using System.Resources;
using System.Collections;
using System.Security.Permissions;
using System.Configuration.Assemblies;
using System.Reflection;
using System.Diagnostics;
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Versioning;
using System.Diagnostics.Contracts;
using System.Text;

namespace Arithmetic_Interpreter_UWP {
	public static class Tokenizer {
		private static int Length = Text.Length;

		public static string Text;

		public static LinkedList<string> GetTokens(string text) {
			string[] tokens = text.Split(new char[] { ' ', '(', ')', '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
			LinkedList<string> result = new LinkedList<string>();
			foreach (var token in tokens) {
				result.AddLast(token);
			}
			return result;
		}

		private static String[] Split(params char[] separator) {
			Contract.Ensures(Contract.Result<String[]>() != null);
			return SplitInternal(separator, Int32.MaxValue, StringSplitOptions.None);
		}

		private static T Result<T>() { return default(T); }

		private static String[] SplitInternal(char[] separator, int count, StringSplitOptions options) {
			if (count < 0)
				throw new ArgumentOutOfRangeException("count");

			if (options < StringSplitOptions.None || options > StringSplitOptions.RemoveEmptyEntries)
				throw new ArgumentException();
			Contract.Ensures(Contract.Result<String[]>() != null);
			Contract.EndContractBlock();

			bool omitEmptyEntries = (options == StringSplitOptions.RemoveEmptyEntries);

			if ((count == 0) || (omitEmptyEntries && Length == 0)) {
				return new String[0];
			}

			int[] sepList = new int[Length];
			int numReplaces = MakeSeparatorList(separator, ref sepList);

			//Handle the special case of no replaces and special count.
			if (0 == numReplaces || count == 1) {
				String[] stringArray = new String[1];
				stringArray[0] = Text;
				return stringArray;
			}

			//if (omitEmptyEntries) {
			//	return InternalSplitOmitEmptyEntries(sepList, null, numReplaces, count);
			//}
			//else {
			//	return InternalSplitKeepEmptyEntries(sepList, null, numReplaces, count);
			//}

			return InternalSplitOmitEmptyEntries(sepList, null, numReplaces, count);
		}

		// This function will not keep the Empty String 
		private static String[] InternalSplitOmitEmptyEntries(Int32[] sepList, Int32[] lengthList, Int32 numReplaces, int count) {
			Contract.Requires(numReplaces >= 0);
			Contract.Requires(count >= 2);
			Contract.Ensures(Contract.Result<String[]>() != null);

			// 分配数组以保存项目。 在此函数中可能无法完全填充此数组，我们将创建一个新数组并复制对该新数组的字符串引用。

			int maxItems = (numReplaces < count) ? (numReplaces + 1) : count;
			String[] splitStrings = new String[maxItems];

			int currIndex = 0;
			int arrIndex = 0;

			for (int i = 0; i < numReplaces && currIndex < Length; i++) {
				if (sepList[i] - currIndex > 0) {
					splitStrings[arrIndex++] = Text.Substring(currIndex, sepList[i] - currIndex);
				}
				currIndex = sepList[i] + ((lengthList == null) ? 1 : lengthList[i]);
				if (arrIndex == count - 1) {
					// If all the remaining entries at the end are empty, skip them
					while (i < numReplaces - 1 && currIndex == sepList[++i]) {
						currIndex += ((lengthList == null) ? 1 : lengthList[i]);
					}
					break;
				}
			}

			// we must have at least one slot left to fill in the last string.
			Contract.Assert(arrIndex < maxItems);

			//Handle the last string at the end of the array if there is one.
			if (currIndex < Length) {
				splitStrings[arrIndex++] = Text.Substring(currIndex);
			}

			String[] stringArray = splitStrings;
			if (arrIndex != maxItems) {
				stringArray = new String[arrIndex];
				for (int j = 0; j < arrIndex; j++) {
					stringArray[j] = splitStrings[j];
				}
			}
			return stringArray;
		}

		// This function returns number of the places within baseString where 
        // instances of characters in Separator occur.         
        // Args: separator  -- A string containing all of the split characters.
        //       sepList    -- an array of ints for split char indicies.
        //--------------------------------------------------------------------    
        [System.Security.SecuritySafeCritical]  // auto-generated
        private static unsafe int MakeSeparatorList(char[] separator, ref int[] sepList) {
            int foundCount=0;
 
            if (separator == null || separator.Length ==0) {
                fixed (char* pwzChars = &m_firstChar) {
                    //If they passed null or an empty string, look for whitespace.
                    for (int i=0; i < Length && foundCount < sepList.Length; i++) {
                        if (Char.IsWhiteSpace(pwzChars[i])) {
                            sepList[foundCount++]=i;
                        }
                    }
                }
            } 
            else {
                int sepListCount = sepList.Length;
                int sepCount = separator.Length;
                //If they passed in a string of chars, actually look for those chars.
                fixed (char* pwzChars = &m_firstChar, pSepChars = separator) {
                    for (int i=0; i< Length && foundCount < sepListCount; i++) {                        
                        char * pSep = pSepChars;
                        for( int j =0; j < sepCount; j++, pSep++) {
                           if ( pwzChars[i] == *pSep) {
                               sepList[foundCount++]=i;
                               break;
                           }
                        }
                    }
                }
            }
            return foundCount;
        }
	}
}
