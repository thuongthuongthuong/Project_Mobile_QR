namespace Project_QR_BS.Helpers
{
    public class HexUtils
    {
        /// <summary> Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>
        /// <param name="s"> The string containing the hex digits (with or without spaces). </param>
        /// <returns> Returns an array of bytes. </returns>
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
            {
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            }
            return buffer;
        }

        public static string BytesToHexString(byte[] byteArray)//byte[]转HEXString
        {
            var str = new System.Text.StringBuilder();

            for (int i = 0; i < byteArray.Length; i++)
            {
                str.Append(String.Format("{0:X2}", byteArray[i]));//var拼接
            }
            string s = str.ToString();
            return s;
        }
    }
}
