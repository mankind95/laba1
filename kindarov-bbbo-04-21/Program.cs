using System;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Text.Json;
using System.Xml.Linq;
using System.Linq;



namespace Laba1
{
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Company { get; set; }
    }

    class Proga
    {
        static void Main()
        {
            Console.WriteLine("1 - DriveInfo");
            Console.WriteLine("2 - Get directories and files");
            Console.WriteLine("3 - Create File (Catalog) - beta");
            Console.WriteLine("4 - DeleteFile");
            Console.WriteLine("5 - WriterFile");
            Console.WriteLine("6 - ReadFile");
            Console.WriteLine("7 - Create JSON file");
            Console.WriteLine("8 - Write JSON file");
            Console.WriteLine("9 - Read JSON file");
            Console.WriteLine("10 - Create XML file");
            Console.WriteLine("11 - Edit XML file");
            Console.WriteLine("12 - Read XML file");
            Console.WriteLine("13 - Read XML 2 Rude");
            Console.WriteLine("14 - Archive file");
            Console.WriteLine("15 - UnArchive file + read data");
            Console.WriteLine("16 - Read file info");
            Console.WriteLine();
            string x = Console.ReadLine();

            switch (x)
            {
                case "1":

                    //DriveInfo
                    DriveInfo[] drives = DriveInfo.GetDrives();

                    foreach (DriveInfo drive in drives)
                    {
                        Console.WriteLine($"Название: {drive.Name}");
                        Console.WriteLine($"Тип: {drive.DriveType}");
                        if (drive.IsReady)
                        {
                            Console.WriteLine($"Формат: {drive.DriveFormat}");
                            Console.WriteLine($"Общий объём: {drive.TotalSize}");
                            Console.WriteLine($"Свободно места: {drive.AvailableFreeSpace}");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    break;


                case "2":
                    //Get directories and files
                    Console.WriteLine("Please enter the path: ");
                    string DirPath = Console.ReadLine();

                    if (Directory.Exists(DirPath))
                    {

                        string[] Pathses = Directory.GetDirectories(DirPath);
                        foreach (string ab in Pathses)
                        {
                            Console.WriteLine();
                            Console.WriteLine("//Подкаталог:");
                            Console.WriteLine(ab);
                            Console.WriteLine("/Файлы:");
                            string[] files = Directory.GetFiles(DirPath);
                            foreach (string ac in files)
                            {
                                Console.WriteLine(ac);
                            }
                        }
                    }
                    Console.WriteLine();
                    break;

                case "3":
                    //Create File (Catalog)
                    Console.WriteLine("Enter the path: ");
                    string pathFile = Console.ReadLine();
                    using (FileStream fstream = new FileStream($@"{pathFile}\note.txt", FileMode.OpenOrCreate)) { }
                    Console.WriteLine();
                    break;

                case "4":
                    //DeleteFile
                    Console.WriteLine("Please enter the path of file for delete: ");
                    string pathDelete = Console.ReadLine();
                    FileInfo fileInf = new FileInfo(pathDelete);
                    if (fileInf.Exists)
                    {
                        fileInf.Delete();
                    }
                    Console.WriteLine();
                    Console.WriteLine("File deleted: ");
                    break;

                case "5":
                    //WriterFile
                    Console.WriteLine("Enter the path: ");
                    string pathWrite = Console.ReadLine();

                    using (StreamWriter sw = new StreamWriter(pathWrite, false, System.Text.Encoding.Default))
                    {
                        string text = Console.ReadLine();
                        sw.WriteLine(text);
                        Console.WriteLine("Текст записан в файл");
                    }
                    Console.WriteLine();
                    break;

                case "6":
                    //ReadFile
                    Console.WriteLine("Enter the path: ");
                    string pathRead = Console.ReadLine();
                    using (StreamReader sr = new StreamReader(pathRead))
                    {
                        Console.WriteLine(sr.ReadToEnd());
                    }
                    Console.WriteLine();
                    break;

                case "7":
                    //CreateJSON
                    Console.WriteLine("Enter the path of JSON file: ");
                    string pathJSON = Console.ReadLine();
                    using (FileStream fstream = new FileStream($@"{pathJSON}\note.JSON", FileMode.OpenOrCreate)) { }
                    Console.WriteLine("JSON file has been created");
                    Console.WriteLine();
                    break;



                case "8":
                    //WriteJSON
                    Console.WriteLine("Enter the JSON file path: ");
                    string pathWrJSON = Console.ReadLine();
                    using (FileStream fs = new FileStream(pathWrJSON, FileMode.OpenOrCreate))
                    {
                        Person Bob = new Person { Name = "Tom", Age = 21 };
                        JsonSerializer.Serialize<Person>(fs, Bob);
                        Console.WriteLine("Data has been saved to file");
                        Console.WriteLine();
                    }
                    break;

                case "9":
                    //ReadJSON
                    Console.WriteLine("Enter the path of JSON file to read: ");
                    string pathJSONRead = Console.ReadLine();
                    using (FileStream readPath = new FileStream($@"{pathJSONRead}", FileMode.OpenOrCreate))
                    {
                        Person restoredPerson = JsonSerializer.Deserialize<Person>(readPath);
                        Console.WriteLine($"Name: {restoredPerson.Name} Age: {restoredPerson.Age}");
                        Console.WriteLine();
                    }
                    break;

                case "10":
                    //Create XML
                    Console.WriteLine("Please enter the path to save the XML file");
                    string xmlFilePath = Console.ReadLine();

                    XDocument xdoc = new XDocument();
                    XElement pc1 = new XElement("PC");
                    XAttribute pc1NameAtr = new XAttribute("name", "Ultra Philips");
                    XElement pc1CompanyElem = new XElement("company", "Digital Eagle");
                    XElement pc1PriceElem = new XElement("price", "180,000");

                    pc1.Add(pc1NameAtr);
                    pc1.Add(pc1CompanyElem);
                    pc1.Add(pc1PriceElem);

                    XElement pc2 = new XElement("PC");
                    XAttribute pc2NameAtr = new XAttribute("name", "Horizon Omega");
                    XElement pc2CompanyElem = new XElement("company", "Apple");
                    XElement pc2PriceElem = new XElement("price", "500,000");

                    pc2.Add(pc2NameAtr);
                    pc2.Add(pc2CompanyElem);
                    pc2.Add(pc2PriceElem);

                    XElement MainPCList = new XElement("PCs");
                    MainPCList.Add(pc1);
                    MainPCList.Add(pc2);

                    xdoc.Add(MainPCList);
                    xdoc.Save($@"{xmlFilePath}\PClist2033.xml");

                    break;

                case "11":
                    //edit XML
                    Console.WriteLine("Please enter the path to edit the XML file");
                    string xmlEditPath = Console.ReadLine();

                    XDocument eDoc = XDocument.Load(xmlEditPath);
                    XElement root = eDoc.Element("PCs");

                    Console.WriteLine("Please enter the name of new PC");
                    string adNameAtr = Console.ReadLine();
                    Console.WriteLine("Please enter the holder or company of new PC");
                    string adCompanyEl = Console.ReadLine();
                    Console.WriteLine("Please enter the price of new PC");
                    string adPriceEl = Console.ReadLine();


                    root.Add(new XElement("PC",
                        new XAttribute("name", adNameAtr),
                        new XElement("company", adCompanyEl),
                        new XElement("price", adPriceEl)));
                    eDoc.Save(xmlEditPath);
                    Console.WriteLine("New pc was added succesfuly");
                    Console.WriteLine();
                    break;

                case "12":
                    //Read XML
                    Console.WriteLine("Please enter the path to read the XML file");
                    string xmlReadPath = Console.ReadLine();
                    Console.WriteLine();

                    XDocument rDoc = XDocument.Load(xmlReadPath);

                    foreach (XElement pcElement in rDoc.Element("PCs").Elements("PC"))
                    {
                        XAttribute nameAttribute = pcElement.Attribute("name");
                        XElement companyElement = pcElement.Element("company");
                        XElement priceElement = pcElement.Element("price");

                        if (nameAttribute != null && companyElement != null && priceElement != null)
                        {
                            Console.WriteLine($"---PC name: {nameAttribute.Value}");
                            Console.WriteLine($"-Company: {companyElement.Value}");
                            Console.WriteLine($"-Price: {priceElement.Value}");
                        }
                    }
                    Console.WriteLine();
                    break;

                case "13":
                    //Read XML 2 Rude
                    Console.WriteLine("Please enter the path to read the XML file");
                    string xmlReadPath2 = Console.ReadLine();
                    XDocument rrDoc = XDocument.Load(xmlReadPath2);
                    Console.WriteLine();
                    Console.WriteLine(rrDoc);
                    Console.WriteLine();
                    break;

                case "14":
                    //Archive file
                    Console.WriteLine("Please enter the path to read the file");
                    string sourceFile = Console.ReadLine();
                    Console.WriteLine("Please enter the path and the name of archived file");
                    string compressedFile = Console.ReadLine();
                    Compress(sourceFile, compressedFile);
                    Console.WriteLine();
                    break;

                case "15":
                    Console.WriteLine("Please enter the path to the file");
                    string sourceFile2 = Console.ReadLine();
                    Console.WriteLine("Please enter the path to read the file");
                    string targetFile = Console.ReadLine();
                    Decompress(sourceFile2, targetFile);
                    Console.WriteLine();
                    break;

                case "16":
                    Console.WriteLine("Please enter the path to read the file");
                    string path = Console.ReadLine();
                    FileInfo fInf = new FileInfo(path);
                    if (fInf.Exists)
                    {
                        Console.WriteLine("Имя файла: {0}", fInf.Name);
                        Console.WriteLine("Время создания: {0}", fInf.CreationTime);
                        Console.WriteLine("Размер: {0}", fInf.Length);
                        Console.WriteLine();
                    }
                    break;

            }
            Main();
        }

        public static void Compress(string sourceFile, string compressedFile)
        {
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream);
                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            }
        }
        public static void Decompress(string compressedFile, string targetFile)
        {
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                using (FileStream targetStream = File.Create(targetFile))
                {
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine("Восстановлен файл: {0}", targetFile);
                    }
                }
            }
        }



    }
}