using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;
namespace prokopyev.Nsudotnet.Enigma {
    class Decryptor {

        internal void Decrypt(String type, String from, String to, String keyFile) {
            byte[] algKey = null;
            byte[] algIV = null;
            using (TextReader tr = new StreamReader(keyFile)) {
                algKey = Convert.FromBase64String(tr.ReadLine());
                algIV = Convert.FromBase64String(tr.ReadLine());
                using (SymmetricAlgorithm algorithm = Codetype.GetType(type)) {
                    ICryptoTransform decryptor = algorithm.CreateDecryptor(algKey, algIV);
                    using (FileStream destination = new FileStream(from, FileMode.Open)) {
                        using (CryptoStream cryptoStream = new CryptoStream(destination, decryptor, CryptoStreamMode.Read)) {
                            using (FileStream fout = new FileStream(to, FileMode.Create)) {
                                int data;
                                while ((data = cryptoStream.ReadByte()) != -1) {
                                    fout.WriteByte((byte)data);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

