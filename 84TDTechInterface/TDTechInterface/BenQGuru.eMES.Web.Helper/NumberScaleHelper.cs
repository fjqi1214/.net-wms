using System;
using System.Collections.Generic;
using System.Text;

namespace BenQGuru.eMES.Web.Helper
{
    public enum NumberScale
    {
        Scale10 = 10,
        Scale16 = 16,
        Scale34 = 34,
        Scale36 = 36
    }

    public class NumberScaleHelper
    {
        private static readonly string _Letters10 = "0123456789";
        private static readonly string _Letters16 = "0123456789ABCDEF";
        private static readonly string _Letters34 = "0123456789ABCDEFGHJKLMNPQRSTUVWXYZ";
        private static readonly string _Letters36 = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static bool CheckLetter(string numberString, NumberScale scale)
        {
            string letters = string.Empty;
            switch (scale)
            {
                case NumberScale.Scale10:
                    letters = _Letters10;
                    break;
                case NumberScale.Scale16:
                    letters = _Letters16;
                    break;
                case NumberScale.Scale34:
                    letters = _Letters34;
                    break;
                case NumberScale.Scale36:
                    letters = _Letters36;
                    break;
                default:
                    throw new Exception("$Error_InvalidSourceScale");
                    break;
            }

            numberString = numberString.ToUpper();
            for (int i = 0; i < numberString.Length; i++)
            {
                if (letters.IndexOf(numberString.Substring(i, 1)) < 0)
                    return false;
            }

            return true;
        }

        public static string ChangeNumber(string sourceNumberString, NumberScale sourceScale, NumberScale targetScale)
        {
            sourceNumberString = sourceNumberString.Trim().ToUpper();

            int sourceBase = 0;
            int targetBase = 0;
            string sourceLetters = string.Empty;
            string targetLetters = string.Empty;
            long m = 0;
            int pos = 0;
            string returnValue = string.Empty;

            switch (sourceScale)
            {
                case NumberScale.Scale10:
                    sourceBase = 10;
                    sourceLetters = _Letters10;
                    break;
                case NumberScale.Scale16:
                    sourceBase = 16;
                    sourceLetters = _Letters16;
                    break;
                case NumberScale.Scale34:
                    sourceBase = 34;
                    sourceLetters = _Letters34;
                    break;
                case NumberScale.Scale36:
                    sourceBase = 36;
                    sourceLetters = _Letters36;
                    break;
                default:
                    throw new Exception("$Error_InvalidSourceScale");
                    break;
            }

            switch (targetScale)
            {
                case NumberScale.Scale10:
                    targetBase = 10;
                    targetLetters = _Letters10;
                    break;
                case NumberScale.Scale16:
                    targetBase = 16;
                    targetLetters = _Letters16;
                    break;
                case NumberScale.Scale34:
                    targetBase = 34;
                    targetLetters = _Letters34;
                    break;
                case NumberScale.Scale36:
                    targetBase = 36;
                    targetLetters = _Letters36;
                    break;
                default:
                    throw new Exception("$Error_InvalidTargetScale");
                    break;
            }

            if (sourceNumberString.Length < 1 || sourceNumberString.Length > 10)
                throw new Exception("$Error_InvalidSourceNumberString");

            if (!CheckLetter(sourceNumberString, sourceScale))
                throw new Exception("$Error_InvalidSourceNumberString");

            for (int i = 0; i < sourceNumberString.Length; i++)
            {
                m = m * sourceBase + sourceLetters.IndexOf(sourceNumberString[i]);
            }

            while (m >= targetBase)
            {
                pos = (int)(m % targetBase);
                returnValue = targetLetters.Substring(pos, 1) + returnValue;
                m = m / targetBase;
            }
            pos = (int)m;
            returnValue = targetLetters.Substring(pos, 1) + returnValue;
            return returnValue;
        }
    }




}
