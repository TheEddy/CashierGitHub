using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Cashier.Model
{
    class DataManager
    {
        private const string psPhrase = "Hi Betsson :)";
        private string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public ObservableCollection<WarehouseItem> GetWarehouseItems()
        {
            try
            {
                string cipherText = File.ReadAllText(path + @"\Cashier\items.json");
                string plainText = Encryption.Decrypt(cipherText, psPhrase);
                //string plainText = cipherText;
                ObservableCollection<WarehouseItem> itemsList = JsonConvert.DeserializeObject<ObservableCollection<WarehouseItem>>(plainText);
                return itemsList;
            }
            catch (Exception e)
            {
                MessageBox.Show("Warehouse items file not exists");
                var emptyList = new ObservableCollection<WarehouseItem>();
                var emptyItem = new WarehouseItem()
                {
                    ItemAmount = 0,
                    ItemCode = 0,
                    ItemName = "Initial, just delete",
                    ItemPrice = 0,
                    ItemType = "0"
                };
                emptyList.Add(emptyItem);
                string plainText = JsonConvert.SerializeObject(emptyList, Formatting.Indented);
                string cipherText = Encryption.Encrypt(plainText, psPhrase);
                File.WriteAllText(path + @"\Cashier\items.json", cipherText);
                return emptyList;
            }
        }

        public void SaveWarehouse(ObservableCollection<WarehouseItem> itemsList)
        {
            string plainText = JsonConvert.SerializeObject(itemsList, Formatting.Indented);
            string cipherText = Encryption.Encrypt(plainText, psPhrase);
            //string cipherText = plainText;
           // MessageBox.Show("Collection serialized" + "\n" + json);a

            File.WriteAllText(path + @"\Cashier\items.json", cipherText);
        }

        public ObservableCollection<Types> GetItemTypes()
        {
            try
            {
                string cipherText = File.ReadAllText(path + @"\Cashier\types.json");
                string plainText = Encryption.Decrypt(cipherText, psPhrase);
                //string plainText = cipherText;
                ObservableCollection<Types> types = JsonConvert.DeserializeObject<ObservableCollection<Types>>(plainText);
                return types;
            }
            catch (Exception e)
            {
                MessageBox.Show("Items types file not exists");
                var emptyList = new ObservableCollection<Types>();
                var emptyItem = new Types()
                {
                    Type = "Initial, delete me"
                };
                emptyList.Add(emptyItem);
                string plainText = JsonConvert.SerializeObject(emptyList, Formatting.Indented);
                string cipherText = Encryption.Encrypt(plainText, psPhrase);
                File.WriteAllText(path + @"\Cashier\types.json", cipherText);
                return emptyList;
            }      
        }

        public void SaveItemTypes(ObservableCollection<Types> itemsTypesList)
        {
            string plainText = JsonConvert.SerializeObject(itemsTypesList, Formatting.Indented);
            string cipherText = Encryption.Encrypt(plainText, psPhrase);
            //string cipherText = plainText;
            // MessageBox.Show("Collection serialized" + "\n" + json);a

            File.WriteAllText(path + @"\Cashier\types.json", cipherText);
        }

        public List<int> GetListID()
        {
            try
            {
                string cipherText = File.ReadAllText(path + @"\Cashier\itemIDs.json");
                string plainText = Encryption.Decrypt(cipherText, psPhrase);
                //string plainText = cipherText;
                List<int> types = JsonConvert.DeserializeObject<List<int>>(plainText);
                return types;
            }
            catch (Exception e)
            {
                MessageBox.Show("Item IDs file not exists");
                var emptyList = new List<int>();
                emptyList.Add(0);
                string plainText = JsonConvert.SerializeObject(emptyList, Formatting.Indented);
                string cipherText = Encryption.Encrypt(plainText, psPhrase);
                try
                {
                    File.WriteAllText(path + @"\Cashier\itemIDs.json", cipherText);
                }
                catch (DirectoryNotFoundException ex)
                {
                    System.IO.Directory.CreateDirectory(path + @"\Cashier\");
                    File.WriteAllText(path + @"\Cashier\itemIDs.json", cipherText);
                }
                
                return emptyList;
            }
        }

        public void SaveListID(List<int> itemsTypesList)
        {
            string plainText = JsonConvert.SerializeObject(itemsTypesList, Formatting.Indented);
            string cipherText = Encryption.Encrypt(plainText, psPhrase);
            //string cipherText = plainText;
            //MessageBox.Show("Collection serialized" + "\n" + json);a

            File.WriteAllText(path + @"\Cashier\itemIDs.json", cipherText);
        }

        public ObservableCollection<HistoryItem> GetHistoryItems()
        {
            try
            {
                string cipherText = File.ReadAllText(path + @"\Cashier\history.json");
                string plainText = Encryption.Decrypt(cipherText, psPhrase);
                //string plainText = cipherText;
                ObservableCollection<HistoryItem> itemsList = JsonConvert.DeserializeObject<ObservableCollection<HistoryItem>>(plainText);
                if (itemsList != null) return itemsList;
                //else new List<int>() { 1 };
            }
            catch (Exception e)
            {
                MessageBox.Show("Warehouse items file not exists");
                var emptyList = new ObservableCollection<HistoryItem>();
                var emptyItem = new HistoryItem()
                {
                    DateTime = DateTime.Now,
                    ItemCode = 0,
                    ItemLine = "Initial",
                    OperationHistory = new ObservableCollection<OperationItem>(),
                    TotalSum = 0
                };
                emptyList.Add(emptyItem);
                string plainText = JsonConvert.SerializeObject(emptyList, Formatting.Indented);
                string cipherText = Encryption.Encrypt(plainText, psPhrase);
                File.WriteAllText(path + @"\Cashier\history.json", cipherText);
                return emptyList;
            }
            return new ObservableCollection<HistoryItem>();

        }

        public void SaveHistory(ObservableCollection<HistoryItem> itemsList)
        {
            string plainText = JsonConvert.SerializeObject(itemsList, Formatting.Indented);
            string cipherText = Encryption.Encrypt(plainText, psPhrase);
            //string cipherText = plainText;
            // MessageBox.Show("Collection serialized" + "\n" + json);a

            File.WriteAllText(path + @"\Cashier\history.json", cipherText);
        }

        public List<int> GetHistoryListID()
        {
            try
            {
                string cipherText = File.ReadAllText(path + @"\Cashier\HistoryItemIDs.json");
                string plainText = Encryption.Decrypt(cipherText, psPhrase);
                //string plainText = cipherText;
                List<int> types = JsonConvert.DeserializeObject<List<int>>(plainText);
                return types;
            }
            catch (Exception e)
            {
                MessageBox.Show("Item IDs file not exists");
                var emptyList = new List<int>();
                emptyList.Add(0);
                string plainText = JsonConvert.SerializeObject(emptyList, Formatting.Indented);
                string cipherText = Encryption.Encrypt(plainText, psPhrase);
                File.WriteAllText(path + @"\Cashier\HistoryItemIDs.json", cipherText);
                return emptyList;
            }
        }

        public void SaveHistoryListID(List<int> itemsTypesList)
        {
            string plainText = JsonConvert.SerializeObject(itemsTypesList, Formatting.Indented);
            string cipherText = Encryption.Encrypt(plainText, psPhrase);
            //string cipherText = plainText;
            //MessageBox.Show("Collection serialized" + "\n" + json);a

            File.WriteAllText(path + @"\Cashier\HistoryItemIDs.json", cipherText);
        }

    }
    class Encryption
    {
        public Encryption()
        {
            new Encryption();
        }

        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }
    }
}
