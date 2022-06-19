using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Лаб_18
{
    class Program
    {
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("[1] - Просмотреть сданные предметы в ломбарде");
            Console.WriteLine("[2] - Добавить новый предмет");
            Console.WriteLine("[3] - Удалить");
            Console.WriteLine("[0] - Выйти");
        }
        static void PrintLending(XDocument xdoc)
        {
            foreach (XElement lendingElement in xdoc.Element("Lendings").Elements("Lending"))
            {
                XElement categoryElement = lendingElement.Element("category");
                XElement clientElement = lendingElement.Element("client");
                XElement descriptionElement = lendingElement.Element("description");
                XElement lendingDataElement = lendingElement.Element("lending_data");
                XElement returnDataElement = lendingElement.Element("return_data");
                XElement sumElement = lendingElement.Element("sum");
                XElement commissionElement = lendingElement.Element("commission");

                Console.WriteLine("Категория товара: {0}", categoryElement.Value);
                Console.WriteLine("Клиент: {0}", clientElement.Value);
                Console.WriteLine("Описание товара: {0}", descriptionElement.Value);
                Console.WriteLine("Дата сдачи: {0}", lendingDataElement.Value);
                Console.WriteLine("Дата возврата: {0}", returnDataElement.Value);
                Console.WriteLine("Сумма: {0}", sumElement.Value);
                Console.WriteLine("Комиссионные: {0}", commissionElement.Value);
                Console.WriteLine();
            }
        }
        static void PrintClients(XDocument xdoc)
        {
            foreach (XElement clientElement in xdoc.Element("clients").Elements("client"))
            {
                XElement clientCElement = clientElement.Element("client");
                XElement clientLastNameElement = clientElement.Element("last_name");
                XElement clientFirstNameElement = clientElement.Element("first_name");
                XElement clientMiddleNameElement = clientElement.Element("middle_name");
                XElement clinetNumPaspElement = clientElement.Element("num_pasp");
                XElement clientSeriaPaspElement = clientElement.Element("seria_pasp");
                XElement clientDataElement = clientElement.Element("data_pasp");
                Console.WriteLine("Клиент: {0}", clientCElement.Value);
                Console.WriteLine("Фамилия: {0}", clientLastNameElement.Value);
                Console.WriteLine("Имя: {0}", clientFirstNameElement.Value);
                Console.WriteLine("Отчество: {0}", clientMiddleNameElement.Value);
                Console.WriteLine("Номер паспорта: {0}", clinetNumPaspElement.Value);
                Console.WriteLine("Серия паспорта: {0}", clientSeriaPaspElement.Value);
                Console.WriteLine("Дата паспорта: {0}", clientDataElement.Value);
                Console.WriteLine();
            }
        }
        static void PrintCategory(XDocument xdoc)
        {
            foreach (XElement categoryElement in xdoc.Element("categories").Elements("category"))
            {
                XElement categoryCElement = categoryElement.Element("category");
                XElement categoryNameElement = categoryElement.Element("name");
                XElement categoryNoteElement = categoryElement.Element("note");
                Console.WriteLine("Категория товара: {0}", categoryCElement.Value);
                Console.WriteLine("Название товара: {0}", categoryNameElement.Value);
                Console.WriteLine("Примечания к товару: {0}", categoryNoteElement.Value);
                Console.WriteLine();
            }
        }
        static void AddXml(XDocument xlending, XDocument xclients, XDocument xcategory)
        {
            Console.Write("Введите id категории:");
            string category = Console.ReadLine();
            Console.Write("Введите название категории:");
            string categoryName = Console.ReadLine();
            Console.Write("Введите примечание товара:");
            string categoryNote = Console.ReadLine();

            Console.Write("Введите  id клиента:");
            string client = Console.ReadLine();
            Console.Write("Введите Фамилию:");
            string clientLName = Console.ReadLine();
            Console.Write("Введите Имя:");
            string clientFName = Console.ReadLine();
            Console.Write("Введите Отчество:");
            string clientMName = Console.ReadLine();
            Console.Write("Введите номер паспорта:");
            string clientNumPasp = Console.ReadLine();
            Console.Write("Введите серию паспорта:");
            string clientSeriaPasp = Console.ReadLine();
            Console.Write("Введите дату паспорта:");
            string clientDataPasp = Console.ReadLine();

            Console.Write("Введите описание товара:");
            string description = Console.ReadLine();
            Console.Write("Введите дату сдачи:");
            string lendingData = Console.ReadLine();
            Console.Write("Введите дату возврата:");
            string returnData = Console.ReadLine();
            Console.Write("Введите сумму:");
            string sum = Console.ReadLine();
            string commission = Convert.ToString(double.Parse(sum)*0.1);
            Console.WriteLine("Коммиссия: {0}",commission);

            XElement Lending = new XElement("Lending",
                   new XElement("category", category),
                   new XElement("client", client),
                   new XElement("description", description),
                   new XElement("lending_data", lendingData),
                   new XElement("return_data", returnData),
                   new XElement("sum", sum),
                   new XElement("commission", commission));

            XElement Category = new XElement("category",
                    new XElement("category", category),
                    new XElement("name", categoryName),
                    new XElement("note", categoryNote));

            XElement Client = new XElement("client",
                    new XElement("client", client),
                    new XElement("last_name", clientLName),
                    new XElement("first_name", clientFName),
                    new XElement("middle_name", clientMName),
                    new XElement("num_pasp", clientNumPasp),
                    new XElement("seria_pasp", clientSeriaPasp),
                    new XElement("data_pasp", clientDataPasp));
            XElement lendings = xlending.Element("Lendings");
            lendings.Add(Lending);
            xlending.Save("lending.xml");
            XElement categories = xcategory.Element("categories");
            categories.Add(Category);
            xcategory.Save("category.xml");
            XElement clients = xclients.Element("clients");
            clients.Add(Client);
            xclients.Save("clients.xml");
        }
        static void DeleteXml(XDocument xdoc, string namXdoc, string paramDelete)
        {
            XElement deletable = null;
            foreach (XElement xelem in xdoc.Elements())
            {
                foreach (XElement xelem1 in xelem.Elements())
                {
                    foreach (XElement xelem2 in xelem1.Elements())
                    {
                        if (xelem2.Value == paramDelete)
                        {
                            deletable = xelem1;
                            break;
                        }
                    }
                }
            }
            if (deletable != null)
            {
                deletable.Remove();
            }
            xdoc.Save(namXdoc);
        }
        static void Main(string[] args)
        {
            var fullpath = AppDomain.CurrentDomain.BaseDirectory + "lending.xml";
            XDocument xlending = XDocument.Load(fullpath);
            fullpath = AppDomain.CurrentDomain.BaseDirectory + "clients.xml";
            XDocument xclients = XDocument.Load(fullpath);
            fullpath = AppDomain.CurrentDomain.BaseDirectory + "category.xml";
            XDocument xcategory = XDocument.Load(fullpath);
            bool run = true;
            bool run1;
            int switch_id;
            int switch_id1;
            string delete;
            while (run)
            {
                Menu();
                try
                {
                    switch_id = int.Parse(Console.ReadLine());
                }
                catch
                {
                    continue;
                }
                switch (switch_id)
                {
                    case 1:
                        run1 = true;
                        while (run1)
                        {
                            Console.Clear();
                            Console.WriteLine("[1] - Просмотреть Документы");
                            Console.WriteLine("[2] - Просмотреть Клиентов");
                            Console.WriteLine("[3] - Просмотреть Товары");
                            Console.WriteLine("[0] - Назад");
                            try
                            {
                                switch_id1 = int.Parse(Console.ReadLine());
                            }
                            catch
                            {
                                continue;
                            }
                            switch (switch_id1)
                            {
                                case 1:
                                    PrintLending(xlending);
                                    Console.ReadKey();
                                    break;
                                case 2:
                                    PrintClients(xclients);
                                    Console.ReadKey();
                                    break;
                                case 3:
                                    PrintCategory(xcategory);
                                    Console.ReadKey();
                                    break;
                                case 0:
                                    run1 = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                        
                        
                    case 2:
                        try
                        {
                            AddXml(xlending,xclients,xcategory);
                            Console.ReadKey();
                        }
                        catch
                        {
                            continue;
                        }

                        break;
                    case 3:
                        run1 = true;
                        while (run1)
                        {
                            Console.Clear();
                            Console.WriteLine("[1] - Удалить Документ");
                            Console.WriteLine("[2] - Удалить Клиента");
                            Console.WriteLine("[3] - Удалить Товар");
                            Console.WriteLine("[0] - Назад");
                            try
                            {
                                switch_id1 = int.Parse(Console.ReadLine());
                            }
                            catch
                            {
                                continue;
                            }
                            switch (switch_id1)
                            {
                                case 1:
                                    Console.Write("Введите категорию товара:");
                                    delete = Console.ReadLine();
                                    DeleteXml(xlending, "lending.xml", delete);
                                    break;
                                case 2:
                                    Console.Write("Введите клиента:");
                                    delete = Console.ReadLine();
                                    DeleteXml(xclients, "clients.xml", delete);
                                    break;
                                case 3:
                                    Console.Write("Введите категорию товара:");
                                    delete = Console.ReadLine();
                                    DeleteXml(xcategory, "category.xml", delete);
                                    break;
                                case 0:
                                    run1 = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 0:
                        run = false;
                        break;
                    default:
                        break;

                }
            }
        }
    }
}
