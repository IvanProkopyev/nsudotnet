using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace prokopyev.Nsudotnet.Enigma{
    class Encryptor{
        internal void Encrypt(String mode, String input, String output) {
           
            byte[] Key = null;
            byte[] IV = null;
            ICryptoTransform encryptor = null;
            bool err = true;
            using (FileStream fsInput = new FileStream(input, FileMode.Open, FileAccess.Read)) {
                if (mode == "aes") {
                    err = false;
                    using (Aes aes = Aes.Create()) {
                        Key = aes.Key;
                        IV = aes.IV;
                        encryptor = aes.CreateEncryptor(Key, IV);
                    }
                }

                if (mode == "des") {
                    err = false;
                    using (DESCryptoServiceProvider des = new DESCryptoServiceProvider()) {
                        Key = des.Key;
                        IV = des.IV;
                        encryptor = des.CreateEncryptor(Key, IV);
                    }
                }

                if (mode == "rc2") {
                    err = false;
                    using (RC2CryptoServiceProvider rc2 = new RC2CryptoServiceProvider()) {
                        Key = rc2.Key;
                        IV = rc2.IV;
                        encryptor = rc2.CreateEncryptor(Key, IV);
                    }
                }

                if (mode == "rijndael") {
                    err = false;
                    using (Rijndael r = Rijndael.Create()) {
                        Key = r.Key;
                        IV = r.IV;
                        encryptor = r.CreateEncryptor(Key, IV);
                    }
                }
                if (err == true) {
                    Console.WriteLine("incorrect alghorytm name. use (rijndael, rc2, aes, des)");
                    return;
                }
                using (FileStream filestreamout = new FileStream(output, FileMode.Create, FileAccess.Write)) {
                    using (CryptoStream csEncrypt = new CryptoStream(filestreamout, encryptor, CryptoStreamMode.Write)) {
                        int data;
                        while ((data = fsInput.ReadByte()) != -1) {
                            csEncrypt.WriteByte((byte)data);
                        }
                        Console.WriteLine("binary file created");
                    
                    }
                }
                string path = Path.GetDirectoryName(input);
                string keyfile = input + ".key.txt";
                string fileKey = Path.Combine(path, keyfile);
                using (FileStream fout = new FileStream(fileKey, FileMode.OpenOrCreate, FileAccess.ReadWrite)) {
                    using (StreamWriter sw = new StreamWriter(fout)) {
                        String key = Convert.ToBase64String(Key);
                        String iV = Convert.ToBase64String(IV);
                        sw.WriteLine(key);
                        sw.WriteLine(iV);
                       
                    }
                  
                    Console.WriteLine("key-file created");
                    
                }
            }
        }
    }       
}

   
