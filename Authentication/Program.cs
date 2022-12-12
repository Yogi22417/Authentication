using Microsoft.VisualBasic;
using System;
using System.Reflection;

namespace Authentication;

class Program
{
    static string[] first_name = { };
    static string[] last_name = { };
    static string[] fullname = { };
    static string[] username = { };
    static string[] password = { };
    public static void Main(string[] args)
    {
        BerandaMenu();
    }

    static void BerandaMenu()
    {
        Console.Clear();
        Console.WriteLine("== BASIC AUTHENTICATION==");
        Console.WriteLine("1. Create User");
        Console.WriteLine("2. Show User");
        Console.WriteLine("3. Search User");
        Console.WriteLine("4. Login User");
        Console.WriteLine("5. Exit");
        Console.Write("Input : ");
        int input = Convert.ToInt32(Console.ReadLine());
        switch (input)
        {
            case 1:
                CreateUser();
                break;
            case 2:
                ShowUser();
                break;
            case 3:
                SearchUser();
                break;
            case 4:
                Login();
                break;
        }
    }

    static void CreateUser()
    {
        Console.Clear();
        Console.Write("First Name: ");
        string input_firstname = Console.ReadLine();
        Console.Write("Last Name: ");
        string input_lastname = Console.ReadLine();
        Console.Write("Password: ");
        string input_password = Console.ReadLine();
        string passwords = CekPassword(input_password);
        User(input_firstname, input_lastname, passwords);
        Console.WriteLine();
        Console.WriteLine("Data user berhasil dibuat");
        Console.ReadKey();
        BerandaMenu();
    }
    static void ShowUser()
    {
        Console.Clear();
        int no = 1;
        Console.WriteLine("==SHOW USER==");
        for (int i = 0; i < username.Length; i++)
        {
            Console.WriteLine("========================");
            Console.WriteLine($"ID \t : {no++}");
            Console.WriteLine($"Name \t : {first_name[i]} {last_name[i]}");
            Console.WriteLine($"Username : {username[i]}");
            Console.WriteLine($"Password : {password[i]}");
            Console.WriteLine("========================");
        }

        Console.WriteLine("\nMenu");
        Console.WriteLine("1. Edit User");
        Console.WriteLine("2. Delete User");
        Console.WriteLine("3. Back");
        int pilih = Convert.ToInt32(Console.ReadLine());
        switch (pilih)
        {
            case 1:
                EditUser();
                break;
            case 2:
                DeleteUser();
                break;
            case 3:
                BerandaMenu();
                break;
            default:
                Console.WriteLine("== Pilihan Tidak Ada ==");
                ShowUser();
                break;
        }
    }
    static void EditUser()
    {
        Console.Write("Id Yang Ingin Diubah : ");
        int pilih = Convert.ToInt32(Console.ReadLine()) - 1;
        if (pilih >= 0 && pilih <= first_name.Length)
        {
            Console.Write("First Name : ");
            string edit_first_name = Console.ReadLine();
            Console.Write("Last Name : ");
            string edit_last_name = Console.ReadLine();
            Console.Write("Password : ");
            string edit_password = Console.ReadLine();
            string passwords = CekPassword(edit_password); //Fungsi untuk cek password
            first_name.SetValue(edit_first_name, pilih);
            last_name.SetValue(edit_last_name, pilih);
            username.SetValue(edit_first_name.Substring(0, 2) + edit_last_name.Substring(0, 2), pilih);
            fullname.SetValue(String.Concat(edit_first_name, edit_last_name), pilih);
            password.SetValue(passwords, pilih);
            Console.WriteLine("User Sudah Berhasil Di Edit");
            Console.ReadKey();
            ShowUser();

        }
        else if (pilih > first_name.Length)
        {
            Console.WriteLine("User Tidak Ditemukan !!!");
            EditUser();
        }
        else if (pilih < 0)
        {
            ShowUser();
        }
    }

