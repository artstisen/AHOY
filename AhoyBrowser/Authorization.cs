using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace AhoyBrowser
{
    class Welcome
    {
        // INFO TO THE "WELCOME" (Authorization) CLASS - 24-09-2016
        // Masked encryption keys refCATA, refCATB, refCATC below (Don't change them!)
        // "Ref" is for Reference (encrypted text content), "CAT" is for Catalyst (encryption key)
        // -names are masked on purpose to make the code less obvious on disassembly
        // -this is also why the Pool encryption key is hardcoded as encrypted string with
        // several sub-keys (refCATA, refCATB, refCATC) to hide the original key from the code.
        // NOTE: These comments will not appear inside the release build.
        //------------------------------------------------------------
        /*
            The route through deciphering the original key:

            Encryption of original pool key:
         ->  Original    through sub-key     becomes this
            "xxxxxxxx" -> "xxxxxxxx" -> "i7LpiDtiQUkuqFXDB4KJvw==" (refCATA)
            ----------------------------------------------------
         ->  Sub-key         through sub-sub-key              becomes this
            "xxxxxxxx" -> "rNIDicTKNPMYgkZpXRcaPA==" -> "L7ZDqTw/7uAuu6B6X2wA5g==" (refCATB)
            ----------------------------------------------------
         -> The sub-sub-key was generated like this, but the values before the
            result are never used. This is just to create a key string with
            the same signature as the other two (A and B):
            "xxxxxxxx" -> "xxxxxxxx" -> "rNIDicTKNPMYgkZpXRcaPA==" (refCATC)
            ----------------------------------------------------
        */
        protected const string refCATA = "i7LpiDtiQUkuqFXDB4KJvw=="; // Encrypted pool key (See description above)
        protected const string refCATB = "L7ZDqTw/7uAuu6B6X2wA5g=="; // Encrypted sub-key used to decrypt pool key
        protected const string refCATC = "rNIDicTKNPMYgkZpXRcaPA=="; // Sub-key to Decrypt Encrypted sub-key above (refCATB)
        protected string refCAT { get; set; } // Decrypted pool key, goes in this variable

        // Just methods to get the values from refCATA, refCATB and refCATC
        // instead of retrieving them directly (obfuscation)
        protected string GCATA()
        { return refCATA; }
        protected string GCATB()
        { return refCATB; }
        protected string GCATC()
        { return refCATC; }

        // Get Encryption key (Decrypt refCATA using the key refCATB
        // and add it to the refCAT string (variable names are obscured on purpose)
        // (Method name "RefCATG" = Reference + Catalyst + Get eg. GetPassword or GetPoolKey)
        protected string tempCATB;
        protected string RefCATG()
        {
            tempCATB = RefCATD(GCATB(), GCATC()); // Decrypt B-key using C-key
            refCAT = RefCATD(GCATA(), tempCATB); // Decrypt A-key using B-key
            tempCATB = string.Empty; // Clear decrypted B-key
            return refCAT; // Return decrypted A-key
        }

        // Encrypt a string
        // (name obscured on purpose: RefE = Reference + Encrypt or "EncryptString")
        // (encCatalyst = Encryption Password)
        internal string RefE(string reference)
        {
            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because
            // the algorithm is operating in its default 
            string encCatalyst = RefCATG();

            // Then, we need to turn the password into Key and IV 
            // We are using salt to make it harder to guess our key
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(encCatalyst,
            new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            encCatalyst = string.Empty; // Clear key

            byte[] key = pdb.GetBytes(16);
            byte[] iv = pdb.GetBytes(8);

            // Instantiate the class with the arrays
            TripleDES des = new TripleDES(key, iv);

            try
            {
                return des.Encrypt(reference);
            }
            catch
            {
                return "error processing reference"; // ("error encrypting content")
            }
        }

        // Decrypt a string
        // (name obscured on purpose: RefD = Reference + Decrypt or "DecryptString")
        internal string RefD(string reference)
        {
            string decCatalyst = RefCATG();
            // Then, we need to turn the password into Key and IV 
            // We are using salt to make it harder to guess our key
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(decCatalyst,
            new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            decCatalyst = string.Empty; // Clear key
            byte[] key = pdb.GetBytes(16);
            byte[] iv = pdb.GetBytes(8);
            // Instantiate the class with the arrays
            TripleDES des = new TripleDES(key, iv);

            try
            {
                return des.Decrypt(reference);
            }
            catch
            {
                return "error processing reference"; // ("error decrypting content")
            }
        }

        // Same as the method above but with two parameters and no comments
        // Used by the RefCATG method
        // (name obscured on purpose: RefCATD = Reference + Catalyst + Decrypt)
        protected string RefCATD(string reference, string catalyst)
        {
            string decCatalyst = catalyst;
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(decCatalyst,
            new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
            decCatalyst = string.Empty; // Clear key
            byte[] key = pdb.GetBytes(16);
            byte[] iv = pdb.GetBytes(8);
            TripleDES des = new TripleDES(key, iv);
            try
            {
                return des.Decrypt(reference);
            }
            catch
            {
                return "error processing reference";
            }
        }



        #region Cypher
        // Encrypt a string
        internal string refCATE(string reference, string catalyst)
        {
            // Now set the key and the IV. 
            // We need the IV (Initialization Vector) because
            // the algorithm is operating in its default 
            string encCat = catalyst;

            // Then, we need to turn the password into Key and IV 
            // We are using salt to make it harder to guess our key
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(encCat,
            new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            byte[] key = pdb.GetBytes(16);
            byte[] iv = pdb.GetBytes(8);

            // Instantiate the class with the arrays
            TripleDES des = new TripleDES(key, iv);

            try
            {
                return des.Encrypt(reference);
            }
            catch
            {
                return "errorcode001";
            }
        }

        // Decrypt a string
        internal string refCATD(string reference, string catalyst)
        {
            string decCAT = catalyst;

            // Then, we need to turn the password into Key and IV 
            // We are using salt to make it harder to guess our key
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(decCAT,
            new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d,
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});

            byte[] key = pdb.GetBytes(16);
            byte[] iv = pdb.GetBytes(8);
            // Instantiate the class with the arrays
            TripleDES des = new TripleDES(key, iv);

            try
            {
                return des.Decrypt(reference);
            }
            catch
            {
                return "errorcode002";
            }
        }
        #endregion



    }

    #region Cryptography alogrithm
    class TripleDES
    {
        // Define the triple des provider (new version)
        private System.Security.Cryptography.TripleDES m_des = System.Security.Cryptography.TripleDES.Create();
        // Define the triple des provider (old version -deprecated)
        //private TripleDESCryptoServiceProvider m_des =
        //         new TripleDESCryptoServiceProvider();

        // Define the string handler
        private UTF8Encoding m_utf8 = new UTF8Encoding();

        // Define the local property arrays
        private byte[] m_key;
        private byte[] m_iv;

        internal TripleDES(byte[] key, byte[] iv)
        {
            this.m_key = key;
            this.m_iv = iv;
        }


        internal string Encrypt(string text)
        {
            byte[] input = m_utf8.GetBytes(text);
            byte[] output = Transform(input,
                            m_des.CreateEncryptor(m_key, m_iv));
            return Convert.ToBase64String(output);
        }

        internal string Decrypt(string text)
        {
            byte[] input = Convert.FromBase64String(text);
            byte[] output = Transform(input,
                            m_des.CreateDecryptor(m_key, m_iv));
            return m_utf8.GetString(output);
        }

        private byte[] Transform(byte[] input,
                       ICryptoTransform CryptoTransform)
        {
            // Create the necessary streams
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptStream = new CryptoStream(memStream,
                         CryptoTransform, CryptoStreamMode.Write);
            // Transform the bytes as requested
            cryptStream.Write(input, 0, input.Length);
            cryptStream.FlushFinalBlock();
            // Read the memory stream and
            // convert it back into byte array
            memStream.Position = 0;
            byte[] result = memStream.ToArray();
            // Close and release the streams
            memStream.Close();
            try
            {
                cryptStream.Close();
            }
            catch
            {
                // Do nothing
            }
            // Hand back the encrypted buffer
            return result;
        }
    }
    #endregion
}
