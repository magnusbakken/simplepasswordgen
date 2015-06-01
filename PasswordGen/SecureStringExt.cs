namespace PasswordGen
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    public static class SecureStringExt
    {
        /// <summary>
        /// Converts this SecureString to a regular string, freeing the memory left behind.
        /// </summary>
        /// <remarks>
        /// Thanks to http://blogs.msdn.com/b/fpintos/archive/2009/06/12/how-to-properly-convert-securestring-to-string.aspx.
        /// </remarks>
        /// <param name="secureString">The secure string to work on.</param>
        /// <returns>The SecureString as plaintext.</returns>
        public static string ConvertToString(this SecureString secureString)
        {
            if (secureString == null)
            {
                throw new ArgumentNullException("secureString");
            }

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }
    }
}