    static void DeleteUser()
    {
        Console.Write("Id Yang Ingin Dihapus : ");
        int pilih = Convert.ToInt32(Console.ReadLine()) - 1;
        if (pilih >= 0 && pilih <= first_name.Length)
        {
            first_name = first_name.Where((source, index) => index != pilih).ToArray();
            last_name = last_name.Where((source, index) => index != pilih).ToArray();
            fullname = fullname.Where((source, index) => index != pilih).ToArray();
            username = username.Where((source, index) => index != pilih).ToArray();
            password = password.Where((source, index) => index != pilih).ToArray();
            Console.WriteLine("Akun Berhasil Di Hapus");
            Console.ReadKey();
            ShowUser();
        }
        else if (pilih > first_name.Length)
        {
            Console.WriteLine("User Tidak Ditemukan !!!");
            DeleteUser();
        }
        else if (pilih < 0)
        {
            ShowUser();
        }
    }
    static void SearchUser()
    {
        Console.Clear();
        Console.WriteLine("* Keterangan : Sensitif Case");
        Console.WriteLine("==Cari Akun==");
        Console.Write("Masukkan Nama : ");
        string input = Console.ReadLine();
        int[] index = { };
        string nama;
        int indexs;
        int no = 1;

        for (int i = 0; i < fullname.Length; i++)
        {
            string[] name_raw = { fullname[i] };
            nama = Array.Find(name_raw, n => n.Contains(input));
            indexs = Array.IndexOf(fullname, nama);
            if (indexs != -1)
            {
                index = index.Append(indexs).ToArray();
            }
        }

        if (index.Length > 0)
        {
            for (int i = 0; i < index.Length; i++)
            {
                Console.WriteLine("========================");
                Console.WriteLine($"ID \t : {no++}");
                Console.WriteLine($"Name \t : {first_name[index[i]]} {last_name[index[i]]}");
                Console.WriteLine($"Username : {username[index[i]]}");
                Console.WriteLine($"Password : {password[index[i]]}");
                Console.WriteLine("========================");
            }
            Console.ReadKey();
            BerandaMenu();
        }
        else
        {
            Console.WriteLine("== Akun Tidak Ditemukan ==");
            Console.ReadKey();
            BerandaMenu();
        }
    }
    static void Login()
    {
        Console.Clear();
        // Username
        string c_username;
        int index_username;
        //Password
        string c_password;
        int index_password;

        Console.WriteLine("==LOGIN==");
        Console.Write("USERNAME : ");
        string input_username = Console.ReadLine();
        Console.Write("PASSWORD : ");
        string input_password = Console.ReadLine();


        c_username = Array.Find(username, n => n == input_username);
        index_username = Array.IndexOf(username, c_username);

        c_password = Array.Find(password, n => n == input_password);
        index_password = Array.IndexOf(password, c_password);

        if (index_username == -1 || index_password == -1)
        {
            Console.WriteLine("Login Gagal");
            Console.ReadKey();
            BerandaMenu();
        }
        else if (index_username == index_password)
        {
            Console.WriteLine("Login Berhasil");
            Console.ReadKey();
            BerandaMenu();
        }
        else
        {
            Console.WriteLine("Login Gagal");
            Console.ReadKey();
            BerandaMenu();
        }
    }
    static void User(string firstName, string lastName, string passwords)
    {
        first_name = first_name.Append(firstName).ToArray();
        last_name = last_name.Append(lastName).ToArray();
        fullname = fullname.Append(firstName + " " + lastName).ToArray();
        username = username.Append(firstName.Substring(0, 2) + lastName.Substring(0, 2)).ToArray();
        password = password.Append(passwords).ToArray();
    }

    static string CekPassword(string passwords)
    {
        while (true)
        {
            bool flag = true;
            if ((passwords.Length > 7) && (Enumerable.Any<char>((IEnumerable<char>)passwords, new Func<char, bool>(char.IsUpper)) && (Enumerable.Any<char>((IEnumerable<char>)passwords, new Func<char, bool>(char.IsLower)) && Enumerable.Any<char>((IEnumerable<char>)passwords, new Func<char, bool>(char.IsNumber)))))
            {
                flag = false;
            }
            else
            {
                Console.WriteLine("\nPassword must have at least 8 characters\n with at least one Capital letter, at least one lower case letter and at least one number.");
                Console.Write("Password: ");
                passwords = Console.ReadLine();
                flag = true;
            }
            if (!flag)
            {
                return passwords;
            }
        }
    }
}